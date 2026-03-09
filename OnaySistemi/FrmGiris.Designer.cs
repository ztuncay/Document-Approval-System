namespace OnaySistemi
{
    partial class FrmGiris
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

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlKart = new System.Windows.Forms.Panel();
            this.lblHata = new System.Windows.Forms.Label();
            this.btnGiris = new System.Windows.Forms.Button();
            this.chkSifreGoster = new System.Windows.Forms.CheckBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.lblSifre = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.lblAltMetin = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlKart.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlHeader.Controls.Add(this.lblBaslik);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(440, 52);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblBaslik
            // 
            this.lblBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblBaslik.Size = new System.Drawing.Size(220, 52);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "DDOS Giriþ";
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.SystemColors.Control;
            this.pnlContent.Controls.Add(this.pnlKart);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 52);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12, 10, 12, 12);
            this.pnlContent.Size = new System.Drawing.Size(440, 318);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlKart
            // 
            this.pnlKart.BackColor = System.Drawing.Color.White;
            this.pnlKart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKart.Controls.Add(this.lblHata);
            this.pnlKart.Controls.Add(this.btnGiris);
            this.pnlKart.Controls.Add(this.chkSifreGoster);
            this.pnlKart.Controls.Add(this.txtSifre);
            this.pnlKart.Controls.Add(this.lblSifre);
            this.pnlKart.Controls.Add(this.txtKullaniciAdi);
            this.pnlKart.Controls.Add(this.lblKullaniciAdi);
            this.pnlKart.Controls.Add(this.lblAltMetin);
            this.pnlKart.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlKart.Location = new System.Drawing.Point(12, 10);
            this.pnlKart.Name = "pnlKart";
            this.pnlKart.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.pnlKart.Size = new System.Drawing.Size(416, 240);
            this.pnlKart.TabIndex = 0;
            // 
            // lblHata
            // 
            this.lblHata.ForeColor = System.Drawing.Color.Firebrick;
            this.lblHata.Location = new System.Drawing.Point(16, 190);
            this.lblHata.Name = "lblHata";
            this.lblHata.Size = new System.Drawing.Size(380, 42);
            this.lblHata.TabIndex = 7;
            this.lblHata.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGiris
            // 
            this.btnGiris.BackColor = System.Drawing.SystemColors.Control;
            this.btnGiris.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnGiris.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.btnGiris.ForeColor = System.Drawing.Color.Black;
            this.btnGiris.Location = new System.Drawing.Point(304, 154);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(94, 28);
            this.btnGiris.TabIndex = 6;
            this.btnGiris.Text = "Giriþ Yap";
            this.btnGiris.UseVisualStyleBackColor = false;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // chkSifreGoster
            // 
            this.chkSifreGoster.AutoSize = true;
            this.chkSifreGoster.Location = new System.Drawing.Point(340, 126);
            this.chkSifreGoster.Name = "chkSifreGoster";
            this.chkSifreGoster.Size = new System.Drawing.Size(55, 19);
            this.chkSifreGoster.TabIndex = 5;
            this.chkSifreGoster.Text = "Göster";
            this.chkSifreGoster.UseVisualStyleBackColor = true;
            this.chkSifreGoster.CheckedChanged += new System.EventHandler(this.chkSifreGoster_CheckedChanged);
            // 
            // txtSifre
            // 
            this.txtSifre.BackColor = System.Drawing.Color.White;
            this.txtSifre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSifre.Location = new System.Drawing.Point(132, 123);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.PasswordChar = '*';
            this.txtSifre.Size = new System.Drawing.Size(200, 23);
            this.txtSifre.TabIndex = 4;
            this.txtSifre.TextChanged += new System.EventHandler(this.txtSifre_TextChanged);
            // 
            // lblSifre
            // 
            this.lblSifre.AutoSize = true;
            this.lblSifre.Location = new System.Drawing.Point(16, 126);
            this.lblSifre.Name = "lblSifre";
            this.lblSifre.Size = new System.Drawing.Size(33, 15);
            this.lblSifre.TabIndex = 3;
            this.lblSifre.Text = "Þifre";
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.BackColor = System.Drawing.Color.White;
            this.txtKullaniciAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKullaniciAdi.Location = new System.Drawing.Point(132, 80);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(200, 23);
            this.txtKullaniciAdi.TabIndex = 2;
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Location = new System.Drawing.Point(16, 83);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(71, 15);
            this.lblKullaniciAdi.TabIndex = 1;
            this.lblKullaniciAdi.Text = "Kullanýcý Adý";
            // 
            // lblAltMetin
            // 
            this.lblAltMetin.AutoSize = true;
            this.lblAltMetin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblAltMetin.Location = new System.Drawing.Point(16, 20);
            this.lblAltMetin.Name = "lblAltMetin";
            this.lblAltMetin.Size = new System.Drawing.Size(242, 15);
            this.lblAltMetin.TabIndex = 0;
            this.lblAltMetin.Text = "Lütfen kurum kullanýcý bilgilerinizle giriþ yapýn.";
            // 
            // FrmGiris
            // 
            this.AcceptButton = this.btnGiris;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(440, 360);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDOS - Giriþ";
            this.Load += new System.EventHandler(this.FrmGiris_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlKart.ResumeLayout(false);
            this.pnlKart.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlKart;
        private System.Windows.Forms.Label lblAltMetin;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label lblSifre;
        private System.Windows.Forms.CheckBox chkSifreGoster;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.Label lblHata;
    }
}
