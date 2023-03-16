using app_backend.Datas;
using app_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavController : ControllerBase
    {
        private readonly GolunchDbContext _context;

        public FavController(GolunchDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retourne tous les favoris de tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fav>>> GetAllFavoris()
        {
            return await _context.Favoris.ToListAsync();
        }

        /// <summary>
        /// Retourne les restaurants favoris selon un userid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Restaurant>>> GetFavoriteRestaurants(int id)
        {
            var favorite = await _context.Restaurants
                .Join(_context.Favoris, r => r.Id, ru => ru.RestaurantId, (r,
               ru) => new { r, ru })
                .Where(u => u.ru.UserId == id)
                .Select(e => new Restaurant()
                {
                    Id = e.r.Id,
                    Nom = e.r.Nom,
                    Description = e.r.Description,
                    RestoPhoto = e.r.RestoPhoto,
                    Localisation = e.r.Localisation
                })
                .ToListAsync();
            return favorite;
        }

        /// <summary>
        /// Enregistre un favoris
        /// </summary>
        /// <remarks>
        ///  {
        ///  "id": 0,
        ///  "restaurantId": 4,
        ///  "restaurant": null,
        ///  "userId": 1,
        ///  "user": null
        ///  }
        /// </remarks>
        /// <param name="favorite"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Fav>> PostFavortie([FromBody] Fav favorite)
        {   

            _context.Favoris.Add(favorite);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavorisExists(favorite.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavorite", new { id = favorite.Id }, favorite);
        }

        /// <summary>
        /// Supprimer un favoris
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoris(int id)
        {
            var favorite = await _context.Favoris.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favoris.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Retourne un favoris
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Fav>> GetFavorite(int id)
        {
            var favorite = await _context.Favoris.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return Ok(favorite);
        }

        /// <summary>
        /// Retourne id Favoris
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet("get/{rid}/{uid}")]
        public async Task<ActionResult<Fav>> GetIdFavorite(int rid, int uid)
        {

            var idFav = _context.Favoris.FirstOrDefault(f => f.UserId == uid && f.RestaurantId == rid);

            if (idFav == null)
            {
                return NotFound();
            }

            return Ok(idFav);
        }


        /// <summary>
        /// Check si favoris existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool FavorisExists(int id)
        {
            return _context.Favoris.Any(e => e.Id == id);
        }
    }
}
