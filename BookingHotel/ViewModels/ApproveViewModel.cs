using BookingHotel.Models;

namespace BookingHotel.ViewModels
{
    public class ApproveViewModel
    {
        public Request Request { get; set; }
        public List<Room> AvailableRooms { get; set; }
    }
}
