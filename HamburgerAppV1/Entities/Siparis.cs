using HamburgerAppV1.Data;
using HamburgerAppV1.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Entities
{
    public class Siparis
    {
        public Siparis()
        {
            ToplamTutar = 0;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen bir menü seçiniz")]
        public Menu Menu { get; set; } = null!;

        public MenuBoyutu Boyut { get; set; } = MenuBoyutu.Kucuk; //Enum

        public List<EkstraMalzeme> EkstraMalzemeler { get; set; } = new List<EkstraMalzeme>();


        public int MenuId { get; set; }

        public int Adet { get; set; }

        public decimal ToplamTutar { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public string IdentityUserId { get; set; } = null!;

        public IdentityUser Kullanici { get; set; } = null!;


        public void Hesapla()
        {
            if (Menu != null)
            {
                ToplamTutar = 0;
                ToplamTutar += Menu.MenuFiyat;

                switch (Boyut)
                {

                    case MenuBoyutu.Orta:
                        ToplamTutar += ToplamTutar * 0.1M;
                        break;
                    case MenuBoyutu.Buyuk:
                        ToplamTutar += ToplamTutar * 0.2M;
                        break;

                }

                ToplamTutar *= Adet;

                foreach (EkstraMalzeme item in EkstraMalzemeler)
                {
                    ToplamTutar += item.EktraMalzemeFiyat;
                }
            }
        }
    }
}
