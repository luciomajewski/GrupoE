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
            PalletCol = new ColumnHeader();
            checkBoxPalletCliente = new CheckBox();
            buttonNuevoPallet = new Button();
            labelPalletActual = new Label();
            buttonQuitIRem = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // Remito
            // 
            Remito.FormattingEnabled = true;
            Remito.Location = new Point(24, 20);
            Remito.Margin = new Padding(2, 3, 2, 3);
            Remito.Name = "Remito";
            Remito.Size = new Size(792, 150);
            Remito.TabIndex = 17;
            Remito.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // ClieRem
            // 
            ClieRem.AutoSize = true;
            ClieRem.BackColor = SystemColors.ButtonHighlight;
            ClieRem.ForeColor = SystemColors.ActiveCaptionText;
            ClieRem.Location = new Point(40, 43);
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
            NRem.Location = new Point(40, 83);
            NRem.Margin = new Padding(2, 0, 2, 0);
            NRem.Name = "NRem";
            NRem.Size = new Size(78, 20);
            NRem.TabIndex = 5;
            NRem.Text = "N° Remito";
            // 
            // TranspRem
            // 
            TranspRem.AutoSize = true;
            TranspRem.BackColor = SystemColors.ButtonHighlight;
            TranspRem.ForeColor = SystemColors.ActiveCaptionText;
            TranspRem.Location = new Point(40, 123);
            TranspRem.Margin = new Padding(2, 0, 2, 0);
            TranspRem.Name = "TranspRem";
            TranspRem.Size = new Size(94, 20);
            TranspRem.TabIndex = 9;
            TranspRem.Text = "Transportista";
            // 
            // comboBoxClieR
            // 
            comboBoxClieR.FormattingEnabled = true;
            comboBoxClieR.Location = new Point(176, 40);
            comboBoxClieR.Margin = new Padding(2, 3, 2, 3);
            comboBoxClieR.Name = "comboBoxClieR";
            comboBoxClieR.Size = new Size(196, 28);
            comboBoxClieR.TabIndex = 0;
            comboBoxClieR.SelectedIndexChanged += comboBoxClieR_SelectedIndexChanged;
            comboBoxClieR.KeyPress += comboBoxClieR_KeyPress;
            comboBoxClieR.Leave += comboBoxClieR_Leave;
            // 
            // textBoxNRem
            // 
            textBoxNRem.Location = new Point(176, 80);
            textBoxNRem.Margin = new Padding(2, 3, 2, 3);
            textBoxNRem.Name = "textBoxNRem";
            textBoxNRem.Size = new Size(196, 27);
            textBoxNRem.TabIndex = 1;
            textBoxNRem.TextChanged += textBoxNRem_TextChanged;
            textBoxNRem.KeyPress += textBoxNRem_KeyPress;
            textBoxNRem.Leave += textBoxNRem_Leave;
            // 
            // textBoxTransRem
            // 
            textBoxTransRem.Location = new Point(176, 120);
            textBoxTransRem.Margin = new Padding(2, 3, 2, 3);
            textBoxTransRem.Name = "textBoxTransRem";
            textBoxTransRem.Size = new Size(196, 27);
            textBoxTransRem.TabIndex = 3;
            textBoxTransRem.KeyPress += textBoxTransRem_KeyPress;
            // 
            // dateTimePickerFRem
            // 
            dateTimePickerFRem.Location = new Point(548, 40);
            dateTimePickerFRem.Margin = new Padding(2, 3, 2, 3);
            dateTimePickerFRem.Name = "dateTimePickerFRem";
            dateTimePickerFRem.Size = new Size(252, 27);
            dateTimePickerFRem.TabIndex = 2;
            // 
            // FecRem
            // 
            FecRem.AutoSize = true;
            FecRem.BackColor = SystemColors.ButtonHighlight;
            FecRem.ForeColor = SystemColors.ActiveCaptionText;
            FecRem.Location = new Point(412, 43);
            FecRem.Margin = new Padding(2, 0, 2, 0);
            FecRem.Name = "FecRem";
            FecRem.Size = new Size(124, 20);
            FecRem.TabIndex = 3;
            FecRem.Text = "Fecha del Remito";
            // 
            // NARem
            // 
            NARem.AutoSize = true;
            NARem.BackColor = SystemColors.ButtonHighlight;
            NARem.ForeColor = SystemColors.ActiveCaptionText;
            NARem.Location = new Point(412, 83);
            NARem.Margin = new Padding(2, 0, 2, 0);
            NARem.Name = "NARem";
            NARem.Size = new Size(136, 20);
            NARem.TabIndex = 7;
            NARem.Text = "Nombre y Apellido";
            // 
            // textBoxNARem
            // 
            textBoxNARem.Location = new Point(548, 80);
            textBoxNARem.Margin = new Padding(2, 3, 2, 3);
            textBoxNARem.Name = "textBoxNARem";
            textBoxNARem.Size = new Size(252, 27);
            textBoxNARem.TabIndex = 4;
            textBoxNARem.KeyPress += textBoxNARem_KeyPress;
            // 
            // PatenR
            // 
            PatenR.AutoSize = true;
            PatenR.BackColor = SystemColors.ButtonHighlight;
            PatenR.ForeColor = SystemColors.ActiveCaptionText;
            PatenR.Location = new Point(412, 123);
            PatenR.Margin = new Padding(2, 0, 2, 0);
            PatenR.Name = "PatenR";
            PatenR.Size = new Size(58, 20);
            PatenR.TabIndex = 11;
            PatenR.Text = "Patente";
            // 
            // As
            // 
            As.Location = new Point(548, 120);
            As.Margin = new Padding(2, 3, 2, 3);
            As.Name = "As";
            As.Size = new Size(252, 27);
            As.TabIndex = 5;
            // 
            // CantRem
            // 
            CantRem.AutoSize = true;
            CantRem.BackColor = Color.Transparent;
            CantRem.ForeColor = SystemColors.ActiveCaptionText;
            CantRem.Location = new Point(322, 192);
            CantRem.Margin = new Padding(2, 0, 2, 0);
            CantRem.Name = "CantRem";
            CantRem.Size = new Size(94, 20);
            CantRem.TabIndex = 15;
            CantRem.Text = "Cant. Remito";
            CantRem.Click += label10_Click;
            // 
            // CantContRem
            // 
            CantContRem.AutoSize = true;
            CantContRem.BackColor = Color.Transparent;
            CantContRem.ForeColor = SystemColors.ActiveCaptionText;
            CantContRem.Location = new Point(188, 192);
            CantContRem.Margin = new Padding(2, 0, 2, 0);
            CantContRem.Name = "CantContRem";
            CantContRem.Size = new Size(102, 20);
            CantContRem.TabIndex = 14;
            CantContRem.Text = "Cant. Contada";
            CantContRem.Click += label11_Click;
            // 
            // buttonAgregIRem
            // 
            buttonAgregIRem.BackColor = SystemColors.ButtonFace;
            buttonAgregIRem.Location = new Point(456, 214);
            buttonAgregIRem.Margin = new Padding(2, 3, 2, 3);
            buttonAgregIRem.Name = "buttonAgregIRem";
            buttonAgregIRem.Size = new Size(140, 31);
            buttonAgregIRem.TabIndex = 11;
            buttonAgregIRem.Text = "+ Agregar ítem";
            buttonAgregIRem.UseVisualStyleBackColor = true;
            buttonAgregIRem.Click += buttonAgregIRem_Click;
            // 
            // textBoxSKURem
            // 
            textBoxSKURem.Location = new Point(24, 216);
            textBoxSKURem.Margin = new Padding(2, 3, 2, 3);
            textBoxSKURem.Name = "textBoxSKURem";
            textBoxSKURem.Size = new Size(150, 27);
            textBoxSKURem.TabIndex = 8;
            // 
            // CancRem
            // 
            CancRem.BackColor = SystemColors.ButtonFace;
            CancRem.Location = new Point(504, 596);
            CancRem.Margin = new Padding(2, 3, 2, 3);
            CancRem.Name = "CancRem";
            CancRem.Size = new Size(128, 34);
            CancRem.TabIndex = 15;
            CancRem.Text = "Cancelar";
            CancRem.UseVisualStyleBackColor = true;
            CancRem.Click += CancRem_Click;
            // 
            // ConfRem
            // 
            ConfRem.BackColor = SystemColors.ButtonFace;
            ConfRem.Location = new Point(640, 596);
            ConfRem.Margin = new Padding(2, 3, 2, 3);
            ConfRem.Name = "ConfRem";
            ConfRem.Size = new Size(176, 34);
            ConfRem.TabIndex = 16;
            ConfRem.Text = "Confirmar Recepción";
            ConfRem.UseVisualStyleBackColor = true;
            ConfRem.Click += button5_Click;
            // 
            // ObsRem
            // 
            ObsRem.AutoSize = true;
            ObsRem.BackColor = Color.Transparent;
            ObsRem.ForeColor = SystemColors.ActiveCaptionText;
            ObsRem.Location = new Point(24, 520);
            ObsRem.Margin = new Padding(2, 0, 2, 0);
            ObsRem.Name = "ObsRem";
            ObsRem.Size = new Size(108, 20);
            ObsRem.TabIndex = 26;
            ObsRem.Text = "Observaciones:";
            // 
            // textBoxObsRem
            // 
            textBoxObsRem.Location = new Point(24, 544);
            textBoxObsRem.Margin = new Padding(2, 3, 2, 3);
            textBoxObsRem.Name = "textBoxObsRem";
            textBoxObsRem.Size = new Size(792, 27);
            textBoxObsRem.TabIndex = 14;
            textBoxObsRem.PlaceholderText = "Caja con golpes visibles...";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // SKUREM
            // 
            SKUREM.AutoSize = true;
            SKUREM.BackColor = Color.Transparent;
            SKUREM.ForeColor = SystemColors.ActiveCaptionText;
            SKUREM.Location = new Point(24, 192);
            SKUREM.Margin = new Padding(2, 0, 2, 0);
            SKUREM.Name = "SKUREM";
            SKUREM.Size = new Size(95, 20);
            SKUREM.TabIndex = 13;
            SKUREM.Text = "SKU / Código";
            SKUREM.Click += label7_Click_1;
            // 
            // textBoxCatnCRem
            // 
            textBoxCatnCRem.Location = new Point(188, 216);
            textBoxCatnCRem.Margin = new Padding(2, 3, 2, 3);
            textBoxCatnCRem.Name = "textBoxCatnCRem";
            textBoxCatnCRem.Size = new Size(120, 27);
            textBoxCatnCRem.TabIndex = 9;
            textBoxCatnCRem.KeyPress += textBoxCatnCRem_KeyPress;
            // 
            // textBoxCanRem
            // 
            textBoxCanRem.Location = new Point(322, 216);
            textBoxCanRem.Margin = new Padding(2, 3, 2, 3);
            textBoxCanRem.Name = "textBoxCanRem";
            textBoxCanRem.Size = new Size(120, 27);
            textBoxCanRem.TabIndex = 10;
            textBoxCanRem.KeyPress += textBoxCanRem_KeyPress;
            // 
            // DetMercRRem
            // 
            DetMercRRem.AutoSize = true;
            DetMercRRem.BackColor = Color.Transparent;
            DetMercRRem.ForeColor = SystemColors.ActiveCaptionText;
            DetMercRRem.Location = new Point(24, 268);
            DetMercRRem.Margin = new Padding(2, 0, 2, 0);
            DetMercRRem.Name = "DetMercRRem";
            DetMercRRem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            DetMercRRem.Size = new Size(219, 20);
            DetMercRRem.TabIndex = 22;
            DetMercRRem.Text = "Detalle de mercadería recibida";
            DetMercRRem.Click += label8_Click;
            // 
            // listViewRem
            // 
            listViewRem.Columns.AddRange(new ColumnHeader[] { SkuCodigo, Descripción, CantcR, CantR, PalletCol });
            listViewRem.Location = new Point(24, 294);
            listViewRem.Name = "listViewRem";
            listViewRem.GridLines = true;
            listViewRem.FullRowSelect = true;
            listViewRem.Size = new Size(792, 170);
            listViewRem.TabIndex = 12;
            listViewRem.UseCompatibleStateImageBehavior = false;
            listViewRem.View = View.Details;
            listViewRem.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // SkuCodigo
            // 
            SkuCodigo.Text = "SKU";
            SkuCodigo.Width = 100;
            // 
            // Descripción
            // 
            Descripción.Text = "Descripción";
            Descripción.Width = 260;
            // 
            // CantcR
            // 
            CantcR.Text = "Cant. contada";
            CantcR.Width = 100;
            // 
            // CantR
            // 
            CantR.Text = "Cant. remito";
            CantR.Width = 100;
            // 
            // PalletCol
            // 
            PalletCol.Text = "Pallet";
            PalletCol.Width = 100;
            // 
            // checkBoxPalletCliente
            // 
            checkBoxPalletCliente.AutoSize = true;
            checkBoxPalletCliente.Location = new Point(616, 191);
            checkBoxPalletCliente.Name = "checkBoxPalletCliente";
            checkBoxPalletCliente.Size = new Size(191, 24);
            checkBoxPalletCliente.TabIndex = 6;
            checkBoxPalletCliente.Text = "Pallet armado por el cliente";
            checkBoxPalletCliente.UseVisualStyleBackColor = true;
            checkBoxPalletCliente.CheckedChanged += checkBoxPalletCliente_CheckedChanged;
            // 
            // buttonNuevoPallet
            // 
            buttonNuevoPallet.BackColor = SystemColors.ButtonFace;
            buttonNuevoPallet.Enabled = false;
            buttonNuevoPallet.Location = new Point(616, 214);
            buttonNuevoPallet.Margin = new Padding(2, 3, 2, 3);
            buttonNuevoPallet.Name = "buttonNuevoPallet";
            buttonNuevoPallet.Size = new Size(140, 31);
            buttonNuevoPallet.TabIndex = 7;
            buttonNuevoPallet.Text = "Nuevo pallet";
            buttonNuevoPallet.UseVisualStyleBackColor = true;
            buttonNuevoPallet.Click += buttonNuevoPallet_Click;
            // 
            // labelPalletActual
            // 
            labelPalletActual.AutoSize = true;
            labelPalletActual.Location = new Point(616, 268);
            labelPalletActual.Name = "labelPalletActual";
            labelPalletActual.Size = new Size(140, 20);
            labelPalletActual.TabIndex = 23;
            labelPalletActual.Text = "Pallet: carga suelta";
            // 
            // buttonQuitIRem
            // 
            buttonQuitIRem.BackColor = SystemColors.ButtonFace;
            buttonQuitIRem.Location = new Point(24, 472);
            buttonQuitIRem.Margin = new Padding(2, 3, 2, 3);
            buttonQuitIRem.Name = "buttonQuitIRem";
            buttonQuitIRem.Size = new Size(140, 31);
            buttonQuitIRem.TabIndex = 13;
            buttonQuitIRem.Text = "− Quitar ítem";
            buttonQuitIRem.UseVisualStyleBackColor = true;
            buttonQuitIRem.Click += buttonQuitIRem_Click;
            // 
            // RecepcionMercaderiaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(840, 650);
            Controls.Add(labelPalletActual);
            Controls.Add(buttonNuevoPallet);
            Controls.Add(checkBoxPalletCliente);
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
            Cursor = Cursors.Default;
            Margin = new Padding(2, 3, 2, 3);
            Name = "RecepcionMercaderiaForm";
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Recepción de mercadería";
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
        private ColumnHeader PalletCol;
        private CheckBox checkBoxPalletCliente;
        private Button buttonNuevoPallet;
        private Label labelPalletActual;
    }
}