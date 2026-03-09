namespace OnaySistemi
{
    partial class FrmVersiyonGecmisi
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
            this.dgvVersiyonlar = new System.Windows.Forms.DataGridView();
            this.btnVersiyonBelgeAc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiyonlar)).BeginInit();
            this.SuspendLayout();
            this.dgvVersiyonlar.AllowUserToAddRows = false;
            this.dgvVersiyonlar.AllowUserToDeleteRows = false;
            this.dgvVersiyonlar.AllowUserToResizeRows = false;
            this.dgvVersiyonlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVersiyonlar.Location = new System.Drawing.Point(12, 12);
            this.dgvVersiyonlar.MultiSelect = false;
            this.dgvVersiyonlar.Name = "dgvVersiyonlar";
            this.dgvVersiyonlar.ReadOnly = true;
            this.dgvVersiyonlar.RowHeadersVisible = false;
            this.dgvVersiyonlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVersiyonlar.Size = new System.Drawing.Size(760, 300);
            this.dgvVersiyonlar.TabIndex = 0;
            this.dgvVersiyonlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                            | System.Windows.Forms.AnchorStyles.Left)
                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVersiyonBelgeAc.Location = new System.Drawing.Point(12, 318);
            this.btnVersiyonBelgeAc.Name = "btnVersiyonBelgeAc";
            this.btnVersiyonBelgeAc.Size = new System.Drawing.Size(200, 30);
            this.btnVersiyonBelgeAc.TabIndex = 1;
            this.btnVersiyonBelgeAc.Text = "Seçili Versiyonun Belgesini Aç";
            this.btnVersiyonBelgeAc.UseVisualStyleBackColor = true;
            this.btnVersiyonBelgeAc.Click += new System.EventHandler(this.btnVersiyonBelgeAc_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.btnVersiyonBelgeAc);
            this.Controls.Add(this.dgvVersiyonlar);
            this.Name = "FrmVersiyonGecmisi";
            this.Text = "Belge Versiyon Geçmişi";
            this.Load += new System.EventHandler(this.FrmVersiyonGecmisi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiyonlar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVersiyonlar;
        private System.Windows.Forms.Button btnVersiyonBelgeAc;
    }
}
