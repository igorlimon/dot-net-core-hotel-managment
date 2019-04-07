using HotelManagment.Data;
using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagment.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.Select(r => r.ToViewModel()).ToListAsync());
        }

        // GET: RoomViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomViewModels = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomViewModels == null)
            {
                return NotFound();
            }

            return View(roomViewModels.ToViewModel());
        }

        // GET: RoomViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Price,Type")] RoomViewModels roomViewModels)
        {
            var room = roomViewModels.ToEntity();
            if (ModelState.IsValid)
            {
                room.Id = Guid.NewGuid();
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room.ToViewModel());
        }

        // GET: RoomViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room.ToViewModel());
        }

        // POST: RoomViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Number,Price,Type")] RoomViewModels roomViewModels)
        {
            var room = roomViewModels.ToEntity();
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomViewModelsExists(room.Id))
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
            return View(room.ToViewModel());
        }

        // GET: RoomViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms.ToViewModel());
        }

        // POST: RoomViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rooms = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomViewModelsExists(Guid id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
