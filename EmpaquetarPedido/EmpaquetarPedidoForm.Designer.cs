namespace GrupoE.EmpaquetarPedido
{
    partial class EmpaquetarPedidoForm
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
            Prodaprep = new Label();
            listViewProdPrep = new ListView();
            Producto = new ColumnHeader();
            Cantidad = new ColumnHeader();
            Tipo_de_Pedido = new ColumnHeader();
            buttonConfIEmp = new Button();
            OrdenPrepNro = new Label();
            SuspendLayout();
            // 
            // Prodaprep
            // 
            Prodaprep.AutoSize = true;
            Prodaprep.Location = new Point(20, 84);
            Prodaprep.Margin = new Padding(2, 0, 2, 0);
            Prodaprep.Name = "Prodaprep";
            Prodaprep.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Prodaprep.Size = new Size(179, 25);
            Prodaprep.TabIndex = 1;
            Prodaprep.Text = "Productos a preparar";
            // 
            // listViewProdPrep
            // 
            listViewProdPrep.Columns.AddRange(new ColumnHeader[] { Producto, Cantidad, Tipo_de_Pedido });
            listViewProdPrep.Location = new Point(20, 116);
            listViewProdPrep.Margin = new Padding(2, 4, 2, 4);
            listViewProdPrep.Name = "listViewProdPrep";
            listViewProdPrep.GridLines = true;
            listViewProdPrep.FullRowSelect = true;
            listViewProdPrep.Size = new Size(680, 470);
            listViewProdPrep.TabIndex = 2;
            listViewProdPrep.UseCompatibleStateImageBehavior = false;
            listViewProdPrep.View = View.Details;
            // listViewProdPrep.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // Producto
            // 
            Producto.Text = "Producto";
            Producto.Width = 140;
            // 
            // Cantidad
            // 
            Cantidad.Text = "Cantidad";
            Cantidad.Width = 100;
            // 
            // Tipo_de_Pedido
            // 
            Tipo_de_Pedido.Text = "Tipo de pedido";
            Tipo_de_Pedido.Width = 160;
            // 
            // buttonConfIEmp
            // 
            buttonConfIEmp.BackColor = SystemColors.Control;
            buttonConfIEmp.Location = new Point(440, 604);
            buttonConfIEmp.Margin = new Padding(2, 4, 2, 4);
            buttonConfIEmp.Name = "buttonConfIEmp";
            buttonConfIEmp.Size = new Size(260, 50);
            buttonConfIEmp.TabIndex = 3;
            buttonConfIEmp.Text = "Confirmar ingreso";
            buttonConfIEmp.UseVisualStyleBackColor = true;
            buttonConfIEmp.Click += buttonConfIEmp_Click;
            // 
            // OrdenPrepNro
            // 
            OrdenPrepNro.AutoSize = true;
            OrdenPrepNro.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            OrdenPrepNro.Location = new Point(20, 20);
            OrdenPrepNro.Margin = new Padding(4, 0, 4, 0);
            OrdenPrepNro.Name = "OrdenPrepNro";
            OrdenPrepNro.Size = new Size(318, 41);
            OrdenPrepNro.TabIndex = 0;
            OrdenPrepNro.Text = "[Orden de prep. Nro...]";
            // 
            // EmpaquetarPedidoForm
            // 
            AccessibleName = "";
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 676);
            Controls.Add(OrdenPrepNro);
            Controls.Add(buttonConfIEmp);
            Controls.Add(listViewProdPrep);
            Controls.Add(Prodaprep);
            Margin = new Padding(2, 4, 2, 4);
            Name = "EmpaquetarPedidoForm";
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Empaquetar pedido y generar orden de entrega";
            FormClosing += EmpaquetarPedidoForm_FormClosing;
            Load += EmpaquetarPedidoForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label Prodaprep;
        private ListView listViewProdPrep;
        private ColumnHeader Producto;
        private ColumnHeader Cantidad;
        private ColumnHeader Tipo_de_Pedido;
        private Button buttonConfIEmp;
        private Label OrdenPrepNro;
    }
}