using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharamcyApp.Data;
using PharamcyApp.Models;
using PharamcyApp.ViewModel;

namespace PharamcyApp.Controllers
{
    [Authorize]
  
    public class PharmaciesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PharmaciesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pharmacies
        [Route("pharma")]
        public async Task<IActionResult> Index()
        {
            var orderd = _context.Pharmacy.OrderByDescending(a => a.PharmacyLocation).ThenBy(a => a.PharmacyName);
            return View(await orderd.ToListAsync());

        }

        // GET: Pharmacies/Details/5
        [Route("d/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.Pharmacy
                .FirstOrDefaultAsync(m => m.PharmacyID == id);
            if (pharmacy == null)
            {
                return NotFound();
            }
            //report
            var meds = await _context.Sell.Where(s => s.PharmacyID == id).ToListAsync();
            pharmacy.sells = meds;
            foreach(var a in pharmacy.sells)
            {
               // a.Medicine = _context.Medicine.Find(pharmacy.sells => pharmacy.sells  == id   );
            }
            return View(pharmacy);

           
        }

        // GET: Pharmacies/Create
        [Route("cr/{id?}")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pharmacies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cr/{id?}")]
        public async Task<IActionResult> Create([Bind("PharmacyID,PharmacyName,PharmacyLocation,phonenumber,workhour,email,emailConf,CoverImage,BackCoverImage")] Pharmacy pharmacy, IFormFile CoverImage, IFormFile BackCoverImage)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (BackCoverImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(BackCoverImage.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //This Code copy image to wwwroot
                    BackCoverImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    pharmacy.BackCoverImage = uniqueFileName;
                }
                if (CoverImage != null)
                {
                    //This Code copy image to database
                    var Stream = new MemoryStream();
                    await CoverImage.CopyToAsync(Stream);
                    pharmacy.CoverImage = Stream.ToArray();
                }
                _context.Add(pharmacy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacy);
        }

        // GET: Pharmacies/Edit/5
        [Route("ed/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.Pharmacy.FindAsync(id);
            if (pharmacy == null)
            {
                return NotFound();
            }
            return View(pharmacy);
        }

        // POST: Pharmacies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ed/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("PharmacyID,PharmacyName,PharmacyLocation,phonenumber,workhour,email,emailConf")] Pharmacy pharmacy)
        {
            if (id != pharmacy.PharmacyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyExists(pharmacy.PharmacyID))
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
            return View(pharmacy);
        }

        // GET: Pharmacies/Delete/5
        [Route("del/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.Pharmacy
                .FirstOrDefaultAsync(m => m.PharmacyID == id);
            if (pharmacy == null)
            {
                return NotFound();
            }

            return View(pharmacy);
        }

        // POST: Pharmacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("del/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacy = await _context.Pharmacy.FindAsync(id);
            _context.Pharmacy.Remove(pharmacy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("ser/{id?}")]
        //for search ........
        public async Task<IActionResult> Search()
        {
            SearchModel model = new SearchModel
            {
                PharmacyLocation = Cities.Amman,
                pharmacies = await _context.Pharmacy.ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        [Route("ser/{id?}")]
        public async Task<IActionResult> Search(ViewModel.SearchModel model)
        {
            var result = await _context.Pharmacy.ToListAsync();

            if ((model.PharmacyName ?? "") != "")
                result = result.Where(b => b.PharmacyName.ToLower().Contains(model.PharmacyName.ToLower())).ToList();

            result = result.Where(b => b.PharmacyLocation == model.PharmacyLocation).ToList();

            model.pharmacies = result;
            return View(model);
        }
        private bool PharmacyExists(int id)
        {
            return _context.Pharmacy.Any(e => e.PharmacyID == id);
        }
    }
}
