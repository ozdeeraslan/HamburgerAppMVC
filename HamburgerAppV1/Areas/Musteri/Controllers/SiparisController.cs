using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerAppV1.Data;
using HamburgerAppV1.Entities;
using HamburgerAppV1.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    [Authorize(Roles = "Musteri")]
    public class SiparisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SiparisController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("Menuler")]
        public async Task<IActionResult> MenuleriIncele()
        {
            return View(await _context.Menuler.ToListAsync());
        }

        // GET: Musteri/Siparis
        [Route("Siparislerim")]
        public async Task<IActionResult> Index()
        {
            var kullanici = await _userManager.GetUserAsync(User);

            var applicationDbContext = _context.Siparisler
                .Include(s => s.Kullanici)
                .Include(s => s.Menu)
                .Include(s => s.EkstraMalzemeler)
                .Where(s => s.IdentityUserId == kullanici.Id);

            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> SiparisBilgileri(Siparis siparis, List<int> ekstraMalzemeler)
        {
            return View(siparis);
        }


        [HttpPost]
        public IActionResult SiparisBilgileri()
        {
            return RedirectToAction("MenuleriIncele");
        }


        // GET: Musteri/Siparis/Details/5
        [Route("SiparisDetaylari/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler
                .Include(s => s.Kullanici)
                .Include(s => s.Menu)
                .Include(s => s.EkstraMalzemeler)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // GET: Musteri/Siparis/Create
        [Route("YeniSiparis")]
        public IActionResult Create()
        {
            ViewBag.Menuler = _context.Menuler.ToList();
            ViewBag.Boyutlar = Enum.GetValues(typeof(MenuBoyutu))
                    .Cast<MenuBoyutu>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = GetEnumDisplayName(e)
                    })
                    .ToList();

            ViewBag.EkstraMalzemeler = _context.EkstraMalzemeler.ToList();

            return View();
        }


        private string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            return attribute == null ? value.ToString() : attribute.GetName();
        }


        // POST: Musteri/Siparis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("YeniSiparis")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Siparis siparis, List<int> ekstraMalzemeler)
        {
            ViewBag.Menuler = _context.Menuler.ToList();
            ViewBag.Boyutlar = Enum.GetValues(typeof(MenuBoyutu)).Cast<MenuBoyutu>().Select(e => new SelectListItem { Value = e.ToString(), Text = Enum.GetName(typeof(MenuBoyutu), e) }).ToList();
            ViewBag.EkstraMalzemeler = _context.EkstraMalzemeler.ToList();


            if (siparis.MenuId == 0)
            {
                TempData["Hata"] = "Lütfen Menü Seçiniz";

                return View();
            }

            var menu = _context.Menuler.Find(siparis.MenuId);

            siparis.Menu = menu;
            siparis.EkstraMalzemeler = _context.EkstraMalzemeler.Where(e => ekstraMalzemeler.Contains(e.Id)).ToList();
            siparis.SiparisTarihi = DateTime.Now;
            siparis.IdentityUserId = _userManager.GetUserId(User);
            siparis.Kullanici = _userManager.GetUserAsync(User).Result; // Bu satırda `.Result` kullanarak asenkron beklemeyi sağlıyoruz.

            siparis.Hesapla();

            if (siparis != null)
            {

                _context.Siparisler.Add(siparis);
                _context.SaveChanges();

                return View("SiparisBilgileri", siparis);
            }



            return View(siparis);
        }


        //// GET: Musteri/Siparis/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var siparis = await _context.Siparisler.FindAsync(id);
        //    if (siparis == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Menuler = _context.Menuler.ToList();
        //    ViewBag.Boyutlar = Enum.GetValues(typeof(MenuBoyutu)).Cast<MenuBoyutu>().Select(e => new SelectListItem { Value = e.ToString(), Text = Enum.GetName(typeof(MenuBoyutu), e) }).ToList();
        //    ViewBag.EkstraMalzemeler = _context.EkstraMalzemeler.ToList();

        //    return View(siparis);
        //}

        // POST: Musteri/Siparis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Siparis siparis, List<int> ekstraMalzemeler)
        //{
        //    if (id != siparis.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var menu = await _context.Menuler.FindAsync(siparis.MenuId);
        //            if (menu == null)
        //            {
        //                TempData["Hata"] = "Lütfen Menü Seçiniz.";
        //                return View(siparis);
        //            }

        //            siparis.Menu = menu;
        //            siparis.EkstraMalzemeler = _context.EkstraMalzemeler.Where(e => ekstraMalzemeler.Contains(e.Id)).ToList();
        //            siparis.SiparisTarihi = DateTime.Now;
        //            siparis.IdentityUserId = _userManager.GetUserId(User);
        //            siparis.Kullanici = await _userManager.GetUserAsync(User);

        //            _context.Update(siparis);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SiparisExists(siparis.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.Menuler = _context.Menuler.ToList();
        //    ViewBag.Boyutlar = Enum.GetValues(typeof(MenuBoyutu)).Cast<MenuBoyutu>().Select(e => new SelectListItem { Value = e.ToString(), Text = Enum.GetName(typeof(MenuBoyutu), e) }).ToList();
        //    ViewBag.EkstraMalzemeler = _context.EkstraMalzemeler.ToList();
        //    return View(siparis);
        //}

        // GET: Musteri/Siparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler
                .Include(s => s.Kullanici)
                .Include(s => s.Menu)
                .Include(s => s.EkstraMalzemeler)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: Musteri/Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siparis = await _context.Siparisler.FindAsync(id);
            if (siparis != null)
            {
                _context.Siparisler.Remove(siparis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
            return _context.Siparisler.Any(e => e.Id == id);
        }
    }
}
