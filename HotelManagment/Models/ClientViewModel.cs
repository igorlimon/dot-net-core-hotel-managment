using System;
using HotelManagment.Data.Entities;

namespace HotelManagment.Models
{
    public class ClientViewModels
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string CNP { get; set; }

        public Client ToViewModels()
        {
            return new Client()
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
