using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerAppV1.Data;
using HamburgerAppV1.Entities;
using Microsoft.AspNetCore.Authorization;

namespace HamburgerAppV1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]

    public class EkstraMalzemeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EkstraMalzemeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/EkstraMalzeme
        public async Task<IActionResult> Index()
        {           
            return View(await _context.EkstraMalzemeler.ToListAsync());
        }

        // GET: Admin/EkstraMalzeme/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ekstraMalzeme = await _context.EkstraMalzemeler
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ekstraMalzeme == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ekstraMalzeme);
        //}

        // GET: Admin/EkstraMalzeme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/EkstraMalzeme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EktraMalzemeAd,EktraMalzemeFiyat")] EkstraMalzeme ekstraMalzeme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekstraMalzeme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ekstraMalzeme);
        }

        // GET: Admin/EkstraMalzeme/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekstraMalzeme = await _context.EkstraMalzemeler.FindAsync(id);
            if (ekstraMalzeme == null)
            {
                return NotFound();
            }
            return View(ekstraMalzeme);
        }

        // POST: Admin/EkstraMalzeme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EktraMalzemeAd,EktraMalzemeFiyat")] EkstraMalzeme ekstraMalzeme)
        {
            if (id != ekstraMalzeme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ekstraMalzeme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EkstraMalzemeExists(ekstraMalzeme.Id))
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
            return View(ekstraMalzeme);
        }

        // GET: Admin/EkstraMalzeme/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekstraMalzeme = await _context.EkstraMalzemeler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ekstraMalzeme == null)
            {
                return NotFound();
            }

            return View(ekstraMalzeme);
        }

        // POST: Admin/EkstraMalzeme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ekstraMalzeme = await _context.EkstraMalzemeler.FindAsync(id);
            if (ekstraMalzeme != null)
            {
                _context.EkstraMalzemeler.Remove(ekstraMalzeme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EkstraMalzemeExists(int id)
        {
            return _context.EkstraMalzemeler.Any(e => e.Id == id);
        }
    }
}
