using GreenGardenCatalog.Entities;
using GreenGardenCatalog.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenGardenCatalog.Controllers
{
    public class SoilsController : Controller
    {
        private readonly GreenGardenCatalogDbContext _context;

        public SoilsController(GreenGardenCatalogDbContext context)
        {
            _context = context;
        }

        // GET: Soils
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soils.ToListAsync());
        }

        // GET: Soils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soil = await _context.Soils
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soil == null)
            {
                return NotFound();
            }

            return View(soil);
        }

        // GET: Soils/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Soil soil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(soil);
        }

        // GET: Soils/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soil = await _context.Soils.FindAsync(id);
            if (soil == null)
            {
                return NotFound();
            }
            return View(soil);
        }

        // POST: Soils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ph")] Soil soil)
        {
            if (id != soil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoilExists(soil.Id))
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
            return View(soil);
        }

        // GET: Soils/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soil = await _context.Soils
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soil == null)
            {
                return NotFound();
            }

            return View(soil);
        }

        // POST: Soils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soil = await _context.Soils.FindAsync(id);
            if (soil != null)
            {
                _context.Soils.Remove(soil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoilExists(int id)
        {
            return _context.Soils.Any(e => e.Id == id);
        }
    }
}
