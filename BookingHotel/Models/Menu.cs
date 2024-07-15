using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Menu
    {
        public int ID { get; set; }
        [Display(Name = "Dish Name")]
        [Required]
        public string dishName { get; set; }
        [Display(Name = "Dish Description")]
        [Required]
        public string dishDescription { get; set; }
        [Display(Name = "Dish Price")]
        [Required]
        public double dishPrice { get; set; }

        [Display(Name = "Dish Image")]
        public string dishImage { get; set; }
    }
}
