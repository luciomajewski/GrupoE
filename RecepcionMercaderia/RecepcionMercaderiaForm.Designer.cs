namespace GrupoE.RecepcionMercaderia
{
    partial class RecepcionMercaderiaForm
    {
        
        private System.ComponentModel.IContainer components = null;

        
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

        
        private void InitializeComponent()
        {
            Remito = new ListBox();
            ClieRem = new Label();
            NRem = new Label();
            TranspRem = new Label();
            comboBoxClieR = new ComboBox();
            textBoxNRem = new TextBox();
            textBoxTransRem = new TextBox();
            dateTimePickerFRem = new DateTimePicker();
            FecRem = new Label();
            NARem = new Label();
            textBoxNARem = new TextBox();
            PatenR = new Label();
            As = new TextBox();
            CantRem = new Label();
            CantContRem = new Label();
            buttonAgregIRem = new Button();
            textBoxSKURem = new TextBox();
            CancRem = new Button();
            ConfRem = new Button();
            ObsRem = new Label();
            textBoxObsRem = new TextBox();
            fileSystemWatcher1 = new FileSystemWatcher();
            SKUREM = new Label();
            textBoxCatnCRem = new TextBox();
            textBoxCanRem = new TextBox();
            DetMercRRem = new Label();
            listViewRem = new ListView();
            SkuCodigo = new ColumnHeader();
            Descripción = new ColumnHeader();
            CantcR = new ColumnHeader();
            CantR = new ColumnHeader();
            buttonQuitIRem = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // Remito
            // 
            Remito.FormattingEnabled = true;
            Remito.Location = new Point(17, 27);
            Remito.Margin = new Padding(2, 3, 2, 3);
            Remito.Name = "Remito";
            Remito.Size = new Size(791, 144);
            Remito.TabIndex = 0;
            Remito.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // ClieRem
            // 
            ClieRem.AutoSize = true;
            ClieRem.BackColor = SystemColors.ButtonHighlight;
            ClieRem.ForeColor = SystemColors.ActiveCaptionText;
            ClieRem.Location = new Point(25, 67);
            ClieRem.Margin = new Padding(2, 0, 2, 0);
            ClieRem.Name = "ClieRem";
            ClieRem.Size = new Size(55, 20);
            ClieRem.TabIndex = 1;
            ClieRem.Text = "Cliente";
            // 
            // NRem
            // 
            NRem.AutoSize = true;
            NRem.BackColor = SystemColors.ButtonHighlight;
            NRem.ForeColor = SystemColors.ActiveCaptionText;
            NRem.Location = new Point(25, 96);
            NRem.Margin = new Padding(2, 0, 2, 0);
            NRem.Name = "NRem";
            NRem.Size = new Size(78, 20);
            NRem.TabIndex = 2;
            NRem.Text = "N° Remito";
            // 
            // TranspRem
            // 
            TranspRem.AutoSize = true;
            TranspRem.BackColor = SystemColors.ButtonHighlight;
            TranspRem.ForeColor = SystemColors.ActiveCaptionText;
            TranspRem.Location = new Point(25, 123);
            TranspRem.Margin = new Padding(2, 0, 2, 0);
            TranspRem.Name = "TranspRem";
            TranspRem.Size = new Size(94, 20);
            TranspRem.TabIndex = 3;
            TranspRem.Text = "Transportista";
            // 
            // comboBoxClieR
            // 
            comboBoxClieR.FormattingEnabled = true;
            comboBoxClieR.Location = new Point(97, 64);
            comboBoxClieR.Margin = new Padding(2, 3, 2, 3);
            comboBoxClieR.Name = "comboBoxClieR";
            comboBoxClieR.Size = new Size(246, 28);
            comboBoxClieR.TabIndex = 4;
            comboBoxClieR.SelectedIndexChanged += comboBoxClieR_SelectedIndexChanged;
            comboBoxClieR.KeyPress += comboBoxClieR_KeyPress;
            comboBoxClieR.Leave += comboBoxClieR_Leave;
            // 
            // textBoxNRem
            // 
            textBoxNRem.Location = new Point(102, 97);
            textBoxNRem.Margin = new Padding(2, 3, 2, 3);
            textBoxNRem.Name = "textBoxNRem";
            textBoxNRem.Size = new Size(241, 27);
            textBoxNRem.TabIndex = 5;
            textBoxNRem.TextChanged += textBoxNRem_TextChanged;
            textBoxNRem.KeyPress += textBoxNRem_KeyPress;
            textBoxNRem.Leave += textBoxNRem_Leave;
            // 
            // textBoxTransRem
            // 
            textBoxTransRem.Location = new Point(126, 125);
            textBoxTransRem.Margin = new Padding(2, 3, 2, 3);
            textBoxTransRem.Name = "textBoxTransRem";
            textBoxTransRem.Size = new Size(217, 27);
            textBoxTransRem.TabIndex = 6;
            textBoxTransRem.KeyPress += textBoxTransRem_KeyPress;
            // 
            // dateTimePickerFRem
            // 
            dateTimePickerFRem.Location = new Point(504, 61);
            dateTimePickerFRem.Margin = new Padding(2, 3, 2, 3);
            dateTimePickerFRem.Name = "dateTimePickerFRem";
            dateTimePickerFRem.Size = new Size(291, 27);
            dateTimePickerFRem.TabIndex = 7;
            // 
            // FecRem
            // 
            FecRem.AutoSize = true;
            FecRem.BackColor = SystemColors.ButtonHighlight;
            FecRem.ForeColor = SystemColors.ActiveCaptionText;
            FecRem.Location = new Point(363, 67);
            FecRem.Margin = new Padding(2, 0, 2, 0);
            FecRem.Name = "FecRem";
            FecRem.Size = new Size(124, 20);
            FecRem.TabIndex = 8;
            FecRem.Text = "Fecha del Remito";
            // 
            // NARem
            // 
            NARem.AutoSize = true;
            NARem.BackColor = SystemColors.ButtonHighlight;
            NARem.ForeColor = SystemColors.ActiveCaptionText;
            NARem.Location = new Point(363, 96);
            NARem.Margin = new Padding(2, 0, 2, 0);
            NARem.Name = "NARem";
            NARem.Size = new Size(136, 20);
            NARem.TabIndex = 9;
            NARem.Text = "Nombre y Apellido";
            // 
            // textBoxNARem
            // 
            textBoxNARem.Location = new Point(504, 93);
            textBoxNARem.Margin = new Padding(2, 3, 2, 3);
            textBoxNARem.Name = "textBoxNARem";
            textBoxNARem.Size = new Size(291, 27);
            textBoxNARem.TabIndex = 10;
            textBoxNARem.KeyPress += textBoxNARem_KeyPress;
            // 
            // PatenR
            // 
            PatenR.AutoSize = true;
            PatenR.BackColor = SystemColors.ButtonHighlight;
            PatenR.ForeColor = SystemColors.ActiveCaptionText;
            PatenR.Location = new Point(363, 131);
            PatenR.Margin = new Padding(2, 0, 2, 0);
            PatenR.Name = "PatenR";
            PatenR.Size = new Size(58, 20);
            PatenR.TabIndex = 11;
            PatenR.Text = "Patente";
            // 
            // As
            // 
            As.Location = new Point(504, 128);
            As.Margin = new Padding(2, 3, 2, 3);
            As.Name = "As";
            As.Size = new Size(291, 27);
            As.TabIndex = 12;
            // 
            // CantRem
            // 
            CantRem.AutoSize = true;
            CantRem.BackColor = SystemColors.ControlLight;
            CantRem.ForeColor = SystemColors.ActiveCaptionText;
            CantRem.Location = new Point(317, 188);
            CantRem.Margin = new Padding(2, 0, 2, 0);
            CantRem.Name = "CantRem";
            CantRem.Size = new Size(94, 20);
            CantRem.TabIndex = 17;
            CantRem.Text = "Cant. Remito";
            CantRem.Click += label10_Click;
            // 
            // CantContRem
            // 
            CantContRem.AutoSize = true;
            CantContRem.BackColor = SystemColors.ControlLight;
            CantContRem.ForeColor = SystemColors.ActiveCaptionText;
            CantContRem.Location = new Point(170, 188);
            CantContRem.Margin = new Padding(2, 0, 2, 0);
            CantContRem.Name = "CantContRem";
            CantContRem.Size = new Size(102, 20);
            CantContRem.TabIndex = 18;
            CantContRem.Text = "Cant. Contada";
            CantContRem.Click += label11_Click;
            // 
            // buttonAgregIRem
            // 
            buttonAgregIRem.BackColor = SystemColors.ButtonFace;
            buttonAgregIRem.Location = new Point(480, 225);
            buttonAgregIRem.Margin = new Padding(2, 3, 2, 3);
            buttonAgregIRem.Name = "buttonAgregIRem";
            buttonAgregIRem.Size = new Size(120, 27);
            buttonAgregIRem.TabIndex = 20;
            buttonAgregIRem.Text = "+ Agregar Ítem";
            buttonAgregIRem.UseVisualStyleBackColor = false;
            buttonAgregIRem.Click += buttonAgregIRem_Click;
            // 
            // textBoxSKURem
            // 
            textBoxSKURem.Location = new Point(17, 224);
            textBoxSKURem.Margin = new Padding(2, 3, 2, 3);
            textBoxSKURem.Name = "textBoxSKURem";
            textBoxSKURem.Size = new Size(138, 27);
            textBoxSKURem.TabIndex = 24;
            // 
            // CancRem
            // 
            CancRem.BackColor = SystemColors.ButtonFace;
            CancRem.Location = new Point(562, 533);
            CancRem.Margin = new Padding(2, 3, 2, 3);
            CancRem.Name = "CancRem";
            CancRem.Size = new Size(120, 27);
            CancRem.TabIndex = 33;
            CancRem.Text = "Cancelar";
            CancRem.UseVisualStyleBackColor = false;
            CancRem.Click += CancRem_Click;
            // 
            // ConfRem
            // 
            ConfRem.BackColor = SystemColors.ButtonFace;
            ConfRem.Location = new Point(688, 533);
            ConfRem.Margin = new Padding(2, 3, 2, 3);
            ConfRem.Name = "ConfRem";
            ConfRem.Size = new Size(120, 27);
            ConfRem.TabIndex = 34;
            ConfRem.Text = "Confirmar Recepeción";
            ConfRem.UseVisualStyleBackColor = false;
            ConfRem.Click += button5_Click;
            // 
            // ObsRem
            // 
            ObsRem.AutoSize = true;
            ObsRem.BackColor = SystemColors.ControlLight;
            ObsRem.ForeColor = SystemColors.ActiveCaptionText;
            ObsRem.Location = new Point(18, 471);
            ObsRem.Margin = new Padding(2, 0, 2, 0);
            ObsRem.Name = "ObsRem";
            ObsRem.Size = new Size(108, 20);
            ObsRem.TabIndex = 41;
            ObsRem.Text = "Observaciones:";
            // 
            // textBoxObsRem
            // 
            textBoxObsRem.Location = new Point(18, 493);
            textBoxObsRem.Margin = new Padding(2, 3, 2, 3);
            textBoxObsRem.Name = "textBoxObsRem";
            textBoxObsRem.Size = new Size(267, 27);
            textBoxObsRem.TabIndex = 42;
            textBoxObsRem.Text = "Caja con golpes visibles...";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // SKUREM
            // 
            SKUREM.AutoSize = true;
            SKUREM.BackColor = SystemColors.ControlLight;
            SKUREM.ForeColor = SystemColors.ActiveCaptionText;
            SKUREM.Location = new Point(17, 188);
            SKUREM.Margin = new Padding(2, 0, 2, 0);
            SKUREM.Name = "SKUREM";
            SKUREM.Size = new Size(95, 20);
            SKUREM.TabIndex = 43;
            SKUREM.Text = "SKU/ Código";
            SKUREM.Click += label7_Click_1;
            // 
            // textBoxCatnCRem
            // 
            textBoxCatnCRem.Location = new Point(170, 224);
            textBoxCatnCRem.Margin = new Padding(2, 3, 2, 3);
            textBoxCatnCRem.Name = "textBoxCatnCRem";
            textBoxCatnCRem.Size = new Size(138, 27);
            textBoxCatnCRem.TabIndex = 44;
            textBoxCatnCRem.KeyPress += textBoxCatnCRem_KeyPress;
            // 
            // textBoxCanRem
            // 
            textBoxCanRem.Location = new Point(317, 224);
            textBoxCanRem.Margin = new Padding(2, 3, 2, 3);
            textBoxCanRem.Name = "textBoxCanRem";
            textBoxCanRem.Size = new Size(138, 27);
            textBoxCanRem.TabIndex = 45;
            textBoxCanRem.KeyPress += textBoxCanRem_KeyPress;
            // 
            // DetMercRRem
            // 
            DetMercRRem.AutoSize = true;
            DetMercRRem.BackColor = SystemColors.ControlLight;
            DetMercRRem.ForeColor = SystemColors.ActiveCaptionText;
            DetMercRRem.Location = new Point(17, 272);
            DetMercRRem.Margin = new Padding(2, 0, 2, 0);
            DetMercRRem.Name = "DetMercRRem";
            DetMercRRem.Size = new Size(219, 20);
            DetMercRRem.TabIndex = 46;
            DetMercRRem.Text = "Detalle de Mercaderia Recibida";
            DetMercRRem.Click += label8_Click;
            // 
            // listViewRem
            // 
            listViewRem.Columns.AddRange(new ColumnHeader[] { SkuCodigo, Descripción, CantcR, CantR });
            listViewRem.Location = new Point(18, 309);
            listViewRem.Name = "listViewRem";
            listViewRem.Size = new Size(566, 119);
            listViewRem.TabIndex = 47;
            listViewRem.UseCompatibleStateImageBehavior = false;
            listViewRem.View = View.Details;
            listViewRem.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // SkuCodigo
            // 
            SkuCodigo.Text = "SkuCodigo";
            SkuCodigo.Width = 100;
            // 
            // Descripción
            // 
            Descripción.Text = "Descripción";
            Descripción.Width = 100;
            // 
            // CantcR
            // 
            CantcR.Text = "Cantidad contada";
            CantcR.Width = 100;
            // 
            // CantR
            // 
            CantR.Text = "Cantidad remito";
            CantR.Width = 100;
            // 
            // buttonQuitIRem
            // 
            buttonQuitIRem.BackColor = SystemColors.ButtonFace;
            buttonQuitIRem.Location = new Point(18, 432);
            buttonQuitIRem.Margin = new Padding(2, 3, 2, 3);
            buttonQuitIRem.Name = "buttonQuitIRem";
            buttonQuitIRem.Size = new Size(566, 27);
            buttonQuitIRem.TabIndex = 48;
            buttonQuitIRem.Text = "- Quitar Ítem";
            buttonQuitIRem.UseVisualStyleBackColor = false;
            buttonQuitIRem.Click += buttonQuitIRem_Click;
            // 
            // RecepcionMercaderiaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(825, 720);
            Controls.Add(buttonQuitIRem);
            Controls.Add(listViewRem);
            Controls.Add(DetMercRRem);
            Controls.Add(textBoxCanRem);
            Controls.Add(textBoxCatnCRem);
            Controls.Add(SKUREM);
            Controls.Add(textBoxObsRem);
            Controls.Add(ObsRem);
            Controls.Add(ConfRem);
            Controls.Add(CancRem);
            Controls.Add(textBoxSKURem);
            Controls.Add(buttonAgregIRem);
            Controls.Add(CantContRem);
            Controls.Add(CantRem);
            Controls.Add(As);
            Controls.Add(PatenR);
            Controls.Add(textBoxNARem);
            Controls.Add(NARem);
            Controls.Add(FecRem);
            Controls.Add(dateTimePickerFRem);
            Controls.Add(textBoxTransRem);
            Controls.Add(textBoxNRem);
            Controls.Add(comboBoxClieR);
            Controls.Add(TranspRem);
            Controls.Add(NRem);
            Controls.Add(ClieRem);
            Controls.Add(Remito);
            Cursor = Cursors.AppStarting;
            Margin = new Padding(2, 3, 2, 3);
            Name = "RecepcionMercaderiaForm";
            Text = "RecepcionMercaderia";
            FormClosing += RecepcionMercaderiaForm_FormClosing;
            Load += RecepcionMercaderiaForm_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox Remito;
        private Label ClieRem;
        private Label NRem;
        private Label TranspRem;
        private ComboBox comboBoxClieR;
        private TextBox textBoxNRem;
        private TextBox textBoxTransRem;
        private DateTimePicker dateTimePickerFRem;
        private Label FecRem;
        private Label NARem;
        private TextBox textBoxNARem;
        private Label PatenR;
        private TextBox As;
        private Label CantRem;
        private Label CantContRem;
        private Button buttonAgregIRem;
        private TextBox textBoxSKURem;
        private Button CancRem;
        private Button ConfRem;
        private Label ObsRem;
        private TextBox textBoxObsRem;
        private FileSystemWatcher fileSystemWatcher1;
        private Label SKUREM;
        private TextBox textBoxCanRem;
        private TextBox textBoxCatnCRem;
        private Label DetMercRRem;
        private ListView listViewRem;
        private ColumnHeader SkuCodigo;
        private ColumnHeader Descripción;
        private Button buttonQuitIRem;
        private ColumnHeader CantcR;
        private ColumnHeader CantR;
    }
}