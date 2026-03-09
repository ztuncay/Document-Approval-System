namespace OnaySistemi
{
    partial class FrmBelgeListesi
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlKart = new System.Windows.Forms.Panel();
            this.dgvBelgeler = new System.Windows.Forms.DataGridView();
            this.pnlUst = new System.Windows.Forms.Panel();
            this.btnFiltreTemizle = new System.Windows.Forms.Button();
            this.btnFiltreUygula = new System.Windows.Forms.Button();
            this.lblDurum = new System.Windows.Forms.Label();
            this.cmbDurum = new System.Windows.Forms.ComboBox();
            this.lblTarihAraligi = new System.Windows.Forms.Label();
            this.dtpBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dtpBitis = new System.Windows.Forms.DateTimePicker();
            this.labelTire = new System.Windows.Forms.Label();
            this.btnVersiyonGecmisi = new System.Windows.Forms.Button();
            this.btnBelgeAc = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlKart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBelgeler)).BeginInit();
            this.pnlUst.SuspendLayout();
            this.SuspendLayout();
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlHeader.Controls.Add(this.lblBaslik);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlHeader.TabIndex = 0;
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(20, 16);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(135, 32);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Belge Listesi";
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.Controls.Add(this.pnlKart);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(1000, 540);
            this.pnlContent.TabIndex = 1;
            this.pnlKart.BackColor = System.Drawing.Color.White;
            this.pnlKart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKart.Controls.Add(this.dgvBelgeler);
            this.pnlKart.Controls.Add(this.pnlUst);
            this.pnlKart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKart.Location = new System.Drawing.Point(20, 20);
            this.pnlKart.Name = "pnlKart";
            this.pnlKart.Padding = new System.Windows.Forms.Padding(10);
            this.pnlKart.Size = new System.Drawing.Size(960, 500);
            this.pnlKart.TabIndex = 0;
            this.dgvBelgeler.AllowUserToAddRows = false;
            this.dgvBelgeler.AllowUserToDeleteRows = false;
            this.dgvBelgeler.AllowUserToOrderColumns = true;
            this.dgvBelgeler.BackgroundColor = System.Drawing.Color.White;
            this.dgvBelgeler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBelgeler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBelgeler.Location = new System.Drawing.Point(10, 66);
            this.dgvBelgeler.MultiSelect = false;
            this.dgvBelgeler.Name = "dgvBelgeler";
            this.dgvBelgeler.ReadOnly = true;
            this.dgvBelgeler.RowHeadersVisible = false;
            this.dgvBelgeler.RowTemplate.Height = 24;
            this.dgvBelgeler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBelgeler.Size = new System.Drawing.Size(938, 424);
            this.dgvBelgeler.TabIndex = 1;
            this.dgvBelgeler.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBelgeler_CellDoubleClick);
            this.pnlUst.Controls.Add(this.btnFiltreTemizle);
            this.pnlUst.Controls.Add(this.btnFiltreUygula);
            this.pnlUst.Controls.Add(this.lblDurum);
            this.pnlUst.Controls.Add(this.cmbDurum);
            this.pnlUst.Controls.Add(this.lblTarihAraligi);
            this.pnlUst.Controls.Add(this.dtpBaslangic);
            this.pnlUst.Controls.Add(this.dtpBitis);
            this.pnlUst.Controls.Add(this.labelTire);
            this.pnlUst.Controls.Add(this.btnVersiyonGecmisi);
            this.pnlUst.Controls.Add(this.btnBelgeAc);
            this.pnlUst.Controls.Add(this.btnYenile);
            this.pnlUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUst.Location = new System.Drawing.Point(10, 10);
            this.pnlUst.Name = "pnlUst";
            this.pnlUst.Size = new System.Drawing.Size(938, 56);
            this.pnlUst.TabIndex = 0;
            this.btnFiltreTemizle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFiltreTemizle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFiltreTemizle.Location = new System.Drawing.Point(810, 13);
            this.btnFiltreTemizle.Name = "btnFiltreTemizle";
            this.btnFiltreTemizle.Size = new System.Drawing.Size(110, 28);
            this.btnFiltreTemizle.TabIndex = 10;
            this.btnFiltreTemizle.Text = "Filtre Temizle";
            this.btnFiltreTemizle.UseVisualStyleBackColor = true;
            this.btnFiltreTemizle.Click += new System.EventHandler(this.btnFiltreTemizle_Click);
            this.btnFiltreUygula.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFiltreUygula.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFiltreUygula.Location = new System.Drawing.Point(700, 13);
            this.btnFiltreUygula.Name = "btnFiltreUygula";
            this.btnFiltreUygula.Size = new System.Drawing.Size(104, 28);
            this.btnFiltreUygula.TabIndex = 9;
            this.btnFiltreUygula.Text = "Filtre Uygula";
            this.btnFiltreUygula.UseVisualStyleBackColor = true;
            this.btnFiltreUygula.Click += new System.EventHandler(this.btnFiltreUygula_Click);
            this.lblDurum.AutoSize = true;
            this.lblDurum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDurum.Location = new System.Drawing.Point(370, 18);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(51, 20);
            this.lblDurum.TabIndex = 8;
            this.lblDurum.Text = "Durum";
            this.cmbDurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDurum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDurum.FormattingEnabled = true;
            this.cmbDurum.Location = new System.Drawing.Point(427, 14);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Size = new System.Drawing.Size(160, 28);
            this.cmbDurum.TabIndex = 7;
            this.lblTarihAraligi.AutoSize = true;
            this.lblTarihAraligi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTarihAraligi.Location = new System.Drawing.Point(170, 18);
            this.lblTarihAraligi.Name = "lblTarihAraligi";
            this.lblTarihAraligi.Size = new System.Drawing.Size(40, 20);
            this.lblTarihAraligi.TabIndex = 6;
            this.lblTarihAraligi.Text = "Tarih";
            this.dtpBaslangic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBaslangic.Location = new System.Drawing.Point(216, 14);
            this.dtpBaslangic.Name = "dtpBaslangic";
            this.dtpBaslangic.Size = new System.Drawing.Size(100, 27);
            this.dtpBaslangic.TabIndex = 5;
            this.dtpBitis.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBitis.Location = new System.Drawing.Point(322, 14);
            this.dtpBitis.Name = "dtpBitis";
            this.dtpBitis.Size = new System.Drawing.Size(100, 27);
            this.dtpBitis.TabIndex = 4;
            this.labelTire.AutoSize = true;
            this.labelTire.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelTire.Location = new System.Drawing.Point(313, 18);
            this.labelTire.Name = "labelTire";
            this.labelTire.Size = new System.Drawing.Size(13, 20);
            this.labelTire.TabIndex = 3;
            this.labelTire.Text = "-";
            this.btnVersiyonGecmisi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnVersiyonGecmisi.Location = new System.Drawing.Point(92, 13);
            this.btnVersiyonGecmisi.Name = "btnVersiyonGecmisi";
            this.btnVersiyonGecmisi.Size = new System.Drawing.Size(72, 28);
            this.btnVersiyonGecmisi.TabIndex = 2;
            this.btnVersiyonGecmisi.Text = "Vers.";
            this.btnVersiyonGecmisi.UseVisualStyleBackColor = true;
            this.btnVersiyonGecmisi.Click += new System.EventHandler(this.btnVersiyonGecmisi_Click);
            this.btnBelgeAc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBelgeAc.Location = new System.Drawing.Point(10, 13);
            this.btnBelgeAc.Name = "btnBelgeAc";
            this.btnBelgeAc.Size = new System.Drawing.Size(76, 28);
            this.btnBelgeAc.TabIndex = 1;
            this.btnBelgeAc.Text = "Belge Aç";
            this.btnBelgeAc.UseVisualStyleBackColor = true;
            this.btnBelgeAc.Click += new System.EventHandler(this.btnBelgeAc_Click);
            this.btnYenile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.Location = new System.Drawing.Point(620, 13);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(74, 28);
            this.btnYenile.TabIndex = 0;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.UseVisualStyleBackColor = true;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FrmBelgeListesi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Belge Listesi";
            this.Load += new System.EventHandler(this.FrmBelgeListesi_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlKart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBelgeler)).EndInit();
            this.pnlUst.ResumeLayout(false);
            this.pnlUst.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlKart;
        private System.Windows.Forms.DataGridView dgvBelgeler;
        private System.Windows.Forms.Panel pnlUst;
        private System.Windows.Forms.Button btnFiltreTemizle;
        private System.Windows.Forms.Button btnFiltreUygula;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.ComboBox cmbDurum;
        private System.Windows.Forms.Label lblTarihAraligi;
        private System.Windows.Forms.DateTimePicker dtpBaslangic;
        private System.Windows.Forms.DateTimePicker dtpBitis;
        private System.Windows.Forms.Label labelTire;
        private System.Windows.Forms.Button btnVersiyonGecmisi;
        private System.Windows.Forms.Button btnBelgeAc;
        private System.Windows.Forms.Button btnYenile;
    }
}
