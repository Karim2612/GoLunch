using app_backend.Datas;
using app_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Linq;
using System.Runtime.InteropServices;



namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly GolunchDbContext _context;

        public MenuController(GolunchDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retourne toute la liste de tous les menus
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetAllMenus()
        {
            //Flo test pour Many to One
            return await _context.Menus.Include(x => x.Type).Include(l => l.Localisation).ToListAsync();
            //return await _context.Menus.ToListAsync();
        }

        /// <summary>
        /// Filtre les menu par distance
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [HttpGet("filtre/{latitude}/{longitude}/{distance}")]
        public async Task<ActionResult<List<Localisation>>> GetLocalisationByLatLong(double latitude, double longitude, double distance)
        {
            var distanceToDegree = distance / 100000;
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var malocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            var nearby = await _context.Localisations
                            .OrderBy(x => x.Position.Distance(malocation))
                            .Where(x => x.Position.IsWithinDistance(malocation, distanceToDegree))
                            .ToListAsync();

            //var result = await _context.Localisations.Where(x=> x.Position.Distance(malocation) < distance).ToListAsync();

            return Ok(nearby);
        }

        /// <summary>
        /// Filtres avec de multiples paramètres optionnels
        /// </summary>
        /// <param name="plat"></param>
        /// <param name="prixmax"></param>
        /// <param name="city"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="distance"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("filtres")]
        public async Task<ActionResult<IEnumerable<Menu>>> Filtres(string? plat, int? prixmax,string? city, double? latitude, double? longitude, double? distance, int? type)
        {

            var menufilter = _context.Menus.Include(m => m.Type).Include(l => l.Localisation).AsQueryable();

            //Flo Debug console
            System.Diagnostics.Debug.WriteLine("LOG FLOW :");
            System.Diagnostics.Debug.WriteLine(menufilter);

            if(distance != null) {
                //Flo retourne menus dans le perimetres de l'utilisateur
                var distanceToDegree = distance / 100000;
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                var malocation = geometryFactory.CreatePoint(new Coordinate((double)longitude, (double)latitude));

                menufilter = menufilter
                            .OrderBy(x => x.Localisation.Position.Distance(malocation))
                            .Where(x => x.Localisation.Position.IsWithinDistance(malocation, (double)distanceToDegree));


            }

            if (!string.IsNullOrEmpty(city))
            {
                //Flo Attente relation
                menufilter = menufilter.Where(e => e.Localisation.Ville.ToLower().Contains(city.ToLower()));
            }

            if (prixmax != null)
            {
                menufilter = menufilter.Where(m => m.Prix <= prixmax);
            }

            if (!string.IsNullOrEmpty(plat))
            {
                //Flo Recherche un plat, tout mettre toLower pour ne pas être sensible à la case burger = Burger
                menufilter = menufilter.Where(m => m.Plat.ToLower().Contains(plat.ToLower()));
            }

            if (type != null)
            {
                //Flo Montrer les menus d'un certain Type Ex.Buisness 
                menufilter = menufilter.Where(m => m.TypeId == type);
            }


            return await menufilter.ToListAsync();

        }

        /// <summary>
        /// Retourne un menu selon son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menus.Include(m => m.Type).Include(l => l.Localisation).FirstOrDefaultAsync(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }


        /// <summary>
        /// Publie un nouveau menu
        /// </summary>
        /// <remarks>
        /// Exemple de requete:
        /// 
        ///     POST api/Menu
        ///     {
        ///        "id": 0,
        ///        "entree": "Soupe au Kimchi",
        ///        "plat": "Bibimbap riz légumes et boeuf mariné",
        ///        "dessert": "",
        ///        "prix": 23.5,
        ///        "type": null,
        ///        "inclusBoisson": false,
        ///        "inclusCafe": false,
        ///        "dateMenu": "2022-02-14T00:00:00",
        ///        "dateModif": "2022-02-12T13:41:01.778+01:00"
        ///      }
        /// </remarks>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<Menu>>> PostMenu([FromBody] Menu menu)
        {
            //Flo Lors de la publication envoyer le Type de menu en Null et renseigner typeId pour que le back fasse la relation, sinon erreur de clé de contrainte
            var type = await _context.MenuTypes.FindAsync(menu.TypeId);
            if (type != null)
                menu.Type = type;

            //Flo Utiliser localisation mobile utilisateur pour géolocaliser un menu, si un restaurant est séléctionné utiliser la géoloc du restaurant 
            var resto = await _context.Restaurants.Include(l => l.Localisation).FirstOrDefaultAsync(r => r.Id == menu.RestaurantId);
            if (resto != null)
               menu.Localisation = resto.Localisation;
                
            _context.Menus.Add(menu);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuExists(menu.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        /// <summary>
        /// Reza : Modifie un ou plusieurs paramètres d'un Menu
        /// </summary>
        /// <remarks>
        /// Exemple de requete :
        /// 
        /// [
        ///  {
        ///     "op": "replace",
        ///     "value": "best",
        ///     "path": "/nom"
        ///  }
        ///]
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public IActionResult PatchRestaurant(int id, [FromBody] JsonPatchDocument<Menu> menu)
        {
            var myMenu = _context.Menus.FirstOrDefault(x => x.Id == id);

            if (myMenu == null)
            {
                return NotFound();
            }

            menu.ApplyTo(myMenu, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Update(myMenu);

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(myMenu);
        }


        /// <summary>
        /// Modifie un menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Supprimer un menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Check si restaurant existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }

        private string FirstLetterToUpper(string str)
        {
            str = str.ToLower();
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
        /// <summary>
        /// Karim : Recherche les menu par prix
        /// </summary>
        /// <param name="prix_minimum"></param>
        /// <param name="prix_maximum"></param>
        /// <returns></returns>
        private async Task<ActionResult<IEnumerable<Menu>>> RecherchePrix(double prix_minimum, double prix_maximum)
        {
            try
            {
                IQueryable<Menu> query = _context.Menus;

                if (prix_minimum != null)
                {
                    query = query.Where(e => e.Prix >= prix_minimum);
                    query = query.Where(e => e.Prix <= prix_maximum);
                }

                var result = await query.ToListAsync();

                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur");
            }
        }

        /// <summary>
        /// Karim : Recherche les menu par plat
        /// </summary>
        /// <param name="plat"></param>
        /// <returns></returns>
        private async Task<ActionResult<IEnumerable<Menu>>> RecherchePlat(string plat)
        {
            plat = FirstLetterToUpper(plat);

            try
            {
                IQueryable<Menu> query = _context.Menus;

                if (!string.IsNullOrEmpty(plat))
                {
                    query = query.Where(e => e.Plat.Contains(plat));
                }

                var result = await query.ToListAsync();

                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur");
            }
        }

    }
}
