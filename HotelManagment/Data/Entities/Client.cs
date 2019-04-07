using System;
using System.Collections.Generic;
using HotelManagment.Models;

namespace HotelManagment.Data.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string CNP { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }

        public ClientViewModels ToViewModels()
        {
            return new ClientViewModels()
            {
                Id = Id,
                Nume = Nume,
                Prenume = Prenume,
                Email = Email,
                Telefon = Telefon,
                Adresa = Adresa,
                CNP = CNP
            };
        }
    }
}
