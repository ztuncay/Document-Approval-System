namespace OnaySistemi
{
    partial class FrmBelgeDurumuDegistir
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMevcutDurum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbYeniDurum = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.lblAciklamaUyari = new System.Windows.Forms.Label();
            this.btnDegistir = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblMevcutDurum.AutoSize = true;
            this.lblMevcutDurum.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMevcutDurum.Location = new System.Drawing.Point(20, 20);
            this.lblMevcutDurum.Name = "lblMevcutDurum";
            this.lblMevcutDurum.Size = new System.Drawing.Size(150, 23);
            this.lblMevcutDurum.TabIndex = 0;
            this.lblMevcutDurum.Text = "Mevcut Durum: ";

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Yeni Durum:";

            this.cmbYeniDurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYeniDurum.Location = new System.Drawing.Point(20, 75);
            this.cmbYeniDurum.Name = "cmbYeniDurum";
            this.cmbYeniDurum.Size = new System.Drawing.Size(300, 21);
            this.cmbYeniDurum.TabIndex = 2;
            this.cmbYeniDurum.SelectedIndexChanged += new System.EventHandler(this.cmbYeniDurum_SelectedIndexChanged);

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Açýklama:";

            this.txtAciklama.Location = new System.Drawing.Point(20, 125);
            this.txtAciklama.Multiline = true;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(300, 80);
            this.txtAciklama.TabIndex = 4;

            this.lblAciklamaUyari.AutoSize = true;
            this.lblAciklamaUyari.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblAciklamaUyari.ForeColor = System.Drawing.Color.Gray;
            this.lblAciklamaUyari.Location = new System.Drawing.Point(80, 110);
            this.lblAciklamaUyari.Name = "lblAciklamaUyari";
            this.lblAciklamaUyari.Size = new System.Drawing.Size(70, 13);
            this.lblAciklamaUyari.TabIndex = 5;
            this.lblAciklamaUyari.Text = "(Opsiyonel)";

            this.btnDegistir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnDegistir.ForeColor = System.Drawing.Color.White;
            this.btnDegistir.Location = new System.Drawing.Point(20, 220);
            this.btnDegistir.Name = "btnDegistir";
            this.btnDegistir.Size = new System.Drawing.Size(120, 35);
            this.btnDegistir.TabIndex = 6;
            this.btnDegistir.Text = "Deðiþtir";
            this.btnDegistir.UseVisualStyleBackColor = false;
            this.btnDegistir.Click += new System.EventHandler(this.btnDegistir_Click);

            this.btnIptal.Location = new System.Drawing.Point(150, 220);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(120, 35);
            this.btnIptal.TabIndex = 7;
            this.btnIptal.Text = "Ýptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 270);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnDegistir);
            this.Controls.Add(this.lblAciklamaUyari);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbYeniDurum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMevcutDurum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBelgeDurumuDegistir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Belge Durumunu Deðiþtir";
            this.Load += new System.EventHandler(this.FrmBelgeDurumuDegistir_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblMevcutDurum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbYeniDurum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Label lblAciklamaUyari;
        private System.Windows.Forms.Button btnDegistir;
        private System.Windows.Forms.Button btnIptal;
    }
}
