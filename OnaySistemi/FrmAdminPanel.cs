using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmAdminPanel : Form
    {
        private readonly string _baglantiMetni;

        public FrmAdminPanel()
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
        }

        private void FrmAdminPanel_Load(object sender, EventArgs e)
        {
            lblAdminBaslik.Text = $"Admin Paneli - Hoþgeldiniz {OturumBilgisi.AdSoyad}";
            
            tabControl.SelectedIndex = 0;
            KullaniciListesiYukle();
        }

        private void KullaniciListesiYukle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT Id, KullaniciAdi, AdSoyad, Rol, AktifMi, OlusturulmaTarihi
                    FROM Kullanicilar
                    ORDER BY OlusturulmaTarihi DESC", baglanti))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKullanicilar.DataSource = dt;
                    
                    dgvKullanicilar.Columns["Id"].HeaderText = "ID";
                    dgvKullanicilar.Columns["KullaniciAdi"].HeaderText = "Kullanýcý Adý";
                    dgvKullanicilar.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                    dgvKullanicilar.Columns["Rol"].HeaderText = "Rol";
                    dgvKullanicilar.Columns["AktifMi"].HeaderText = "Aktif";
                    dgvKullanicilar.Columns["OlusturulmaTarihi"].HeaderText = "Oluþturulma Tarihi";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanýcý listesi yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKullaniciEkle_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmKullaniciDuzenle())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    KullaniciListesiYukle();
                }
            }
        }

        private void btnKullaniciDuzenle_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek istediðiniz kullanýcýyý seçin.", "Uyarý");
                return;
            }

            int kullaniciId = (int)dgvKullanicilar.SelectedRows[0].Cells["Id"].Value;
            
            using (var frm = new FrmKullaniciDuzenle(kullaniciId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    KullaniciListesiYukle();
                }
            }
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediðiniz kullanýcýyý seçin.", "Uyarý");
                return;
            }

            int kullaniciId = (int)dgvKullanicilar.SelectedRows[0].Cells["Id"].Value;
            string kullaniciAdi = dgvKullanicilar.SelectedRows[0].Cells["KullaniciAdi"].Value.ToString();
            string adSoyad = dgvKullanicilar.SelectedRows[0].Cells["AdSoyad"].Value.ToString();

            if (MessageBox.Show($"{adSoyad} adlý kullanýcýyý silmek istediðinize emin misiniz?", 
                "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand("DELETE FROM Kullanicilar WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", kullaniciId);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }

                AuditLogHelper.KullaniciSilindi(kullaniciId, adSoyad);

                MessageBox.Show("Kullanýcý baþarýyla silindi.", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KullaniciListesiYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanýcý silinirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                BelgeListesiYukle();
            }
        }

        private void BelgeListesiYukle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT 
                        Id, DokumanTuru, Konu, OnayDurumu, GonderenAdSoyad, 
                        Onaylayan, KayitTarihi, VersiyonNo
                    FROM BelgeGonderim
                    ORDER BY KayitTarihi DESC", baglanti))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBelgeler.DataSource = dt;

                    dgvBelgeler.Columns["Id"].HeaderText = "ID";
                    dgvBelgeler.Columns["DokumanTuru"].HeaderText = "Döküman Türü";
                    dgvBelgeler.Columns["Konu"].HeaderText = "Konu";
                    dgvBelgeler.Columns["OnayDurumu"].HeaderText = "Onay Durumu";
                    dgvBelgeler.Columns["GonderenAdSoyad"].HeaderText = "Gönderen";
                    dgvBelgeler.Columns["Onaylayan"].HeaderText = "Onaylayan";
                    dgvBelgeler.Columns["KayitTarihi"].HeaderText = "Kayýt Tarihi";
                    dgvBelgeler.Columns["VersiyonNo"].HeaderText = "Versiyon";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Belge listesi yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBelgeDurumuDegistir_Click(object sender, EventArgs e)
        {
            if (dgvBelgeler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen durumunu deðiþtirmek istediðiniz belgeyi seçin.", "Uyarý");
                return;
            }

            int belgeId = (int)dgvBelgeler.SelectedRows[0].Cells["Id"].Value;
            string suankiDurum = dgvBelgeler.SelectedRows[0].Cells["OnayDurumu"].Value?.ToString() ?? "";

            using (var frm = new FrmBelgeDurumuDegistir(belgeId, suankiDurum))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BelgeListesiYukle();
                }
            }
        }

        private void btnBelgeSil_Click(object sender, EventArgs e)
        {
            if (dgvBelgeler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediðiniz belgeyi seçin.", "Uyarý");
                return;
            }

            int belgeId = (int)dgvBelgeler.SelectedRows[0].Cells["Id"].Value;
            string konu = dgvBelgeler.SelectedRows[0].Cells["Konu"].Value?.ToString() ?? "Belirtilmemiþ";

            if (MessageBox.Show($"'{konu}' baþlýklý belgeyi silmek istediðinize emin misiniz?\n\nBu iþlem geri alýnamaz!", 
                "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand("DELETE FROM BelgeGonderim WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", belgeId);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }

                AuditLogHelper.BelgeSilindi(belgeId, konu);

                MessageBox.Show("Belge baþarýyla silindi.", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BelgeListesiYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Belge silinirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIslemGecmisi_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmAuditLog())
            {
                frm.ShowDialog(this);
            }
        }
    }
}
