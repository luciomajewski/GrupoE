namespace GrupoE
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            lblEntrada = new Label();
            lblSalida = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button7 = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitulo.Location = new Point(28, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Pampazon S.A.";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9.75F);
            lblSubtitulo.ForeColor = Color.FromArgb(108, 117, 125);
            lblSubtitulo.Location = new Point(32, 68);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Text = "Gestión de depósito: elija la operación a realizar";
            // 
            // lblEntrada
            // 
            lblEntrada.AutoSize = true;
            lblEntrada.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEntrada.ForeColor = Color.FromArgb(13, 110, 253);
            lblEntrada.Location = new Point(32, 116);
            lblEntrada.Name = "lblEntrada";
            lblEntrada.Text = "ENTRADA DE MERCADERÍA";
            // 
            // lblSalida
            // 
            lblSalida.AutoSize = true;
            lblSalida.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSalida.ForeColor = Color.FromArgb(13, 110, 253);
            lblSalida.Location = new Point(344, 116);
            lblSalida.Name = "lblSalida";
            lblSalida.Text = "SALIDA DE MERCADERÍA";
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 10F);
            button7.Location = new Point(32, 148);
            button7.Margin = new Padding(4, 4, 4, 4);
            button7.Name = "button7";
            button7.Size = new Size(280, 52);
            button7.TabIndex = 0;
            button7.Text = "1.  Recepción de mercadería";
            button7.TextAlign = ContentAlignment.MiddleLeft;
            button7.Padding = new Padding(12, 0, 0, 0);
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 10F);
            button3.Location = new Point(32, 212);
            button3.Margin = new Padding(4, 4, 4, 4);
            button3.Name = "button3";
            button3.Size = new Size(280, 52);
            button3.TabIndex = 1;
            button3.Text = "2.  Ingreso a stock";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.Padding = new Padding(12, 0, 0, 0);
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10F);
            button2.Location = new Point(344, 148);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(280, 52);
            button2.TabIndex = 2;
            button2.Text = "1.  Registrar orden de preparación";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.Padding = new Padding(12, 0, 0, 0);
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 10F);
            button4.Location = new Point(344, 212);
            button4.Margin = new Padding(4, 4, 4, 4);
            button4.Name = "button4";
            button4.Size = new Size(280, 52);
            button4.TabIndex = 3;
            button4.Text = "2.  Cumplir orden de selección";
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.Padding = new Padding(12, 0, 0, 0);
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10F);
            button1.Location = new Point(344, 276);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(280, 52);
            button1.TabIndex = 4;
            button1.Text = "3.  Empaquetar pedido";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.Padding = new Padding(12, 0, 0, 0);
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 10F);
            button5.Location = new Point(344, 340);
            button5.Margin = new Padding(4, 4, 4, 4);
            button5.Name = "button5";
            button5.Size = new Size(280, 52);
            button5.TabIndex = 5;
            button5.Text = "4.  Entrega a transportista";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.Padding = new Padding(12, 0, 0, 0);
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(656, 420);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lblSalida);
            Controls.Add(lblEntrada);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pampazon S.A. - Gestión de depósito";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button7;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblEntrada;
        private Label lblSalida;
    }
}
