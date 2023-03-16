using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app_backend.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string? Entree { get; set; }
        [Required]
        public string? Plat { get; set; }
        public string? Dessert { get; set; }
        [Required]
        public double? Prix { get; set; }
        public bool? InclusBoisson { get; set; }
        public bool? InclusCafe { get; set; }
        public string PlatPhoto { get; set; }
        public DateTime? DateMenu { get; set; }
        public DateTime? DateModif { get; set; }
        public MenuType? Type { get; set; } = null;
        public int? TypeId { get; set; }
        public Localisation? Localisation { get; set; }
        public int? RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant? Restaurant { get; set; }
    }
}