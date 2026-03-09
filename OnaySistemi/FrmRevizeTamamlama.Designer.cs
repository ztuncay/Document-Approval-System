namespace OnaySistemi
{
    partial class FrmRevizeTamamlama
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
            this.btnRevizeyiTamamla = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.btnRevizeEdilecekBelge = new System.Windows.Forms.Button();
            this.btnYeniDosyaSec = new System.Windows.Forms.Button();
            this.txtYeniDosyaYolu = new System.Windows.Forms.TextBox();
            this.lblYeniDosya = new System.Windows.Forms.Label();
            this.lblListeAciklama = new System.Windows.Forms.Label();
            this.dgvRevizeIstekleri = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlKart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevizeIstekleri)).BeginInit();
            this.SuspendLayout();
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlHeader.Controls.Add(this.lblBaslik);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 60);
            this.pnlHeader.TabIndex = 0;
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(20, 16);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(191, 32);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Revize Tamamlama";
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.Controls.Add(this.pnlKart);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(900, 490);
            this.pnlContent.TabIndex = 1;
            this.pnlKart.BackColor = System.Drawing.Color.White;
            this.pnlKart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKart.Controls.Add(this.btnRevizeyiTamamla);
            this.pnlKart.Controls.Add(this.btnYenile);
            this.pnlKart.Controls.Add(this.btnRevizeEdilecekBelge);
            this.pnlKart.Controls.Add(this.btnYeniDosyaSec);
            this.pnlKart.Controls.Add(this.txtYeniDosyaYolu);
            this.pnlKart.Controls.Add(this.lblYeniDosya);
            this.pnlKart.Controls.Add(this.lblListeAciklama);
            this.pnlKart.Controls.Add(this.dgvRevizeIstekleri);
            this.pnlKart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKart.Location = new System.Drawing.Point(20, 20);
            this.pnlKart.Name = "pnlKart";
            this.pnlKart.Padding = new System.Windows.Forms.Padding(15);
            this.pnlKart.Size = new System.Drawing.Size(860, 450);
            this.pnlKart.TabIndex = 0;
            this.btnRevizeyiTamamla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRevizeyiTamamla.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRevizeyiTamamla.Location = new System.Drawing.Point(671, 395);
            this.btnRevizeyiTamamla.Name = "btnRevizeyiTamamla";
            this.btnRevizeyiTamamla.Size = new System.Drawing.Size(170, 30);
            this.btnRevizeyiTamamla.TabIndex = 6;
            this.btnRevizeyiTamamla.Text = "Revizeyi Tamamla";
            this.btnRevizeyiTamamla.UseVisualStyleBackColor = true;
            this.btnRevizeyiTamamla.Click += new System.EventHandler(this.btnRevizeyiTamamla_Click);
            this.btnYenile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.Location = new System.Drawing.Point(553, 395);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(110, 30);
            this.btnYenile.TabIndex = 5;
            this.btnYenile.Text = "Listeyi Yenile";
            this.btnYenile.UseVisualStyleBackColor = true;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            this.btnRevizeEdilecekBelge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRevizeEdilecekBelge.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRevizeEdilecekBelge.Location = new System.Drawing.Point(18, 395);
            this.btnRevizeEdilecekBelge.Name = "btnRevizeEdilecekBelge";
            this.btnRevizeEdilecekBelge.Size = new System.Drawing.Size(190, 30);
            this.btnRevizeEdilecekBelge.TabIndex = 3;
            this.btnRevizeEdilecekBelge.Text = "Revize Edilecek Belgeyi Aç";
            this.btnRevizeEdilecekBelge.UseVisualStyleBackColor = true;
            this.btnRevizeEdilecekBelge.Click += new System.EventHandler(this.btnRevizeEdilecekBelge_Click);
            this.btnYeniDosyaSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYeniDosyaSec.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYeniDosyaSec.Location = new System.Drawing.Point(728, 351);
            this.btnYeniDosyaSec.Name = "btnYeniDosyaSec";
            this.btnYeniDosyaSec.Size = new System.Drawing.Size(110, 28);
            this.btnYeniDosyaSec.TabIndex = 4;
            this.btnYeniDosyaSec.Text = "Seç...";
            this.btnYeniDosyaSec.UseVisualStyleBackColor = true;
            this.btnYeniDosyaSec.Click += new System.EventHandler(this.btnYeniDosyaSec_Click);
            this.txtYeniDosyaYolu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYeniDosyaYolu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtYeniDosyaYolu.Location = new System.Drawing.Point(18, 351);
            this.txtYeniDosyaYolu.Name = "txtYeniDosyaYolu";
            this.txtYeniDosyaYolu.ReadOnly = true;
            this.txtYeniDosyaYolu.Size = new System.Drawing.Size(704, 27);
            this.txtYeniDosyaYolu.TabIndex = 3;
            this.lblYeniDosya.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblYeniDosya.AutoSize = true;
            this.lblYeniDosya.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblYeniDosya.Location = new System.Drawing.Point(15, 328);
            this.lblYeniDosya.Name = "lblYeniDosya";
            this.lblYeniDosya.Size = new System.Drawing.Size(163, 20);
            this.lblYeniDosya.TabIndex = 2;
            this.lblYeniDosya.Text = "Yeni dosya (revize sonrası)";
            this.lblListeAciklama.AutoSize = true;
            this.lblListeAciklama.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblListeAciklama.Location = new System.Drawing.Point(15, 11);
            this.lblListeAciklama.Name = "lblListeAciklama";
            this.lblListeAciklama.Size = new System.Drawing.Size(214, 20);
            this.lblListeAciklama.TabIndex = 1;
            this.lblListeAciklama.Text = "Revize talep edilmiş belgeler listesi";
            this.dgvRevizeIstekleri.AllowUserToAddRows = false;
            this.dgvRevizeIstekleri.AllowUserToDeleteRows = false;
            this.dgvRevizeIstekleri.AllowUserToOrderColumns = true;
            this.dgvRevizeIstekleri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRevizeIstekleri.BackgroundColor = System.Drawing.Color.White;
            this.dgvRevizeIstekleri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRevizeIstekleri.Location = new System.Drawing.Point(18, 34);
            this.dgvRevizeIstekleri.MultiSelect = false;
            this.dgvRevizeIstekleri.Name = "dgvRevizeIstekleri";
            this.dgvRevizeIstekleri.ReadOnly = true;
            this.dgvRevizeIstekleri.RowHeadersVisible = false;
            this.dgvRevizeIstekleri.RowTemplate.Height = 24;
            this.dgvRevizeIstekleri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRevizeIstekleri.Size = new System.Drawing.Size(823, 285);
            this.dgvRevizeIstekleri.TabIndex = 0;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "FrmRevizeTamamlama";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revize Tamamlama";
            this.Load += new System.EventHandler(this.FrmRevizeTamamlama_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlKart.ResumeLayout(false);
            this.pnlKart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevizeIstekleri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlKart;
        private System.Windows.Forms.DataGridView dgvRevizeIstekleri;
        private System.Windows.Forms.Label lblListeAciklama;
        private System.Windows.Forms.Label lblYeniDosya;
        private System.Windows.Forms.TextBox txtYeniDosyaYolu;
        private System.Windows.Forms.Button btnYeniDosyaSec;
        private System.Windows.Forms.Button btnRevizeyiTamamla;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Button btnRevizeEdilecekBelge;
    }
}
