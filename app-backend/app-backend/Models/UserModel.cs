using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace app_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; } = string.Empty;   
        [EmailAddress]
        public string? Email { get; set; }
        public byte[]? PassHash { get; set; }
        public byte[]? PassSalt { get; set; }
        [Phone]
        public string? MobileNum { get; set; }
        public string? Prenom { get; set; }
        public string? Nom { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime? DateInscription { get; set; }
        public Role Role { get; set; }

        //Navigations properties
        public List<Fav> Favoris { get; set; }

    }
}
