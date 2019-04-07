using System;
using HotelManagment.Data;
using HotelManagment.Data.Entities;

namespace HotelManagment.Models
{
    public class RoomViewModels
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public RoomType Type { get; set; }


        public Room ToEntity()
        {
            return new Room()
            {
                Id = Id,
                Number = Number,
                Price = Price,
                Type = (byte)Type
            };
        }
    }
}
