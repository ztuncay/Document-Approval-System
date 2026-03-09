using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OnaySistemi
{

    public partial class FrmRevizeTamamlama : Form
    {
        private string secilenYeniDosyaYolu;

        public FrmRevizeTamamlama()
        {
            InitializeComponent();
        }

        private void FrmRevizeTamamlama_Load(object sender, EventArgs e)
        {
            RevizeKayitlariniYukle();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            RevizeKayitlariniYukle();
        }

        private void RevizeKayitlariniYukle()
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                string sql = @"
                    SELECT Id, DokumanTuru, Direktorluk, Konu, Aciklama,
                           DosyaYolu, KayitTarihi, OnayDurumu,
                           RevizeAciklama, VersiyonNo, OncekiId
                    FROM BelgeGonderim
                    WHERE OnayDurumu = 'Revize Talep Edildi'
                    ORDER BY KayitTarihi ASC;";

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, baglanti))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRevizeIstekleri.DataSource = dt;
                }

                if (dgvRevizeIstekleri.Columns["Id"] != null)
                    dgvRevizeIstekleri.Columns["Id"].HeaderText = "Id";
                if (dgvRevizeIstekleri.Columns["DokumanTuru"] != null)
                    dgvRevizeIstekleri.Columns["DokumanTuru"].HeaderText = "Doküman Türü";
                if (dgvRevizeIstekleri.Columns["Direktorluk"] != null)
                    dgvRevizeIstekleri.Columns["Direktorluk"].HeaderText = "Direktörlük";
                if (dgvRevizeIstekleri.Columns["Konu"] != null)
                    dgvRevizeIstekleri.Columns["Konu"].HeaderText = "Konu";
                if (dgvRevizeIstekleri.Columns["Aciklama"] != null)
                    dgvRevizeIstekleri.Columns["Aciklama"].HeaderText = "Açýklama";
                if (dgvRevizeIstekleri.Columns["DosyaYolu"] != null)
                    dgvRevizeIstekleri.Columns["DosyaYolu"].HeaderText = "Mevcut Dosya";
                if (dgvRevizeIstekleri.Columns["KayitTarihi"] != null)
                    dgvRevizeIstekleri.Columns["KayitTarihi"].HeaderText = "Kayýt Tarihi";
                if (dgvRevizeIstekleri.Columns["OnayDurumu"] != null)
                    dgvRevizeIstekleri.Columns["OnayDurumu"].HeaderText = "Onay Durumu";
                if (dgvRevizeIstekleri.Columns["RevizeAciklama"] != null)
                    dgvRevizeIstekleri.Columns["RevizeAciklama"].HeaderText = "Revize Gerekçesi";
                if (dgvRevizeIstekleri.Columns["VersiyonNo"] != null)
                    dgvRevizeIstekleri.Columns["VersiyonNo"].HeaderText = "Versiyon No";
                if (dgvRevizeIstekleri.Columns["OncekiId"] != null)
                    dgvRevizeIstekleri.Columns["OncekiId"].HeaderText = "Önceki Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayýtlar yüklenirken hata oluþtu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBelgeAc_Click(object sender, EventArgs e)
        {
            if (dgvRevizeIstekleri.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden bir satýr seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dosyaYolu = dgvRevizeIstekleri.CurrentRow.Cells["DosyaYolu"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(dosyaYolu) || !File.Exists(dosyaYolu))
            {
                MessageBox.Show("Dosya bulunamadý:\n" + dosyaYolu, "Hata",
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
                MessageBox.Show("Dosya açýlýrken hata oluþtu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDosyaSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Revize dosyasýný seçiniz";
                dlg.Filter = "Word Dosyalarý (*.docx;*.doc)|*.docx;*.doc|Tüm Dosyalar (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    secilenYeniDosyaYolu = dlg.FileName;
                    txtYeniDosyaYolu.Text = secilenYeniDosyaYolu;
                }
            }
        }

        private void btnRevizeyiTamamla_Click(object sender, EventArgs e)
        {
            if (dgvRevizeIstekleri.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden bir satýr seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(secilenYeniDosyaYolu))
            {
                MessageBox.Show("Lütfen yeni revize dosyasýný seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string uzanti = Path.GetExtension(secilenYeniDosyaYolu).ToLowerInvariant();
            if (uzanti != ".doc" && uzanti != ".docx")
            {
                MessageBox.Show("Lütfen Word formatýnda (*.doc veya *.docx) bir dosya seçiniz.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int eskiId = Convert.ToInt32(dgvRevizeIstekleri.CurrentRow.Cells["Id"].Value);
            string dokumanTuru = dgvRevizeIstekleri.CurrentRow.Cells["DokumanTuru"].Value?.ToString();
            string direktorluk = dgvRevizeIstekleri.CurrentRow.Cells["Direktorluk"].Value?.ToString();
            string konu = dgvRevizeIstekleri.CurrentRow.Cells["Konu"].Value?.ToString();
            string aciklama = dgvRevizeIstekleri.CurrentRow.Cells["Aciklama"].Value?.ToString();
            string revizeGerekcesi = dgvRevizeIstekleri.CurrentRow.Cells["RevizeAciklama"].Value?.ToString();

            int eskiVersiyonNo = 1;
            if (dgvRevizeIstekleri.CurrentRow.Cells["VersiyonNo"].Value != DBNull.Value)
                eskiVersiyonNo = Convert.ToInt32(dgvRevizeIstekleri.CurrentRow.Cells["VersiyonNo"].Value);

            int yeniVersiyonNo = eskiVersiyonNo + 1;

            if (MessageBox.Show(
                    $"Seçili kayýt için yeni versiyon (v{yeniVersiyonNo}) oluþturulacak. Devam edilsin mi?",
                    "Revizeyi Tamamla",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                // Eski belgeden onaycý ve gönderen bilgilerini çek
                int? gonderenId = null;
                string gonderenKullaniciAdi = null;
                string gonderenAdSoyad = null;
                int? onayci1Id = null;
                int? onayci2Id = null;

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT GonderenId, GonderenKullaniciAdi, GonderenAdSoyad, 
                           Onayci1Id, Onayci2Id
                    FROM BelgeGonderim 
                    WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", eskiId);
                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["GonderenId"] != DBNull.Value)
                                gonderenId = Convert.ToInt32(dr["GonderenId"]);
                            gonderenKullaniciAdi = dr["GonderenKullaniciAdi"]?.ToString();
                            gonderenAdSoyad = dr["GonderenAdSoyad"]?.ToString();
                            
                            if (dr["Onayci1Id"] != DBNull.Value)
                                onayci1Id = Convert.ToInt32(dr["Onayci1Id"]);
                            if (dr["Onayci2Id"] != DBNull.Value)
                                onayci2Id = Convert.ToInt32(dr["Onayci2Id"]);
                        }
                    }
                }

                string belgeKlasoru = ConfigurationManager.AppSettings["BelgeKlasoru"];
                if (string.IsNullOrWhiteSpace(belgeKlasoru))
                {
                    MessageBox.Show("App.config içinde 'BelgeKlasoru' ayarý bulunamadý.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Directory.Exists(belgeKlasoru))
                    Directory.CreateDirectory(belgeKlasoru);

                string hedefTamYol = RevizeDosyasiniPdfHazirla(secilenYeniDosyaYolu, belgeKlasoru, yeniVersiyonNo);

                int yeniBelgeId = 0;
                
                string sqlInsert = @"
                    INSERT INTO BelgeGonderim
                        (DokumanTuru, Direktorluk, Konu, Aciklama,
                         DosyaYolu, OnayDurumu, RevizeAciklama,
                         OncekiId, VersiyonNo, 
                         GonderenId, GonderenKullaniciAdi, GonderenAdSoyad,
                         Onayci1Id, Onayci2Id,
                         ImzaliDosya1Yolu, ImzaliDosya2Yolu,
                         KayitTarihi)
                    VALUES
                        (@DokumanTuru, @Direktorluk, @Konu, @Aciklama,
                         @DosyaYolu, @OnayDurumu, @RevizeAciklama,
                         @OncekiId, @VersiyonNo,
                         @GonderenId, @GonderenKullaniciAdi, @GonderenAdSoyad,
                         @Onayci1Id, @Onayci2Id,
                         NULL, NULL,
                         SYSDATETIME());
                    SELECT SCOPE_IDENTITY();";

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(sqlInsert, baglanti))
                {
                    komut.Parameters.AddWithValue("@DokumanTuru", dokumanTuru);
                    komut.Parameters.AddWithValue("@Direktorluk", direktorluk);
                    komut.Parameters.AddWithValue("@Konu", konu);
                    komut.Parameters.AddWithValue("@Aciklama", (object)aciklama ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@DosyaYolu", hedefTamYol);
                    komut.Parameters.AddWithValue("@OnayDurumu", "Beklemede");
                    komut.Parameters.AddWithValue("@RevizeAciklama", (object)revizeGerekcesi ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@OncekiId", eskiId);
                    komut.Parameters.AddWithValue("@VersiyonNo", yeniVersiyonNo);
                    
                    komut.Parameters.AddWithValue("@GonderenId", gonderenId.HasValue ? (object)gonderenId.Value : DBNull.Value);
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

                // Eski kaydý listeden düþürmek için durumu güncelle
                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(
                    "UPDATE BelgeGonderim SET OnayDurumu = @Durum WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Durum", "Revize Tamamlandý");
                    komut.Parameters.AddWithValue("@Id", eskiId);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }

                // Yeni belge için log kaydý oluþtur
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
                            komut.Parameters.AddWithValue("@IslemTipi", "Revize Tamamlama");
                            komut.Parameters.AddWithValue("@IslemYapanId", OturumBilgisi.KullaniciId);
                            komut.Parameters.AddWithValue("@IslemYapanAdi", adSoyad);
                            komut.Parameters.AddWithValue("@Aciklama", $"v{yeniVersiyonNo} oluþturuldu - Önceki imzalar sýfýrlandý");

                            baglanti.Open();
                            komut.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                    }

                    // Eski belge için log kaydý
                    try
                    {
                        string adSoyad = OturumBilgisi.AdSoyad ?? OturumBilgisi.KullaniciAdi ?? "Bilinmeyen";
                        string sqlLog = @"
                            INSERT INTO BelgeLog (BelgeId, IslemTipi, IslemYapanId, IslemYapanAdi, Aciklama, IslemTarihi)
                            VALUES (@BelgeId, @IslemTipi, @IslemYapanId, @IslemYapanAdi, @Aciklama, SYSDATETIME());";

                        using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                        using (SqlCommand komut = new SqlCommand(sqlLog, baglanti))
                        {
                            komut.Parameters.AddWithValue("@BelgeId", eskiId);
                            komut.Parameters.AddWithValue("@IslemTipi", "Revize Tamamlandý");
                            komut.Parameters.AddWithValue("@IslemYapanId", OturumBilgisi.KullaniciId);
                            komut.Parameters.AddWithValue("@IslemYapanAdi", adSoyad);
                            komut.Parameters.AddWithValue("@Aciklama", $"Yeni belge (Id={yeniBelgeId}, v{yeniVersiyonNo}) oluþturuldu");

                            baglanti.Open();
                            komut.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                    }

                    // Audit log ve 1. Onaycýya email bildirimi gönder
                    try
                    {
                        AuditLogHelper.BelgeGonderildi(yeniBelgeId, konu ?? "Revize Belgesi", direktorluk ?? "");
                    }
                    catch (Exception exMail)
                    {
                        System.Diagnostics.Debug.WriteLine("AuditLog/Mail hatasi: " + exMail.Message);
                    }
                }

                MessageBox.Show(
                    $"Yeni versiyon (v{yeniVersiyonNo}) oluþturuldu.\n\n" +
                    "? Belge 'Beklemede' durumuna alýndý\n" +
                    "? Önceki imzalar sýfýrlandý\n" +
                    "? 1. Onaycýya email gönderildi",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                secilenYeniDosyaYolu = null;
                txtYeniDosyaYolu.Text = string.Empty;

                RevizeKayitlariniYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Revize iþlemi sýrasýnda hata oluþtu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnYeniDosyaSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Yeni (revize edilmiþ) dosyayý seçin";
                ofd.Filter = "Word Dosyalarý (*.docx;*.doc)|*.docx;*.doc|Tüm Dosyalar (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    secilenYeniDosyaYolu = ofd.FileName;
                    txtYeniDosyaYolu.Text = secilenYeniDosyaYolu;
                }
            }
        }

        private void btnRevizeEdilecekBelge_Click(object sender, EventArgs e)
        {
            if (dgvRevizeIstekleri.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden bir satýr seçin.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object yolObj = null;
            try
            {
                yolObj = dgvRevizeIstekleri.CurrentRow.Cells["OrijinalDosyaYolu"].Value;
            }
            catch
            {
            }

            string dosyaYolu = yolObj == null ? "" : Convert.ToString(yolObj);

            if (string.IsNullOrWhiteSpace(dosyaYolu))
            {
                MessageBox.Show("Seçili kayýtta dosya yolu bulunamadý.", "Uyarý",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(dosyaYolu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya açýlamadý: " + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RevizeDosyasiniPdfHazirla(string kaynakDosyaYolu, string hedefKlasoru, int yeniVersiyonNo)
        {
            string zamanDamgasi = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string dosyaAdi = Path.GetFileNameWithoutExtension(kaynakDosyaYolu);
            string hedefPdf = Path.Combine(hedefKlasoru, $"{zamanDamgasi}_v{yeniVersiyonNo}_{dosyaAdi}.pdf");

            WordDosyasiniPdfyeCevir(kaynakDosyaYolu, hedefPdf);
            return hedefPdf;
        }

        private void WordDosyasiniPdfyeCevir(string kaynakDocYolu, string hedefPdfYolu)
        {
            Type wordType = Type.GetTypeFromProgID("Word.Application");
            if (wordType == null)
                throw new InvalidOperationException("Microsoft Word bulunamadý. Word yüklü olmalý.");

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
