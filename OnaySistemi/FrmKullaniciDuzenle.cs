using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmKullaniciDuzenle : Form
    {
        private readonly string _baglantiMetni;
        private int _kullaniciId;
        private bool _yeniKullanici;

        public FrmKullaniciDuzenle()
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
            _kullaniciId = 0;
            _yeniKullanici = true;
        }

        public FrmKullaniciDuzenle(int kullaniciId)
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
            _kullaniciId = kullaniciId;
            _yeniKullanici = false;
        }

        private void FrmKullaniciDuzenle_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Kalite");
            cmbRol.Items.Add("Onayci");
            cmbRol.Items.Add("Goruntuleme");
            cmbRol.Items.Add("Yonetici");
            cmbRol.SelectedIndex = 0;

            cmbDirektorluk.Items.Clear();
            cmbDirektorluk.Items.Add("-- Seçiniz --");
            cmbDirektorluk.Items.Add("Direktörlük 1 (Zeynep Tuncay)");
            cmbDirektorluk.Items.Add("Tesis Yönetimi Direktörlüðü");
            cmbDirektorluk.Items.Add("Ýnsan Kaynaklarý ve Kalite Direktörlüðü");
            cmbDirektorluk.Items.Add("Diðer");
            cmbDirektorluk.SelectedIndex = 0;

            if (_yeniKullanici)
            {
                this.Text = "Yeni Kullanýcý";
                lblBaslik.Text = "Yeni Kullanýcý Ekle";
                txtSifre.ReadOnly = false;
                txtSifreDogrula.ReadOnly = false;
            }
            else
            {
                this.Text = "Kullanýcý Düzenle";
                lblBaslik.Text = "Kullanýcý Düzenle";
                KullaniciYukle();
                txtSifre.ReadOnly = true;
                txtSifreDogrula.ReadOnly = true;
                chkSifreDegistir.Visible = true;
                lblSifreDegistir.Visible = true;
            }
        }

        private void KullaniciYukle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT KullaniciAdi, AdSoyad, Rol, Direktorluk, Email, MailBildirimAktif, AktifMi 
                    FROM Kullanicilar 
                    WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", _kullaniciId);
                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtKullaniciAdi.Text = dr["KullaniciAdi"].ToString();
                            txtAdSoyad.Text = dr["AdSoyad"].ToString();
                            cmbRol.SelectedItem = dr["Rol"].ToString();
                            txtEmail.Text = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : "";
                            chkMailBildirim.Checked = dr["MailBildirimAktif"] != DBNull.Value && (bool)dr["MailBildirimAktif"];
                            chkAktif.Checked = (bool)dr["AktifMi"];

                            string direktorluk = dr["Direktorluk"] != DBNull.Value ? dr["Direktorluk"].ToString() : "";
                            if (!string.IsNullOrWhiteSpace(direktorluk))
                            {
                                int idx = cmbDirektorluk.Items.IndexOf(direktorluk);
                                cmbDirektorluk.SelectedIndex = idx >= 0 ? idx : 0;
                            }

                            txtKullaniciAdi.ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanýcý bilgileri yüklenirken hata: " + ex.Message);
            }
        }

        private void chkSifreDegistir_CheckedChanged(object sender, EventArgs e)
        {
            txtSifre.ReadOnly = !chkSifreDegistir.Checked;
            txtSifreDogrula.ReadOnly = !chkSifreDegistir.Checked;

            if (!chkSifreDegistir.Checked)
            {
                txtSifre.Clear();
                txtSifreDogrula.Clear();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Kullanýcý adý boþ býrakýlamaz.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad boþ býrakýlamaz.");
                return;
            }

            if (_yeniKullanici)
            {
                if (string.IsNullOrWhiteSpace(txtSifre.Text))
                {
                    MessageBox.Show("Þifre boþ býrakýlamaz.");
                    return;
                }

                if (txtSifre.Text != txtSifreDogrula.Text)
                {
                    MessageBox.Show("Þifreler eþleþmiyor.");
                    return;
                }
            }
            else if (chkSifreDegistir.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtSifre.Text))
                {
                    MessageBox.Show("Yeni þifre boþ býrakýlamaz.");
                    return;
                }

                if (txtSifre.Text != txtSifreDogrula.Text)
                {
                    MessageBox.Show("Þifreler eþleþmiyor.");
                    return;
                }
            }

            try
            {
                if (_yeniKullanici)
                {
                    YeniKullaniciEkle();
                }
                else
                {
                    KullaniciGuncelle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ýþlem sýrasýnda hata: " + ex.Message);
            }
        }

        private void YeniKullaniciEkle()
        {
            string hashliSifre = SifreServisi.SifreHashle(txtSifre.Text);

            using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
            using (SqlCommand komut = new SqlCommand(@"
                INSERT INTO Kullanicilar (KullaniciAdi, AdSoyad, Sifre, Rol, Direktorluk, Email, MailBildirimAktif, AktifMi, OlusturulmaTarihi)
                VALUES (@KullaniciAdi, @AdSoyad, @Sifre, @Rol, @Direktorluk, @Email, @MailBildirimAktif, @AktifMi, SYSDATETIME())", baglanti))
            {
                komut.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text.Trim());
                komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text.Trim());
                komut.Parameters.AddWithValue("@Sifre", hashliSifre);
                komut.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@Direktorluk", cmbDirektorluk.SelectedIndex > 0 ? (object)cmbDirektorluk.SelectedItem.ToString() : DBNull.Value);
                komut.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                komut.Parameters.AddWithValue("@MailBildirimAktif", chkMailBildirim.Checked);
                komut.Parameters.AddWithValue("@AktifMi", chkAktif.Checked);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }

            AuditLogHelper.KullaniciEklendi(0, txtKullaniciAdi.Text.Trim(), txtAdSoyad.Text.Trim(), cmbRol.SelectedItem.ToString());

            MessageBox.Show("Kullanýcý baþarýyla eklendi.", "Baþarýlý");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void KullaniciGuncelle()
        {
            using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
            using (SqlCommand komut = new SqlCommand("", baglanti))
            {
                if (chkSifreDegistir.Checked)
                {
                    string hashliSifre = SifreServisi.SifreHashle(txtSifre.Text);
                    komut.CommandText = @"
                        UPDATE Kullanicilar 
                        SET AdSoyad = @AdSoyad, Sifre = @Sifre, Rol = @Rol, Direktorluk = @Direktorluk, 
                            Email = @Email, MailBildirimAktif = @MailBildirimAktif, AktifMi = @AktifMi
                        WHERE Id = @Id";
                    komut.Parameters.AddWithValue("@Sifre", hashliSifre);
                }
                else
                {
                    komut.CommandText = @"
                        UPDATE Kullanicilar 
                        SET AdSoyad = @AdSoyad, Rol = @Rol, Direktorluk = @Direktorluk, 
                            Email = @Email, MailBildirimAktif = @MailBildirimAktif, AktifMi = @AktifMi
                        WHERE Id = @Id";
                }

                komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text.Trim());
                komut.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@Direktorluk", cmbDirektorluk.SelectedIndex > 0 ? (object)cmbDirektorluk.SelectedItem.ToString() : DBNull.Value);
                komut.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                komut.Parameters.AddWithValue("@MailBildirimAktif", chkMailBildirim.Checked);
                komut.Parameters.AddWithValue("@AktifMi", chkAktif.Checked);
                komut.Parameters.AddWithValue("@Id", _kullaniciId);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }

            AuditLogHelper.KullaniciGuncellendi(_kullaniciId, txtAdSoyad.Text.Trim(), cmbRol.SelectedItem.ToString());

            MessageBox.Show("Kullanýcý baþarýyla güncellendi.", "Baþarýlý");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
