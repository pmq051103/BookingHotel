using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class RoomTypeDetail
    {
        public int roomTypeDetailID { get; set; }
        [Display(Name = "Room Type")]
        public int roomTypeID { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }
        [Display(Name = "Occupancy")]
        [Required(ErrorMessage = "Occupancy is required.")]
        public int maxPeople { get; set; }
        [Display(Name = "View")]
        [Required(ErrorMessage = "View is required.")]
        public string view { get; set; }
        [Display(Name = "Size")]
        [Required(ErrorMessage = "Size is required.")]
        public string size { get; set; }
        [Display(Name = "Bed")]
        [Required(ErrorMessage = "Bed is required.")]
        public string bed { get; set; }


        public RoomType RoomType { get; set; }
        public Service Service { get; set; }
        public ICollection<RoomTypeImage> Images { get; set; }
    }
}
