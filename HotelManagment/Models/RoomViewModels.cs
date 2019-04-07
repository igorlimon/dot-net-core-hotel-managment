using System;
using HotelManagment.Data;

namespace HotelManagment.Models
{
    public class RoomViewModels
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public RoomType Type { get; set; }
    }
}
