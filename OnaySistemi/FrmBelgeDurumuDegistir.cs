using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace OnaySistemi
{
    public partial class FrmBelgeDurumuDegistir : Form
    {
        private readonly string _baglantiMetni;
        private int _belgeId;
        private string _suankiDurum;

        public FrmBelgeDurumuDegistir(int belgeId, string suankiDurum)
        {
            InitializeComponent();
            _baglantiMetni = ConfigurationManager.ConnectionStrings["OnaySystem"].ConnectionString;
            _belgeId = belgeId;
            _suankiDurum = suankiDurum;
        }

        private void FrmBelgeDurumuDegistir_Load(object sender, EventArgs e)
        {
            lblMevcutDurum.Text = "Mevcut Durum: " + _suankiDurum;

            cmbYeniDurum.Items.Clear();
            cmbYeniDurum.Items.Add("Beklemede");
            cmbYeniDurum.Items.Add("1. Onaylandý, 2. Beklemede");
            cmbYeniDurum.Items.Add("Onaylandý");
            cmbYeniDurum.Items.Add("Tam Onaylandý");
            cmbYeniDurum.Items.Add("Red");
            cmbYeniDurum.Items.Add("Revize Gerekli");
            cmbYeniDurum.SelectedIndex = 0;
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (cmbYeniDurum.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen yeni bir durum seçin.");
                return;
            }

            string yeniDurum = cmbYeniDurum.SelectedItem.ToString();
            string aciklama = txtAciklama.Text.Trim();

            if (yeniDurum == _suankiDurum)
            {
                MessageBox.Show("Belgenin durumu zaten bu durumdadýr.");
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand(@"
                    UPDATE BelgeGonderim 
                    SET OnayDurumu = @YeniDurum, 
                        RevizeAciklama = CASE 
                            WHEN @YeniDurum = 'Revize Gerekli' THEN @Aciklama
                            ELSE RevizeAciklama 
                        END,
                        OnayTarihi = CASE
                            WHEN @YeniDurum IN ('Onaylandý', 'Tam Onaylandý', 'Red') THEN SYSDATETIME()
                            ELSE OnayTarihi
                        END,
                        Onaylayan = CASE
                            WHEN @YeniDurum IN ('Onaylandý', 'Tam Onaylandý', 'Red') THEN @Onaylayan
                            ELSE Onaylayan
                        END
                    WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", _belgeId);
                    komut.Parameters.AddWithValue("@YeniDurum", yeniDurum);
                    komut.Parameters.AddWithValue("@Aciklama", aciklama ?? "");
                    komut.Parameters.AddWithValue("@Onaylayan", OturumBilgisi.AdSoyad ?? OturumBilgisi.KullaniciAdi);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }

                string konu = "Belirtilmemiþ";
                using (SqlConnection baglanti = new SqlConnection(_baglantiMetni))
                using (SqlCommand komut = new SqlCommand("SELECT Konu FROM BelgeGonderim WHERE Id = @Id", baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", _belgeId);
                    baglanti.Open();
                    object result = komut.ExecuteScalar();
                    if (result != null)
                        konu = result.ToString();
                }

                AuditLogHelper.BelgeDurumuDegisti(_belgeId, _suankiDurum, yeniDurum, konu);

                MessageBox.Show($"Belge durumu '{yeniDurum}' olarak güncellendi.", "Baþarýlý");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durum deðiþtirilirken hata: " + ex.Message, "Hata");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbYeniDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seciliDurum = cmbYeniDurum.SelectedItem?.ToString() ?? "";
            
            if (seciliDurum == "Revize Gerekli")
            {
                lblAciklamaUyari.Text = "*Revize açýklamasý zorunludur";
                lblAciklamaUyari.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblAciklamaUyari.Text = "(Opsiyonel)";
                lblAciklamaUyari.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
