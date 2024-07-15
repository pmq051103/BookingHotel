namespace BookingHotel.Models
{
    public class Enrollment
    {
        public int enrollmentID { get; set; }
        public int roomID { get; set; }
        public int accountID { get; set; }
        public int RequestID { get; set; }
        public DateTime dateOfReceipt { get; set; }

        public Room Room { get; set; }
        public Account Account { get; set; }
    }
}
