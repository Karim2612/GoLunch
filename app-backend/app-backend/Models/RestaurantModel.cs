using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace app_backend.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nom { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactTel { get; set; }
        [DisplayName("Site web URL")]
        [StringLength(1024)]
        public string? UrlSite { get; set; }
        public string? RestoPhoto { get; set; }
        public Localisation? Localisation { get; set; }
        public List<Menu> Menus { get; set; }

        //Navigations properties
        public List<Fav> Favoris { get; set; }

    }
}
