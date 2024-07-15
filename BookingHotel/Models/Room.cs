using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Room
    {
        public int roomID { get; set; }
        [Display(Name = "Room Name")]
        public string roomName { get; set; }
        [Display(Name = "Room Type")]
        public int roomTypeID { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        public RoomType RoomType { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; }
    }
}
