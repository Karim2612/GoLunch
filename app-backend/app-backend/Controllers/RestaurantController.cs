using app_backend.Datas;
using app_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly GolunchDbContext _context;

        public RestaurantController(GolunchDbContext context)
        {
            _context = context;
        } 

        /// <summary>
        /// Retourne toute la liste des restaurants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants()
        {
            //return restaurants.GetAll();
             return await _context.Restaurants.Include(x => x.Localisation).Include(c => c.Menus).ToListAsync();
        }

        /// <summary>
        /// Retourne un restaurant selon son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.Include(x => x.Localisation).Include(c => c.Menus).FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        /// <summary>
        /// Publie un nouveau restaurant
        /// </summary>
        /// <remarks>
        /// Exemple de requete:
        /// 
        ///     POST api/Restaurant
        ///     { 
        ///       "id": 0,  
        ///       "nom": "Chez toto",
        ///       "description": "Restaurant sympa",
        ///       "contactEmail": "toto@golun.ch",
        ///       "contactTel: none,
        ///       "urlSite": "https://golun.ch"
        ///       
        ///     }
        /// </remarks>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<Restaurant>> PostRestaurant([FromBody] Restaurant restaurant)
        {

            double? lat = restaurant.Localisation.PosLatitude;
            double? lng = restaurant.Localisation.PosLongitude;
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            restaurant.Localisation.Position = geometryFactory.CreatePoint(new Coordinate((double)lng, (double)lat));

            _context.Restaurants.Add(restaurant);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestaurantExists(restaurant.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetResturant", new { id = restaurant.Id }, restaurant);
        }

        /// <summary>
        /// Modifie un ou plusieurs paramètres d'une restaurant
        /// </summary>
        /// <remarks>
        /// Exemple de requete :
        /// 
        /// [
        ///  {
        ///     "op": "replace",
        ///     "value": "Restooooo",
        ///     "path": "/nom"
        ///  }
        ///]
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public IActionResult PatchRestaurant(int id, [FromBody] JsonPatchDocument<Restaurant> restaurant)
        {
            var resto = _context.Restaurants.FirstOrDefault(x => x.Id == id);

            if (resto == null)
            {
                return NotFound();
            }

            restaurant.ApplyTo(resto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Update(resto);

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(resto);
        }

        /// <summary>
        /// Modifie un restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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
        /// Supprimer un restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Check si restaurant existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}
