using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Models
{
    public class EkstraMalzemeViewModel
    {
        [Required(ErrorMessage = "Malzeme adı boş olamaz.")]
        public string EktraMalzemeAd { get; set; } = null!;


        [Required(ErrorMessage = "Malzeme fiyatı boş olamaz.")]
        public decimal EktraMalzemeFiyat { get; set; }
    }
}
