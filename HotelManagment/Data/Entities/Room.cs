using System;

namespace HotelManagment.Data.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public byte Type { get; set; }
    }
}
