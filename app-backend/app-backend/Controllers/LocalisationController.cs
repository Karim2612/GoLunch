using app_backend.Datas;
using app_backend.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalisationController : ControllerBase
    {
        private readonly GolunchDbContext _context;

        public LocalisationController(GolunchDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retourne toute la liste des localisations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localisation>>> GetAllLocalisations()
        {
            //return localisations.GetAll();
            return await _context.Localisations.ToListAsync();
        }

        /// <summary>
        /// Retourne une localisation selon son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Localisation>> GetLocalisation(int id)
        {
            var localisation = await _context.Localisations.FindAsync(id);

            if (localisation == null)
            {
                return NotFound();
            }

            return Ok(localisation);
        }

        /// <summary>
        /// Publie une nouvelle localisation
        /// </summary>
        /// <remarks>
        /// Exemple de requete:
        /// 
        ///     POST api/Localisation
        ///     { 
        ///       "id": 0,  
        ///       "adresse": "Place de la Gare",
        ///       "cp": 1004,
        ///       "ville": "Lausanne",
        ///       "canton": "Vaud",
        ///       "pays": "Suisse",
        ///       "posLatitude": 46.5172633100878,
        ///       "posLongitude": 6.629190546614018
        ///     }
        /// </remarks>
        /// <param name="localisation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Localisation>> PostLocalisation([FromBody] Localisation localisation)
        {   
            //Flo créer le type Point avec la longitude et latitude  
            double? lat = localisation.PosLatitude;
            double? lng = localisation.PosLongitude;
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            localisation.Position = geometryFactory.CreatePoint(new Coordinate((double)lng, (double)lat));

            _context.Localisations.Add(localisation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocalisationExists(localisation.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLocalisation", new { id = localisation.Id }, localisation);
        }

       

        /// <summary>
        /// Retourne des localisations selon la ville
        /// </summary>
        /// <param name="ville"></param>
        /// <returns></returns>
        [HttpGet("City/{ville}")]
        public async Task<ActionResult<List<Localisation>>> GetLocalisationByCity(string ville)
        {
            ville = FirstLetterToUpper(ville);

            var localisation = await _context.Localisations.Where(x => x.Ville == ville).ToListAsync();

            if (localisation == null)
            {
                return NotFound();
            }

            return Ok(localisation);
        }

        /// <summary>
        /// Retourne des localisations selon Latitude, Longitude et Distance 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [HttpGet("Nearby/{latitude}/{longitude}/{distance}")]
        public async Task<ActionResult<List<Localisation>>> GetLocalisationByLatLong(double latitude, double longitude, double distance)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var malocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            var nearby = await _context.Localisations
                            .OrderBy(x => x.Position.Distance(malocation))
                            .Where(x => x.Position.IsWithinDistance(malocation, distance))
                            .ToListAsync();

            //var result = await _context.Localisations.Where(x=> x.Position.Distance(malocation) < distance).ToListAsync();

            return Ok(nearby);
        }

        /// <summary>
        /// Modifie une ou plusieurs paramètres d'une Localisation
        /// </summary>
        /// <remarks>
        /// Exemple de requete :
        /// 
        /// [
        ///  {
        ///     "op": "replace",
        ///     "value": "Bern",
        ///     "path": "/Adresse"
        ///  }
        ///]
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public IActionResult PatchLocalisation(int id, [FromBody] JsonPatchDocument<Localisation> localisation)
        {
            var loc = _context.Localisations.FirstOrDefault(x => x.Id == id);

            if (loc == null)
            {
                return NotFound();
            }

            localisation.ApplyTo(loc, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Update(loc);

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalisationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(loc);
        }

        /// <summary>
        /// Modifie une localisation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="localisation"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateLocalisation(int id, [FromBody] Localisation localisation)
        {
            if (id != localisation.Id)
            {
                return BadRequest();
            }

            _context.Entry(localisation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalisationExists(id))
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
        /// Supprimer une localisation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteLocalisation(int id)
        {
            var localisation = await _context.Localisations.FindAsync(id);
            if (localisation == null)
            {
                return NotFound();
            }

            _context.Localisations.Remove(localisation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check si localisation existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LocalisationExists(int id)
        {
            return _context.Localisations.Any(e => e.Id == id);
        }

        /// <summary>
        /// Met tout une chaine de caractère en minuscule puis la 1ere Lettre en Majsucule
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string FirstLetterToUpper(string str)
        {
            str = str.ToLower();
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
