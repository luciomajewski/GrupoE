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
            Prodaprep.Location = new Point(16, 111);
            Prodaprep.Margin = new Padding(2, 0, 2, 0);
            Prodaprep.Name = "Prodaprep";
            Prodaprep.Size = new Size(179, 25);
            Prodaprep.TabIndex = 3;
            Prodaprep.Text = "Productos a preparar";
            // 
            // listViewProdPrep
            // 
            listViewProdPrep.Columns.AddRange(new ColumnHeader[] { Producto, Cantidad, Tipo_de_Pedido });
            listViewProdPrep.Location = new Point(16, 189);
            listViewProdPrep.Margin = new Padding(2, 4, 2, 4);
            listViewProdPrep.Name = "listViewProdPrep";
            listViewProdPrep.Size = new Size(675, 499);
            listViewProdPrep.TabIndex = 4;
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
            Cantidad.Width = 140;
            // 
            // Tipo_de_Pedido
            // 
            Tipo_de_Pedido.Text = "Tipo_de_pedido";
            Tipo_de_Pedido.Width = 140;
            // 
            // buttonConfIEmp
            // 
            buttonConfIEmp.BackColor = SystemColors.ControlLight;
            buttonConfIEmp.Location = new Point(16, 696);
            buttonConfIEmp.Margin = new Padding(2, 4, 2, 4);
            buttonConfIEmp.Name = "buttonConfIEmp";
            buttonConfIEmp.Size = new Size(690, 64);
            buttonConfIEmp.TabIndex = 24;
            buttonConfIEmp.Text = "Confirmar ingreso";
            buttonConfIEmp.UseVisualStyleBackColor = false;
            buttonConfIEmp.Click += buttonConfIEmp_Click;
            // 
            // OrdenPrepNro
            // 
            OrdenPrepNro.AutoSize = true;
            OrdenPrepNro.Font = new Font("Segoe UI", 15F);
            OrdenPrepNro.Location = new Point(398, 34);
            OrdenPrepNro.Margin = new Padding(4, 0, 4, 0);
            OrdenPrepNro.Name = "OrdenPrepNro";
            OrdenPrepNro.Size = new Size(318, 41);
            OrdenPrepNro.TabIndex = 25;
            OrdenPrepNro.Text = "[Orden de prep. Nro...]";
            // 
            // EmpaquetarPedidoForm
            // 
            AccessibleName = "";
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(722, 815);
            Controls.Add(OrdenPrepNro);
            Controls.Add(buttonConfIEmp);
            Controls.Add(listViewProdPrep);
            Controls.Add(Prodaprep);
            Margin = new Padding(2, 4, 2, 4);
            Name = "EmpaquetarPedidoForm";
            Text = "Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form";
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