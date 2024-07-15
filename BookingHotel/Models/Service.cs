namespace BookingHotel.Models
{
    public class Service
    {
        public int serviceID { get; set; }
        public string roomHighlights { get; set; }
        public string bedAndBath { get; set; }
        public string servicesAndAmenities { get; set; }
        public int roomTypeDetailID { get; set; }

        public RoomTypeDetail RoomTypeDetail { get; set; }
    }
}
