using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Enums
{
    public enum MenuBoyutu
    {
        [Display(Name = "Küçük")]
        Kucuk,
        [Display(Name = "Orta")]
        Orta,
        [Display(Name = "Büyük")]
        Buyuk
    }
}
