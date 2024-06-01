namespace vcap
{
    partial class View
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
            comboBox1 = new ComboBox();
            button1 = new Button();
            button5 = new Button();
            button2 = new Button();
            button6 = new Button();
            buttonExit = new Button();
            buttonOpenImage = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(14, 14);
            comboBox1.Margin = new Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(233, 23);
            comboBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(14, 45);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(233, 27);
            button1.TabIndex = 1;
            button1.Text = "Iniciar Vídeo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button5
            // 
            button5.Location = new Point(14, 78);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new Size(233, 27);
            button5.TabIndex = 2;
            button5.Text = "Parar Vídeo";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.Location = new Point(14, 112);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(233, 27);
            button2.TabIndex = 3;
            button2.Text = "Capturar Imagem";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button6
            // 
            button6.Location = new Point(14, 145);
            button6.Margin = new Padding(4, 3, 4, 3);
            button6.Name = "button6";
            button6.Size = new Size(233, 27);
            button6.TabIndex = 4;
            button6.Text = "Aplicar Filtro Grayscale";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(14, 212);
            buttonExit.Margin = new Padding(4, 3, 4, 3);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(233, 27);
            buttonExit.TabIndex = 5;
            buttonExit.Text = "Sair";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonOpenImage
            // 
            buttonOpenImage.Location = new Point(14, 179);
            buttonOpenImage.Margin = new Padding(4, 3, 4, 3);
            buttonOpenImage.Name = "buttonOpenImage";
            buttonOpenImage.Size = new Size(233, 27);
            buttonOpenImage.TabIndex = 6;
            buttonOpenImage.Text = "Abrir Imagem";
            buttonOpenImage.UseVisualStyleBackColor = true;
            buttonOpenImage.Click += buttonOpenImage_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.InactiveBorder;
            pictureBox1.Location = new Point(266, 12);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(640, 420);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.InactiveBorder;
            pictureBox2.Location = new Point(914, 12);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(640, 420);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // View
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1564, 471);
            Controls.Add(buttonOpenImage);
            Controls.Add(buttonExit);
            Controls.Add(button6);
            Controls.Add(button2);
            Controls.Add(button5);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Margin = new Padding(4, 3, 4, 3);
            Name = "View";
            Text = "Vcap";
            FormClosed += View_FormClosed;
            Load += Vcap_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonOpenImage; // Botão para abrir imagem
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
