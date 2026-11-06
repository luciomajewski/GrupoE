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
            labtemp = new Label();
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
            labusuario.Location = new Point(12, 9);
            labusuario.Name = "labusuario";
            labusuario.Size = new Size(50, 15);
            labusuario.TabIndex = 0;
            labusuario.Text = "Usuario:";
            // 
            // labtemp
            // 
            labtemp.AutoSize = true;
            labtemp.Location = new Point(68, 9);
            labtemp.Name = "labtemp";
            labtemp.Size = new Size(238, 15);
            labtemp.TabIndex = 1;
            labtemp.Text = "(Nombre del cliente actualmente loggeado)";
            // 
            // textnomtrans
            // 
            textnomtrans.Location = new Point(496, 286);
            textnomtrans.Name = "textnomtrans";
            textnomtrans.Size = new Size(122, 23);
            textnomtrans.TabIndex = 2;
            textnomtrans.TextChanged += textsololetras_TextChanged;
            textnomtrans.KeyPress += textsololetras_KeyPress;
            // 
            // labdatostrans
            // 
            labdatostrans.AutoSize = true;
            labdatostrans.Location = new Point(496, 237);
            labdatostrans.Name = "labdatostrans";
            labdatostrans.Size = new Size(230, 15);
            labdatostrans.TabIndex = 3;
            labdatostrans.Text = "Datos del transportista autorizado a reitrar:";
            // 
            // labnomtrans
            // 
            labnomtrans.AutoSize = true;
            labnomtrans.Location = new Point(498, 268);
            labnomtrans.Name = "labnomtrans";
            labnomtrans.Size = new Size(51, 15);
            labnomtrans.TabIndex = 4;
            labnomtrans.Text = "Nombre";
            // 
            // labDNI
            // 
            labDNI.AutoSize = true;
            labDNI.Location = new Point(652, 268);
            labDNI.Name = "labDNI";
            labDNI.Size = new Size(27, 15);
            labDNI.TabIndex = 5;
            labDNI.Text = "DNI";
            // 
            // textDNI
            // 
            textDNI.Location = new Point(652, 286);
            textDNI.MaxLength = 8;
            textDNI.Name = "textDNI";
            textDNI.Size = new Size(79, 23);
            textDNI.TabIndex = 6;
            textDNI.TextChanged += textsolonumeros_TextChanged;
            textDNI.KeyPress += textsolonumeros_KeyPress;
            // 
            // labmodalidad
            // 
            labmodalidad.AutoSize = true;
            labmodalidad.Location = new Point(12, 119);
            labmodalidad.Name = "labmodalidad";
            labmodalidad.Size = new Size(160, 15);
            labmodalidad.TabIndex = 8;
            labmodalidad.Text = "Modalidad de Empaquetado:";
            // 
            // radiocerrado
            // 
            radiocerrado.AutoSize = true;
            radiocerrado.Location = new Point(12, 149);
            radiocerrado.Name = "radiocerrado";
            radiocerrado.Size = new Size(96, 19);
            radiocerrado.TabIndex = 9;
            radiocerrado.TabStop = true;
            radiocerrado.Text = "Palet Cerrado";
            radiocerrado.UseVisualStyleBackColor = true;
            radiocerrado.CheckedChanged += radiomodalidad_CheckedChanged;
            // 
            // radiofullfilment
            // 
            radiofullfilment.AutoSize = true;
            radiofullfilment.Location = new Point(114, 149);
            radiofullfilment.Name = "radiofullfilment";
            radiofullfilment.Size = new Size(82, 19);
            radiofullfilment.TabIndex = 10;
            radiofullfilment.TabStop = true;
            radiofullfilment.Text = "Fullfilment";
            radiofullfilment.UseVisualStyleBackColor = true;
            radiofullfilment.CheckedChanged += radiomodalidad_CheckedChanged;
            // 
            // datefecha
            // 
            datefecha.Location = new Point(498, 187);
            datefecha.Name = "datefecha";
            datefecha.Size = new Size(228, 23);
            datefecha.TabIndex = 11;
            // 
            // labfecha
            // 
            labfecha.AutoSize = true;
            labfecha.Location = new Point(498, 169);
            labfecha.Name = "labfecha";
            labfecha.Size = new Size(91, 15);
            labfecha.TabIndex = 12;
            labfecha.Text = "Fecha de Retiro:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(255, 128, 128);
            label8.Location = new Point(572, 18);
            label8.Name = "label8";
            label8.Size = new Size(0, 15);
            label8.TabIndex = 13;
            // 
            // comboprodretirar
            // 
            comboprodretirar.DropDownStyle = ComboBoxStyle.DropDownList;
            comboprodretirar.FormattingEnabled = true;
            comboprodretirar.Items.AddRange(new object[] { "Prod 1", "Prod 2", "Prod 3" });
            comboprodretirar.Location = new Point(12, 213);
            comboprodretirar.Name = "comboprodretirar";
            comboprodretirar.Size = new Size(165, 23);
            comboprodretirar.TabIndex = 14;
            // 
            // labprodretirar
            // 
            labprodretirar.AutoSize = true;
            labprodretirar.Location = new Point(12, 195);
            labprodretirar.Name = "labprodretirar";
            labprodretirar.Size = new Size(107, 15);
            labprodretirar.TabIndex = 15;
            labprodretirar.Text = "Productos a retirar:";
            // 
            // btnagregar
            // 
            btnagregar.Location = new Point(267, 382);
            btnagregar.Name = "btnagregar";
            btnagregar.Size = new Size(127, 41);
            btnagregar.TabIndex = 16;
            btnagregar.Text = "Agregar a la Orden";
            btnagregar.UseVisualStyleBackColor = true;
            btnagregar.Click += btnagregar_Click;
            // 
            // textcantretirar
            // 
            textcantretirar.Location = new Point(202, 213);
            textcantretirar.Name = "textcantretirar";
            textcantretirar.Size = new Size(92, 23);
            textcantretirar.TabIndex = 17;
            textcantretirar.TextChanged += textsolonumeros_TextChanged;
            textcantretirar.KeyPress += textsolonumeros_KeyPress;
            // 
            // labcantretirar
            // 
            labcantretirar.AutoSize = true;
            labcantretirar.Location = new Point(202, 195);
            labcantretirar.Name = "labcantretirar";
            labcantretirar.Size = new Size(104, 15);
            labcantretirar.TabIndex = 18;
            labcantretirar.Text = "Cantidad a Retirar:";
            // 
            // listordenact
            // 
            listordenact.Columns.AddRange(new ColumnHeader[] { SKU, Producto, Cantidad });
            listordenact.Location = new Point(12, 242);
            listordenact.Name = "listordenact";
            listordenact.Size = new Size(382, 134);
            listordenact.TabIndex = 19;
            listordenact.UseCompatibleStateImageBehavior = false;
            listordenact.View = View.Details;
            // 
            // SKU
            // 
            SKU.Text = "SKU";
            SKU.Width = 100;
            // 
            // Producto
            // 
            Producto.Text = "Producto";
            Producto.Width = 100;
            // 
            // Cantidad
            // 
            Cantidad.Text = "Cantidad";
            Cantidad.Width = 100;
            // 
            // labdatosetiq
            // 
            labdatosetiq.AutoSize = true;
            labdatosetiq.Location = new Point(498, 70);
            labdatosetiq.Name = "labdatosetiq";
            labdatosetiq.Size = new Size(138, 15);
            labdatosetiq.TabIndex = 21;
            labdatosetiq.Text = "Datos para el Etiquetado:";
            // 
            // textnombre
            // 
            textnombre.Location = new Point(498, 129);
            textnombre.Name = "textnombre";
            textnombre.Size = new Size(138, 23);
            textnombre.TabIndex = 22;
            textnombre.TextChanged += textsololetras_TextChanged;
            textnombre.KeyPress += textsololetras_KeyPress;
            // 
            // labnombre
            // 
            labnombre.AutoSize = true;
            labnombre.Location = new Point(498, 100);
            labnombre.Name = "labnombre";
            labnombre.Size = new Size(139, 15);
            labnombre.TabIndex = 23;
            labnombre.Text = "Nombre del Destinatario:";
            // 
            // labdireccion
            // 
            labdireccion.AutoSize = true;
            labdireccion.Location = new Point(700, 100);
            labdireccion.Name = "labdireccion";
            labdireccion.Size = new Size(119, 15);
            labdireccion.TabIndex = 24;
            labdireccion.Text = "Dirección de Destino:";
            // 
            // textdireccion
            // 
            textdireccion.Location = new Point(700, 129);
            textdireccion.Name = "textdireccion";
            textdireccion.Size = new Size(119, 23);
            textdireccion.TabIndex = 25;
            // 
            // labempresa
            // 
            labempresa.AutoSize = true;
            labempresa.Location = new Point(761, 268);
            labempresa.Name = "labempresa";
            labempresa.Size = new Size(126, 15);
            labempresa.TabIndex = 26;
            labempresa.Text = "Empresa de Transporte";
            // 
            // textempresatrans
            // 
            textempresatrans.Location = new Point(761, 286);
            textempresatrans.Name = "textempresatrans";
            textempresatrans.Size = new Size(126, 23);
            textempresatrans.TabIndex = 27;
            // 
            // btnconfirmar
            // 
            btnconfirmar.BackColor = SystemColors.Control;
            btnconfirmar.Location = new Point(749, 404);
            btnconfirmar.Name = "btnconfirmar";
            btnconfirmar.Size = new Size(103, 45);
            btnconfirmar.TabIndex = 28;
            btnconfirmar.Text = "Confirmar Orden";
            btnconfirmar.UseVisualStyleBackColor = false;
            btnconfirmar.Click += btnconfirmar_Click;
            // 
            // btncancelar
            // 
            btncancelar.Location = new Point(592, 404);
            btncancelar.Name = "btncancelar";
            btncancelar.Size = new Size(103, 45);
            btncancelar.TabIndex = 29;
            btncancelar.Text = "Cancelar";
            btncancelar.UseVisualStyleBackColor = true;
            btncancelar.Click += btncancelar_Click;
            // 
            // labdepo
            // 
            labdepo.AutoSize = true;
            labdepo.Location = new Point(12, 44);
            labdepo.Name = "labdepo";
            labdepo.Size = new Size(57, 15);
            labdepo.TabIndex = 30;
            labdepo.Text = "Depósito:";
            // 
            // combodepo
            // 
            combodepo.DropDownStyle = ComboBoxStyle.DropDownList;
            combodepo.FormattingEnabled = true;
            combodepo.Items.AddRange(new object[] { "Depo 1", "Depo 2", "Depo 3" });
            combodepo.Location = new Point(12, 62);
            combodepo.Name = "combodepo";
            combodepo.Size = new Size(343, 23);
            combodepo.TabIndex = 31;
            combodepo.SelectedIndexChanged += combodepo_SelectedIndexChanged;
            // 
            // RegistrarOrdenDePreparacionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 474);
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
            Controls.Add(labtemp);
            Controls.Add(labusuario);
            Name = "RegistrarOrdenDePreparacionForm";
            Text = "Registrar_orden_de_preparación";
            Load += RegistrarOrdenDePreparacionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labusuario;
        private Label labtemp;
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