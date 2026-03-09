using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmBelgeGonderim : Form
    {
        private string secilenDosyaYolu;

        public FrmBelgeGonderim()
        {
            InitializeComponent();
        }

        private void FrmBelgeGonderim_Load(object sender, EventArgs e)
        {
            OnaycilariYukle();
            DokumanTurleriniYukle();
            DirektorlukleriYukle();
        }

        private void DokumanTurleriniYukle()
        {
            if (lstDokumanTuru.Items.Count == 0)
            {
                lstDokumanTuru.Items.Add("Kalite El Kitabý");
                lstDokumanTuru.Items.Add("Çevre El Kitabý");
                lstDokumanTuru.Items.Add("ÝSG El Kitabý");
                lstDokumanTuru.Items.Add("Prosedürler");
                lstDokumanTuru.Items.Add("Genel Talimatlar");
                lstDokumanTuru.Items.Add("Hizmet Talimatlar");
                lstDokumanTuru.Items.Add("Özel Talimatlar");
                lstDokumanTuru.Items.Add("Proses Kartlarý");
                lstDokumanTuru.Items.Add("Yönergeler");
            }
        }

        private void DirektorlukleriYukle()
        {
            if (lstDirektorluk.Items.Count == 0)
            {
                lstDirektorluk.Items.Add("Tesis Yönetimi Direktörlüðü");
                lstDirektorluk.Items.Add("Ýnsan Kaynaklarý ve Kalite Direktörlüðü");
                lstDirektorluk.Items.Add("Diðer");
            }
        }

        private DataTable _tumOnaycilari;

        private void OnaycilariYukle()
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                string sql = @"
            SELECT Id, AdSoyad, KullaniciAdi, Direktorluk
            FROM Kullanicilar
            WHERE Rol = 'Onayci' AND AktifMi = 1
            ORDER BY AdSoyad;";

                _tumOnaycilari = new DataTable();

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, baglanti))
                {
                    da.Fill(_tumOnaycilari);
                }

                OnaycilariComboboxaDoldur(_tumOnaycilari);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Onaycý listesi yüklenirken hata oluþtu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnaycilariComboboxaDoldur(DataTable dt)
        {
            DataTable dt1 = dt.Copy();
            DataRow dr1 = dt1.NewRow();
            dr1["Id"] = DBNull.Value;
            dr1["AdSoyad"] = "-- Seçiniz --";
            dt1.Rows.InsertAt(dr1, 0);

            cmbOnayci1.DataSource = dt1;
            cmbOnayci1.DisplayMember = "AdSoyad";
            cmbOnayci1.ValueMember = "Id";

            DataTable dt2 = dt.Copy();
            DataRow dr2 = dt2.NewRow();
            dr2["Id"] = DBNull.Value;
            dr2["AdSoyad"] = "-- Seçiniz --";
            dt2.Rows.InsertAt(dr2, 0);

            cmbOnayci2.DataSource = dt2;
            cmbOnayci2.DisplayMember = "AdSoyad";
            cmbOnayci2.ValueMember = "Id";
        }

        private void btnDosyaSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Gönderilecek dosyayý seçiniz";
                dlg.Filter = "Tüm Dosyalar (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    secilenDosyaYolu = dlg.FileName;
                    txtDosyaYolu.Text = secilenDosyaYolu;
                }
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (lstDokumanTuru.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir doküman türü seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstDirektorluk.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir direktörlük seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKonu.Text))
            {
                MessageBox.Show("Lütfen konu alanýný doldurunuz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(secilenDosyaYolu))
            {
                MessageBox.Show("Lütfen bir dosya seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbOnayci1.SelectedValue == null || cmbOnayci1.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Lütfen 1. onaycýyý seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? onayci1Id = null;
            int? onayci2Id = null;

            if (cmbOnayci1.SelectedValue != null && cmbOnayci1.SelectedValue != DBNull.Value)
                onayci1Id = Convert.ToInt32(cmbOnayci1.SelectedValue);

            if (cmbOnayci2.SelectedValue != null && cmbOnayci2.SelectedValue != DBNull.Value)
                onayci2Id = Convert.ToInt32(cmbOnayci2.SelectedValue);

            string dokumanTuru = lstDokumanTuru.SelectedItem.ToString();
            string direktorluk = lstDirektorluk.SelectedItem.ToString();
            string konu = txtKonu.Text.Trim();
            string aciklama = txtAciklama.Text.Trim();

            string gonderenKullaniciAdi = OturumBilgisi.KullaniciAdi;
            string gonderenAdSoyad = OturumBilgisi.AdSoyad;

            string belgeKlasoru = ConfigurationManager.AppSettings["BelgeKlasoru"];

            if (string.IsNullOrWhiteSpace(belgeKlasoru))
            {
                MessageBox.Show("App.config içinde 'BelgeKlasoru' ayarý bulunamadý.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!Directory.Exists(belgeKlasoru))
                {
                    Directory.CreateDirectory(belgeKlasoru);
                }

                string dosyaYolu = DosyaHazirlaVeKopyala(secilenDosyaYolu, belgeKlasoru);

                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                string sql = @"
                    INSERT INTO BelgeGonderim
                        (DokumanTuru, Direktorluk, Konu, Aciklama, DosyaYolu, 
                         OnayDurumu, GonderenId, GonderenKullaniciAdi, GonderenAdSoyad,
                         Onayci1Id, Onayci2Id, VersiyonNo, KayitTarihi)
                    VALUES
                        (@DokumanTuru, @Direktorluk, @Konu, @Aciklama, @DosyaYolu, 
                         @OnayDurumu, @GonderenId, @GonderenKullaniciAdi, @GonderenAdSoyad,
                         @Onayci1Id, @Onayci2Id, 1, SYSDATETIME());
                    SELECT SCOPE_IDENTITY();";

                int yeniBelgeId = 0;

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue("@DokumanTuru", dokumanTuru);
                    komut.Parameters.AddWithValue("@Direktorluk", direktorluk);
                    komut.Parameters.AddWithValue("@Konu", konu);
                    komut.Parameters.AddWithValue("@Aciklama", (object)aciklama ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@DosyaYolu", dosyaYolu);
                    komut.Parameters.AddWithValue("@OnayDurumu", "Beklemede");
                    komut.Parameters.AddWithValue("@GonderenId", OturumBilgisi.KullaniciId);
                    komut.Parameters.AddWithValue("@GonderenKullaniciAdi", (object)gonderenKullaniciAdi ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@GonderenAdSoyad", (object)gonderenAdSoyad ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@Onayci1Id", onayci1Id.HasValue ? (object)onayci1Id.Value : DBNull.Value);
                    komut.Parameters.AddWithValue("@Onayci2Id", onayci2Id.HasValue ? (object)onayci2Id.Value : DBNull.Value);

                    baglanti.Open();
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                    {
                        yeniBelgeId = Convert.ToInt32(sonuc);
                    }
                }

                if (yeniBelgeId > 0)
                {
                    try
                    {
                        string adSoyad = OturumBilgisi.AdSoyad ?? OturumBilgisi.KullaniciAdi ?? "Bilinmeyen";
                        string sqlLog = @"
                            INSERT INTO BelgeLog (BelgeId, IslemTipi, IslemYapanId, IslemYapanAdi, Aciklama, IslemTarihi)
                            VALUES (@BelgeId, @IslemTipi, @IslemYapanId, @IslemYapanAdi, @Aciklama, SYSDATETIME());";

                        using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                        using (SqlCommand komut = new SqlCommand(sqlLog, baglanti))
                        {
                            komut.Parameters.AddWithValue("@BelgeId", yeniBelgeId);
                            komut.Parameters.AddWithValue("@IslemTipi", "Gönderildi");
                            komut.Parameters.AddWithValue("@IslemYapanId", OturumBilgisi.KullaniciId);
                            komut.Parameters.AddWithValue("@IslemYapanAdi", adSoyad);
                            komut.Parameters.AddWithValue("@Aciklama", $"{dokumanTuru} / {direktorluk}");

                            baglanti.Open();
                            komut.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exLog)
                    {
                        System.Diagnostics.Debug.WriteLine("BelgeLog yazma hatasi: " + exLog.Message);
                    }

                    try
                    {
                        AuditLogHelper.BelgeGonderildi(yeniBelgeId, konu, direktorluk);
                    }
                    catch (Exception exMail)
                    {
                        System.Diagnostics.Debug.WriteLine("AuditLog/Mail hatasi: " + exMail.Message);
                    }
                }

                MessageBox.Show("Dosya kopyalandý ve kayýt veritabanýna eklendi.",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ýþlem sýrasýnda hata oluþtu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstDirektorluk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDirektorluk.SelectedItem == null || _tumOnaycilari == null)
                return;

            string secim = lstDirektorluk.SelectedItem.ToString();

            DataTable filtrelenmis = _tumOnaycilari.Clone();
            foreach (DataRow row in _tumOnaycilari.Rows)
            {
                string direktorluk = row["Direktorluk"]?.ToString() ?? "";
                if (string.Equals(direktorluk, secim, StringComparison.CurrentCultureIgnoreCase))
                {
                    filtrelenmis.ImportRow(row);
                }
            }

            if (filtrelenmis.Rows.Count > 0)
            {
                OnaycilariComboboxaDoldur(filtrelenmis);
                if (cmbOnayci1.Items.Count > 1)
                    cmbOnayci1.SelectedIndex = 1;
            }
            else
            {
                OnaycilariComboboxaDoldur(_tumOnaycilari);

                int p1 = secim.IndexOf('(');
                int p2 = secim.IndexOf(')');
                if (p1 >= 0 && p2 > p1)
                {
                    string adSoyad = secim.Substring(p1 + 1, p2 - p1 - 1).Trim();
                    OtomatikOnayci1Sec(adSoyad);
                }
            }
        }
        private void OtomatikOnayci1Sec(string adSoyad)
        {
            if (string.IsNullOrWhiteSpace(adSoyad))
                return;

            for (int i = 0; i < cmbOnayci1.Items.Count; i++)
            {
                var drv = cmbOnayci1.Items[i] as System.Data.DataRowView;
                if (drv == null)
                    continue;

                string ad = drv["AdSoyad"]?.ToString();

                if (string.Equals(ad, adSoyad, StringComparison.CurrentCultureIgnoreCase))
                {
                    cmbOnayci1.SelectedIndex = i;
                    return;
                }
            }

            if (cmbOnayci1.Items.Count > 0)
                cmbOnayci1.SelectedIndex = 0;
        }

        private string DosyaHazirlaVeKopyala(string kaynakDosya, string hedefKlasor)
        {
            string ext = Path.GetExtension(kaynakDosya).ToLowerInvariant();
            string zamanDamgasi = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string dosyaAdi = Path.GetFileNameWithoutExtension(kaynakDosya);

            if (ext == ".pdf")
            {
                string hedefPdf = Path.Combine(hedefKlasor, $"{zamanDamgasi}_{dosyaAdi}.pdf");
                File.Copy(kaynakDosya, hedefPdf, true);
                return hedefPdf;
            }

            if (ext == ".doc" || ext == ".docx")
            {
                string hedefPdf = Path.Combine(hedefKlasor, $"{zamanDamgasi}_{dosyaAdi}.pdf");
                WordDosyasiniPdfyeCevir(kaynakDosya, hedefPdf);
                return hedefPdf;
            }

            throw new InvalidOperationException("Sadece PDF veya Word (.doc/.docx) dosyasi yukleyebilirsiniz.");
        }

        private void WordDosyasiniPdfyeCevir(string kaynakDocYolu, string hedefPdfYolu)
        {
            Type wordType = Type.GetTypeFromProgID("Word.Application");
            if (wordType == null)
                throw new InvalidOperationException("Microsoft Word bulunamadi. Word yuklu olmali.");

            dynamic wordApp = Activator.CreateInstance(wordType);
            try
            {
                wordApp.Visible = false;
                wordApp.DisplayAlerts = 0;

                dynamic doc = wordApp.Documents.Open(kaynakDocYolu, ReadOnly: true, Visible: false);
                try
                {
                    doc.ExportAsFixedFormat(
                        OutputFileName: hedefPdfYolu,
                        ExportFormat: 17,
                        OpenAfterExport: false,
                        OptimizeFor: 0,
                        Range: 0,
                        From: Type.Missing,
                        To: Type.Missing,
                        Item: 0,
                        IncludeDocProps: true,
                        KeepIRM: true,
                        CreateBookmarks: 1,
                        DocStructureTags: true,
                        BitmapMissingFonts: true,
                        UseISO19005_1: true);
                }
                finally
                {
                    doc.Close(false);
                    Marshal.FinalReleaseComObject(doc);
                }
            }
            finally
            {
                wordApp.Quit(false);
                Marshal.FinalReleaseComObject(wordApp);
            }
        }

    }
}
