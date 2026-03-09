using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmAuditLog : Form
    {
        private readonly string _baglantiMetni;

        public FrmAuditLog()
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
        }

        private void FrmAuditLog_Load(object sender, EventArgs e)
        {
            lblBaslik.Text = $"Ýþlem Geçmiþi - Yapýlan Tüm Deðiþiklikler";
            
            cmbIslemTipi.Items.Clear();
            cmbIslemTipi.Items.Add("-- Tümü --");
            cmbIslemTipi.Items.Add("Kullanici_Ekle");
            cmbIslemTipi.Items.Add("Kullanici_Duzenle");
            cmbIslemTipi.Items.Add("Kullanici_Sil");
            cmbIslemTipi.Items.Add("Belge_Durum_Degistir");
            cmbIslemTipi.Items.Add("Belge_Sil");
            cmbIslemTipi.SelectedIndex = 0;

            cmbTablo.Items.Clear();
            cmbTablo.Items.Add("-- Tümü --");
            cmbTablo.Items.Add("Kullanicilar");
            cmbTablo.Items.Add("BelgeGonderim");
            cmbTablo.SelectedIndex = 0;

            dtpBaslangic.Value = DateTime.Today.AddDays(-30);
            dtpBitis.Value = DateTime.Today.AddDays(1);

            LoglariYukle();
        }

        private void LoglariYukle()
        {
            try
            {
                string islemTipi = cmbIslemTipi.SelectedItem?.ToString() ?? "-- Tümü --";
                string tablo = cmbTablo.SelectedItem?.ToString() ?? "-- Tümü --";
                DateTime baslangic = dtpBaslangic.Value;
                DateTime bitis = dtpBitis.Value;

                string sorgu = @"
                    SELECT 
                        Id,
                        IslemTarihi,
                        KullaniciAdi,
                        IslemTipi,
                        Tablo,
                        KaydId,
                        Aciklama
                    FROM AuditLog
                    WHERE IslemTarihi >= @Baslangic AND IslemTarihi <= @Bitis";

                if (islemTipi != "-- Tümü --")
                    sorgu += " AND IslemTipi = @IslemTipi";

                if (tablo != "-- Tümü --")
                    sorgu += " AND Tablo = @Tablo";

                if (!string.IsNullOrWhiteSpace(txtAra.Text))
                    sorgu += " AND (KullaniciAdi LIKE @Ara OR Aciklama LIKE @Ara)";

                sorgu += " ORDER BY IslemTarihi DESC";

                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@Baslangic", baslangic);
                    komut.Parameters.AddWithValue("@Bitis", bitis);

                    if (islemTipi != "-- Tümü --")
                        komut.Parameters.AddWithValue("@IslemTipi", islemTipi);

                    if (tablo != "-- Tümü --")
                        komut.Parameters.AddWithValue("@Tablo", tablo);

                    if (!string.IsNullOrWhiteSpace(txtAra.Text))
                        komut.Parameters.AddWithValue("@Ara", "%" + txtAra.Text + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvLoglar.DataSource = dt;

                    dgvLoglar.Columns["Id"].HeaderText = "ID";
                    dgvLoglar.Columns["IslemTarihi"].HeaderText = "Ýþlem Tarihi";
                    dgvLoglar.Columns["KullaniciAdi"].HeaderText = "Kullanýcý";
                    dgvLoglar.Columns["IslemTipi"].HeaderText = "Ýþlem Tipi";
                    dgvLoglar.Columns["Tablo"].HeaderText = "Tablo";
                    dgvLoglar.Columns["KaydId"].HeaderText = "Kayýt ID";
                    dgvLoglar.Columns["Aciklama"].HeaderText = "Açýklama";

                    lblToplamIslem.Text = $"Toplam Ýþlem: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loglar yüklenirken hata: " + ex.Message, "Hata");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            LoglariYukle();
        }

        private void cmbIslemTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoglariYukle();
        }

        private void cmbTablo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoglariYukle();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            LoglariYukle();
        }

        private void dtpBaslangic_ValueChanged(object sender, EventArgs e)
        {
            LoglariYukle();
        }

        private void dtpBitis_ValueChanged(object sender, EventArgs e)
        {
            LoglariYukle();
        }

        private void btnDetay_Click(object sender, EventArgs e)
        {
            if (dgvLoglar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir iþlem seçin.");
                return;
            }

            DataGridViewRow seciliSatir = dgvLoglar.SelectedRows[0];
            int logId = (int)seciliSatir.Cells["Id"].Value;

            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(
                    "SELECT * FROM AuditLog WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", logId);
                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string detay = $@"
ÝÞLEM GEÇMÝÞÝ DETAYI
{'='*50}

Ýþlem ID: {dr["Id"]}
Ýþlem Tarihi: {dr["IslemTarihi"]}
Kullanýcý: {dr["KullaniciAdi"]}
Ýþlem Tipi: {dr["IslemTipi"]}
Tablo: {dr["Tablo"]}
Kayýt ID: {dr["KaydId"]}
Açýklama: {dr["Aciklama"]}

{'='*50}";

                            MessageBox.Show(detay, "Ýþlem Detayý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Detay yüklenirken hata: " + ex.Message);
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
