using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmGiris : Form
    {
        private readonly string _baglantiMetni;

        public FrmGiris()
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            lblHata.Text = "";
        }

        private void chkSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = chkSifreGoster.Checked ? '\0' : '*';
        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {
            lblHata.Text = "";
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            lblHata.Text = "";

            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text;

            if (string.IsNullOrWhiteSpace(kullaniciAdi))
            {
                lblHata.Text = "Kullanıcı adı boş bırakılamaz.";
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT TOP 1 Id, KullaniciAdi, AdSoyad, Rol, Sifre
                    FROM Kullanicilar  
                    WHERE KullaniciAdi = @KullaniciAdi;", baglanti))
                {
                    komut.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            lblHata.Text = "Kullanıcı bulunamadı.";
                            return;
                        }

                        OturumBilgisi.KullaniciId = Convert.ToInt32(dr["Id"]);
                        OturumBilgisi.KullaniciAdi = Convert.ToString(dr["KullaniciAdi"]);
                        OturumBilgisi.AdSoyad = Convert.ToString(dr["AdSoyad"]);
                        OturumBilgisi.Rol = Convert.ToString(dr["Rol"]);

                        AuditLogHelper.KullaniciGiris(
                            OturumBilgisi.KullaniciId,
                            OturumBilgisi.KullaniciAdi,
                            OturumBilgisi.AdSoyad,
                            OturumBilgisi.Rol);

                        this.Hide();
                        using (var frm = new FrmAnaMenu())
                        {
                            frm.ShowDialog();
                        }
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                lblHata.Text = "Giriş sırasında hata oluştu: " + ex.Message;
            }
        }
    }
}