namespace OnaySistemi
{
    partial class FrmAuditLog
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
            this.pnlFiltreler = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIslemTipi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTablo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBaslangic = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBitis = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAra = new System.Windows.Forms.TextBox();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.dgvLoglar = new System.Windows.Forms.DataGridView();
            this.lblToplamIslem = new System.Windows.Forms.Label();
            this.btnDetay = new System.Windows.Forms.Button();
            this.btnKapat = new System.Windows.Forms.Button();
            this.pnlFiltreler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoglar)).BeginInit();
            this.SuspendLayout();

            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(200, 32);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Ýþlem Geçmiþi";

            this.pnlFiltreler.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlFiltreler.Location = new System.Drawing.Point(20, 60);
            this.pnlFiltreler.Name = "pnlFiltreler";
            this.pnlFiltreler.Size = new System.Drawing.Size(960, 100);
            this.pnlFiltreler.TabIndex = 1;
            this.pnlFiltreler.Padding = new System.Windows.Forms.Padding(10);

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Text = "Ýþlem Tipi:";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.pnlFiltreler.Controls.Add(this.label1);

            this.cmbIslemTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIslemTipi.Location = new System.Drawing.Point(10, 25);
            this.cmbIslemTipi.Name = "cmbIslemTipi";
            this.cmbIslemTipi.Size = new System.Drawing.Size(150, 21);
            this.cmbIslemTipi.TabIndex = 0;
            this.cmbIslemTipi.SelectedIndexChanged += new System.EventHandler(this.cmbIslemTipi_SelectedIndexChanged);
            this.pnlFiltreler.Controls.Add(this.cmbIslemTipi);

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 10);
            this.label2.Text = "Tablo:";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.pnlFiltreler.Controls.Add(this.label2);

            this.cmbTablo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTablo.Location = new System.Drawing.Point(170, 25);
            this.cmbTablo.Name = "cmbTablo";
            this.cmbTablo.Size = new System.Drawing.Size(150, 21);
            this.cmbTablo.TabIndex = 1;
            this.cmbTablo.SelectedIndexChanged += new System.EventHandler(this.cmbTablo_SelectedIndexChanged);
            this.pnlFiltreler.Controls.Add(this.cmbTablo);

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 55);
            this.label3.Text = "Baþlangýç:";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.pnlFiltreler.Controls.Add(this.label3);

            this.dtpBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBaslangic.Location = new System.Drawing.Point(10, 70);
            this.dtpBaslangic.Name = "dtpBaslangic";
            this.dtpBaslangic.Size = new System.Drawing.Size(150, 20);
            this.dtpBaslangic.TabIndex = 2;
            this.dtpBaslangic.ValueChanged += new System.EventHandler(this.dtpBaslangic_ValueChanged);
            this.pnlFiltreler.Controls.Add(this.dtpBaslangic);

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 55);
            this.label4.Text = "Bitiþ:";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.pnlFiltreler.Controls.Add(this.label4);

            this.dtpBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBitis.Location = new System.Drawing.Point(170, 70);
            this.dtpBitis.Name = "dtpBitis";
            this.dtpBitis.Size = new System.Drawing.Size(150, 20);
            this.dtpBitis.TabIndex = 3;
            this.dtpBitis.ValueChanged += new System.EventHandler(this.dtpBitis_ValueChanged);
            this.pnlFiltreler.Controls.Add(this.dtpBitis);

            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 10);
            this.label5.Text = "Ara:";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.pnlFiltreler.Controls.Add(this.label5);

            this.txtAra.Location = new System.Drawing.Point(330, 25);
            this.txtAra.Name = "txtAra";
            this.txtAra.Size = new System.Drawing.Size(200, 20);
            this.txtAra.TabIndex = 4;
            this.txtAra.TextChanged += new System.EventHandler(this.txtAra_TextChanged);
            this.pnlFiltreler.Controls.Add(this.txtAra);

            this.btnGuncelle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnGuncelle.Location = new System.Drawing.Point(540, 45);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(100, 35);
            this.btnGuncelle.TabIndex = 5;
            this.btnGuncelle.Text = "Yenile";
            this.btnGuncelle.UseVisualStyleBackColor = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            this.pnlFiltreler.Controls.Add(this.btnGuncelle);

            this.dgvLoglar.AllowUserToAddRows = false;
            this.dgvLoglar.AllowUserToDeleteRows = false;
            this.dgvLoglar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoglar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLoglar.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoglar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoglar.Location = new System.Drawing.Point(20, 170);
            this.dgvLoglar.Name = "dgvLoglar";
            this.dgvLoglar.ReadOnly = true;
            this.dgvLoglar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoglar.Size = new System.Drawing.Size(960, 350);
            this.dgvLoglar.TabIndex = 2;

            this.lblToplamIslem.AutoSize = true;
            this.lblToplamIslem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblToplamIslem.Location = new System.Drawing.Point(20, 530);
            this.lblToplamIslem.Name = "lblToplamIslem";
            this.lblToplamIslem.Size = new System.Drawing.Size(100, 16);
            this.lblToplamIslem.TabIndex = 3;
            this.lblToplamIslem.Text = "Toplam Ýþlem: 0";

            this.btnDetay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnDetay.ForeColor = System.Drawing.Color.White;
            this.btnDetay.Location = new System.Drawing.Point(800, 530);
            this.btnDetay.Name = "btnDetay";
            this.btnDetay.Size = new System.Drawing.Size(90, 30);
            this.btnDetay.TabIndex = 4;
            this.btnDetay.Text = "Detay";
            this.btnDetay.UseVisualStyleBackColor = false;
            this.btnDetay.Click += new System.EventHandler(this.btnDetay_Click);

            this.btnKapat.Location = new System.Drawing.Point(900, 530);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(80, 30);
            this.btnKapat.TabIndex = 5;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.UseVisualStyleBackColor = true;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnDetay);
            this.Controls.Add(this.lblToplamIslem);
            this.Controls.Add(this.dgvLoglar);
            this.Controls.Add(this.pnlFiltreler);
            this.Controls.Add(this.lblBaslik);
            this.MinimumSize = new System.Drawing.Size(1000, 580);
            this.Name = "FrmAuditLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDOS - Ýþlem Geçmiþi";
            this.Load += new System.EventHandler(this.FrmAuditLog_Load);
            this.pnlFiltreler.ResumeLayout(false);
            this.pnlFiltreler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoglar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel pnlFiltreler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIslemTipi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTablo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpBaslangic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpBitis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.DataGridView dgvLoglar;
        private System.Windows.Forms.Label lblToplamIslem;
        private System.Windows.Forms.Button btnDetay;
        private System.Windows.Forms.Button btnKapat;
    }
}
