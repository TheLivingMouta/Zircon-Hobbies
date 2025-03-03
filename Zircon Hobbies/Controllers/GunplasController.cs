using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zircon_Hobbies.Data;
using Zircon_Hobbies.Models;

namespace Zircon_Hobbies.Controllers
{
    public class GunplasController : Controller
    {
        private readonly Zircon_HobbiesContext _context;

        public GunplasController(Zircon_HobbiesContext context)
        {
            _context = context;
        }

        // GET: Gunplas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gunpla.ToListAsync());
        }

        // GET: Gunplas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gunpla = await _context.Gunpla
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gunpla == null)
            {
                return NotFound();
            }

            return View(gunpla);
        }

        // GET: Gunplas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gunplas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,scale,ProductionCompanyId,Price")] Gunpla gunpla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gunpla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gunpla);
        }

        // GET: Gunplas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gunpla = await _context.Gunpla.FindAsync(id);
            if (gunpla == null)
            {
                return NotFound();
            }
            return View(gunpla);
        }

        // POST: Gunplas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,scale,ProductionCompanyId,Price")] Gunpla gunpla)
        {
            if (id != gunpla.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gunpla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GunplaExists(gunpla.Id))
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
            return View(gunpla);
        }

        // GET: Gunplas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gunpla = await _context.Gunpla
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gunpla == null)
            {
                return NotFound();
            }

            return View(gunpla);
        }

        // POST: Gunplas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gunpla = await _context.Gunpla.FindAsync(id);
            if (gunpla != null)
            {
                _context.Gunpla.Remove(gunpla);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GunplaExists(int id)
        {
            return _context.Gunpla.Any(e => e.Id == id);
        }
    }
}
