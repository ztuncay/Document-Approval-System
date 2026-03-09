namespace OnaySistemi
{
    partial class FrmSifreDeðiþtir
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
            this.lblKullanici = new System.Windows.Forms.Label();
            this.lblGereksinimler = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEskiSifre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYeniSifre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYeniSifreDoðrula = new System.Windows.Forms.TextBox();
            this.btnDeðiþtir = new System.Windows.Forms.Button();
            this.btnKapat = new System.Windows.Forms.Button();
            this.lblMesaj = new System.Windows.Forms.Label();
            this.chkSifreGoster = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();

            this.lblKullanici.AutoSize = true;
            this.lblKullanici.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKullanici.Location = new System.Drawing.Point(20, 20);
            this.lblKullanici.Name = "lblKullanici";
            this.lblKullanici.Size = new System.Drawing.Size(200, 20);
            this.lblKullanici.TabIndex = 0;
            this.lblKullanici.Text = "Kullanýcý: Ad Soyad (kullanici)";

            this.lblGereksinimler.AutoSize = true;
            this.lblGereksinimler.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblGereksinimler.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblGereksinimler.Location = new System.Drawing.Point(20, 50);
            this.lblGereksinimler.Name = "lblGereksinimler";
            this.lblGereksinimler.Size = new System.Drawing.Size(350, 15);
            this.lblGereksinimler.TabIndex = 1;
            this.lblGereksinimler.Text = "Gereken: Minimum 6 karakter, en az 1 rakam, en az 1 büyük harf";

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Eski Þifre:";

            this.txtEskiSifre.Location = new System.Drawing.Point(20, 100);
            this.txtEskiSifre.Name = "txtEskiSifre";
            this.txtEskiSifre.PasswordChar = '?';
            this.txtEskiSifre.Size = new System.Drawing.Size(300, 20);
            this.txtEskiSifre.TabIndex = 3;

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Yeni Þifre:";

            this.txtYeniSifre.Location = new System.Drawing.Point(20, 150);
            this.txtYeniSifre.Name = "txtYeniSifre";
            this.txtYeniSifre.PasswordChar = '?';
            this.txtYeniSifre.Size = new System.Drawing.Size(300, 20);
            this.txtYeniSifre.TabIndex = 5;

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Yeni Þifre (Doðrulama):";

            this.txtYeniSifreDoðrula.Location = new System.Drawing.Point(20, 200);
            this.txtYeniSifreDoðrula.Name = "txtYeniSifreDoðrula";
            this.txtYeniSifreDoðrula.PasswordChar = '?';
            this.txtYeniSifreDoðrula.Size = new System.Drawing.Size(300, 20);
            this.txtYeniSifreDoðrula.TabIndex = 7;

            this.chkSifreGoster.AutoSize = true;
            this.chkSifreGoster.Location = new System.Drawing.Point(330, 150);
            this.chkSifreGoster.Name = "chkSifreGoster";
            this.chkSifreGoster.Size = new System.Drawing.Size(88, 17);
            this.chkSifreGoster.TabIndex = 8;
            this.chkSifreGoster.Text = "Þifreleri Göster";
            this.chkSifreGoster.UseVisualStyleBackColor = true;
            this.chkSifreGoster.CheckedChanged += new System.EventHandler(this.chkSifreGoster_CheckedChanged);

            this.lblMesaj.AutoSize = true;
            this.lblMesaj.ForeColor = System.Drawing.Color.Red;
            this.lblMesaj.Location = new System.Drawing.Point(20, 240);
            this.lblMesaj.Name = "lblMesaj";
            this.lblMesaj.Size = new System.Drawing.Size(0, 13);
            this.lblMesaj.TabIndex = 9;

            this.btnDeðiþtir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnDeðiþtir.ForeColor = System.Drawing.Color.White;
            this.btnDeðiþtir.Location = new System.Drawing.Point(20, 270);
            this.btnDeðiþtir.Name = "btnDeðiþtir";
            this.btnDeðiþtir.Size = new System.Drawing.Size(100, 35);
            this.btnDeðiþtir.TabIndex = 10;
            this.btnDeðiþtir.Text = "Deðiþtir";
            this.btnDeðiþtir.UseVisualStyleBackColor = false;
            this.btnDeðiþtir.Click += new System.EventHandler(this.btnDeðiþtir_Click);

            this.btnKapat.Location = new System.Drawing.Point(130, 270);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(100, 35);
            this.btnKapat.TabIndex = 11;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.UseVisualStyleBackColor = true;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnDeðiþtir);
            this.Controls.Add(this.lblMesaj);
            this.Controls.Add(this.chkSifreGoster);
            this.Controls.Add(this.txtYeniSifreDoðrula);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtYeniSifre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEskiSifre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGereksinimler);
            this.Controls.Add(this.lblKullanici);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSifreDeðiþtir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Þifre Deðiþtir";
            this.Load += new System.EventHandler(this.FrmSifreDeðiþtir_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblKullanici;
        private System.Windows.Forms.Label lblGereksinimler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEskiSifre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYeniSifre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYeniSifreDoðrula;
        private System.Windows.Forms.Button btnDeðiþtir;
        private System.Windows.Forms.Button btnKapat;
        private System.Windows.Forms.Label lblMesaj;
        private System.Windows.Forms.CheckBox chkSifreGoster;
    }
}
