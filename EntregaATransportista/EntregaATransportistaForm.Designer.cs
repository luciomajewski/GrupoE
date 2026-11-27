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
            lblDni.Location = new Point(16, 16);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(27, 15);
            lblDni.TabIndex = 0;
            lblDni.Text = "DNI";
            //
            // lblTransportista
            //
            lblTransportista.AutoSize = true;
            lblTransportista.Location = new Point(16, 72);
            lblTransportista.Name = "lblTransportista";
            lblTransportista.Size = new Size(75, 15);
            lblTransportista.TabIndex = 3;
            lblTransportista.Text = "Transportista";
            //
            // txtTransportista
            //
            txtTransportista.Location = new Point(16, 90);
            txtTransportista.MaxLength = 50;
            txtTransportista.Name = "txtTransportista";
            txtTransportista.ReadOnly = true;
            txtTransportista.Size = new Size(388, 23);
            txtTransportista.TabIndex = 4;
            txtTransportista.TabStop = false;
            //
            // txtDni
            //
            txtDni.Location = new Point(16, 34);
            txtDni.MaxLength = 8;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(232, 23);
            txtDni.TabIndex = 1;
            txtDni.TextChanged += txtDni_TextChanged;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(16, 130);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDetalle.Size = new Size(122, 15);
            lblDetalle.TabIndex = 5;
            lblDetalle.Text = "Detalle de la entrega";
            //
            // lstDetalle
            //
            lstDetalle.Columns.AddRange(new ColumnHeader[] { colOrden, colPaquetes });
            lstDetalle.FullRowSelect = true;
            lstDetalle.Location = new Point(16, 150);
            lstDetalle.MultiSelect = false;
            lstDetalle.Name = "lstDetalle";
            lstDetalle.GridLines = true;
            lstDetalle.Size = new Size(388, 170);
            lstDetalle.TabIndex = 6;
            lstDetalle.UseCompatibleStateImageBehavior = false;
            lstDetalle.View = View.Details;
            //
            // colOrden
            //
            colOrden.Text = "Orden";
            colOrden.Width = 200;
            //
            // colPaquetes
            //
            colPaquetes.Text = "Paquetes";
            colPaquetes.TextAlign = HorizontalAlignment.Right;
            colPaquetes.Width = 120;
            //
            // btnBuscar
            //
            btnBuscar.BackColor = SystemColors.Control;
            btnBuscar.FlatStyle = FlatStyle.Popup;
            btnBuscar.Location = new Point(260, 33);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(144, 25);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            //
            // btnConfirmarDespacho
            //
            btnConfirmarDespacho.Enabled = false;
            btnConfirmarDespacho.Location = new Point(264, 388);
            btnConfirmarDespacho.Name = "btnConfirmarDespacho";
            btnConfirmarDespacho.Size = new Size(140, 34);
            btnConfirmarDespacho.TabIndex = 10;
            btnConfirmarDespacho.Text = "Confirmar despacho";
            btnConfirmarDespacho.UseVisualStyleBackColor = true;
            btnConfirmarDespacho.Click += btnConfirmarDespacho_Click;
            //
            // btnCancelar
            //
            btnCancelar.Location = new Point(156, 388);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            //
            // chkRemitoFirmado
            //
            chkRemitoFirmado.AutoSize = true;
            chkRemitoFirmado.Enabled = false;
            chkRemitoFirmado.Location = new Point(172, 338);
            chkRemitoFirmado.Name = "chkRemitoFirmado";
            chkRemitoFirmado.Size = new Size(111, 19);
            chkRemitoFirmado.TabIndex = 8;
            chkRemitoFirmado.Text = "Remito firmado";
            chkRemitoFirmado.UseVisualStyleBackColor = true;
            chkRemitoFirmado.CheckedChanged += chkRemitoFirmado_CheckedChanged;
            //
            // btnGenerarRemito
            //
            btnGenerarRemito.Enabled = false;
            btnGenerarRemito.Location = new Point(16, 332);
            btnGenerarRemito.Name = "btnGenerarRemito";
            btnGenerarRemito.Size = new Size(140, 30);
            btnGenerarRemito.TabIndex = 7;
            btnGenerarRemito.Text = "Generar remito";
            btnGenerarRemito.UseVisualStyleBackColor = true;
            btnGenerarRemito.Click += btnGenerarRemito_Click;
            //
            // EntregaATransportistaForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 438);
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
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
