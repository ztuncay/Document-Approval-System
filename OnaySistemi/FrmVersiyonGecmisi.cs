using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace OnaySistemi
{
    public partial class FrmVersiyonGecmisi : Form
    {
        private readonly int _baslangicId;

        public FrmVersiyonGecmisi(int baslangicId)
        {
            InitializeComponent();
            _baslangicId = baslangicId;
        }

        private void FrmVersiyonGecmisi_Load(object sender, EventArgs e)
        {
            VersiyonlariYukle();
        }

        private void VersiyonlariYukle()
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                List<int> idList = new List<int>();
                int? currentId = _baslangicId;

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                {
                    baglanti.Open();

                    while (currentId != null)
                    {
                        idList.Add(currentId.Value);

                        using (SqlCommand cmd = new SqlCommand(
                            "SELECT OncekiId FROM BelgeGonderim WHERE Id = @Id", baglanti))
                        {
                            cmd.Parameters.AddWithValue("@Id", currentId.Value);
                            object onceki = cmd.ExecuteScalar();

                            if (onceki == null || onceki == DBNull.Value)
                                currentId = null;
                            else
                                currentId = Convert.ToInt32(onceki);
                        }
                    }

                    if (idList.Count == 0)
                    {
                        MessageBox.Show("Bu kayıt için versiyon bilgisi bulunamadı.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string idListeMetni = string.Join(",", idList);

                    string sql = $@"
                        SELECT Id, DokumanTuru, Direktorluk, Konu, Aciklama,
                               DosyaYolu, KayitTarihi, OnayDurumu,
                               RevizeAciklama, VersiyonNo
                        FROM BelgeGonderim
                        WHERE Id IN ({idListeMetni})
                        ORDER BY VersiyonNo ASC;";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, baglanti))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvVersiyonlar.DataSource = dt;
                    }
                }

                if (dgvVersiyonlar.Columns["Id"] != null)
                    dgvVersiyonlar.Columns["Id"].HeaderText = "Id";
                if (dgvVersiyonlar.Columns["DokumanTuru"] != null)
                    dgvVersiyonlar.Columns["DokumanTuru"].HeaderText = "Doküman Türü";
                if (dgvVersiyonlar.Columns["Direktorluk"] != null)
                    dgvVersiyonlar.Columns["Direktorluk"].HeaderText = "Direktörlük";
                if (dgvVersiyonlar.Columns["Konu"] != null)
                    dgvVersiyonlar.Columns["Konu"].HeaderText = "Konu";
                if (dgvVersiyonlar.Columns["Aciklama"] != null)
                    dgvVersiyonlar.Columns["Aciklama"].HeaderText = "Açıklama";
                if (dgvVersiyonlar.Columns["DosyaYolu"] != null)
                    dgvVersiyonlar.Columns["DosyaYolu"].HeaderText = "Dosya Yolu";
                if (dgvVersiyonlar.Columns["KayitTarihi"] != null)
                    dgvVersiyonlar.Columns["KayitTarihi"].HeaderText = "Kayıt Tarihi";
                if (dgvVersiyonlar.Columns["OnayDurumu"] != null)
                    dgvVersiyonlar.Columns["OnayDurumu"].HeaderText = "Onay Durumu";
                if (dgvVersiyonlar.Columns["RevizeAciklama"] != null)
                    dgvVersiyonlar.Columns["RevizeAciklama"].HeaderText = "Revize Açıklaması";
                if (dgvVersiyonlar.Columns["VersiyonNo"] != null)
                    dgvVersiyonlar.Columns["VersiyonNo"].HeaderText = "Versiyon No";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Versiyonlar yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVersiyonBelgeAc_Click(object sender, EventArgs e)
        {
            if (dgvVersiyonlar.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden bir versiyon seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dosyaYolu = dgvVersiyonlar.CurrentRow.Cells["DosyaYolu"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(dosyaYolu) || !File.Exists(dosyaYolu))
            {
                MessageBox.Show("Dosya bulunamadı:\n" + dosyaYolu, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = dosyaYolu,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya açılırken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
