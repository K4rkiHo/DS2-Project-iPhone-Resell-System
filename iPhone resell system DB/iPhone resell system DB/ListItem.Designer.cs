
namespace iPhone_resell_system_DB.Forms
{
    partial class ListItem
    {
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItem));
            this.iphone_name = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cena = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.i = new System.Windows.Forms.Label();
            this.kosik = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // iphone_name
            // 
            this.iphone_name.AutoSize = true;
            this.iphone_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.iphone_name.Location = new System.Drawing.Point(222, 12);
            this.iphone_name.Name = "iphone_name";
            this.iphone_name.Size = new System.Drawing.Size(82, 42);
            this.iphone_name.TabIndex = 0;
            this.iphone_name.Text = "test";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(224, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "CENA:";
            // 
            // cena
            // 
            this.cena.AutoSize = true;
            this.cena.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cena.Location = new System.Drawing.Point(307, 156);
            this.cena.Name = "cena";
            this.cena.Size = new System.Drawing.Size(108, 37);
            this.cena.TabIndex = 2;
            this.cena.Text = "label2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(563, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 53);
            this.button1.TabIndex = 3;
            this.button1.Text = "Přidat do košíku";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(450, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 53);
            this.button2.TabIndex = 4;
            this.button2.Text = "Detail";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // i
            // 
            this.i.AutoSize = true;
            this.i.Location = new System.Drawing.Point(635, 3);
            this.i.Name = "i";
            this.i.Size = new System.Drawing.Size(35, 13);
            this.i.TabIndex = 5;
            this.i.Text = "label2";
            this.i.Visible = false;
            // 
            // kosik
            // 
            this.kosik.AutoSize = true;
            this.kosik.Location = new System.Drawing.Point(634, 20);
            this.kosik.Name = "kosik";
            this.kosik.Size = new System.Drawing.Size(32, 13);
            this.kosik.TabIndex = 6;
            this.kosik.Text = "kosik";
            this.kosik.Visible = false;
            // 
            // ListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.kosik);
            this.Controls.Add(this.i);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cena);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.iphone_name);
            this.Name = "ListItem";
            this.Size = new System.Drawing.Size(673, 204);
            this.Load += new System.EventHandler(this.ListItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label iphone_name;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label cena;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label i;
        private System.Windows.Forms.Label kosik;
    }
}
