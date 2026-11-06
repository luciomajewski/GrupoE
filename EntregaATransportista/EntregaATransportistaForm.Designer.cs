namespace GrupoE.EntregaATransportista
{
    partial class EntregaATransportistaForm
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
            lblDni = new Label();
            lblTransportista = new Label();
            txtTransportista = new TextBox();
            txtDni = new TextBox();
            lblDetalle = new Label();
            lstDetalle = new ListView();
            colOrden = new ColumnHeader();
            colPaquetes = new ColumnHeader();
            btnBuscar = new Button();
            btnConfirmarDespacho = new Button();
            btnCancelar = new Button();
            chkRemitoFirmado = new CheckBox();
            btnGenerarRemito = new Button();
            SuspendLayout();
            //
            // lblDni
            //
            lblDni.AutoSize = true;
            lblDni.Location = new Point(12, 15);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(27, 15);
            lblDni.TabIndex = 0;
            lblDni.Text = "DNI";
            //
            // lblTransportista
            //
            lblTransportista.AutoSize = true;
            lblTransportista.Location = new Point(12, 74);
            lblTransportista.Name = "lblTransportista";
            lblTransportista.Size = new Size(75, 15);
            lblTransportista.TabIndex = 3;
            lblTransportista.Text = "Transportista";
            //
            // txtTransportista
            //
            txtTransportista.Location = new Point(12, 92);
            txtTransportista.MaxLength = 50;
            txtTransportista.Name = "txtTransportista";
            txtTransportista.ReadOnly = true;
            txtTransportista.Size = new Size(244, 23);
            txtTransportista.TabIndex = 4;
            txtTransportista.TabStop = false;
            //
            // txtDni
            //
            txtDni.Location = new Point(12, 36);
            txtDni.MaxLength = 8;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(122, 23);
            txtDni.TabIndex = 1;
            txtDni.TextChanged += txtDni_TextChanged;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(12, 143);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(122, 15);
            lblDetalle.TabIndex = 5;
            lblDetalle.Text = "Detalles de la Entrega:";
            //
            // lstDetalle
            //
            lstDetalle.Columns.AddRange(new ColumnHeader[] { colOrden, colPaquetes });
            lstDetalle.FullRowSelect = true;
            lstDetalle.Location = new Point(12, 161);
            lstDetalle.MultiSelect = false;
            lstDetalle.Name = "lstDetalle";
            lstDetalle.Size = new Size(244, 155);
            lstDetalle.TabIndex = 6;
            lstDetalle.UseCompatibleStateImageBehavior = false;
            lstDetalle.View = View.Details;
            //
            // colOrden
            //
            colOrden.Text = "Órden";
            colOrden.Width = 120;
            //
            // colPaquetes
            //
            colPaquetes.Text = "Paquetes";
            colPaquetes.TextAlign = HorizontalAlignment.Right;
            colPaquetes.Width = 100;
            //
            // btnBuscar
            //
            btnBuscar.BackColor = SystemColors.ControlLight;
            btnBuscar.FlatStyle = FlatStyle.Popup;
            btnBuscar.Location = new Point(152, 30);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(104, 35);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            //
            // btnConfirmarDespacho
            //
            btnConfirmarDespacho.Enabled = false;
            btnConfirmarDespacho.Location = new Point(157, 386);
            btnConfirmarDespacho.Name = "btnConfirmarDespacho";
            btnConfirmarDespacho.Size = new Size(99, 63);
            btnConfirmarDespacho.TabIndex = 10;
            btnConfirmarDespacho.Text = "Confirmar Despacho";
            btnConfirmarDespacho.UseVisualStyleBackColor = true;
            btnConfirmarDespacho.Click += btnConfirmarDespacho_Click;
            //
            // btnCancelar
            //
            btnCancelar.Location = new Point(17, 386);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(105, 63);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            //
            // chkRemitoFirmado
            //
            chkRemitoFirmado.AutoSize = true;
            chkRemitoFirmado.Enabled = false;
            chkRemitoFirmado.Location = new Point(17, 337);
            chkRemitoFirmado.Name = "chkRemitoFirmado";
            chkRemitoFirmado.Size = new Size(111, 19);
            chkRemitoFirmado.TabIndex = 8;
            chkRemitoFirmado.Text = "Remito Firmado";
            chkRemitoFirmado.UseVisualStyleBackColor = true;
            chkRemitoFirmado.CheckedChanged += chkRemitoFirmado_CheckedChanged;
            //
            // btnGenerarRemito
            //
            btnGenerarRemito.Enabled = false;
            btnGenerarRemito.Location = new Point(157, 326);
            btnGenerarRemito.Name = "btnGenerarRemito";
            btnGenerarRemito.Size = new Size(99, 40);
            btnGenerarRemito.TabIndex = 7;
            btnGenerarRemito.Text = "Generar Remito";
            btnGenerarRemito.UseVisualStyleBackColor = true;
            btnGenerarRemito.Click += btnGenerarRemito_Click;
            //
            // EntregaATransportistaForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 486);
            Controls.Add(btnGenerarRemito);
            Controls.Add(chkRemitoFirmado);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmarDespacho);
            Controls.Add(btnBuscar);
            Controls.Add(lstDetalle);
            Controls.Add(lblDetalle);
            Controls.Add(txtDni);
            Controls.Add(txtTransportista);
            Controls.Add(lblTransportista);
            Controls.Add(lblDni);
            Name = "EntregaATransportistaForm";
            Text = "Entrega a transportista";
            FormClosing += EntregaATransportistaForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblDni;
        private Label lblTransportista;
        private TextBox txtTransportista;
        private TextBox txtDni;
        private Label lblDetalle;
        private ListView lstDetalle;
        private ColumnHeader colOrden;
        private ColumnHeader colPaquetes;
        private Button btnBuscar;
        private Button btnConfirmarDespacho;
        private Button btnCancelar;
        private CheckBox chkRemitoFirmado;
        private Button btnGenerarRemito;
    }
}
