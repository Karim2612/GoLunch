using NetTopologySuite.Geometries;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app_backend.Models
{
    public class Localisation
    {
        [Key]
        public int Id { get; set; }
        public string? Adresse { get; set; } = string.Empty;
        public int? CP { get; set; }
        public string? Ville { get; set; }
        public string? Canton { get; set; }
        public string? Pays { get; set; }
        //[Column(TypeName = "geography")]
        [JsonIgnore]
        public Point? Position { get; set; }
        public double? PosLatitude { get; set; }
        public double? PosLongitude { get; set; }


    }

}
