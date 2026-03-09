namespace OnaySistemi
{
    partial class FrmAdminPanel
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
            this.lblAdminBaslik = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabKullanicilar = new System.Windows.Forms.TabPage();
            this.dgvKullanicilar = new System.Windows.Forms.DataGridView();
            this.pnlKullaniciButonlar = new System.Windows.Forms.Panel();
            this.btnKullaniciEkle = new System.Windows.Forms.Button();
            this.btnKullaniciDuzenle = new System.Windows.Forms.Button();
            this.btnKullaniciSil = new System.Windows.Forms.Button();
            this.tabBelgeler = new System.Windows.Forms.TabPage();
            this.dgvBelgeler = new System.Windows.Forms.DataGridView();
            this.pnlBelgeButonlar = new System.Windows.Forms.Panel();
            this.btnBelgeDurumuDegistir = new System.Windows.Forms.Button();
            this.btnBelgeSil = new System.Windows.Forms.Button();
            this.btnIslemGecmisi = new System.Windows.Forms.Button();
            this.btnKapat = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabKullanicilar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKullanicilar)).BeginInit();
            this.pnlKullaniciButonlar.SuspendLayout();
            this.tabBelgeler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBelgeler)).BeginInit();
            this.pnlBelgeButonlar.SuspendLayout();
            this.SuspendLayout();

            this.lblAdminBaslik.AutoSize = true;
            this.lblAdminBaslik.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAdminBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblAdminBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblAdminBaslik.Name = "lblAdminBaslik";
            this.lblAdminBaslik.Size = new System.Drawing.Size(300, 32);
            this.lblAdminBaslik.TabIndex = 0;
            this.lblAdminBaslik.Text = "Admin Paneli";

            this.tabControl.Controls.Add(this.tabKullanicilar);
            this.tabControl.Controls.Add(this.tabBelgeler);
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(960, 500);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);

            this.tabKullanicilar.Controls.Add(this.dgvKullanicilar);
            this.tabKullanicilar.Controls.Add(this.pnlKullaniciButonlar);
            this.tabKullanicilar.Location = new System.Drawing.Point(4, 24);
            this.tabKullanicilar.Name = "tabKullanicilar";
            this.tabKullanicilar.Padding = new System.Windows.Forms.Padding(10);
            this.tabKullanicilar.Size = new System.Drawing.Size(952, 472);
            this.tabKullanicilar.TabIndex = 0;
            this.tabKullanicilar.Text = "Kullanýcýlar";
            this.tabKullanicilar.UseVisualStyleBackColor = true;

            this.dgvKullanicilar.AllowUserToAddRows = false;
            this.dgvKullanicilar.AllowUserToDeleteRows = false;
            this.dgvKullanicilar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKullanicilar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvKullanicilar.BackgroundColor = System.Drawing.Color.White;
            this.dgvKullanicilar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKullanicilar.Location = new System.Drawing.Point(10, 10);
            this.dgvKullanicilar.Name = "dgvKullanicilar";
            this.dgvKullanicilar.ReadOnly = true;
            this.dgvKullanicilar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKullanicilar.Size = new System.Drawing.Size(932, 380);
            this.dgvKullanicilar.TabIndex = 0;

            this.pnlKullaniciButonlar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKullaniciButonlar.Controls.Add(this.btnKullaniciEkle);
            this.pnlKullaniciButonlar.Controls.Add(this.btnKullaniciDuzenle);
            this.pnlKullaniciButonlar.Controls.Add(this.btnKullaniciSil);
            this.pnlKullaniciButonlar.Location = new System.Drawing.Point(10, 395);
            this.pnlKullaniciButonlar.Name = "pnlKullaniciButonlar";
            this.pnlKullaniciButonlar.Size = new System.Drawing.Size(932, 50);
            this.pnlKullaniciButonlar.TabIndex = 1;

            this.btnKullaniciEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnKullaniciEkle.ForeColor = System.Drawing.Color.White;
            this.btnKullaniciEkle.Location = new System.Drawing.Point(10, 10);
            this.btnKullaniciEkle.Name = "btnKullaniciEkle";
            this.btnKullaniciEkle.Size = new System.Drawing.Size(120, 35);
            this.btnKullaniciEkle.TabIndex = 0;
            this.btnKullaniciEkle.Text = "+ Yeni Kullanýcý";
            this.btnKullaniciEkle.UseVisualStyleBackColor = false;
            this.btnKullaniciEkle.Click += new System.EventHandler(this.btnKullaniciEkle_Click);

            this.btnKullaniciDuzenle.Location = new System.Drawing.Point(140, 10);
            this.btnKullaniciDuzenle.Name = "btnKullaniciDuzenle";
            this.btnKullaniciDuzenle.Size = new System.Drawing.Size(120, 35);
            this.btnKullaniciDuzenle.TabIndex = 1;
            this.btnKullaniciDuzenle.Text = "Düzenle";
            this.btnKullaniciDuzenle.UseVisualStyleBackColor = true;
            this.btnKullaniciDuzenle.Click += new System.EventHandler(this.btnKullaniciDuzenle_Click);

            this.btnKullaniciSil.ForeColor = System.Drawing.Color.White;
            this.btnKullaniciSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnKullaniciSil.Location = new System.Drawing.Point(270, 10);
            this.btnKullaniciSil.Name = "btnKullaniciSil";
            this.btnKullaniciSil.Size = new System.Drawing.Size(120, 35);
            this.btnKullaniciSil.TabIndex = 2;
            this.btnKullaniciSil.Text = "Sil";
            this.btnKullaniciSil.UseVisualStyleBackColor = false;
            this.btnKullaniciSil.Click += new System.EventHandler(this.btnKullaniciSil_Click);

            this.tabBelgeler.Controls.Add(this.dgvBelgeler);
            this.tabBelgeler.Controls.Add(this.pnlBelgeButonlar);
            this.tabBelgeler.Location = new System.Drawing.Point(4, 24);
            this.tabBelgeler.Name = "tabBelgeler";
            this.tabBelgeler.Padding = new System.Windows.Forms.Padding(10);
            this.tabBelgeler.Size = new System.Drawing.Size(952, 472);
            this.tabBelgeler.TabIndex = 1;
            this.tabBelgeler.Text = "Belgeler";
            this.tabBelgeler.UseVisualStyleBackColor = true;

            this.dgvBelgeler.AllowUserToAddRows = false;
            this.dgvBelgeler.AllowUserToDeleteRows = false;
            this.dgvBelgeler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBelgeler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBelgeler.BackgroundColor = System.Drawing.Color.White;
            this.dgvBelgeler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBelgeler.Location = new System.Drawing.Point(10, 10);
            this.dgvBelgeler.Name = "dgvBelgeler";
            this.dgvBelgeler.ReadOnly = true;
            this.dgvBelgeler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBelgeler.Size = new System.Drawing.Size(932, 380);
            this.dgvBelgeler.TabIndex = 0;

            this.pnlBelgeButonlar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBelgeButonlar.Controls.Add(this.btnBelgeDurumuDegistir);
            this.pnlBelgeButonlar.Controls.Add(this.btnBelgeSil);
            this.pnlBelgeButonlar.Location = new System.Drawing.Point(10, 395);
            this.pnlBelgeButonlar.Name = "pnlBelgeButonlar";
            this.pnlBelgeButonlar.Size = new System.Drawing.Size(932, 50);
            this.pnlBelgeButonlar.TabIndex = 1;

            this.btnBelgeDurumuDegistir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnBelgeDurumuDegistir.ForeColor = System.Drawing.Color.White;
            this.btnBelgeDurumuDegistir.Location = new System.Drawing.Point(10, 10);
            this.btnBelgeDurumuDegistir.Name = "btnBelgeDurumuDegistir";
            this.btnBelgeDurumuDegistir.Size = new System.Drawing.Size(150, 35);
            this.btnBelgeDurumuDegistir.TabIndex = 0;
            this.btnBelgeDurumuDegistir.Text = "Durumunu Deðiþtir";
            this.btnBelgeDurumuDegistir.UseVisualStyleBackColor = false;
            this.btnBelgeDurumuDegistir.Click += new System.EventHandler(this.btnBelgeDurumuDegistir_Click);

            this.btnBelgeSil.ForeColor = System.Drawing.Color.White;
            this.btnBelgeSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnBelgeSil.Location = new System.Drawing.Point(170, 10);
            this.btnBelgeSil.Name = "btnBelgeSil";
            this.btnBelgeSil.Size = new System.Drawing.Size(120, 35);
            this.btnBelgeSil.TabIndex = 1;
            this.btnBelgeSil.Text = "Sil";
            this.btnBelgeSil.UseVisualStyleBackColor = false;
            this.btnBelgeSil.Click += new System.EventHandler(this.btnBelgeSil_Click);

            this.btnKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKapat.Location = new System.Drawing.Point(900, 580);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(80, 35);
            this.btnKapat.TabIndex = 2;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.UseVisualStyleBackColor = true;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            this.btnIslemGecmisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIslemGecmisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(50)))));
            this.btnIslemGecmisi.ForeColor = System.Drawing.Color.White;
            this.btnIslemGecmisi.Location = new System.Drawing.Point(770, 580);
            this.btnIslemGecmisi.Name = "btnIslemGecmisi";
            this.btnIslemGecmisi.Size = new System.Drawing.Size(120, 35);
            this.btnIslemGecmisi.TabIndex = 3;
            this.btnIslemGecmisi.Text = "Ýþlem Geçmiþi";
            this.btnIslemGecmisi.UseVisualStyleBackColor = false;
            this.btnIslemGecmisi.Click += new System.EventHandler(this.btnIslemGecmisi_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 630);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnIslemGecmisi);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblAdminBaslik);
            this.MinimumSize = new System.Drawing.Size(1000, 630);
            this.Name = "FrmAdminPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDOS - Admin Paneli";
            this.Load += new System.EventHandler(this.FrmAdminPanel_Load);
            this.tabControl.ResumeLayout(false);
            this.tabKullanicilar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKullanicilar)).EndInit();
            this.pnlKullaniciButonlar.ResumeLayout(false);
            this.tabBelgeler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBelgeler)).EndInit();
            this.pnlBelgeButonlar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblAdminBaslik;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabKullanicilar;
        private System.Windows.Forms.DataGridView dgvKullanicilar;
        private System.Windows.Forms.Panel pnlKullaniciButonlar;
        private System.Windows.Forms.Button btnKullaniciEkle;
        private System.Windows.Forms.Button btnKullaniciDuzenle;
        private System.Windows.Forms.Button btnKullaniciSil;
        private System.Windows.Forms.TabPage tabBelgeler;
        private System.Windows.Forms.DataGridView dgvBelgeler;
        private System.Windows.Forms.Panel pnlBelgeButonlar;
        private System.Windows.Forms.Button btnBelgeDurumuDegistir;
        private System.Windows.Forms.Button btnBelgeSil;
        private System.Windows.Forms.Button btnIslemGecmisi;
        private System.Windows.Forms.Button btnKapat;
    }
}
