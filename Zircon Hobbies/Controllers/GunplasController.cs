using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
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
        public async Task<IActionResult> Index(string GunplaType, string GunplaScale, string searchString)
        {

            if (_context.Gunpla == null)
            {
                return Problem("Enttiy set 'Zircon_HobbiesContext.Gunpla' is null");
            }

            IQueryable<string> TypeQuery = from g in _context.Gunpla
                                           orderby g.Type
                                           select g.Type;

            IQueryable<string> ScaleQuery = from s in _context.Gunpla
                                            orderby s.Scale
                                            select s.Scale;

            var gunplas = _context.Gunpla
         .Include(g => g.ProductionCompany)
         .AsQueryable();



            if (!string.IsNullOrEmpty(searchString))
            {
                gunplas = gunplas.Where(z => z.Name!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(GunplaScale))
            {
                gunplas = gunplas.Where(c => c.Scale == GunplaScale);
            }

            if (!string.IsNullOrEmpty(GunplaType))
            {
                gunplas = gunplas.Where(x => x.Type == GunplaType);
            }

            var GunplaVM = new GunplaViewModel
            {
                Types = new SelectList(await TypeQuery.Distinct().ToListAsync()),
                Scale = new SelectList(await ScaleQuery.Distinct().ToListAsync()),
                Gunplas = await gunplas.ToListAsync()
            };

            return View(GunplaVM);

        }

        // GET: Gunplas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gunpla = await _context.Gunpla
                .Include(g => g.ProductionCompany)
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

            ViewBag.ProductionCompany = _context.Production_Company
            .Select(pc => new SelectListItem
            {
                Value = pc.Id.ToString(),
                Text = pc.Name
            }).ToList();

            return View();
        }

        // POST: Gunplas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Scale,ProductionCompanyId,Price")] Gunpla gunpla)
        {

            ModelState.Remove("ProductionCompany");

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

            ViewBag.ProductionCompany = _context.Production_Company
        .Select(pc => new SelectListItem
        {
            Value = pc.Id.ToString(),
            Text = pc.Name
        }).ToList();

            return View(gunpla);
        }

        // POST: Gunplas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Scale,ProductionCompanyId,Price")] Gunpla gunpla)
        {
            if (id != gunpla.Id)
            {
                return NotFound();
            }

            ModelState.Remove("ProductionCompany");

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

            ViewBag.ProductionCompany = _context.Production_Company
        .Select(pc => new SelectListItem
        {
            Value = pc.Id.ToString(),
            Text = pc.Name
        }).ToList();

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
