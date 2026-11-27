namespace GrupoE.RegistrarOrdenDePreparacion
{
    partial class RegistrarOrdenDePreparacionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labusuario = new Label();
            combocliente = new ComboBox();
            textnomtrans = new TextBox();
            labdatostrans = new Label();
            labnomtrans = new Label();
            labDNI = new Label();
            textDNI = new TextBox();
            labmodalidad = new Label();
            radiocerrado = new RadioButton();
            radiofullfilment = new RadioButton();
            datefecha = new DateTimePicker();
            labfecha = new Label();
            label8 = new Label();
            comboprodretirar = new ComboBox();
            labprodretirar = new Label();
            btnagregar = new Button();
            textcantretirar = new TextBox();
            labcantretirar = new Label();
            listordenact = new ListView();
            SKU = new ColumnHeader();
            Producto = new ColumnHeader();
            Cantidad = new ColumnHeader();
            labdatosetiq = new Label();
            textnombre = new TextBox();
            labnombre = new Label();
            labdireccion = new Label();
            textdireccion = new TextBox();
            labempresa = new Label();
            textempresatrans = new TextBox();
            btnconfirmar = new Button();
            btncancelar = new Button();
            labdepo = new Label();
            combodepo = new ComboBox();
            SuspendLayout();
            // 
            // labusuario
            // 
            labusuario.AutoSize = true;
            labusuario.Location = new Point(16, 14);
            labusuario.Name = "labusuario";
            labusuario.Size = new Size(50, 15);
            labusuario.TabIndex = 0;
            labusuario.Text = "Cliente:";
            // 
            // combocliente
            // 
            combocliente.DropDownStyle = ComboBoxStyle.DropDownList;
            combocliente.FormattingEnabled = true;
            combocliente.Location = new Point(70, 10);
            combocliente.Name = "combocliente";
            combocliente.Size = new Size(354, 23);
            combocliente.TabIndex = 0;
            combocliente.SelectedIndexChanged += combocliente_SelectedIndexChanged;
            // 
            // textnomtrans
            // 
            textnomtrans.Location = new Point(456, 146);
            textnomtrans.Name = "textnomtrans";
            textnomtrans.Size = new Size(160, 23);
            textnomtrans.TabIndex = 9;
            textnomtrans.TextChanged += textsololetras_TextChanged;
            textnomtrans.KeyPress += textsololetras_KeyPress;
            // 
            // labdatostrans
            // 
            labdatostrans.AutoSize = true;
            labdatostrans.Location = new Point(456, 104);
            labdatostrans.Name = "labdatostrans";
            labdatostrans.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labdatostrans.Size = new Size(230, 15);
            labdatostrans.TabIndex = 8;
            labdatostrans.Text = "Datos del transportista autorizado a retirar";
            // 
            // labnomtrans
            // 
            labnomtrans.AutoSize = true;
            labnomtrans.Location = new Point(456, 128);
            labnomtrans.Name = "labnomtrans";
            labnomtrans.Size = new Size(51, 15);
            labnomtrans.TabIndex = 11;
            labnomtrans.Text = "Nombre";
            // 
            // labDNI
            // 
            labDNI.AutoSize = true;
            labDNI.Location = new Point(628, 128);
            labDNI.Name = "labDNI";
            labDNI.Size = new Size(27, 15);
            labDNI.TabIndex = 12;
            labDNI.Text = "DNI";
            // 
            // textDNI
            // 
            textDNI.Location = new Point(628, 146);
            textDNI.MaxLength = 8;
            textDNI.Name = "textDNI";
            textDNI.Size = new Size(90, 23);
            textDNI.TabIndex = 10;
            textDNI.TextChanged += textsolonumeros_TextChanged;
            textDNI.KeyPress += textsolonumeros_KeyPress;
            // 
            // labmodalidad
            // 
            labmodalidad.AutoSize = true;
            labmodalidad.Location = new Point(16, 104);
            labmodalidad.Name = "labmodalidad";
            labmodalidad.Size = new Size(160, 15);
            labmodalidad.TabIndex = 7;
            labmodalidad.Text = "Modalidad de empaquetado";
            // 
            // radiocerrado
            // 
            radiocerrado.AutoSize = true;
            radiocerrado.Location = new Point(16, 124);
            radiocerrado.Name = "radiocerrado";
            radiocerrado.Size = new Size(96, 19);
            radiocerrado.TabIndex = 2;
            radiocerrado.TabStop = true;
            radiocerrado.Text = "Palet cerrado";
            radiocerrado.UseVisualStyleBackColor = true;
            radiocerrado.CheckedChanged += radiomodalidad_CheckedChanged;
            // 
            // radiofullfilment
            // 
            radiofullfilment.AutoSize = true;
            radiofullfilment.Location = new Point(140, 124);
            radiofullfilment.Name = "radiofullfilment";
            radiofullfilment.Size = new Size(82, 19);
            radiofullfilment.TabIndex = 3;
            radiofullfilment.TabStop = true;
            radiofullfilment.Text = "Fulfillment";
            radiofullfilment.UseVisualStyleBackColor = true;
            radiofullfilment.CheckedChanged += radiomodalidad_CheckedChanged;
            // 
            // datefecha
            // 
            datefecha.Location = new Point(456, 66);
            datefecha.Name = "datefecha";
            datefecha.Size = new Size(250, 23);
            datefecha.TabIndex = 8;
            // 
            // labfecha
            // 
            labfecha.AutoSize = true;
            labfecha.Location = new Point(456, 48);
            labfecha.Name = "labfecha";
            labfecha.Size = new Size(91, 15);
            labfecha.TabIndex = 4;
            labfecha.Text = "Fecha de retiro";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(255, 128, 128);
            label8.Location = new Point(572, 18);
            label8.Name = "label8";
            label8.Size = new Size(0, 15);
            label8.TabIndex = 2;
            // 
            // comboprodretirar
            // 
            comboprodretirar.DropDownStyle = ComboBoxStyle.DropDownList;
            comboprodretirar.FormattingEnabled = true;
            comboprodretirar.Items.AddRange(new object[] { "Prod 1", "Prod 2", "Prod 3" });
            comboprodretirar.Location = new Point(16, 178);
            comboprodretirar.Name = "comboprodretirar";
            comboprodretirar.Size = new Size(200, 23);
            comboprodretirar.TabIndex = 4;
            // 
            // labprodretirar
            // 
            labprodretirar.AutoSize = true;
            labprodretirar.Location = new Point(16, 160);
            labprodretirar.Name = "labprodretirar";
            labprodretirar.Size = new Size(107, 15);
            labprodretirar.TabIndex = 18;
            labprodretirar.Text = "Producto a retirar";
            // 
            // btnagregar
            // 
            btnagregar.Location = new Point(310, 177);
            btnagregar.Name = "btnagregar";
            btnagregar.Size = new Size(114, 25);
            btnagregar.TabIndex = 6;
            btnagregar.Text = "Agregar a la orden";
            btnagregar.UseVisualStyleBackColor = true;
            btnagregar.Click += btnagregar_Click;
            // 
            // textcantretirar
            // 
            textcantretirar.Location = new Point(228, 178);
            textcantretirar.Name = "textcantretirar";
            textcantretirar.Size = new Size(70, 23);
            textcantretirar.TabIndex = 5;
            textcantretirar.TextChanged += textsolonumeros_TextChanged;
            textcantretirar.KeyPress += textsolonumeros_KeyPress;
            // 
            // labcantretirar
            // 
            labcantretirar.AutoSize = true;
            labcantretirar.Location = new Point(228, 160);
            labcantretirar.Name = "labcantretirar";
            labcantretirar.Size = new Size(104, 15);
            labcantretirar.TabIndex = 20;
            labcantretirar.Text = "Cantidad";
            // 
            // listordenact
            // 
            listordenact.Columns.AddRange(new ColumnHeader[] { SKU, Producto, Cantidad });
            listordenact.Location = new Point(16, 214);
            listordenact.Name = "listordenact";
            listordenact.GridLines = true;
            listordenact.FullRowSelect = true;
            listordenact.Size = new Size(408, 200);
            listordenact.TabIndex = 7;
            listordenact.UseCompatibleStateImageBehavior = false;
            listordenact.View = View.Details;
            // 
            // SKU
            // 
            SKU.Text = "SKU";
            SKU.Width = 90;
            // 
            // Producto
            // 
            Producto.Text = "Producto";
            Producto.Width = 200;
            // 
            // Cantidad
            // 
            Cantidad.Text = "Cantidad";
            Cantidad.Width = 90;
            // 
            // labdatosetiq
            // 
            labdatosetiq.AutoSize = true;
            labdatosetiq.Location = new Point(456, 190);
            labdatosetiq.Name = "labdatosetiq";
            labdatosetiq.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labdatosetiq.Size = new Size(138, 15);
            labdatosetiq.TabIndex = 22;
            labdatosetiq.Text = "Datos para el etiquetado";
            // 
            // textnombre
            // 
            textnombre.Location = new Point(456, 232);
            textnombre.Name = "textnombre";
            textnombre.Size = new Size(198, 23);
            textnombre.TabIndex = 12;
            textnombre.TextChanged += textsololetras_TextChanged;
            textnombre.KeyPress += textsololetras_KeyPress;
            // 
            // labnombre
            // 
            labnombre.AutoSize = true;
            labnombre.Location = new Point(456, 214);
            labnombre.Name = "labnombre";
            labnombre.Size = new Size(139, 15);
            labnombre.TabIndex = 24;
            labnombre.Text = "Nombre del destinatario";
            // 
            // labdireccion
            // 
            labdireccion.AutoSize = true;
            labdireccion.Location = new Point(666, 214);
            labdireccion.Name = "labdireccion";
            labdireccion.Size = new Size(119, 15);
            labdireccion.TabIndex = 25;
            labdireccion.Text = "Dirección de destino";
            // 
            // textdireccion
            // 
            textdireccion.Location = new Point(666, 232);
            textdireccion.Name = "textdireccion";
            textdireccion.Size = new Size(198, 23);
            textdireccion.TabIndex = 13;
            // 
            // labempresa
            // 
            labempresa.AutoSize = true;
            labempresa.Location = new Point(730, 128);
            labempresa.Name = "labempresa";
            labempresa.Size = new Size(126, 15);
            labempresa.TabIndex = 13;
            labempresa.Text = "Empresa de transporte";
            // 
            // textempresatrans
            // 
            textempresatrans.Location = new Point(730, 146);
            textempresatrans.Name = "textempresatrans";
            textempresatrans.Size = new Size(134, 23);
            textempresatrans.TabIndex = 11;
            // 
            // btnconfirmar
            // 
            btnconfirmar.BackColor = SystemColors.Control;
            btnconfirmar.Location = new Point(748, 436);
            btnconfirmar.Name = "btnconfirmar";
            btnconfirmar.Size = new Size(116, 32);
            btnconfirmar.TabIndex = 15;
            btnconfirmar.Text = "Confirmar orden";
            btnconfirmar.UseVisualStyleBackColor = true;
            btnconfirmar.Click += btnconfirmar_Click;
            // 
            // btncancelar
            // 
            btncancelar.Location = new Point(624, 436);
            btncancelar.Name = "btncancelar";
            btncancelar.Size = new Size(112, 32);
            btncancelar.TabIndex = 14;
            btncancelar.Text = "Cancelar";
            btncancelar.UseVisualStyleBackColor = true;
            btncancelar.Click += btncancelar_Click;
            // 
            // labdepo
            // 
            labdepo.AutoSize = true;
            labdepo.Location = new Point(16, 48);
            labdepo.Name = "labdepo";
            labdepo.Size = new Size(57, 15);
            labdepo.TabIndex = 3;
            labdepo.Text = "Depósito";
            // 
            // combodepo
            // 
            combodepo.DropDownStyle = ComboBoxStyle.DropDownList;
            combodepo.FormattingEnabled = true;
            combodepo.Items.AddRange(new object[] { "Depo 1", "Depo 2", "Depo 3" });
            combodepo.Location = new Point(16, 66);
            combodepo.Name = "combodepo";
            combodepo.Size = new Size(408, 23);
            combodepo.TabIndex = 1;
            combodepo.SelectedIndexChanged += combodepo_SelectedIndexChanged;
            // 
            // RegistrarOrdenDePreparacionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 484);
            Controls.Add(combodepo);
            Controls.Add(labdepo);
            Controls.Add(btncancelar);
            Controls.Add(btnconfirmar);
            Controls.Add(textempresatrans);
            Controls.Add(labempresa);
            Controls.Add(textdireccion);
            Controls.Add(labdireccion);
            Controls.Add(labnombre);
            Controls.Add(textnombre);
            Controls.Add(labdatosetiq);
            Controls.Add(listordenact);
            Controls.Add(labcantretirar);
            Controls.Add(textcantretirar);
            Controls.Add(btnagregar);
            Controls.Add(labprodretirar);
            Controls.Add(comboprodretirar);
            Controls.Add(label8);
            Controls.Add(labfecha);
            Controls.Add(datefecha);
            Controls.Add(radiofullfilment);
            Controls.Add(radiocerrado);
            Controls.Add(labmodalidad);
            Controls.Add(textDNI);
            Controls.Add(labDNI);
            Controls.Add(labnomtrans);
            Controls.Add(labdatostrans);
            Controls.Add(textnomtrans);
            Controls.Add(combocliente);
            Controls.Add(labusuario);
            Name = "RegistrarOrdenDePreparacionForm";
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Registrar orden de preparación";
            Load += RegistrarOrdenDePreparacionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labusuario;
        private ComboBox combocliente;
        private TextBox textnomtrans;
        private Label labdatostrans;
        private Label labnomtrans;
        private Label labDNI;
        private TextBox textDNI;
        private Label labmodalidad;
        private RadioButton radiocerrado;
        private RadioButton radiofullfilment;
        private DateTimePicker datefecha;
        private Label labfecha;
        private Label label8;
        private ComboBox comboprodretirar;
        private Label labprodretirar;
        private Button btnagregar;
        private TextBox textcantretirar;
        private Label labcantretirar;
        private ListView listordenact;
        private ColumnHeader Producto;
        private ColumnHeader Cantidad;
        private ColumnHeader SKU;
        private Label labdatosetiq;
        private TextBox textnombre;
        private Label labnombre;
        private Label labdireccion;
        private TextBox textdireccion;
        private Label labempresa;
        private TextBox textempresatrans;
        private Button btnconfirmar;
        private Button btncancelar;
        private Label labdepo;
        private ComboBox combodepo;
    }
}