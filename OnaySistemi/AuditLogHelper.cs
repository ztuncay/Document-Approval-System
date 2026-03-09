using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace OnaySistemi
{
    public static class AuditLogHelper
    {
        private static readonly string _baglantiMetni = 
            ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;

        public static void KayitYap(string islemTipi, string tablo, int? kaydId, string aciklama)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    INSERT INTO AuditLog (KullaniciAdi, IslemTipi, Tablo, KaydId, Aciklama, IslemTarihi)
                    VALUES (@KullaniciAdi, @IslemTipi, @Tablo, @KaydId, @Aciklama, SYSDATETIME())", baglanti))
                {
                    komut.Parameters.AddWithValue("@KullaniciAdi", OturumBilgisi.KullaniciAdi ?? "Sistem");
                    komut.Parameters.AddWithValue("@IslemTipi", islemTipi);
                    komut.Parameters.AddWithValue("@Tablo", tablo);
                    komut.Parameters.AddWithValue("@KaydId", kaydId.HasValue ? (object)kaydId.Value : DBNull.Value);
                    komut.Parameters.AddWithValue("@Aciklama", aciklama ?? "");

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AuditLog yazma hatasi: " + ex.Message);
            }
        }

        public static void KullaniciEklendi(int kullaniciId, string kullaniciAdi, string adSoyad, string rol)
        {
            KayitYap("Kullanici_Ekle", "Kullanicilar", kullaniciId, 
                $"Yeni kullanici eklendi: {adSoyad} ({kullaniciAdi}) - Rol: {rol}");
        }

        public static void KullaniciGuncellendi(int kullaniciId, string adSoyad, string rol)
        {
            KayitYap("Kullanici_Duzenle", "Kullanicilar", kullaniciId,
                $"Kullanici guncellendi: {adSoyad} - Rol: {rol}");
        }

        public static void KullaniciSilindi(int kullaniciId, string adSoyad)
        {
            KayitYap("Kullanici_Sil", "Kullanicilar", kullaniciId,
                $"Kullanici silindi: {adSoyad}");
        }

        public static void SifreGuncellendi(int kullaniciId, string kullaniciAdi)
        {
            KayitYap("Sifre_Degistir", "Kullanicilar", kullaniciId,
                $"Sifre degistirildi: {kullaniciAdi}");
        }

        public static void KullaniciGiris(int kullaniciId, string kullaniciAdi, string adSoyad, string rol)
        {
            KayitYap("Kullanici_Giris", "Kullanicilar", kullaniciId,
                $"Kullanici giris yapti: {adSoyad} ({kullaniciAdi}) - Rol: {rol}");
        }

        public static void KullaniciCikis(int kullaniciId, string kullaniciAdi, string adSoyad)
        {
            KayitYap("Kullanici_Cikis", "Kullanicilar", kullaniciId,
                $"Kullanici cikis yapti: {adSoyad} ({kullaniciAdi})");
        }

        public static void BelgeDurumuDegisti(int belgeId, string eskiDurum, string yeniDurum, string konu)
        {
            KayitYap("Belge_Durum_Degistir", "BelgeGonderim", belgeId,
                $"Belge durumu degistirildi: '{konu}' - {eskiDurum} -> {yeniDurum}");

            BelgeDurumuDegisikligiMailGonder(belgeId, eskiDurum, yeniDurum, konu);
        }

        public static void BelgeSilindi(int belgeId, string konu)
        {
            KayitYap("Belge_Sil", "BelgeGonderim", belgeId,
                $"Belge silindi: {konu}");
        }

        public static void BelgeGonderildi(int belgeId, string konu, string direktorluk)
        {
            KayitYap("Belge_Gonder", "BelgeGonderim", belgeId,
                $"Belge gonderildi: {konu} ({direktorluk})");

            BelgeGonderildiMailGonder(belgeId, konu);
        }

        private static string KullaniciEmailGetir(int kullaniciId)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT Email, MailBildirimAktif 
                    FROM Kullanicilar 
                    WHERE Id = @KullaniciId", baglanti))
                {
                    komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                    baglanti.Open();
                    
                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bool mailAktif = dr["MailBildirimAktif"] != DBNull.Value && 
                                           Convert.ToBoolean(dr["MailBildirimAktif"]);
                            
                            if (mailAktif && dr["Email"] != DBNull.Value)
                            {
                                return dr["Email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Email getirme hatasi: {ex.Message}");
            }
            
            return null;
        }

        private static void BelgeGonderildiMailGonder(int belgeId, string konu)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[MAIL DEBUG] BelgeGonderildiMailGonder basladi. BelgeId={belgeId}, Konu={konu}");

                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT 
                        bg.OnayDurumu,
                        bg.GonderenId,
                        bg.GonderenKullaniciAdi,
                        COALESCE(g.AdSoyad, bg.GonderenAdSoyad) as GonderenAd,
                        bg.Onayci1Id,
                        o1.AdSoyad as Onayci1Ad,
                        o1.Email as Onayci1Email,
                        o1.MailBildirimAktif as Onayci1MailAktif,
                        bg.Onayci2Id,
                        o2.AdSoyad as Onayci2Ad,
                        o2.Email as Onayci2Email
                    FROM BelgeGonderim bg
                    LEFT JOIN Kullanicilar g ON bg.GonderenId = g.Id OR bg.GonderenKullaniciAdi = g.KullaniciAdi
                    LEFT JOIN Kullanicilar o1 ON bg.Onayci1Id = o1.Id
                    LEFT JOIN Kullanicilar o2 ON bg.Onayci2Id = o2.Id
                    WHERE bg.Id = @BelgeId", baglanti))
                {
                    komut.Parameters.AddWithValue("@BelgeId", belgeId);
                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string gonderenAd = dr["GonderenAd"]?.ToString() ?? "Bilinmeyen";

                            if (dr["Onayci1Id"] != DBNull.Value)
                            {
                                int onayci1Id = Convert.ToInt32(dr["Onayci1Id"]);
                                string onayci1Ad = dr["Onayci1Ad"]?.ToString() ?? "";
                                string onayci1Email = dr["Onayci1Email"]?.ToString();

                                System.Diagnostics.Debug.WriteLine($"[MAIL DEBUG] Onayci1: Id={onayci1Id}, Ad={onayci1Ad}, Email={onayci1Email ?? "BOÞ"}");

                                if (string.IsNullOrWhiteSpace(onayci1Email))
                                {
                                    System.Diagnostics.Debug.WriteLine($"[MAIL DEBUG] Onayci1 email BOÞ! Mail gonderilemez. Kullanici tablosunda Email alani dolu mu kontrol edin.");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"[MAIL DEBUG] Mail gonderiliyor: {onayci1Email}");
                                    MailHelper.BelgeOnayBildirimi(onayci1Email, onayci1Ad, konu, gonderenAd, belgeId);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"[MAIL DEBUG] Onayci1Id BOÞ! Belgeye onayci atanmamis.");
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"[MAIL DEBUG] BelgeId={belgeId} icin kayit BULUNAMADI!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Belge gonderildi mail hatasi: {ex.Message}");
            }
        }

        private static void BelgeDurumuDegisikligiMailGonder(int belgeId, string eskiDurum, string yeniDurum, string konu)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    SELECT 
                        bg.GonderenId,
                        bg.GonderenKullaniciAdi,
                        COALESCE(g.AdSoyad, bg.GonderenAdSoyad) as GonderenAd,
                        g.Email as GonderenEmail,
                        g.MailBildirimAktif as GonderenMailAktif,
                        bg.Onayci1Id,
                        o1.AdSoyad as Onayci1Ad,
                        o1.Email as Onayci1Email,
                        o1.MailBildirimAktif as Onayci1MailAktif,
                        bg.Onayci2Id,
                        o2.AdSoyad as Onayci2Ad,
                        o2.Email as Onayci2Email,
                        bg.RevizeAciklama
                    FROM BelgeGonderim bg
                    LEFT JOIN Kullanicilar g ON bg.GonderenId = g.Id OR bg.GonderenKullaniciAdi = g.KullaniciAdi
                    LEFT JOIN Kullanicilar o1 ON bg.Onayci1Id = o1.Id
                    LEFT JOIN Kullanicilar o2 ON bg.Onayci2Id = o2.Id
                    WHERE bg.Id = @BelgeId", baglanti))
                {
                    komut.Parameters.AddWithValue("@BelgeId", belgeId);
                    baglanti.Open();

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string gonderenAd = dr["GonderenAd"]?.ToString() ?? "";
                            string onaylayan = OturumBilgisi.AdSoyad ?? "Bilinmeyen";
                            string revizeAciklama = dr["RevizeAciklama"]?.ToString() ?? "Belirtilmedi";

                            string gonderenEmail = dr["GonderenEmail"]?.ToString();
                            bool gonderenMailAktif = dr["GonderenMailAktif"] != DBNull.Value && 
                                                    Convert.ToBoolean(dr["GonderenMailAktif"]);

                            // Gönderene email gönder
                            if (gonderenMailAktif && !string.IsNullOrWhiteSpace(gonderenEmail))
                            {
                                if (yeniDurum.Contains("Onaylandý") || yeniDurum == "Tam Onaylandý")
                                {
                                    MailHelper.BelgeOnaylandi(gonderenEmail, gonderenAd, konu, onaylayan, belgeId);
                                }
                                else if (yeniDurum == "Reddedildi")
                                {
                                    MailHelper.BelgeReddedildi(gonderenEmail, gonderenAd, konu, onaylayan, "Belirtilmedi", belgeId);
                                }
                                else if (yeniDurum == "Revize Talep Edildi")
                                {
                                    MailHelper.BelgeRevizeTalep(gonderenEmail, gonderenAd, konu, onaylayan, revizeAciklama, belgeId);
                                }
                                else
                                {
                                    MailHelper.BelgeDurumDegisikligi(gonderenEmail, gonderenAd, konu, eskiDurum, yeniDurum, belgeId);
                                }
                            }

                            // 2. onaycý revize istediðinde 1. onaycýya da bilgilendirme emaili gönder
                            if (yeniDurum == "Revize Talep Edildi" && eskiDurum == "1. Onaylandý, 2. Beklemede")
                            {
                                if (dr["Onayci1Id"] != DBNull.Value)
                                {
                                    string onayci1Email = dr["Onayci1Email"]?.ToString();
                                    bool onayci1MailAktif = dr["Onayci1MailAktif"] != DBNull.Value && 
                                                           Convert.ToBoolean(dr["Onayci1MailAktif"]);
                                    string onayci1Ad = dr["Onayci1Ad"]?.ToString() ?? "";

                                    if (onayci1MailAktif && !string.IsNullOrWhiteSpace(onayci1Email))
                                    {
                                        string govde = "<p style=\"font-size:15px;color:#333;margin:0 0 8px 0;\">Sayýn <strong>" + onayci1Ad + "</strong>,</p>"
                                            + "<p style=\"font-size:14px;color:#555;line-height:1.6;\">2. onaycý tarafýndan <strong>revize talep edilmiþtir</strong>. Bu belge için sizin onayýnýz da tekrar gerekecektir. Bilginize sunulur.</p>"
                                            + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #e8e8e8;border-radius:6px;overflow:hidden;margin:16px 0;\">"
                                            + "<tr><td style=\"padding:8px 12px;font-size:13px;color:#666;width:140px;border-bottom:1px solid #f0f0f0;\">Belge No</td>"
                                            + "<td style=\"padding:8px 12px;font-size:13px;color:#333;font-weight:600;border-bottom:1px solid #f0f0f0;\">" + belgeId.ToString() + "</td></tr>"
                                            + "<tr><td style=\"padding:8px 12px;font-size:13px;color:#666;width:140px;border-bottom:1px solid #f0f0f0;\">Belge Konusu</td>"
                                            + "<td style=\"padding:8px 12px;font-size:13px;color:#333;font-weight:600;border-bottom:1px solid #f0f0f0;\">" + konu + "</td></tr>"
                                            + "<tr><td style=\"padding:8px 12px;font-size:13px;color:#666;width:140px;border-bottom:1px solid #f0f0f0;\">Revize Talep Eden</td>"
                                            + "<td style=\"padding:8px 12px;font-size:13px;color:#333;font-weight:600;border-bottom:1px solid #f0f0f0;\">" + onaylayan + " (2. Onaycý)</td></tr>"
                                            + "<tr><td style=\"padding:8px 12px;font-size:13px;color:#666;width:140px;border-bottom:1px solid #f0f0f0;\">Tarih</td>"
                                            + "<td style=\"padding:8px 12px;font-size:13px;color:#333;font-weight:600;border-bottom:1px solid #f0f0f0;\">" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "</td></tr>"
                                            + "</table>"
                                            + "<div style=\"background-color:#fff8e1;border:1px solid #ffe082;border-radius:6px;padding:14px 16px;margin:12px 0;\">"
                                            + "<p style=\"margin:0 0 4px 0;font-size:12px;color:#e65100;font-weight:bold;\">Revize Gerekçesi:</p>"
                                            + "<p style=\"margin:0;font-size:13px;color:#333;\">" + revizeAciklama + "</p>"
                                            + "</div>";

                                        string html = "<html><head><meta charset=\"utf-8\"/></head>"
                                            + "<body style=\"margin:0;padding:0;background-color:#f4f4f4;font-family:Segoe UI,Arial,sans-serif;\">"
                                            + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:#f4f4f4;padding:20px 0;\"><tr><td align=\"center\">"
                                            + "<table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);\">"
                                            + "<tr><td style=\"background-color:#ff9800;padding:24px 30px;\">"
                                            + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr>"
                                            + "<td><span style=\"font-size:20px;font-weight:bold;color:#ffffff;\">2. Onaycý Revize Talep Etti (Bilgilendirme)</span></td>"
                                            + "<td align=\"right\"><span style=\"font-size:12px;color:rgba(255,255,255,0.8);\">DDOS</span></td>"
                                            + "</tr></table></td></tr>"
                                            + "<tr><td style=\"padding:30px;\">" + govde + "</td></tr>"
                                            + "<tr><td style=\"background-color:#f8f8f8;padding:16px 30px;border-top:1px solid #e0e0e0;\">"
                                            + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr>"
                                            + "<td style=\"font-size:11px;color:#999;\">Bu otomatik bir bildirimdir.</td>"
                                            + "<td align=\"right\" style=\"font-size:11px;color:#999;\">" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "</td>"
                                            + "</tr></table></td></tr>"
                                            + "</table></td></tr></table></body></html>";

                                        MailHelper.MailGonder(onayci1Email, "2. Onaycý Revize Talep Etti - Bilgilendirme", html);
                                    }
                                }
                            }

                            // 1. onaycý onayladýktan sonra 2. onaycýya email gönder (ADIM 7A)
                            if (yeniDurum == "1. Onaylandý, 2. Beklemede" && dr["Onayci2Id"] != DBNull.Value)
                            {
                                string onayci2Ad = dr["Onayci2Ad"]?.ToString() ?? "";
                                string onayci2Email = dr["Onayci2Email"]?.ToString();

                                if (!string.IsNullOrWhiteSpace(onayci2Email))
                                {
                                    string onayci1Ad = dr["Onayci1Ad"]?.ToString() ?? "";
                                    MailHelper.BelgeOnayBildirimi(onayci2Email, onayci2Ad, konu, onayci1Ad, belgeId);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Belge durum degisikligi mail hatasi: {ex.Message}");
            }
        }
    }
}
