using System.ComponentModel.DataAnnotations;

namespace app_backend.Models
{
    //Inscription Utilisateur  Dto pour Data transfert Object
    public class UserRegister
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
