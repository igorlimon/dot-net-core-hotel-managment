using System;
using System.Collections.Generic;
using System.Linq;
using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagment.Data.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public virtual IEnumerable<RoomReservation> RoomReservations { get; set; }


        public ReservationViewModel ToViewModel()
        {
            var viewModel = new ReservationViewModel()
            {
                ClientId = Client.Id,
                //RoomId = Ro.Nume,
                Id = Id,
                CheckIn = CheckIn,
                CheckOut = CheckOut,
            };
            return viewModel;
        }
    }
}
