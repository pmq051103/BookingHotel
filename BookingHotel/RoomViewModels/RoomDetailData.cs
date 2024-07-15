using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingHotel.Models.RoomViewModels
{
    public class RoomDetailData
    {
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
