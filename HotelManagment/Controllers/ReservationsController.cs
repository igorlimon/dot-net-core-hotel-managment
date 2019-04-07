using HotelManagment.Data;
using HotelManagment.Data.Entities;
using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagment.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservation = new List<ReservationViewModel>();
            var dbReservations = await _context.Reservations.Include(r => r.Client).ToListAsync();
            foreach (var dbReservation in dbReservations)
            {
                var viewModel = dbReservation.ToViewModel();
                viewModel.Rooms = _context.Rooms.Select(r => new SelectListItem(r.Number, r.Id.ToString()));
                reservation.Add(viewModel);
            }

            return View(reservation);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationViewModel = await _context.Reservations
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationViewModel == null)
            {
                return NotFound();
            }
            var viewModel = reservationViewModel.ToViewModel();

            viewModel.Rooms = _context.Rooms.Select(r => new SelectListItem(r.Number, r.Id.ToString()));
            viewModel.Clients = _context.Client.Select(r => new SelectListItem(r.Nume, r.Id.ToString()));

            return View(viewModel);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            var viewModel = new ReservationViewModel();
            viewModel.Rooms = _context.Rooms.Select(r => new SelectListItem(r.Number, r.Id.ToString()));
            viewModel.Clients = _context.Client.Select(r => new SelectListItem(r.Nume, r.Id.ToString()));

            return View(viewModel);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _context.Client.Find(reservationViewModel.ClientId);
                var reservation = new Reservation()
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    CheckIn = reservationViewModel.CheckIn,
                    CheckOut = reservationViewModel.CheckOut
                };
                _context.Add(reservation);
                foreach (var roomId in reservationViewModel.RoomIds)
                {
                    var room = _context.Rooms.Find(roomId);
                    var roomReservation = new RoomReservation();
                    roomReservation.Room = room;
                    roomReservation.Reservation = reservation;
                    _context.Add(roomReservation);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationViewModel);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationViewModel = _context.Reservations
                .Where(r => r.Id == id)
                .Include(c => c.Client)
                .FirstOrDefault();
            if (reservationViewModel == null)
            {
                return NotFound();
            }
           
            var viewModel = reservationViewModel.ToViewModel();
            viewModel.Rooms = _context.Rooms.Select(r => new SelectListItem(r.Number, r.Id.ToString()));
            viewModel.Clients = _context.Client.Select(r => new SelectListItem(r.Nume, r.Id.ToString()));

            return View(viewModel);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClientName,ClientId,CheckIn,CheckOut,RoomIds")] ReservationViewModel reservationViewModel)
        {
            
            if (id != reservationViewModel.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var reservationFound = _context.Reservations
                    .Where(r => r.Id == id)
                    .Include(c => c.Client)
                    .FirstOrDefault();

                try
                {
                //    foreach (var roomId in reservationViewModel.RoomIds)
                //    {

                //        _context.RoomReservations.Remove(new RoomReservation {Id = roomId});

                //    }
                    reservationFound.CheckIn = reservationViewModel.CheckIn;
                    reservationFound.CheckOut = reservationViewModel.CheckOut;
                    reservationFound.Client = _context.Client.Find(reservationViewModel.ClientId);
                    reservationFound.RoomReservations=new List<RoomReservation>();

                    

                    foreach (var roomId in reservationViewModel.RoomIds)
                    {
                        var room = _context.Rooms.Find(roomId);
                        var roomReservation = new RoomReservation();
                        roomReservation.Room = room;
                        roomReservation.Reservation = reservationFound;
                        _context.Add(roomReservation);
                    }
                    _context.Update(reservationFound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationViewModelExists(reservationViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservationViewModel);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _context.Reservations
                .Where(r => r.Id == id)
                 .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel == null)
            {
                return NotFound();
            }

            var reservationViewModel = viewModel.ToViewModel();
            return View(reservationViewModel);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservation = await _context.Reservations 
                .Where(r => r.Id == id)
                .Include(c => c.RoomReservations)
                .FirstOrDefaultAsync(m => m.Id == id);
            foreach (var roomReservation in reservation.RoomReservations)
            {
                _context.RoomReservations.Remove(roomReservation);
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationViewModelExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
