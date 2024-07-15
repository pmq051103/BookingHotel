using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class RoomType
    {
        public int roomTypeID { get; set; }
        [Display(Name = "Room Type Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string roomTypeName { get; set; }
        [Display(Name = "Room Left")]
        public int roomLeft { get; set; }
        [Display(Name = "Price")]
        public double price { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public RoomTypeDetail RoomTypeDetail { get; set; }
        public ICollection<Request> Bookings { get; set; }
    }
}
