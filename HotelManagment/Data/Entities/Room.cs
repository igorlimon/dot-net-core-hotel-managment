using System;
using HotelManagment.Models;

namespace HotelManagment.Data.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public byte Type { get; set; }

        public RoomViewModels ToViewModel()
        {
            return new RoomViewModels()
            {
                Id= Id,
                Number = Number,
                Price = Price,
                Type = Type.MapToRoomType()
            };
        }
    }
}
