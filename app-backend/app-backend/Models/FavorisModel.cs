using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app_backend.Models
{
    public class Fav
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
