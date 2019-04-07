using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagment.Data.Entities
{
    public class RoomReservation
    {
        public Guid Id { get; set; }
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
    }
}
