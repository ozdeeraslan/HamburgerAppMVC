using HamburgerAppV1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamburgerAppV1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menuler { get; set; }

        public DbSet<EkstraMalzeme> EkstraMalzemeler { get; set; }

        public DbSet<Siparis> Siparisler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EkstraMalzeme>()
                .Property(em => em.EktraMalzemeFiyat)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Menu>()
                .Property(m => m.MenuFiyat)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Siparis>()
                .Property(m => m.ToplamTutar)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Menu>().HasData(
                new Menu() { Id = 1, MenuAd = "Cheeseburger Menü", MenuFiyat = 100, ResimYolu = "cheeseburger-menu.png" },
                new Menu() { Id = 2, MenuAd = "BigKing Menü", MenuFiyat = 120, ResimYolu = "big-king-menu.png" },
                new Menu() { Id = 3, MenuAd = "King Chicken Menü", MenuFiyat = 110, ResimYolu = "king-chicken-menu.png" },
                new Menu() { Id = 4, MenuAd = "Whopper Menü", MenuFiyat = 115, ResimYolu = "whopper-menu.png" },
                new Menu() { Id = 5, MenuAd = "Köfteburger Menü", MenuFiyat = 90, ResimYolu = "kofteburger-menu.png" }
                );

            modelBuilder.Entity<EkstraMalzeme>().HasData(
               new EkstraMalzeme() { Id = 1, EktraMalzemeAd = "Ketçap", EktraMalzemeFiyat = 2 },
               new EkstraMalzeme() { Id = 2, EktraMalzemeAd = "Mayonez", EktraMalzemeFiyat = 2 },
               new EkstraMalzeme() { Id = 3, EktraMalzemeAd = "Ranch Sos", EktraMalzemeFiyat = 4 },
               new EkstraMalzeme() { Id = 4, EktraMalzemeAd = "Buffalo Sos", EktraMalzemeFiyat = 4 },
               new EkstraMalzeme() { Id = 5, EktraMalzemeAd = "Barbekü Sos", EktraMalzemeFiyat = 4 }
               );




            base.OnModelCreating(modelBuilder);
        }
    }
}
