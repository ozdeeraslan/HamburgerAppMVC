using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Entities
{
    public class EkstraMalzeme
    {
        public int Id { get; set; }

        [Display(Name = "Malzeme Adi")]
        public string EktraMalzemeAd { get; set; } = null!;

        [Display(Name = "Malzeme Fiyati")]
        public decimal EktraMalzemeFiyat { get; set; }

        public List<Siparis> Siparisler { get; set; } = new List<Siparis>();
    }
}
