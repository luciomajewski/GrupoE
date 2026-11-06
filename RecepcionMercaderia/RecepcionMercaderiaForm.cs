#nullable enable
namespace GrupoE.RecepcionMercaderia
{
    public partial class RecepcionMercaderiaForm : Form
    {
        private readonly RecepcionMercaderiaModelo modelo = new();

        public RecepcionMercaderiaForm()
        {
            InitializeComponent();
        }

        private void RecepcionMercaderiaForm_Load(object? sender, EventArgs e)
        {
            // Excepción paso 3.
            string? error = modelo.ValidarHayClientes();
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            comboBoxClieR.DisplayMember = nameof(Cliente.RazonSocial);
            comboBoxClieR.Items.Clear();
            comboBoxClieR.Items.AddRange(modelo.ClientesConContratoVigente.ToArray());

            dateTimePickerFRem.Value = DateTime.Today;
            listViewRem.Items.Clear();
        }

        private void comboBoxClieR_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = !modelo.AdmiteSoloLetras(e.KeyChar);
        }

        // El cliente escrito a mano debe coincidir con uno de la lista.
        private void comboBoxClieR_Leave(object? sender, EventArgs e)
        {
            string? error = modelo.ValidarClienteEscrito(comboBoxClieR.Text);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxClieR.SelectedItem = modelo.ClienteSeleccionado;
                comboBoxClieR.Text = modelo.ClienteSeleccionado?.RazonSocial ?? "";
                return;
            }

            comboBoxClieR.SelectedItem = modelo.BuscarCliente(comboBoxClieR.Text);
        }

        private void comboBoxClieR_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Cliente? nuevo = comboBoxClieR.SelectedItem as Cliente;
            if (nuevo == modelo.ClienteSeleccionado)
            {
                return;
            }

            string? pregunta = modelo.PreguntarCambioDeCliente(nuevo);
            if (pregunta != null && MessageBox.Show(pregunta, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                comboBoxClieR.SelectedItem = modelo.ClienteSeleccionado;
                return;
            }

            modelo.SeleccionarCliente(nuevo);
            listViewRem.Items.Clear();
        }

        // N° Remito: solo números; el guion se agrega solo después del 5° número.
        private void textBoxNRem_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = !modelo.AdmiteSoloNumeros(e.KeyChar);
        }

        private void textBoxNRem_TextChanged(object? sender, EventArgs e)
        {
            string formateado = modelo.FormatearNumeroRemito(textBoxNRem.Text);
            if (textBoxNRem.Text != formateado)
            {
                textBoxNRem.Text = formateado;
                textBoxNRem.SelectionStart = formateado.Length;
            }
        }

        // Excepción paso 6: el remito ya fue registrado para el cliente.
        private void textBoxNRem_Leave(object? sender, EventArgs e)
        {
            string? error = modelo.ValidarRemitoNoRegistrado(textBoxNRem.Text);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNRem.Clear();
                textBoxNRem.Focus();
            }
        }

        private void textBoxTransRem_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = !modelo.AdmiteSoloLetras(e.KeyChar);
        }

        private void textBoxNARem_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = !modelo.AdmiteSoloLetras(e.KeyChar);
        }

        private void textBoxCatnCRem_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = !modelo.AdmiteSoloNumeros(e.KeyChar);
        }

        private void textBoxCanRem_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = !modelo.AdmiteSoloNumeros(e.KeyChar);
        }

        private void buttonAgregIRem_Click(object? sender, EventArgs e)
        {
            string? error = modelo.AgregarItem(textBoxSKURem.Text, textBoxCatnCRem.Text, textBoxCanRem.Text);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (modelo.CodigoInvalido)
                {
                    textBoxSKURem.Clear();
                }
                if (modelo.CantidadesInvalidas)
                {
                    textBoxCatnCRem.Clear();
                    textBoxCanRem.Clear();
                }
                return;
            }

            listViewRem.Items.Clear();
            foreach (ItemRecepcion item in modelo.Items)
            {
                listViewRem.Items.Add(new ListViewItem(new[] { item.Codigo, item.Descripcion, item.CantidadContada.ToString(), item.CantidadRemito.ToString() }));
            }

            textBoxSKURem.Clear();
            textBoxCatnCRem.Clear();
            textBoxCanRem.Clear();
            textBoxSKURem.Focus();
        }

        // Excepción paso 11: quita el ítem seleccionado.
        private void buttonQuitIRem_Click(object? sender, EventArgs e)
        {
            int indice = listViewRem.SelectedIndices.Count > 0 ? listViewRem.SelectedIndices[0] : -1;

            string? error = modelo.QuitarItem(indice);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            listViewRem.Items.RemoveAt(indice);
        }

        // ConfRem ("Confirmar Recepción").
        private void button5_Click(object? sender, EventArgs e)
        {
            modelo.NumeroRemito = textBoxNRem.Text;
            modelo.FechaRemito = dateTimePickerFRem.Value;
            modelo.Transportista = textBoxTransRem.Text;
            modelo.Chofer = textBoxNARem.Text;
            modelo.Patente = As.Text;
            modelo.Observaciones = textBoxObsRem.Text;

            // Excepción paso 15.
            string? error = modelo.ValidarRecepcion();
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Paso 16 y excepción paso 17: con No se cierra el mensaje sin guardar nada.
            if (MessageBox.Show(modelo.PreguntaConfirmacion, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            foreach (string mensaje in modelo.RegistrarRecepcion())
            {
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            comboBoxClieR.SelectedIndex = -1;
            comboBoxClieR.Text = "";
            textBoxNRem.Clear();
            textBoxTransRem.Clear();
            dateTimePickerFRem.Value = DateTime.Today;
            textBoxNARem.Clear();
            As.Clear();
            textBoxSKURem.Clear();
            textBoxCatnCRem.Clear();
            textBoxCanRem.Clear();
            textBoxObsRem.Clear();
            listViewRem.Items.Clear();
        }

        // La pregunta de Cancelar se hace al cerrar la pantalla (RecepcionMercaderiaForm_FormClosing).
        private void CancRem_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // Cancelar o la X de la ventana: cartel con "Sí, descartar cambios" y "No".
        private void RecepcionMercaderiaForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (modelo.ValidarHayClientes() != null)
            {
                return;
            }

            var cartel = new Form
            {
                Text = "Cancelar",
                ClientSize = new Size(420, 120),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            var labelMensaje = new Label { Text = modelo.MensajeCancelacion, Location = new Point(20, 20), Size = new Size(380, 40) };
            var buttonDescartar = new Button { Text = "Sí, descartar cambios", Location = new Point(120, 70), Size = new Size(170, 32), DialogResult = DialogResult.Yes };
            var buttonNoCancelar = new Button { Text = "No", Location = new Point(300, 70), Size = new Size(100, 32), DialogResult = DialogResult.No };
            cartel.Controls.Add(labelMensaje);
            cartel.Controls.Add(buttonDescartar);
            cartel.Controls.Add(buttonNoCancelar);
            cartel.CancelButton = buttonNoCancelar;

            if (cartel.ShowDialog(this) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            cartel.Dispose();
        }

        // Remito: es el recuadro de fondo, no se usa para seleccionar.
        private void listBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Remito.ClearSelected();
        }

        // CantRem ("Cant. Remito"): el clic en la etiqueta lleva al campo.
        private void label10_Click(object? sender, EventArgs e)
        {
            textBoxCanRem.Focus();
        }

        // CantContRem ("Cant. Contada"): el clic en la etiqueta lleva al campo.
        private void label11_Click(object? sender, EventArgs e)
        {
            textBoxCatnCRem.Focus();
        }

        // SKUREM ("SKU/ Código"): el clic en la etiqueta lleva al campo.
        private void label7_Click_1(object? sender, EventArgs e)
        {
            textBoxSKURem.Focus();
        }

        // DetMercRRem ("Detalle de Mercaderia Recibida"): el clic en la etiqueta lleva al detalle.
        private void label8_Click(object? sender, EventArgs e)
        {
            listViewRem.Focus();
        }

        // listViewRem: el ítem seleccionado se usa en "- Quitar Ítem".
        private void listView1_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }
    }
}
