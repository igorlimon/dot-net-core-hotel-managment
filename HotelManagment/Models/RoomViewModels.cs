using System;

namespace HotelManagment.Models
{
    public class RoomViewModels
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
    }
}
