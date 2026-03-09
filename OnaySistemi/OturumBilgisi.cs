using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnaySistemi
{
    public static class OturumBilgisi
    {
        public static int KullaniciId { get; set; }
        public static string KullaniciAdi { get; set; }
        public static string AdSoyad { get; set; }
        public static string Rol { get; set; }

        public static void Temizle()
        {
            KullaniciId = 0;
            KullaniciAdi = null;
            AdSoyad = null;
            Rol = null;
        }
    }
}
