namespace OnaySistemi
{
    partial class FrmKullaniciDuzenle
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
            this.lblBaslik = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdSoyad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.lblDirektorluk = new System.Windows.Forms.Label();
            this.cmbDirektorluk = new System.Windows.Forms.ComboBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSifreDogrula = new System.Windows.Forms.TextBox();
            this.chkAktif = new System.Windows.Forms.CheckBox();
            this.chkMailBildirim = new System.Windows.Forms.CheckBox();
            this.chkSifreDegistir = new System.Windows.Forms.CheckBox();
            this.lblSifreDegistir = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(150, 21);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Yeni Kullanýcý";

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kullanýcý Adý:";

            this.txtKullaniciAdi.Location = new System.Drawing.Point(20, 78);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(300, 23);
            this.txtKullaniciAdi.TabIndex = 2;

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ad Soyad:";

            this.txtAdSoyad.Location = new System.Drawing.Point(20, 126);
            this.txtAdSoyad.Name = "txtAdSoyad";
            this.txtAdSoyad.Size = new System.Drawing.Size(300, 23);
            this.txtAdSoyad.TabIndex = 4;

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Rol:";

            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.Location = new System.Drawing.Point(20, 174);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(300, 23);
            this.cmbRol.TabIndex = 6;

            this.lblDirektorluk.AutoSize = true;
            this.lblDirektorluk.Location = new System.Drawing.Point(20, 204);
            this.lblDirektorluk.Name = "lblDirektorluk";
            this.lblDirektorluk.Size = new System.Drawing.Size(73, 15);
            this.lblDirektorluk.TabIndex = 7;
            this.lblDirektorluk.Text = "Direktörlük:";

            this.cmbDirektorluk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirektorluk.Location = new System.Drawing.Point(20, 222);
            this.cmbDirektorluk.Name = "cmbDirektorluk";
            this.cmbDirektorluk.Size = new System.Drawing.Size(300, 23);
            this.cmbDirektorluk.TabIndex = 8;

            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(20, 252);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(82, 15);
            this.labelEmail.TabIndex = 9;
            this.labelEmail.Text = "Email Adresi:";

            this.txtEmail.Location = new System.Drawing.Point(20, 270);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 23);
            this.txtEmail.TabIndex = 10;

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Þifre:";

            this.txtSifre.Location = new System.Drawing.Point(20, 318);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.PasswordChar = '?';
            this.txtSifre.Size = new System.Drawing.Size(300, 23);
            this.txtSifre.TabIndex = 12;

            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 348);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Þifre (Doðrulama):";

            this.txtSifreDogrula.Location = new System.Drawing.Point(20, 366);
            this.txtSifreDogrula.Name = "txtSifreDogrula";
            this.txtSifreDogrula.PasswordChar = '?';
            this.txtSifreDogrula.Size = new System.Drawing.Size(300, 23);
            this.txtSifreDogrula.TabIndex = 14;

            this.chkAktif.AutoSize = true;
            this.chkAktif.Checked = true;
            this.chkAktif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAktif.Location = new System.Drawing.Point(20, 400);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Size = new System.Drawing.Size(102, 19);
            this.chkAktif.TabIndex = 15;
            this.chkAktif.Text = "Aktif Kullanýcý";
            this.chkAktif.UseVisualStyleBackColor = true;

            this.chkMailBildirim.AutoSize = true;
            this.chkMailBildirim.Checked = true;
            this.chkMailBildirim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMailBildirim.Location = new System.Drawing.Point(20, 425);
            this.chkMailBildirim.Name = "chkMailBildirim";
            this.chkMailBildirim.Size = new System.Drawing.Size(142, 19);
            this.chkMailBildirim.TabIndex = 16;
            this.chkMailBildirim.Text = "Mail Bildirimleri Aktif";
            this.chkMailBildirim.UseVisualStyleBackColor = true;

            this.chkSifreDegistir.AutoSize = true;
            this.chkSifreDegistir.Location = new System.Drawing.Point(20, 300);
            this.chkSifreDegistir.Name = "chkSifreDegistir";
            this.chkSifreDegistir.Size = new System.Drawing.Size(107, 19);
            this.chkSifreDegistir.TabIndex = 17;
            this.chkSifreDegistir.Text = "Þifre Deðiþtir";
            this.chkSifreDegistir.UseVisualStyleBackColor = true;
            this.chkSifreDegistir.Visible = false;
            this.chkSifreDegistir.CheckedChanged += new System.EventHandler(this.chkSifreDegistir_CheckedChanged);

            this.lblSifreDegistir.AutoSize = true;
            this.lblSifreDegistir.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblSifreDegistir.ForeColor = System.Drawing.Color.Gray;
            this.lblSifreDegistir.Location = new System.Drawing.Point(130, 302);
            this.lblSifreDegistir.Name = "lblSifreDegistir";
            this.lblSifreDegistir.Size = new System.Drawing.Size(184, 13);
            this.lblSifreDegistir.TabIndex = 18;
            this.lblSifreDegistir.Text = "(Þifreyi deðiþtirmek için iþaretleyin)";
            this.lblSifreDegistir.Visible = false;

            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(0, 120, 200);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(20, 460);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(120, 35);
            this.btnKaydet.TabIndex = 19;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);

            this.btnIptal.Location = new System.Drawing.Point(150, 460);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(120, 35);
            this.btnIptal.TabIndex = 20;
            this.btnIptal.Text = "Ýptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 515);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.lblSifreDegistir);
            this.Controls.Add(this.chkSifreDegistir);
            this.Controls.Add(this.chkMailBildirim);
            this.Controls.Add(this.chkAktif);
            this.Controls.Add(this.txtSifreDogrula);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.cmbDirektorluk);
            this.Controls.Add(this.lblDirektorluk);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAdSoyad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKullaniciAdi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBaslik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmKullaniciDuzenle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kullanýcý";
            this.Load += new System.EventHandler(this.FrmKullaniciDuzenle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdSoyad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label lblDirektorluk;
        private System.Windows.Forms.ComboBox cmbDirektorluk;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSifreDogrula;
        private System.Windows.Forms.CheckBox chkAktif;
        private System.Windows.Forms.CheckBox chkMailBildirim;
        private System.Windows.Forms.CheckBox chkSifreDegistir;
        private System.Windows.Forms.Label lblSifreDegistir;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
    }
}
