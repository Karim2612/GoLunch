using app_backend.Datas;
using app_backend.Models;
using app_backend.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        
        private readonly GolunchDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(GolunchDbContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _configuration = config;
            _mapper = mapper;
        }

        /// <summary>
        /// retourne utilisateur, utile pour Register mais invisible dans endpoints
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<User>> CheckUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        /// <summary>
        /// Inscription Utilisateur
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegister request)
        {
            // check si utilisateur existe déjà
            if (_context.Users.Any(x => x.Username == request.Username))
                return BadRequest("Le nom d\'utilisateur '" + request.Username + "' est déjà prit.");

            //Créé hash SHA512
            CreatePasswordHash(request.Password, out byte[] passHash, out byte[] passSalt);

            var user = _mapper.Map<User>(request);

            user.PassHash = passHash;
            user.PassSalt = passSalt;
            user.MobileNum = null;
            user.Role = Role.User;
            user.DateInscription = DateTime.Now;

            //Map les champs
            //user.Username = request.Username;
            //user.PassHash = passHash;
            //user.PassSalt = passSalt;
            //user.Email = request.Email;
            //user.MobileNum = null;
            //user.DateInscription = DateTime.Now;

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("CheckUser", new { id = user.Id }, user);

            //return Ok(user);

        }
        /// <summary>
        /// Connexion de l'utilisateur
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retourne string JWT Token</returns>
        //Flo string au lieu de User car retourne le Token qui comprends les infos de l'utilisateur
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin request)
        {

            var user = _context.Users.SingleOrDefault(x => x.Username == request.Username);
            
            if (user == null)
            {
                return BadRequest("Utilisateur non trouvé.");
            }
            if(!VerifyPasswordHash(request.Password, user.PassHash, user.PassSalt))
            {
                return BadRequest("Mauvais mot de passe.");
            }
            
            string token = CreateToken(user);
            //with System.Text.Json
            //var jsontoken = JsonSerializer.Serialize(token);
            //
            string json = JsonConvert.SerializeObject(new { tokenjwt=token, datecreated=DateTime.Now  });

            return Ok(json);
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT:Secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddDays(7),
                 signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }






    private void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passHash);
            }
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

    }
}
