using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharamcyApp.Data;
using PharamcyApp.Models;

namespace PharamcyApp.Controllers
{
    
    public class SellsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sells
        [Route("s")]
        public async Task<IActionResult> Index()
        {
           var applicationDbContext = _context.Sell.Include(s => s.Medicine).Include(s => s.Pharmacy);
          // var orderd= applicationDbContext.OrderBy(d => d.Pharmacy.PharmacyName);
           var filtterd = applicationDbContext.Where(a => a.Pharmacy.PharmacyLocation == Cities.Zaraqa);
           return View(await filtterd.ToListAsync());
        }

        // GET: Sells/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sell = await _context.Sell
                .Include(s => s.Medicine)
                .Include(s => s.Pharmacy)
                .FirstOrDefaultAsync(m => m.SellID == id);
            if (sell == null)
            {
                return NotFound();
            }
            
            //var p = await _context.Sell.Where(m => m.MedicineID == id).ToListAsync();
            //sell.Medicine.sells = p;
            return View(sell);
        }

        // GET: Sells/Create
        public IActionResult Create()
        {
            ViewData["MedicineID"] = new SelectList(_context.Medicine, "MedicineID", "MedicineName");
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacy, "PharmacyID", "PharmacyName");
            return View();
        }

        // POST: Sells/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellID,MedicineID,PharmacyID")] Sell sell)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineID"] = new SelectList(_context.Medicine, "MedicineID", "MedicineName", sell.MedicineID);
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacy, "PharmacyID", "PharmacyName", sell.PharmacyID);
            return View(sell);
        }

        // GET: Sells/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sell = await _context.Sell.FindAsync(id);
            if (sell == null)
            {
                return NotFound();
            }
            ViewData["MedicineID"] = new SelectList(_context.Medicine, "MedicineID", "MedicineName", sell.MedicineID);
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacy, "PharmacyID", "PharmacyName", sell.PharmacyID);
            return View(sell);
        }

        // POST: Sells/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellID,MedicineID,PharmacyID")] Sell sell)
        {
            if (id != sell.SellID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sell);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellExists(sell.SellID))
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
            ViewData["MedicineID"] = new SelectList(_context.Medicine, "MedicineID", "MedicineName", sell.MedicineID);
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacy, "PharmacyID", "PharmacyName", sell.PharmacyID);
            return View(sell);
        }

        // GET: Sells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sell = await _context.Sell
                .Include(s => s.Medicine)
                .Include(s => s.Pharmacy)
                .FirstOrDefaultAsync(m => m.SellID == id);
            if (sell == null)
            {
                return NotFound();
            }

            return View(sell);
        }

        // POST: Sells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sell = await _context.Sell.FindAsync(id);
            _context.Sell.Remove(sell);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellExists(int id)
        {
            return _context.Sell.Any(e => e.SellID == id);
        }
    }
}
