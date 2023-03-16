using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace app_backend.Models
{
    public class MenuType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nom { get; set; }
        //public ICollection<Menu>? Menus { get; set; }
    }
}
