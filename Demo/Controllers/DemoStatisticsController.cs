using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DemoStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemoStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DemoStatistics
        public async Task<IActionResult> Index()
        {
            return View(await _context.DemoStatistics.ToListAsync());
        }

        // GET: DemoStatistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demoStatistic = await _context.DemoStatistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demoStatistic == null)
            {
                return NotFound();
            }

            return View(demoStatistic);
        }

        // GET: DemoStatistics/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Population")] DemoStatistic demoStatistic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demoStatistic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(demoStatistic);
        }

        // GET: DemoStatistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demoStatistic = await _context.DemoStatistics.FindAsync(id);
            if (demoStatistic == null)
            {
                return NotFound();
            }
            return View(demoStatistic);
        }

        // POST: DemoStatistics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Population")] DemoStatistic demoStatistic)
        {
            if (id != demoStatistic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demoStatistic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemoStatisticExists(demoStatistic.Id))
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
            return View(demoStatistic);
        }

        // GET: DemoStatistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demoStatistic = await _context.DemoStatistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demoStatistic == null)
            {
                return NotFound();
            }

            return View(demoStatistic);
        }

        // POST: DemoStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var demoStatistic = await _context.DemoStatistics.FindAsync(id);
            _context.DemoStatistics.Remove(demoStatistic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemoStatisticExists(int id)
        {
            return _context.DemoStatistics.Any(e => e.Id == id);
        }
    }
}
