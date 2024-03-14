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
using HamburgerAppV1.Models;

namespace HamburgerAppV1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]

    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Menu _menu;

        public MenuController(ApplicationDbContext context, Menu menu)
        {
            _context = context;
            _menu = menu;
        }

        // GET: Admin/Menu
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menuler.ToListAsync());
        }

        // GET: Admin/Menu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menuler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel menuViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //Fiyatın pozitif olması durumu
                    if (menuViewModel.MenuFiyat <= 0)
                        throw new Exception("Fiyat pozitif olmalıdır!");

                    //Aynı ürün isminden başka veremesin.
                    if (_context.Menuler.FirstOrDefault(u => u.MenuAd == menuViewModel.MenuAd) != null)
                        throw new Exception("Eklemek istediğiniz ürün mevcuttur!");

                    //Kişi resim yüklemek zorunda DEĞİL!
                    if (menuViewModel.Resim != null) //kişi resim seçtiyse
                    {
                        var dosyaAdi = menuViewModel.Resim.FileName;

                        var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", dosyaAdi);

                        var akisOrtami = new FileStream(konum, FileMode.Create);

                        menuViewModel.Resim.CopyTo(akisOrtami);

                        akisOrtami.Close();

                        _menu.ResimYolu = dosyaAdi;
                    }

                    _menu.MenuAd = menuViewModel.MenuAd;
                    _menu.MenuFiyat = menuViewModel.MenuFiyat;

                    //Db'ye ekle
                    _context.Menuler.Add(_menu);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View();

            }
            catch (Exception ex)
            {
                TempData["Durum"] = "Hata oluştu! " + ex.Message;
                return View();
            }
        }

        // GET: Admin/Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Menu guncellenecekMenu = await _context.Menuler.FindAsync(id);

            MenuViewModel guncellenecekVm = new MenuViewModel();

            if (guncellenecekMenu == null)
            {
                return NotFound();
            }

            guncellenecekVm.MenuAd = guncellenecekMenu.MenuAd;
            guncellenecekVm.MenuFiyat = guncellenecekMenu.MenuFiyat;

            TempData["Id"] = guncellenecekMenu.Id;

            return View(guncellenecekVm);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuViewModel menuViewModel)
        {

            var guncellenecekMenu = _context.Menuler.Find(TempData["Id"]);

            if (ModelState.IsValid)
            {
                try
                {

                    //Fiyatın pozitif olması durumu
                    if (menuViewModel.MenuFiyat <= 0)
                        throw new Exception("Fiyat pozitif olmalıdır!");

                    //Aynı ürün isminden başka veremesin.
                    if (_context.Menuler.FirstOrDefault(u => u.MenuAd == menuViewModel.MenuAd && u.Id != guncellenecekMenu.Id) != null)
                        throw new Exception("Güncellemek istediğiniz ürün ismi mevcuttur!");



                    //Kişi resim yüklemek zorunda DEĞİL!
                    if (menuViewModel.Resim != null && menuViewModel.Resim.FileName != guncellenecekMenu.ResimYolu) //kişi resim seçtiyse
                    {
                        ResimSil(guncellenecekMenu);

                        var dosyaAdi = menuViewModel.Resim.FileName;

                        var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", dosyaAdi);

                        var akisOrtami = new FileStream(konum, FileMode.Create);

                        menuViewModel.Resim.CopyTo(akisOrtami);

                        akisOrtami.Close();

                        guncellenecekMenu.ResimYolu = dosyaAdi;
                    }

                    guncellenecekMenu.MenuAd = menuViewModel.MenuAd;
                    guncellenecekMenu.MenuFiyat = menuViewModel.MenuFiyat;

                    _context.Menuler.Update(guncellenecekMenu);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Durum"] = "Hata oluştu! " + ex.Message;
                    return View();
                }

            }
            return View();

        }

        // GET: Admin/Menu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menuler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menuler.FindAsync(id);
            if (menu != null)
            {
                _context.Menuler.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menuler.Any(e => e.Id == id);
        }

        public void ResimSil(Menu menu)
        {
            var resmiKullananBaskaVarMi = _context.Menuler.Any(u => u.ResimYolu == menu.ResimYolu && u.Id != menu.Id);

            if (menu.ResimYolu != null && !resmiKullananBaskaVarMi)
            {
                string dosya = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", menu.ResimYolu);

                //Silme metoduna bu patikayı gönder.
                System.IO.File.Delete(dosya);
            }

        }
    }
}
