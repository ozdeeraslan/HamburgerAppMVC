using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Entities
{
    public class Menu
    {
        public int Id { get; set; }

        [Display(Name = "Menü Adı")]
        public string MenuAd { get; set; } = null!;

        [Display(Name = "Menü Fiyati")]
        public decimal MenuFiyat { get; set; }

        public string? ResimYolu { get; set; } 

        public List<Siparis> Siparisler { get; set; } = new();
    }
}
