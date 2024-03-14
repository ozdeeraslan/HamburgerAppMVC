using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Areas.Admin.Models
{
    public class EnCokSiparisEdilenMenuModel
    {
        [Display(Name = "Menu Adı")]
        public string MenuAdi { get; set; } = null!;

        [Display(Name = "Siparis Sayısı")]
        public int SiparisSayisi { get; set; }
    }
}
