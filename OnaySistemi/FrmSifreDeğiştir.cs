using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmSifreDeðiþtir : Form
    {
        private readonly string _baglantiMetni;

        public FrmSifreDeðiþtir()
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
        }

        private void FrmSifreDeðiþtir_Load(object sender, EventArgs e)
        {
            lblKullanici.Text = $"Kullanýcý: {OturumBilgisi.AdSoyad} ({OturumBilgisi.KullaniciAdi})";
            lblGereksinimler.Text = "Gereken: Minimum 6 karakter, en az 1 rakam, en az 1 büyük harf";
            
            txtEskiSifre.Focus();
        }

        private void btnDeðiþtir_Click(object sender, EventArgs e)
        {
            lblMesaj.Text = "";
            lblMesaj.ForeColor = System.Drawing.Color.Red;

            string eskiSifre = txtEskiSifre.Text;
            string yeniSifre = txtYeniSifre.Text;
            string yeniSifreDoðrula = txtYeniSifreDoðrula.Text;

            if (string.IsNullOrWhiteSpace(eskiSifre))
            {
                lblMesaj.Text = "Eski þifre boþ býrakýlamaz.";
                return;
            }

            if (string.IsNullOrWhiteSpace(yeniSifre))
            {
                lblMesaj.Text = "Yeni þifre boþ býrakýlamaz.";
                return;
            }

            if (string.IsNullOrWhiteSpace(yeniSifreDoðrula))
            {
                lblMesaj.Text = "Yeni þifre (doðrulama) boþ býrakýlamaz.";
                return;
            }

            if (yeniSifre != yeniSifreDoðrula)
            {
                lblMesaj.Text = "Yeni þifreler eþleþmiyor.";
                return;
            }

            if (eskiSifre == yeniSifre)
            {
                lblMesaj.Text = "Yeni þifre eski þifre ile ayný olamaz.";
                return;
            }

            var (gecerli, mesaj) = SifreServisi.SifreGeçerliliðiKontrol(yeniSifre);
            if (!gecerli)
            {
                lblMesaj.Text = mesaj;
                return;
            }

            try
            {
                string dbHashliSifre = "";

                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(
                    "SELECT Sifre FROM Kullanicilar WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", OturumBilgisi.KullaniciId);
                    baglanti.Open();

                    object result = komut.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        dbHashliSifre = Convert.ToString(result);
                    }
                }

                if (!SifreServisi.SifreDoðrula(eskiSifre, dbHashliSifre))
                {
                    lblMesaj.Text = "Eski þifre hatalý.";
                    return;
                }

                string yeniHashliSifre = SifreServisi.SifreHashle(yeniSifre);

                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(
                    "UPDATE Kullanicilar SET Sifre = @Sifre WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Sifre", yeniHashliSifre);
                    komut.Parameters.AddWithValue("@Id", OturumBilgisi.KullaniciId);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }

                AuditLogHelper.SifreGuncellendi(OturumBilgisi.KullaniciId, OturumBilgisi.KullaniciAdi);

                lblMesaj.Text = "Þifre baþarýyla deðiþtirildi!";
                lblMesaj.ForeColor = System.Drawing.Color.Green;

                btnKapat.Enabled = true;
                btnDeðiþtir.Enabled = false;

                System.Threading.Thread.Sleep(1500);
                this.Close();
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Þifre deðiþtirirken hata oluþtu: " + ex.Message;
                lblMesaj.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            char passwordChar = chkSifreGoster.Checked ? '\0' : '?';
            txtEskiSifre.PasswordChar = passwordChar;
            txtYeniSifre.PasswordChar = passwordChar;
            txtYeniSifreDoðrula.PasswordChar = passwordChar;
        }
    }
}
