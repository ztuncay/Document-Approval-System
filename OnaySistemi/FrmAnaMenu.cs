using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmAnaMenu : Form
    {
        public FrmAnaMenu()
        {
            InitializeComponent();
        }

        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            string adSoyad = string.IsNullOrWhiteSpace(OturumBilgisi.AdSoyad)
                ? OturumBilgisi.KullaniciAdi
                : OturumBilgisi.AdSoyad;

            lblKullaniciBilgi.Text = $"Kullanıcı: {adSoyad}";

            btnBelgeGonderim.Enabled = false;
            btnOnayEkrani.Enabled = false;
            btnRevizeTamamlama.Enabled = false;
            btnBelgeListesi.Enabled = false;
            btnAdminPanel.Enabled = false;

            string rol = OturumBilgisi.Rol ?? "";

            if (rol.Equals("Yonetici", StringComparison.OrdinalIgnoreCase))
            {
                btnAdminPanel.Enabled = true;
                btnBelgeListesi.Enabled = true; // Admin da belgeleri görebilsin
            }
            else if (rol.Equals("Kalite", StringComparison.OrdinalIgnoreCase))
            {
                btnBelgeGonderim.Enabled = true;
                btnRevizeTamamlama.Enabled = true;
                btnBelgeListesi.Enabled = true;
            }
            else if (rol.Equals("Onayci", StringComparison.OrdinalIgnoreCase))
            {
                btnOnayEkrani.Enabled = true;
                btnBelgeListesi.Enabled = true;
            }
            else if (rol.Equals("Goruntuleme", StringComparison.OrdinalIgnoreCase))
            {
                btnBelgeListesi.Enabled = true;
            }
            else
            {
                btnBelgeListesi.Enabled = true;
            }

            OzetBilgileriYukle();
        }

        private void OzetBilgileriYukle()
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                string rol = OturumBilgisi.Rol ?? "";
                int kullaniciId = OturumBilgisi.KullaniciId;
                string adSoyad = OturumBilgisi.AdSoyad ?? "";

                int bekleyenSayi = 0;
                int ikinciMetrik = 0;
                string ikinciMetin = "";
                string rolGorunum = rol;

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                {
                    baglanti.Open();

                    if (rol.Equals("Kalite", StringComparison.OrdinalIgnoreCase))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*) 
                            FROM BelgeGonderim
                            WHERE OnayDurumu = 'Beklemede';", baglanti))
                        {
                            bekleyenSayi = (int)cmd.ExecuteScalar();
                        }

                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*)
                            FROM BelgeGonderim
                            WHERE CAST(KayitTarihi AS DATE) = CAST(GETDATE() AS DATE);", baglanti))
                        {
                            ikinciMetrik = (int)cmd.ExecuteScalar();
                        }

                        ikinciMetin = "Bugün gönderilen belge:";
                        lblOzetBaslik.Text = "Genel Özet (Kalite)";
                    }
                    else if (rol.Equals("Onayci", StringComparison.OrdinalIgnoreCase))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*) 
                            FROM BelgeGonderim
                            WHERE (OnayDurumu = 'Beklemede'
                                   OR REPLACE(OnayDurumu, ' ', '') IN (N'1.Onaylandı,2.Beklemede', N'1.Onaylandi,2.Beklemede'))
                              AND (Onayci1Id = @KullaniciId OR Onayci2Id = @KullaniciId);", baglanti))
                        {
                            cmd.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                            bekleyenSayi = (int)cmd.ExecuteScalar();
                        }

                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*)
                            FROM BelgeGonderim
                            WHERE Onaylayan = @Onaylayan
                              AND OnayTarihi >= DATEADD(DAY, -7, GETDATE())
                              AND OnayDurumu IN ('Onaylandı','Tam Onaylandı');", baglanti))
                        {
                            cmd.Parameters.AddWithValue("@Onaylayan", adSoyad);
                            ikinciMetrik = (int)cmd.ExecuteScalar();
                        }

                        ikinciMetin = "Son 7 günde onayladığınız:";
                        lblOzetBaslik.Text = "Genel Özet (Onaycı)";
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*) 
                            FROM BelgeGonderim
                            WHERE OnayDurumu = 'Beklemede';", baglanti))
                        {
                            bekleyenSayi = (int)cmd.ExecuteScalar();
                        }

                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*)
                            FROM BelgeGonderim
                            WHERE OnayDurumu IN ('Onaylandı','Tam Onaylandı');", baglanti))
                        {
                            ikinciMetrik = (int)cmd.ExecuteScalar();
                        }

                        ikinciMetin = "Onaylanmış belge sayısı:";
                        lblOzetBaslik.Text = "Genel Özet (Genel)";
                    }
                }

                lblBekleyenDeger.Text = bekleyenSayi.ToString();
                lblBugunDeger.Text = ikinciMetrik.ToString();
                lblBugunText.Text = ikinciMetin;
                lblRolDeger.Text = string.IsNullOrWhiteSpace(rolGorunum) ? "-" : rolGorunum;
            }
            catch (Exception ex)
            {
                lblOzetBaslik.Text = "Genel Özet (Hata)";
                lblBekleyenText.Text = "Hata:";
                lblBekleyenDeger.Text = "!";
                lblBugunText.Text = ex.Message;
                lblBugunDeger.Text = "";
                lblRolDeger.Text = "-";
            }
        }

        private void btnBelgeGonderim_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmBelgeGonderim())
            {
                frm.ShowDialog(this);
            }

            OzetBilgileriYukle();
        }

        private void btnOnayEkrani_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmOnayEkrani())
            {
                frm.ShowDialog(this);
            }

            OzetBilgileriYukle();
        }

        private void btnRevizeTamamlama_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmRevizeTamamlama())
            {
                frm.ShowDialog(this);
            }

            OzetBilgileriYukle();
        }

        private void btnBelgeListesi_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmBelgeListesi())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnSifreDeğiştir_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSifreDeğiştir())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            AuditLogHelper.KullaniciCikis(
                OturumBilgisi.KullaniciId,
                OturumBilgisi.KullaniciAdi,
                OturumBilgisi.AdSoyad);

            Application.Exit();
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmAdminPanel())
            {
                frm.ShowDialog(this);
            }
        }
    }
}
