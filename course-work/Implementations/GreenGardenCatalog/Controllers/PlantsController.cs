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
using X.PagedList;
using X.PagedList.EF;
using X.PagedList.Extensions;

namespace GreenGardenCatalog.Controllers
{
    public class PlantsController : Controller
    {
        private readonly GreenGardenCatalogDbContext _context;

        public PlantsController(GreenGardenCatalogDbContext context)
        {
            _context = context;
        }

        // GET: Plants
        public async Task<IActionResult> Index(string searchPlantingSeason, int? searchSoilId, string sortOrder, int? page)
        {
            //? "Yes" : "No"

            ViewData["PlantingSeasonSortParm"] = String.IsNullOrEmpty(sortOrder) ? "season_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            ViewBag.PlantingSeasonSearch = searchPlantingSeason;
            ViewBag.SoilIdSearch = searchSoilId;

           
            ViewBag.Soils = new SelectList(await _context.Soils.ToListAsync(), "Id", "Name");

            var plants = _context.Plants
                .Include(p => p.Soil)
                .Include(p => p.Fertilizer)
                .AsQueryable();

            
            if (!string.IsNullOrEmpty(searchPlantingSeason))
            {
                plants = plants.Where(p => p.PlantingSeason.Contains(searchPlantingSeason));
            }

            if (searchSoilId.HasValue)
            {
                plants = plants.Where(p => p.SoilId == searchSoilId.Value);
            }

            
            plants = sortOrder switch
            {
                "season_desc" => plants.OrderByDescending(p => p.PlantingSeason),
                "Price" => plants.OrderBy(p => p.Price),
                "price_desc" => plants.OrderByDescending(p => p.Price),
                _ => plants.OrderBy(p => p.PlantingSeason),
            };

            int pageSize = 4;
            int pageNumber = page ?? 1;

            return View(await plants.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plants/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.SoilId = new SelectList(_context.Soils, "Id", "Name");
            ViewBag.FertilizerId = new SelectList(_context.Fertilizers, "Id", "Name");
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Plant plant)
        {
            if (ModelState.IsValid)
            {
                _context.Plants.Add(plant);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.SoilId = new SelectList(_context.Soils, "Id", "Name", plant.SoilId);
            ViewBag.FertilizerId = new SelectList(_context.Fertilizers, "Id", "Name", plant.FertilizerId);
            return View(plant);
        }



        // GET: Plants/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PlantingSeason")] Plant plant)
        {
            if (id != plant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.Id))
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
            return View(plant);
        }

        // GET: Plants/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant != null)
            {
                _context.Plants.Remove(plant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}
