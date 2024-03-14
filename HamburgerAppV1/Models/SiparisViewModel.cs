using HamburgerAppV1.Data;
using HamburgerAppV1.Entities;
using HamburgerAppV1.Enums;
using Microsoft.AspNetCore.Identity;

namespace HamburgerAppV1.Models
{
    public class SiparisViewModel
    {     
        public List<Menu> Menuler { get; set; } = new List<Menu>();

        public MenuBoyutu Boyut { get; set; } = MenuBoyutu.Kucuk; //Enum

        public List<EkstraMalzeme> EkstraMalzemeler { get; set; } = new List<EkstraMalzeme>();

        public int Adet { get; set; }

        public decimal ToplamTutar { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public string IdentityUserId { get; set; } = null!;

        public IdentityUser Kullanici { get; set; } = null!;

    }
}
