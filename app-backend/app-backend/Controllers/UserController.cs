using app_backend.Datas;
using app_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GolunchDbContext _context;

        public UserController(GolunchDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retourne tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            //return Users.GetAll();
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Retourne un utilisateur selon son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        /// <summary>
        /// Modifie un ou plusieurs paramètres d'un utilisateur
        /// </summary>
        /// <remarks>
        /// Exemple de requete :
        /// 
        /// [
        ///  {
        ///     "op": "replace",
        ///     "value": "Florian",
        ///     "path": "/prenom"
        ///  }
        ///]
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> user)
        {
            var getuser = _context.Users.FirstOrDefault(x => x.Id == id);

            if (getuser == null)
            {
                return NotFound();
            }

            user.ApplyTo(getuser, ModelState);   

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Update(getuser);   

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(getuser);
        }

        /// <summary>
        /// Modifie tous les paramètres d'un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        /// Supprime un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check si utilisateur existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // <summary>
        /// Retourne les restaurants favoris selon un userid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/fav")]
        public async Task<ActionResult<List<Restaurant>>> GetFavoriteRestaurants(int id)
        {
            /*var favorite = await _context.Favoris
                .Where(u => u.UserId == id)
                .Include(f => f.Restaurant)
                .Select(ru => new Restaurant()
                {
                    Id = ru.Restaurant.Id
                })
                .ToListAsync();*/



            var favorite = await _context.Restaurants
                .Join(_context.Favoris, r => r.Id, ru => ru.RestaurantId, (r,
               ru) => new { r, ru })
                .Where(u => u.ru.UserId == id)
                .Select(e => new Restaurant()
                {
                    Id = e.r.Id,
                    Nom = e.r.Nom,
                    Description = e.r.Description,
                    Localisation = e.r.Localisation
                })
                .ToListAsync();
            return favorite;
        }
    }
}