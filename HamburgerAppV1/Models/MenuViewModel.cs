using HamburgerAppV1.Attributes;
using HamburgerAppV1.Entities;
using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Models
{
    public class MenuViewModel
    {
        [Display(Name = "Menü Adı")]
        [Required(ErrorMessage = "Menü adı boş olamaz.")]
        public string MenuAd { get; set; } = null!;

        [Display(Name = "Menü Fiyati")]
        [Required(ErrorMessage = "Menü fiyatı boş olamaz.")]
        public decimal MenuFiyat { get; set; }

        [GecerliResim(MaxDosyaBoyutuMB = 1)]
        [Display(Name = "Resim")]
        [Required(ErrorMessage = "Resim boş olamaz.")]
        public IFormFile Resim { get; set; }

    }
}
