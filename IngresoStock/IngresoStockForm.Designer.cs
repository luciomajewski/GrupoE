namespace GrupoE.IngresoStock
{
    partial class IngresoStockForm
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
            groupBox1 = new GroupBox();
            btnBuscar = new Button();
            txtNroOrden = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            txtClienteOrigen = new TextBox();
            label2 = new Label();
            groupBox3 = new GroupBox();
            listViewSkus = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            groupBox4 = new GroupBox();
            btnAgregarUbicacion = new Button();
            txtCantidadUbicar = new TextBox();
            label8 = new Label();
            cmbNivelZ = new ComboBox();
            label7 = new Label();
            cmbFilaY = new ComboBox();
            label6 = new Label();
            cmbPasilloX = new ComboBox();
            label5 = new Label();
            cmbSkuSeleccionado = new ComboBox();
            label4 = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            listView1 = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            button1 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnBuscar);
            groupBox1.Controls.Add(txtNroOrden);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(14, 16);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(709, 80);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Búsqueda de Orden de Recepción";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(491, 28);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(103, 33);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtNroOrden
            // 
            txtNroOrden.Location = new Point(171, 29);
            txtNroOrden.Margin = new Padding(3, 4, 3, 4);
            txtNroOrden.Name = "txtNroOrden";
            txtNroOrden.Size = new Size(297, 27);
            txtNroOrden.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 33);
            label1.Name = "label1";
            label1.Size = new Size(147, 20);
            label1.TabIndex = 0;
            label1.Text = "N° Orden Recepción:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtClienteOrigen);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(14, 104);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(709, 80);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Datos de la Orden de Recepción";
            // 
            // txtClienteOrigen
            // 
            txtClienteOrigen.Location = new Point(131, 33);
            txtClienteOrigen.Margin = new Padding(3, 4, 3, 4);
            txtClienteOrigen.Name = "txtClienteOrigen";
            txtClienteOrigen.ReadOnly = true;
            txtClienteOrigen.Size = new Size(319, 27);
            txtClienteOrigen.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 33);
            label2.Name = "label2";
            label2.Size = new Size(117, 20);
            label2.TabIndex = 0;
            label2.Text = "Cliente / Origen:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(listViewSkus);
            groupBox3.Location = new Point(14, 192);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(849, 213);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Ítems y SKUs a Ubicar";
            // 
            // listViewSkus
            // 
            listViewSkus.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listViewSkus.Location = new Point(17, 29);
            listViewSkus.Margin = new Padding(3, 4, 3, 4);
            listViewSkus.Name = "listViewSkus";
            listViewSkus.Size = new Size(825, 165);
            listViewSkus.TabIndex = 0;
            listViewSkus.UseCompatibleStateImageBehavior = false;
            listViewSkus.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "SKU";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Descripción";
            columnHeader2.Width = 240;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Cantidad Recepcionada";
            columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Cantidad a ubicar";
            columnHeader4.Width = 130;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnAgregarUbicacion);
            groupBox4.Controls.Add(txtCantidadUbicar);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(cmbNivelZ);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(cmbFilaY);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(cmbPasilloX);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(cmbSkuSeleccionado);
            groupBox4.Controls.Add(label4);
            groupBox4.Location = new Point(31, 413);
            groupBox4.Margin = new Padding(3, 4, 3, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 4, 3, 4);
            groupBox4.Size = new Size(709, 173);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Ubicación Física en Racks (Coordenadas X - Y - Z)";
            // 
            // btnAgregarUbicacion
            // 
            btnAgregarUbicacion.Location = new Point(526, 123);
            btnAgregarUbicacion.Margin = new Padding(3, 4, 3, 4);
            btnAgregarUbicacion.Name = "btnAgregarUbicacion";
            btnAgregarUbicacion.Size = new Size(166, 37);
            btnAgregarUbicacion.TabIndex = 10;
            btnAgregarUbicacion.Text = "Agregar Ubicación";
            btnAgregarUbicacion.UseVisualStyleBackColor = true;
            btnAgregarUbicacion.Click += btnAgregarUbicacion_Click;
            // 
            // txtCantidadUbicar
            // 
            txtCantidadUbicar.Location = new Point(589, 29);
            txtCantidadUbicar.Margin = new Padding(3, 4, 3, 4);
            txtCantidadUbicar.Name = "txtCantidadUbicar";
            txtCantidadUbicar.Size = new Size(102, 27);
            txtCantidadUbicar.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(514, 33);
            label8.Name = "label8";
            label8.Size = new Size(72, 20);
            label8.TabIndex = 8;
            label8.Text = "Cantidad:";
            // 
            // cmbNivelZ
            // 
            cmbNivelZ.FormattingEnabled = true;
            cmbNivelZ.Items.AddRange(new object[] { "Nivel 1", "Nivel 2", "Nivel 3" });
            cmbNivelZ.Location = new Point(560, 80);
            cmbNivelZ.Margin = new Padding(3, 4, 3, 4);
            cmbNivelZ.Name = "cmbNivelZ";
            cmbNivelZ.Size = new Size(131, 28);
            cmbNivelZ.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(440, 83);
            label7.Name = "label7";
            label7.Size = new Size(115, 20);
            label7.TabIndex = 6;
            label7.Text = "Nivel/Altura (Z):";
            // 
            // cmbFilaY
            // 
            cmbFilaY.FormattingEnabled = true;
            cmbFilaY.Items.AddRange(new object[] { "Fila 1", "Fila 2", "Fila 3" });
            cmbFilaY.Location = new Point(303, 80);
            cmbFilaY.Margin = new Padding(3, 4, 3, 4);
            cmbFilaY.Name = "cmbFilaY";
            cmbFilaY.Size = new Size(131, 28);
            cmbFilaY.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(246, 84);
            label6.Name = "label6";
            label6.Size = new Size(57, 20);
            label6.TabIndex = 4;
            label6.Text = "Fila (Y):";
            // 
            // cmbPasilloX
            // 
            cmbPasilloX.FormattingEnabled = true;
            cmbPasilloX.Items.AddRange(new object[] { "Pasillo 1", "Pasillo 2", "Pasillo 3" });
            cmbPasilloX.Location = new Point(131, 80);
            cmbPasilloX.Margin = new Padding(3, 4, 3, 4);
            cmbPasilloX.Name = "cmbPasilloX";
            cmbPasilloX.Size = new Size(97, 28);
            cmbPasilloX.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 84);
            label5.Name = "label5";
            label5.Size = new Size(114, 20);
            label5.TabIndex = 2;
            label5.Text = "Pasillo/Rack (X):";
            // 
            // cmbSkuSeleccionado
            // 
            cmbSkuSeleccionado.FormattingEnabled = true;
            cmbSkuSeleccionado.Location = new Point(149, 29);
            cmbSkuSeleccionado.Margin = new Padding(3, 4, 3, 4);
            cmbSkuSeleccionado.Name = "cmbSkuSeleccionado";
            cmbSkuSeleccionado.Size = new Size(342, 28);
            cmbSkuSeleccionado.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 33);
            label4.Name = "label4";
            label4.Size = new Size(132, 20);
            label4.TabIndex = 0;
            label4.Text = "SKU Seleccionado:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(566, 835);
            btnConfirmar.Margin = new Padding(3, 4, 3, 4);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(174, 47);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar Ingreso";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(434, 835);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(114, 47);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6, columnHeader7 });
            listView1.Location = new Point(31, 595);
            listView1.Margin = new Padding(3, 4, 3, 4);
            listView1.Name = "listView1";
            listView1.Size = new Size(708, 116);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "SKU";
            columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Cantidad";
            columnHeader6.Width = 240;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Posicion";
            columnHeader7.Width = 180;
            // 
            // button1
            // 
            button1.Location = new Point(574, 720);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(166, 37);
            button1.TabIndex = 11;
            button1.Text = "Quitar seleccionada";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // IngresoStockForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(991, 925);
            Controls.Add(button1);
            Controls.Add(listView1);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "IngresoStockForm";
            Text = "Pampazon S.A. - Ingreso a Stock";
            Load += IngresoStockForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnBuscar;
        private TextBox txtNroOrden;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox txtClienteOrigen;
        private Label label2;
        private GroupBox groupBox3;
        private ListView listViewSkus;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private GroupBox groupBox4;
        private ComboBox cmbSkuSeleccionado;
        private Label label4;
        private ComboBox cmbPasilloX;
        private Label label5;
        private ComboBox cmbFilaY;
        private Label label6;
        private ComboBox cmbNivelZ;
        private Label label7;
        private TextBox txtCantidadUbicar;
        private Label label8;
        private Button btnAgregarUbicacion;
        private Button btnConfirmar;
        private Button btnCancelar;
        private ListView listView1;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Button button1;
    }
}