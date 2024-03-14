using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Areas.Admin.Models
{
    public class EnCokKullanilanEkstraMalzemeModel
    {
        [Display(Name = "Malzeme Adı")]
        public string MalzemeAdi { get; set; } = null!;

        [Display(Name = "Kullanılma Sayısı")]
        public int KullanilmaSayisi { get; set; }
    }
}
