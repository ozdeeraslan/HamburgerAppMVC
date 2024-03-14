using HamburgerAppV1.Areas.Admin.Models;
using HamburgerAppV1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace HamburgerAppV1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var dashboard = new DashboardViewModel
            {
                EnCokSiparisEdilenMenuler = GetEnCokSiparisEdilenMenuler(),
                EnCokKullanilanEkstraMalzemeler = GetEnCokKullanilanEkstraMalzemeler()
            };

            return View(dashboard);
        }

        public IActionResult EnCokSiparisEdilenMenuler()
        {
            var enCokSiparisEdilenMenuler = GetEnCokSiparisEdilenMenuler();
            var viewModel = new DashboardViewModel
            {
                EnCokSiparisEdilenMenuler = enCokSiparisEdilenMenuler,
                EnCokKullanilanEkstraMalzemeler = new List<EnCokKullanilanEkstraMalzemeModel>()
            };
            return View(viewModel);
        }

        public IActionResult EnCokKullanilanEkstraMalzemeler()
        {
            var enCokKullanilanEkstraMalzemeler = GetEnCokKullanilanEkstraMalzemeler();
            var viewModel = new DashboardViewModel
            {
                EnCokSiparisEdilenMenuler = new List<EnCokSiparisEdilenMenuModel>(),
                EnCokKullanilanEkstraMalzemeler = enCokKullanilanEkstraMalzemeler
            };
            return View(viewModel);
        }

        public async Task<IActionResult> KayitliKullanicilar()
        {
            var kullanici = await _userManager.GetUserAsync(User);

            var applicationDbContext = _db.Siparisler
                .Include(s => s.Kullanici)
                .Where(s => s.IdentityUserId != null)
                .Select(s => s.Kullanici)
                .Distinct();

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> TumSiparisler()
        {
            var siparisler = await _db.Siparisler
                .Include(s => s.Kullanici)
                .Include(s => s.Menu)
                .Include(s => s.EkstraMalzemeler)
                .ToListAsync();

            return View(siparisler);

        }

        private List<EnCokSiparisEdilenMenuModel> GetEnCokSiparisEdilenMenuler()
        {
            return _db.Siparisler
                .GroupBy(s => s.Menu.MenuAd)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => new EnCokSiparisEdilenMenuModel
                {
                    MenuAdi = grp.Key,
                    SiparisSayisi = grp.Count()
                })
                .ToList();
        }

        private List<EnCokKullanilanEkstraMalzemeModel> GetEnCokKullanilanEkstraMalzemeler()
        {
            return _db.Siparisler
                .SelectMany(s => s.EkstraMalzemeler)
                .GroupBy(em => em.EktraMalzemeAd)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => new EnCokKullanilanEkstraMalzemeModel
                {
                    MalzemeAdi = grp.Key,
                    KullanilmaSayisi = grp.Count()
                })
                .ToList();
        }
    }
}
