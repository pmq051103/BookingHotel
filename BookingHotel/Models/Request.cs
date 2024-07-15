using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Request
    {
        public int requestID { get; set; }
        public int accountID { get; set; }
        [Display(Name ="Check-In Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime dateCheckIn { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check-Out Date")]
        public DateTime dateCheckOut { get; set; }
        [Display(Name = "Max People")]
        public int guestCount { get; set; }
        public int roomTypeID { get; set; }
        [Display(Name = "Message")]
        public string message { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        public Account Account { get; set; }
        public RoomType RoomType { get; set; }
    }
}
