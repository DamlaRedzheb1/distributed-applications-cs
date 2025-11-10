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
    public class FertilizersController : Controller
    {
        private readonly GreenGardenCatalogDbContext _context;

        public FertilizersController(GreenGardenCatalogDbContext context)
        {
            _context = context;
        }

        // GET: Fertilizers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fertilizers.ToListAsync());
        }

        // GET: Fertilizers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fertilizer = await _context.Fertilizers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fertilizer == null)
            {
                return NotFound();
            }

            return View(fertilizer);
        }

        // GET: Fertilizers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fertilizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Usage,IsInStock,WeightInKg,Price")] Fertilizer fertilizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fertilizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fertilizer);
        }

        // GET: Fertilizers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fertilizer = await _context.Fertilizers.FindAsync(id);
            if (fertilizer == null)
            {
                return NotFound();
            }
            return View(fertilizer);
        }

        // POST: Fertilizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Usage")] Fertilizer fertilizer)
        {
            if (id != fertilizer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fertilizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FertilizerExists(fertilizer.Id))
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
            return View(fertilizer);
        }

        // GET: Fertilizers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fertilizer = await _context.Fertilizers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fertilizer == null)
            {
                return NotFound();
            }

            return View(fertilizer);
        }

        // POST: Fertilizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fertilizer = await _context.Fertilizers.FindAsync(id);
            if (fertilizer != null)
            {
                _context.Fertilizers.Remove(fertilizer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FertilizerExists(int id)
        {
            return _context.Fertilizers.Any(e => e.Id == id);
        }
    }
}
