using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagment.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagment.Models
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public IEnumerable<Guid> RoomIds { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public IEnumerable<SelectListItem> Rooms { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }

    }
}
