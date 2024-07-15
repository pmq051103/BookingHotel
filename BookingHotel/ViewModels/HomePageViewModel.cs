using BookingHotel.Models;

namespace BookingHotel.ViewModels
{
    public class HomePageViewModel
    {
        public Request Request { get; set; }
        public IEnumerable<RoomTypeViewModel> RoomTypeViewModels { get; set; }

        public IEnumerable<RoomType> RoomTypes { get; set; }

        public IEnumerable<Menu> Menus { get; set; }
    }
}
