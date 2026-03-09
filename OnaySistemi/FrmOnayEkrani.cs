using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OnaySistemi
{
    public partial class FrmOnayEkrani : Form
    {
        private int? _secelibelgeId = null;
        private string _seceliBelgeDosyaYolu = null;

        public FrmOnayEkrani()
        {
            InitializeComponent();
        }

        private void FrmOnayEkrani_Load(object sender, EventArgs e)
        {
            KayitlariYukle();
            
            if (dgvOnayBekleyenler != null)
            {
                dgvOnayBekleyenler.CellClick += DgvOnayBekleyenler_CellClick;
                dgvOnayBekleyenler.DoubleClick += DgvOnayBekleyenler_DoubleClick;
                dgvOnayBekleyenler.SelectionChanged += DgvOnayBekleyenler_SelectionChanged;
            }
        }

        private void DgvOnayBekleyenler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BelgeDetaylariniGoster();
        }

        private void DgvOnayBekleyenler_DoubleClick(object sender, EventArgs e)
        {
            BelgeyiAc();
        }

        private void DgvOnayBekleyenler_SelectionChanged(object sender, EventArgs e)
        {
            BelgeDetaylariniGoster();
        }

        private string SeciliBelgeOzetiGetir()
        {
            if (dgvOnayBekleyenler.CurrentRow == null)
                return "Belge seçili değil.";

            DataGridViewRow satir = dgvOnayBekleyenler.CurrentRow;

            string dokumanTuru = satir.Cells["DokumanTuru"]?.Value?.ToString() ?? "-";
            string direktorluk = satir.Cells["Direktorluk"]?.Value?.ToString() ?? "-";
            string konu = satir.Cells["Konu"]?.Value?.ToString() ?? "-";
            string gonderen = satir.Cells["GonderenAdSoyad"]?.Value?.ToString() ?? "-";

            return $"Konu: {konu}\nDoküman Türü: {dokumanTuru}\nDirektörlük: {direktorluk}\nGönderen: {gonderen}";
        }

        private void BelgeDetaylariniGoster()
        {
            try
            {
                if (dgvOnayBekleyenler.CurrentRow == null)
                {
                    lblKonuDeger.Text = "-";
                    lblAciklamaDeger.Text = "-";
                    lblDokumanTuruDeger.Text = "-";
                    lblDirektorlukDeger.Text = "-";
                    lblGonderenDeger.Text = "-";
                    lblDurumDeger.Text = "-";
                    lblDosyaDeger.Text = "-";
                    return;
                }

                DataGridViewRow satirSecili = dgvOnayBekleyenler.CurrentRow;

                string dokumanTuru = satirSecili.Cells["DokumanTuru"]?.Value?.ToString() ?? "-";
                string direktorluk = satirSecili.Cells["Direktorluk"]?.Value?.ToString() ?? "-";
                string konu = satirSecili.Cells["Konu"]?.Value?.ToString() ?? "-";
                string gonderen = satirSecili.Cells["GonderenAdSoyad"]?.Value?.ToString() ?? "-";
                string durum = satirSecili.Cells["OnayDurumu"]?.Value?.ToString() ?? "-";

                int? belgeId = null;
                if (satirSecili.Cells["Id"]?.Value != null && satirSecili.Cells["Id"]?.Value != DBNull.Value)
                {
                    belgeId = Convert.ToInt32(satirSecili.Cells["Id"].Value);
                }

                string aciklama = "-";
                string dosyaYolu = "-";
                string tamDosyaYolu = null;

                if (belgeId.HasValue)
                {
                    string baglantiMetni = ConfigurationManager
                        .ConnectionStrings["OnaySystem"]
                        .ConnectionString;

                    using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                    using (SqlCommand komut = new SqlCommand(
                        "SELECT Aciklama, DosyaYolu, ImzaliDosya1Yolu, ImzaliDosya2Yolu FROM BelgeGonderim WHERE Id = @Id", baglanti))
                    {
                        komut.Parameters.AddWithValue("@Id", belgeId);
                        baglanti.Open();

                        using (SqlDataReader dr = komut.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                aciklama = dr["Aciklama"]?.ToString() ?? "-";

                                string imzali2 = dr["ImzaliDosya2Yolu"]?.ToString();
                                string imzali1 = dr["ImzaliDosya1Yolu"]?.ToString();
                                string orijinal = dr["DosyaYolu"]?.ToString();

                                tamDosyaYolu = !string.IsNullOrWhiteSpace(imzali2)
                                    ? imzali2
                                    : (!string.IsNullOrWhiteSpace(imzali1) ? imzali1 : orijinal);

                                if (!string.IsNullOrWhiteSpace(tamDosyaYolu))
                                {
                                    dosyaYolu = tamDosyaYolu;
                                    if (dosyaYolu.Length > 50)
                                        dosyaYolu = "..." + dosyaYolu.Substring(dosyaYolu.Length - 47);
                                }
                                else
                                {
                                    dosyaYolu = "-";
                                }
                            }
                        }
                    }
                }
                lblKonuDeger.Text = konu;
                lblAciklamaDeger.Text = aciklama;
                lblDokumanTuruDeger.Text = dokumanTuru;
                lblDirektorlukDeger.Text = direktorluk;
                lblGonderenDeger.Text = gonderen;
                lblDurumDeger.Text = durum;
                lblDosyaDeger.Text = dosyaYolu;

                _secelibelgeId = belgeId;
                _seceliBelgeDosyaYolu = !string.IsNullOrWhiteSpace(tamDosyaYolu) ? tamDosyaYolu : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Belge detayları yüklenirken hata:\n" + ex.Message, "Hata");
            }
        }

        public void BelgeyiAc()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_seceliBelgeDosyaYolu))
                {
                    MessageBox.Show("Belge dosya yolu tanımlı değil.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(_seceliBelgeDosyaYolu))
                {
                    MessageBox.Show($"Dosya bulunamadı:\n{_seceliBelgeDosyaYolu}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = _seceliBelgeDosyaYolu,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dosya açılırken hata:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        Onayci1Id,
                        Onayci2Id
                    FROM BelgeGonderim
                    WHERE (OnayDurumu = 'Beklemede' 
                           OR REPLACE(OnayDurumu, ' ', '') IN (N'1.Onaylandı,2.Beklemede', N'1.Onaylandi,2.Beklemede'))
                      AND (Onayci1Id = @KullaniciId OR Onayci2Id = @KullaniciId)
                    ORDER BY KayitTarihi ASC;";

                DataTable dt = new DataTable();

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(sql, baglanti))
                using (SqlDataAdapter da = new SqlDataAdapter(komut))
                {
                    komut.Parameters.AddWithValue("@KullaniciId", OturumBilgisi.KullaniciId);

                    baglanti.Open();
                    da.Fill(dt);
                }

                dgvOnayBekleyenler.DataSource = dt;

                if (dgvOnayBekleyenler.Columns["DokumanTuru"] != null)
                    dgvOnayBekleyenler.Columns["DokumanTuru"].HeaderText = "Doküman Türü";

                if (dgvOnayBekleyenler.Columns["Direktorluk"] != null)
                    dgvOnayBekleyenler.Columns["Direktorluk"].HeaderText = "Direktörlük";

                if (dgvOnayBekleyenler.Columns["Konu"] != null)
                    dgvOnayBekleyenler.Columns["Konu"].HeaderText = "Konu";

                if (dgvOnayBekleyenler.Columns["GonderenAdSoyad"] != null)
                    dgvOnayBekleyenler.Columns["GonderenAdSoyad"].HeaderText = "Gönderen";

                if (dgvOnayBekleyenler.Columns["OnayDurumu"] != null)
                    dgvOnayBekleyenler.Columns["OnayDurumu"].HeaderText = "Durum";

                if (dgvOnayBekleyenler.Columns["KayitTarihi"] != null)
                    dgvOnayBekleyenler.Columns["KayitTarihi"].HeaderText = "Kayıt Tarihi";

                if (dgvOnayBekleyenler.Columns["DosyaYolu"] != null)
                    dgvOnayBekleyenler.Columns["DosyaYolu"].Visible = false;

                if (dgvOnayBekleyenler.Columns["Id"] != null)
                    dgvOnayBekleyenler.Columns["Id"].Visible = false;

                if (dgvOnayBekleyenler.Columns["Onayci1Id"] != null)
                    dgvOnayBekleyenler.Columns["Onayci1Id"].Visible = false;

                if (dgvOnayBekleyenler.Columns["Onayci2Id"] != null)
                    dgvOnayBekleyenler.Columns["Onayci2Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Onay bekleyen kayıtlar yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? SeciliBelgeIdGetir()
        {
            if (dgvOnayBekleyenler.CurrentRow == null)
                return null;

            object deger = dgvOnayBekleyenler.CurrentRow.Cells["Id"].Value;
            if (deger == null || deger == DBNull.Value)
                return null;

            return Convert.ToInt32(deger);
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            int? id = SeciliBelgeIdGetir();
            if (!id.HasValue)
            {
                MessageBox.Show("Lütfen listeden bir satır seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ozet = SeciliBelgeOzetiGetir();
            string mesaj = "Detayları aşağıdaki gibi olan belgeyi ONAYLAMAK istediğinize emin misiniz?\n\n" + ozet;

            if (MessageBox.Show(mesaj, "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            OnayDurumunuGuncelle(id.Value, "Onayla", null);
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            int? id = SeciliBelgeIdGetir();
            if (!id.HasValue)
            {
                MessageBox.Show("Lütfen listeden bir satır seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ozet = SeciliBelgeOzetiGetir();
            string mesaj = "Detayları aşağıdaki gibi olan belgeyi REDDETMEK istediğinize emin misiniz?\n\n" + ozet;

            if (MessageBox.Show(mesaj, "Red Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            OnayDurumunuGuncelle(id.Value, "Reddet", null);
        }

        private void btnBelgeyiAc_Click(object sender, EventArgs e)
        {
            BelgeyiAc();
        }

        private void btnRevizeTalep_Click(object sender, EventArgs e)
        {
            int? id = SeciliBelgeIdGetir();
            if (!id.HasValue)
            {
                MessageBox.Show("Lütfen listeden bir satır seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string aciklama = Interaction.InputBox(
                "Revize talebinizin gerekçesini yazınız:",
                "Revize Talep",
                "");

            if (string.IsNullOrWhiteSpace(aciklama))
            {
                MessageBox.Show("Revize gerekçesi boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ozet = SeciliBelgeOzetiGetir();
            string mesaj = "Detayları aşağıdaki gibi olan belge için REVİZE TALEP ETMEK istediğinize emin misiniz?\n\n" +
                           ozet + "\n\nRevize Gerekçesi: " + aciklama;

            if (MessageBox.Show(mesaj, "Revize Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            OnayDurumunuGuncelle(id.Value, "Revize", aciklama);
        }

        private void OnayDurumunuGuncelle(int belgeId, string islem, string revizeAciklama)
        {
            try
            {
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;

                int? onayci1Id = null;
                int? onayci2Id = null;
                string mevcutDurum = null;
                string dosyaYolu = null;
                string belgeKonu = null;

                int? imzaPozisyonu = null;
                string imzaliDosyaYoluDb = null;

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(
                    "SELECT Onayci1Id, Onayci2Id, OnayDurumu, DosyaYolu, Konu FROM BelgeGonderim WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", belgeId);
                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["Onayci1Id"] != DBNull.Value)
                                onayci1Id = Convert.ToInt32(dr["Onayci1Id"]);

                            if (dr["Onayci2Id"] != DBNull.Value)
                                onayci2Id = Convert.ToInt32(dr["Onayci2Id"]);

                            mevcutDurum = dr["OnayDurumu"] as string;
                            dosyaYolu = dr["DosyaYolu"] as string;
                            belgeKonu = dr["Konu"] as string;
                        }
                        else
                        {
                            MessageBox.Show("Belge bulunamadı.", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (!onayci1Id.HasValue && !onayci2Id.HasValue)
                {
                    MessageBox.Show("Bu belge için onaycı tanımlı değil.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int aktifKullaniciId = OturumBilgisi.KullaniciId;
                bool aktifOnayci1 = onayci1Id.HasValue && onayci1Id.Value == aktifKullaniciId;
                bool aktifOnayci2 = onayci2Id.HasValue && onayci2Id.Value == aktifKullaniciId;

                bool ikinciOnayBeklemede = DurumIkinciOnayBeklemede(mevcutDurum);

                if (!aktifOnayci1 && !aktifOnayci2)
                {
                    MessageBox.Show("Bu belge için onay yetkiniz yok.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string yeniDurumDb = mevcutDurum;

                string kaynakPdfYolu = null;
                string hedefPdfYolu = null;

                if (!string.IsNullOrWhiteSpace(dosyaYolu))
                {
                    try
                    {
                        dosyaYolu = PdfYoluGarantile(baglantiMetni, belgeId, dosyaYolu);
                    }
                    catch (Exception exConvert)
                    {
                        MessageBox.Show("Belge PDF'e dönüştürülürken hata oluştu:\n" + exConvert.Message,
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (islem == "Onayla")
                {
                    if (string.IsNullOrWhiteSpace(dosyaYolu))
                    {
                        MessageBox.Show("Belge dosya yolu tanımlı değil.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string klasor = Path.GetDirectoryName(dosyaYolu);
                    string ad = Path.GetFileNameWithoutExtension(dosyaYolu);
                    string ext = Path.GetExtension(dosyaYolu);

                    if (aktifOnayci1)
                    {
                        imzaPozisyonu = 1;   // 1. onaycı → sol kutu

                        if (mevcutDurum == "Beklemede")
                        {
                            if (!onayci2Id.HasValue)
                            {
                                yeniDurumDb = "Onaylandı";
                                kaynakPdfYolu = dosyaYolu;
                                hedefPdfYolu = Path.Combine(klasor, ad + "_1_imzali" + ext);
                                imzaliDosyaYoluDb = hedefPdfYolu; // 1. imzalı yol
                            }
                            else
                            {
                                yeniDurumDb = "1. Onaylandı, 2. Beklemede";
                                kaynakPdfYolu = dosyaYolu;
                                hedefPdfYolu = Path.Combine(klasor, ad + "_1_imzali" + ext);
                                imzaliDosyaYoluDb = hedefPdfYolu; // 1. imzalı yol
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu belge şu anda 1. onay aşamasında değil.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (aktifOnayci2)
                    {
                        if (mevcutDurum == "1. Onaylandı, 2. Beklemede")
                        {
                            imzaPozisyonu = 2;   // 2. onaycı → sağ kutu

                            yeniDurumDb = "Tam Onaylandı";

                            string birinciImzali = Path.Combine(klasor, ad + "_1_imzali" + ext);
                            kaynakPdfYolu = File.Exists(birinciImzali) ? birinciImzali : dosyaYolu;
                            hedefPdfYolu = Path.Combine(klasor, ad + "_2_imzali" + ext);
                            imzaliDosyaYoluDb = hedefPdfYolu; // 2. imzalı yol
                        }
                        else
                        {
                            MessageBox.Show("Bu belge için önce 1. onayın tamamlanması gerekiyor.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                else if (islem == "Reddet")
                {
                    if (aktifOnayci2 && !ikinciOnayBeklemede)
                    {
                        MessageBox.Show("Bu belge için önce 1. onayın tamamlanması gerekiyor.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    yeniDurumDb = "Reddedildi";
                }
                else if (islem == "Revize")
                {
                    if (aktifOnayci2 && !ikinciOnayBeklemede)
                    {
                        MessageBox.Show("Bu belge için önce 1. onayın tamamlanması gerekiyor.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    yeniDurumDb = "Revize Talep Edildi";
                }
                else
                {
                    MessageBox.Show("Bilinmeyen işlem tipi.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string onaylayan =
                    !string.IsNullOrWhiteSpace(OturumBilgisi.AdSoyad)
                        ? OturumBilgisi.AdSoyad
                        : (!string.IsNullOrWhiteSpace(OturumBilgisi.KullaniciAdi)
                            ? OturumBilgisi.KullaniciAdi
                            : Environment.UserName);

                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    UPDATE BelgeGonderim
                    SET OnayDurumu      = @OnayDurumu,
                        OnayTarihi      = SYSDATETIME(),
                        Onaylayan       = @Onaylayan,
                        RevizeAciklama  = @RevizeAciklama,
                        ImzaliDosya1Yolu = CASE 
                                               WHEN @ImzaPozisyonu = 1 AND @ImzaliDosyaYolu IS NOT NULL 
                                                   THEN @ImzaliDosyaYolu 
                                               ELSE ImzaliDosya1Yolu 
                                           END,
                        ImzaliDosya2Yolu = CASE 
                                               WHEN @ImzaPozisyonu = 2 AND @ImzaliDosyaYolu IS NOT NULL 
                                                   THEN @ImzaliDosyaYolu 
                                               ELSE ImzaliDosya2Yolu 
                                           END
                    WHERE Id = @Id;", baglanti))
                {
                    komut.Parameters.AddWithValue("@OnayDurumu", yeniDurumDb);
                    komut.Parameters.AddWithValue("@Onaylayan", onaylayan);
                    komut.Parameters.AddWithValue("@RevizeAciklama",
                        (object)revizeAciklama ?? DBNull.Value);

                    komut.Parameters.AddWithValue("@ImzaPozisyonu",
                        (object)imzaPozisyonu ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@ImzaliDosyaYolu",
                        (object)imzaliDosyaYoluDb ?? DBNull.Value);

                    komut.Parameters.AddWithValue("@Id", belgeId);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }

                if (islem == "Onayla" &&
                    !string.IsNullOrWhiteSpace(kaynakPdfYolu) &&
                    !string.IsNullOrWhiteSpace(hedefPdfYolu) &&
                    imzaPozisyonu.HasValue)
                {
                    try
                    {
                        string imzaPngYolu = ImzaServisi.ImzaDosyaYoluGetir(OturumBilgisi.KullaniciAdi);
                        string imzaliYol = ImzaServisi.BelgeyeImzaEkle(
                            kaynakPdfYolu,
                            hedefPdfYolu,
                            imzaPngYolu,
                            OturumBilgisi.AdSoyad,
                            imzaPozisyonu.Value);

                        MessageBox.Show(
                            $"İmzalı belge oluşturuldu:\n{imzaliYol}",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception exImza)
                    {
                        MessageBox.Show(
                            "Onay verildi ancak imzalı PDF oluşturulurken hata oluştu:\n" + exImza.Message,
                            "İmza Hatası",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }

                try
                {
                    string islemTipi = islem switch
                    {
                        "Onayla" => aktifOnayci1 ? "1. Onay" : "2. Onay",
                        "Reddet" => "Red",
                        "Revize" => "Revize Talep",
                        _ => "Bilinmeyen İşlem"
                    };

                    LogYaz(baglantiMetni, belgeId, islemTipi, revizeAciklama);
                }
                catch (Exception exLog)
                {
                    System.Diagnostics.Debug.WriteLine("BelgeLog yazma hatasi: " + exLog.Message);
                }

                try
                {
                    AuditLogHelper.BelgeDurumuDegisti(belgeId, mevcutDurum, yeniDurumDb, belgeKonu ?? "");
                }
                catch (Exception exMail)
                {
                    System.Diagnostics.Debug.WriteLine("AuditLog/Mail hatasi: " + exMail.Message);
                }

                MessageBox.Show($"Kayıt '{yeniDurumDb}' olarak güncellendi.",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                KayitlariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Onay durumu güncellenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string PdfYoluGarantile(string baglantiMetni, int belgeId, string mevcutDosyaYolu)
        {
            string ext = Path.GetExtension(mevcutDosyaYolu).ToLowerInvariant();
            if (ext != ".doc" && ext != ".docx")
                return mevcutDosyaYolu;

            string klasor = Path.GetDirectoryName(mevcutDosyaYolu) ?? "";
            string ad = Path.GetFileNameWithoutExtension(mevcutDosyaYolu);
            string hedefPdf = Path.Combine(klasor, ad + "_pdf.pdf");

            WordDosyasiniPdfyeCevir(mevcutDosyaYolu, hedefPdf);

            using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
            using (SqlCommand komut = new SqlCommand(
                "UPDATE BelgeGonderim SET DosyaYolu = @YeniYol WHERE Id = @Id", baglanti))
            {
                komut.Parameters.AddWithValue("@YeniYol", hedefPdf);
                komut.Parameters.AddWithValue("@Id", belgeId);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }

            return hedefPdf;
        }

        private void WordDosyasiniPdfyeCevir(string kaynakDocYolu, string hedefPdfYolu)
        {
            Type wordType = Type.GetTypeFromProgID("Word.Application");
            if (wordType == null)
                throw new InvalidOperationException("Microsoft Word bulunamadı. Word yüklü olmalı.");

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

        private void LogYaz(string baglantiMetni, int belgeId, string islemTipi, string aciklama)
        {
            string kullaniciAdi = OturumBilgisi.KullaniciAdi ?? "Bilinmeyen";
            string adSoyad = OturumBilgisi.AdSoyad ?? OturumBilgisi.KullaniciAdi ?? "Bilinmeyen";

            string sqlLog = @"
                INSERT INTO BelgeLog (BelgeId, IslemTipi, IslemYapanId, IslemYapanAdi, Aciklama, IslemTarihi)
                VALUES (@BelgeId, @IslemTipi, @IslemYapanId, @IslemYapanAdi, @Aciklama, SYSDATETIME());";

            using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
            using (SqlCommand komut = new SqlCommand(sqlLog, baglanti))
            {
                komut.Parameters.AddWithValue("@BelgeId", belgeId);
                komut.Parameters.AddWithValue("@IslemTipi", islemTipi);
                komut.Parameters.AddWithValue("@IslemYapanId", OturumBilgisi.KullaniciId);
                komut.Parameters.AddWithValue("@IslemYapanAdi", adSoyad);
                komut.Parameters.AddWithValue("@Aciklama", (object)aciklama ?? DBNull.Value);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }
        }

        private bool DurumIkinciOnayBeklemede(string durum)
        {
            if (string.IsNullOrWhiteSpace(durum))
                return false;

            string temiz = durum.Replace(" ", string.Empty).ToLowerInvariant();
            return temiz == "1.onaylandı,2.beklemede" || temiz == "1.onaylandi,2.beklemede";
        }
    }
}

