namespace GrupoE.CumplirOrdenSeleccion
{
    partial class CumplirOrdenSeleccionForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblOrdenTitulo = new Label();
            lblOrden = new Label();
            lblFechaTitulo = new Label();
            lblFecha = new Label();
            lblMercaderia = new Label();
            lvMercaderia = new ListView();
            colPosicion = new ColumnHeader();
            colSKU = new ColumnHeader();
            colDescripcion = new ColumnHeader();
            colCantidad = new ColumnHeader();
            btnConfirmar = new Button();
            SuspendLayout();
            // 
            // lblOrdenTitulo
            // 
            lblOrdenTitulo.AutoSize = true;
            lblOrdenTitulo.Location = new Point(16, 16);
            lblOrdenTitulo.Name = "lblOrdenTitulo";
            lblOrdenTitulo.ForeColor = Color.FromArgb(108, 117, 125);
            lblOrdenTitulo.Size = new Size(108, 15);
            lblOrdenTitulo.TabIndex = 0;
            lblOrdenTitulo.Text = "Orden de selección";
            // 
            // lblOrden
            // 
            lblOrden.AutoSize = true;
            lblOrden.Location = new Point(16, 34);
            lblOrden.Name = "lblOrden";
            lblOrden.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblOrden.Size = new Size(33, 15);
            lblOrden.TabIndex = 1;
            lblOrden.Text = "[nro]";
            // 
            // lblFechaTitulo
            // 
            lblFechaTitulo.AutoSize = true;
            lblFechaTitulo.Location = new Point(240, 16);
            lblFechaTitulo.Name = "lblFechaTitulo";
            lblFechaTitulo.ForeColor = Color.FromArgb(108, 117, 125);
            lblFechaTitulo.Size = new Size(38, 15);
            lblFechaTitulo.TabIndex = 2;
            lblFechaTitulo.Text = "Fecha";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(240, 34);
            lblFecha.Name = "lblFecha";
            lblFecha.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFecha.Size = new Size(96, 15);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "dd/MM/yy [hoy]";
            // 
            // lblMercaderia
            // 
            lblMercaderia.AutoSize = true;
            lblMercaderia.Location = new Point(16, 80);
            lblMercaderia.Name = "lblMercaderia";
            lblMercaderia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMercaderia.Size = new Size(104, 15);
            lblMercaderia.TabIndex = 4;
            lblMercaderia.Text = "Mercadería a bajar";
            // 
            // lvMercaderia
            // 
            lvMercaderia.Columns.AddRange(new ColumnHeader[] { colPosicion, colSKU, colDescripcion, colCantidad });
            lvMercaderia.FullRowSelect = true;
            lvMercaderia.GridLines = true;
            lvMercaderia.Location = new Point(16, 100);
            lvMercaderia.MultiSelect = false;
            lvMercaderia.Name = "lvMercaderia";
            lvMercaderia.Size = new Size(652, 316);
            lvMercaderia.TabIndex = 5;
            lvMercaderia.UseCompatibleStateImageBehavior = false;
            lvMercaderia.View = View.Details;
            // 
            // colPosicion
            // 
            colPosicion.Text = "Posición";
            colPosicion.Width = 110;
            // 
            // colSKU
            // 
            colSKU.Text = "SKU";
            colSKU.Width = 110;
            // 
            // colDescripcion
            // 
            colDescripcion.Text = "Descripción";
            colDescripcion.Width = 300;
            // 
            // colCantidad
            // 
            colCantidad.Text = "Cantidad";
            colCantidad.Width = 90;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(436, 432);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(232, 34);
            btnConfirmar.TabIndex = 6;
            btnConfirmar.Text = "Confirmar entrega a Preparación";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // CumplirOrdenSeleccionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 482);
            Controls.Add(btnConfirmar);
            Controls.Add(lvMercaderia);
            Controls.Add(lblMercaderia);
            Controls.Add(lblFecha);
            Controls.Add(lblFechaTitulo);
            Controls.Add(lblOrden);
            Controls.Add(lblOrdenTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CumplirOrdenSeleccionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cumplir orden de selección";
            Shown += CumplirOrdenSeleccionForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblOrdenTitulo;
        private Label lblOrden;
        private Label lblFechaTitulo;
        private Label lblFecha;
        private Label lblMercaderia;
        private ListView lvMercaderia;
        private ColumnHeader colPosicion;
        private ColumnHeader colSKU;
        private ColumnHeader colDescripcion;
        private ColumnHeader colCantidad;
        private Button btnConfirmar;
    }
}