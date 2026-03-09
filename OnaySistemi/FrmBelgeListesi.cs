using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmBelgeListesi : Form
    {
        private DataTable _tumKayitlar;

        public FrmBelgeListesi()
        {
            InitializeComponent();
        }

        private void FrmBelgeListesi_Load(object sender, EventArgs e)
        {
            dtpBitis.Value = DateTime.Today;
            dtpBaslangic.Value = DateTime.Today.AddDays(-30);

            cmbDurum.Items.Clear();
            cmbDurum.Items.Add("Hepsi");
            cmbDurum.Items.Add("Beklemede");
            cmbDurum.Items.Add("1. Onaylandı, 2. Beklemede");
            cmbDurum.Items.Add("Onaylandı");
            cmbDurum.Items.Add("Tam Onaylandı");
            cmbDurum.Items.Add("Reddedildi");
            cmbDurum.Items.Add("Revize Talep Edildi");
            cmbDurum.SelectedIndex = 0;

            KayitlariYukle();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            KayitlariYukle();
        }

        private void KayitlariYukle()
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                string sql = @"
                    SELECT 
                        Id,
                        DokumanTuru,
                        Direktorluk,
                        Konu,
                        GonderenAdSoyad,
                        OnayDurumu,
                        KayitTarihi,
                        DosyaYolu,
                        ImzaliDosya1Yolu,
                        ImzaliDosya2Yolu
                    FROM BelgeGonderim
                    ORDER BY KayitTarihi DESC;";

                DataTable dt = new DataTable();

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(sql, baglanti))
                using (SqlDataAdapter da = new SqlDataAdapter(komut))
                {
                    baglanti.Open();
                    da.Fill(dt);
                }

                _tumKayitlar = dt;
                dgvBelgeler.DataSource = _tumKayitlar;

                if (dgvBelgeler.Columns["DokumanTuru"] != null)
                    dgvBelgeler.Columns["DokumanTuru"].HeaderText = "Doküman Türü";

                if (dgvBelgeler.Columns["Direktorluk"] != null)
                    dgvBelgeler.Columns["Direktorluk"].HeaderText = "Direktörlük";

                if (dgvBelgeler.Columns["Konu"] != null)
                    dgvBelgeler.Columns["Konu"].HeaderText = "Konu";

                if (dgvBelgeler.Columns["GonderenAdSoyad"] != null)
                    dgvBelgeler.Columns["GonderenAdSoyad"].HeaderText = "Gönderen";

                if (dgvBelgeler.Columns["OnayDurumu"] != null)
                    dgvBelgeler.Columns["OnayDurumu"].HeaderText = "Durum";

                if (dgvBelgeler.Columns["KayitTarihi"] != null)
                    dgvBelgeler.Columns["KayitTarihi"].HeaderText = "Kayıt Tarihi";

                if (dgvBelgeler.Columns["DosyaYolu"] != null)
                    dgvBelgeler.Columns["DosyaYolu"].HeaderText = "Orijinal Belge";

                if (dgvBelgeler.Columns["ImzaliDosya1Yolu"] != null)
                    dgvBelgeler.Columns["ImzaliDosya1Yolu"].HeaderText = "1. Onaylı Belge";

                if (dgvBelgeler.Columns["ImzaliDosya2Yolu"] != null)
                    dgvBelgeler.Columns["ImzaliDosya2Yolu"].HeaderText = "Tam Onaylı Belge";

                if (dgvBelgeler.Columns["Id"] != null)
                    dgvBelgeler.Columns["Id"].Visible = false;

                dgvBelgeler.ReadOnly = true;
                dgvBelgeler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvBelgeler.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Belge listesi yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? SeciliBelgeIdGetir()
        {
            if (dgvBelgeler.CurrentRow == null)
                return null;

            object deger = dgvBelgeler.CurrentRow.Cells["Id"].Value;
            if (deger == null || deger == DBNull.Value)
                return null;

            return Convert.ToInt32(deger);
        }

        private void SeciliBelgeyiAc()
        {
            if (dgvBelgeler.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden bir satır seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var satir = dgvBelgeler.CurrentRow;

            string tamOnayliYol = satir.Cells["ImzaliDosya2Yolu"].Value as string;
            string birinciOnayliYol = satir.Cells["ImzaliDosya1Yolu"].Value as string;
            string orijinalYol = satir.Cells["DosyaYolu"].Value as string;

            string acilacakYol = null;

            if (!string.IsNullOrWhiteSpace(tamOnayliYol) && File.Exists(tamOnayliYol))
            {
                acilacakYol = tamOnayliYol;
            }
            else if (!string.IsNullOrWhiteSpace(birinciOnayliYol) && File.Exists(birinciOnayliYol))
            {
                acilacakYol = birinciOnayliYol;
            }
            else if (!string.IsNullOrWhiteSpace(orijinalYol) && File.Exists(orijinalYol))
            {
                acilacakYol = orijinalYol;
            }

            if (acilacakYol == null)
            {
                MessageBox.Show("Açılacak dosya bulunamadı.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = acilacakYol,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya açılırken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBelgeler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dgvBelgeler.CurrentCell = dgvBelgeler.Rows[e.RowIndex].Cells[e.ColumnIndex];
            SeciliBelgeyiAc();
        }

        private void btnBelgeAc_Click(object sender, EventArgs e)
        {
            SeciliBelgeyiAc();
        }

        private void btnVersiyonGecmisi_Click(object sender, EventArgs e)
        {
            int? belgeId = SeciliBelgeIdGetir();
            if (!belgeId.HasValue)
            {
                MessageBox.Show("Lütfen listeden bir belge seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var frm = new FrmVersiyonGecmisi(belgeId.Value))
                {
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Versiyon geçmişi ekranı açılırken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltreUygula_Click(object sender, EventArgs e)
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                string seciliDurum = cmbDurum.SelectedItem?.ToString() ?? "Hepsi";

                DateTime bas = dtpBaslangic.Value.Date;
                DateTime bit = dtpBitis.Value.Date.AddDays(1).AddTicks(-1);

                string sql = @"
                    SELECT 
                        Id,
                        DokumanTuru,
                        Direktorluk,
                        Konu,
                        GonderenAdSoyad,
                        OnayDurumu,
                        KayitTarihi,
                        DosyaYolu,
                        ImzaliDosya1Yolu,
                        ImzaliDosya2Yolu
                    FROM BelgeGonderim
                    WHERE KayitTarihi >= @Baslangic AND KayitTarihi <= @Bitis";

                if (!string.Equals(seciliDurum, "Hepsi", StringComparison.OrdinalIgnoreCase))
                {
                    sql += " AND OnayDurumu = @Durum";
                }

                sql += " ORDER BY KayitTarihi DESC;";

                DataTable dt = new DataTable();

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(sql, baglanti))
                using (SqlDataAdapter da = new SqlDataAdapter(komut))
                {
                    komut.Parameters.AddWithValue("@Baslangic", bas);
                    komut.Parameters.AddWithValue("@Bitis", bit);

                    if (!string.Equals(seciliDurum, "Hepsi", StringComparison.OrdinalIgnoreCase))
                        komut.Parameters.AddWithValue("@Durum", seciliDurum);

                    baglanti.Open();
                    da.Fill(dt);
                }

                _tumKayitlar = dt;
                dgvBelgeler.DataSource = _tumKayitlar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtre uygulanırken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltreTemizle_Click(object sender, EventArgs e)
        {
            dtpBitis.Value = DateTime.Today;
            dtpBaslangic.Value = DateTime.Today.AddDays(-30);
            cmbDurum.SelectedIndex = 0;
            KayitlariYukle();
        }
    }
}
