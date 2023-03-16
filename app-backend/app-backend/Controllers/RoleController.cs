using app_backend.Datas;
using app_backend.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly GolunchDbContext _context;

        public RoleController(GolunchDbContext context)
        {
            _context = context;
        }

        ///// <summary>
        ///// Retourne toute la liste des roles
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        //{
        //    //return roles.GetAll();
        //    return await _context.Roles.ToListAsync();
        //}

        /// <summary>
        /// Retourne une role selon son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Role>> GetRole(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);

        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(role);
        //}





        /// <summary>
        /// Supprimer une role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete("{id}"), Authorize]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        /// <summary>
        /// Check si role existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //private bool RoleExists(int id)
        //{
        //    return _context.Roles.Any(e => e.Id == id);
        //}


    }
}
