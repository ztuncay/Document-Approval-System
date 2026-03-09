using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                string sifre = "1234";
                string hash = SifreServisi.SifreHashle(sifre);
                
                string baglantiMetni = ConfigurationManager
                    .ConnectionStrings["OnaySystem"]
                    .ConnectionString;
                
                using (SqlConnection baglanti = new SqlConnection(baglantiMetni))
                {
                    baglanti.Open();
                    
                    string sql = @"
                        UPDATE Kullanicilar 
                        SET Sifre = @Hash 
                        WHERE Sifre IS NULL OR Sifre = '';";
                    
                    using (SqlCommand komut = new SqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@Hash", hash);
                        int etkilenenSatir = komut.ExecuteNonQuery();
                        
                        if (etkilenenSatir > 0)
                        {
                            MessageBox.Show(
                                $"? MIGRATION BAÞARILI!\n\n" +
                                $"Þifre: {sifre}\n" +
                                $"Güncellenen Kullanýcý: {etkilenenSatir}\n\n" +
                                $"Artýk giriþ yapabilirsin:\n" +
                                $"  Kullanýcý: ztuncay\n" +
                                $"  Þifre: {sifre}",
                                "Migration Baþarýlý",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"? Migration Hatasý:\n\n{ex.Message}\n\n" +
                    $"Program yine de devam edecek.",
                    "Uyarý",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OturumBilgisi.Temizle();

            using (FrmGiris girisForm = new FrmGiris())
            {
                var sonuc = girisForm.ShowDialog();

                if (sonuc == DialogResult.OK &&
                    !string.IsNullOrEmpty(OturumBilgisi.KullaniciAdi))
                {
                    Application.Run(new FrmAnaMenu());
                }
                else
                {
                    return;
                }
            }
        }
    }
}
