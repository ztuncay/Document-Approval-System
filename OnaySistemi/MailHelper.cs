using System;
using System.Configuration;

namespace OnaySistemi
{
    public static class MailHelper
    {
        private static readonly bool _mailAktif = ConfigurationManager.AppSettings["MailBildirimAktif"] == "true";

        public static bool MailGonder(string aliciMail, string konu, string icerik)
        {
            if (!_mailAktif)
                return false;
            if (string.IsNullOrWhiteSpace(aliciMail))
                return false;

            try
            {
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                    return false;

                dynamic outlookApp = Activator.CreateInstance(outlookType);
                dynamic mailItem = outlookApp.CreateItem(0);
                mailItem.To = aliciMail;
                mailItem.Subject = konu;
                mailItem.HTMLBody = icerik;
                mailItem.Send();
                return true;
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                if (ex.InnerException != null)
                    hata += " | " + ex.InnerException.Message;
                System.Windows.Forms.MessageBox.Show(
                    "Mail gonderilemedi!\n\n" + hata,
                    "Mail Hatasi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
        }

        public static bool MailGonder(string[] aliciMailler, string konu, string icerik)
        {
            if (!_mailAktif || aliciMailler == null || aliciMailler.Length == 0)
                return false;
            bool ok = true;
            foreach (string m in aliciMailler)
                if (!string.IsNullOrWhiteSpace(m))
                    ok &= MailGonder(m, konu, icerik);
            return ok;
        }

        private static string Sablon(string baslik, string baslikRenk, string govde)
        {
            string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            return "<html><head><meta charset=\"utf-8\"/></head>"
                + "<body style=\"margin:0;padding:0;background-color:#f4f4f4;font-family:Segoe UI,Arial,sans-serif;\">"
                + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:#f4f4f4;padding:20px 0;\"><tr><td align=\"center\">"
                + "<table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);\">"
                + "<tr><td style=\"background-color:" + baslikRenk + ";padding:24px 30px;\">"
                + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr>"
                + "<td><span style=\"font-size:20px;font-weight:bold;color:#ffffff;\">" + baslik + "</span></td>"
                + "<td align=\"right\"><span style=\"font-size:12px;color:rgba(255,255,255,0.8);\">DDOS</span></td>"
                + "</tr></table></td></tr>"
                + "<tr><td style=\"padding:30px;\">" + govde + "</td></tr>"
                + "<tr><td style=\"background-color:#f8f8f8;padding:16px 30px;border-top:1px solid #e0e0e0;\">"
                + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr>"
                + "<td style=\"font-size:11px;color:#999;\">Bu otomatik bir bildirimdir.</td>"
                + "<td align=\"right\" style=\"font-size:11px;color:#999;\">" + tarih + "</td>"
                + "</tr></table></td></tr>"
                + "</table></td></tr></table></body></html>";
        }

        private static string Satir(string etiket, string deger)
        {
            return "<tr>"
                + "<td style=\"padding:8px 12px;font-size:13px;color:#666;width:140px;border-bottom:1px solid #f0f0f0;\">" + etiket + "</td>"
                + "<td style=\"padding:8px 12px;font-size:13px;color:#333;font-weight:600;border-bottom:1px solid #f0f0f0;\">" + deger + "</td>"
                + "</tr>";
        }

        private static string TabloAc()
        {
            return "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #e8e8e8;border-radius:6px;overflow:hidden;margin:16px 0;\">";
        }

        private static string TabloKapat()
        {
            return "</table>";
        }

        public static void BelgeOnayBildirimi(string aliciMail, string aliciAd, string belgeKonu, string gonderenAd, int belgeId)
        {
            string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            string govde = "<p style=\"font-size:15px;color:#333;margin:0 0 8px 0;\">Sayýn <strong>" + aliciAd + "</strong>,</p>"
                + "<p style=\"font-size:14px;color:#555;line-height:1.6;\">Uygulamada onayýnýzý bekleyen bir belge bulunmaktadýr. Lütfen aþaðýdaki belgeyi inceleyerek onayýnýzý veriniz.</p>"
                + TabloAc()
                + Satir("Belge No", belgeId.ToString())
                + Satir("Belge Konusu", belgeKonu)
                + Satir("Gonderen", gonderenAd)
                + Satir("Tarih", tarih)
                + TabloKapat()
                + "<p style=\"font-size:13px;color:#888;margin-top:20px;\">DDOS uygulamasýný açarak iþleminizi gerçekleþtirebilirsiniz.</p>";

            string html = Sablon("Onay Bekleyen Belge", "#e6a817", govde);
            MailGonder(aliciMail, "Onayýnýzý Bekleyen Belge Bulunmaktadýr", html);
        }

        public static void BelgeOnaylandi(string aliciMail, string aliciAd, string belgeKonu, string onaylayanAd, int belgeId)
        {
            string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            string govde = "<p style=\"font-size:15px;color:#333;margin:0 0 8px 0;\">Sayýn <strong>" + aliciAd + "</strong>,</p>"
                + "<p style=\"font-size:14px;color:#555;line-height:1.6;\">Göndermiþ olduðunuz belge <strong>onaylanmýþtýr</strong>.</p>"
                + TabloAc()
                + Satir("Belge No", belgeId.ToString())
                + Satir("Belge Konusu", belgeKonu)
                + Satir("Onaylayan", onaylayanAd)
                + Satir("Onay Tarihi", tarih)
                + Satir("Durum", "<span style=\"color:#2e7d32;font-weight:bold;\">ONAYLANDI</span>")
                + TabloKapat();

            string html = Sablon("Belge Onaylandý", "#2e7d32", govde);
            MailGonder(aliciMail, "Gönderdiðiniz '" + belgeKonu + "' Belgesi Onaylandý", html);
        }

        public static void BelgeReddedildi(string aliciMail, string aliciAd, string belgeKonu, string reddedenAd, string redNedeni, int belgeId)
        {
            string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            string neden = (!string.IsNullOrWhiteSpace(redNedeni) && redNedeni != "Belirtilmedi") ? redNedeni : "-";

            string govde = "<p style=\"font-size:15px;color:#333;margin:0 0 8px 0;\">Sayýn <strong>" + aliciAd + "</strong>,</p>"
                + "<p style=\"font-size:14px;color:#555;line-height:1.6;\">Göndermiþ olduðunuz belge <strong>reddedilmiþtir</strong>. Lütfen belgenizi gözden geçirip tekrar gönderiniz.</p>"
                + TabloAc()
                + Satir("Belge No", belgeId.ToString())
                + Satir("Belge Konusu", belgeKonu)
                + Satir("Reddeden", reddedenAd)
                + Satir("Red Nedeni", neden)
                + Satir("Tarih", tarih)
                + Satir("Durum", "<span style=\"color:#c62828;font-weight:bold;\">REDDEDÝLDÝ</span>")
                + TabloKapat();

            string html = Sablon("Belge Reddedildi", "#c62828", govde);
            MailGonder(aliciMail, "Gönderdiðiniz '" + belgeKonu + "' Belgesi Reddedildi", html);
        }

        public static void BelgeRevizeTalep(string aliciMail, string aliciAd, string belgeKonu, string talepEdenAd, string revizeAciklama, int belgeId)
        {
            string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            string govde = "<p style=\"font-size:15px;color:#333;margin:0 0 8px 0;\">Sayýn <strong>" + aliciAd + "</strong>,</p>"
                + "<p style=\"font-size:14px;color:#555;line-height:1.6;\">Göndermiþ olduðunuz belge için <strong>revize talep edilmiþtir</strong>. Lütfen aþaðýdaki gerekçeyi inceleyip belgenizi revize ediniz.</p>"
                + TabloAc()
                + Satir("Belge No", belgeId.ToString())
                + Satir("Belge Konusu", belgeKonu)
                + Satir("Talep Eden", talepEdenAd)
                + Satir("Tarih", tarih)
                + Satir("Durum", "<span style=\"color:#e65100;font-weight:bold;\">REVIZE TALEP EDILDI</span>")
                + TabloKapat()
                + "<div style=\"background-color:#fff8e1;border:1px solid #ffe082;border-radius:6px;padding:14px 16px;margin:12px 0;\">"
                + "<p style=\"margin:0 0 4px 0;font-size:12px;color:#e65100;font-weight:bold;\">Revize Gerekce:</p>"
                + "<p style=\"margin:0;font-size:13px;color:#333;\">" + revizeAciklama + "</p>"
                + "</div>";

            string html = Sablon("Revize Talep Edildi", "#e65100", govde);
            MailGonder(aliciMail, "Gonderdiginiz '" + belgeKonu + "' Belgesi Icin Revize Talep Edildi", html);
        }

        public static void BelgeDurumDegisikligi(string aliciMail, string aliciAd, string belgeKonu, string eskiDurum, string yeniDurum, int belgeId)
        {
            string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            string govde = "<p style=\"font-size:15px;color:#333;margin:0 0 8px 0;\">Sayýn <strong>" + aliciAd + "</strong>,</p>"
                + "<p style=\"font-size:14px;color:#555;line-height:1.6;\">Belgenizin durumu güncellenmistir.</p>"
                + TabloAc()
                + Satir("Belge No", belgeId.ToString())
                + Satir("Belge Konusu", belgeKonu)
                + Satir("Eski Durum", eskiDurum)
                + Satir("Yeni Durum", "<strong>" + yeniDurum + "</strong>")
                + Satir("Tarih", tarih)
                + TabloKapat();

            string html = Sablon("Durum Deðiþikliði", "#1565c0", govde);
            MailGonder(aliciMail, "Belge Durum Deðiþikliði", html);
        }
    }
}
