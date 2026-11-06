#nullable enable
namespace GrupoE.RegistrarOrdenDePreparacion
{
    public partial class RegistrarOrdenDePreparacionForm : Form
    {
        private readonly RegistrarOrdenDePreparacionModelo modelo = new();

        public RegistrarOrdenDePreparacionForm()
        {
            InitializeComponent();
        }

        private void RegistrarOrdenDePreparacionForm_Load(object? sender, EventArgs e)
        {
            labtemp.Text = modelo.NombreCliente;

            combodepo.DisplayMember = nameof(Deposito.Nombre);
            combodepo.Items.Clear();
            combodepo.Items.AddRange(modelo.Depositos.ToArray());

            comboprodretirar.DisplayMember = nameof(RegistrarOrdenDePreparacion.Producto.Nombre);
            comboprodretirar.Items.Clear();

            datefecha.MinDate = modelo.FechaMinimaRetiro;
            datefecha.Value = modelo.FechaRetiro;

            radiomodalidad_CheckedChanged(sender, e);
        }

        private void combodepo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string? mensaje = modelo.SeleccionarDeposito(combodepo.SelectedItem as Deposito);

            comboprodretirar.Items.Clear();
            comboprodretirar.Items.AddRange(modelo.ProductosDisponibles.ToArray());
            listordenact.Items.Clear();

            if (mensaje != null)
            {
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radiomodalidad_CheckedChanged(object? sender, EventArgs e)
        {
            ModalidadEmpaquetado? modalidad = null;
            if (radiocerrado.Checked)
            {
                modalidad = ModalidadEmpaquetado.PaletCerrado;
            }
            else if (radiofullfilment.Checked)
            {
                modalidad = ModalidadEmpaquetado.Fullfilment;
            }

            string? mensaje = modelo.SeleccionarModalidad(modalidad);

            labdatosetiq.Visible = modelo.RequiereDatosEtiquetado;
            labnombre.Visible = modelo.RequiereDatosEtiquetado;
            textnombre.Visible = modelo.RequiereDatosEtiquetado;
            labdireccion.Visible = modelo.RequiereDatosEtiquetado;
            textdireccion.Visible = modelo.RequiereDatosEtiquetado;

            if (mensaje != null)
            {
                listordenact.Items.Clear();
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnagregar_Click(object? sender, EventArgs e)
        {
            int? cantidad = int.TryParse(textcantretirar.Text, out int cantidadIngresada) ? cantidadIngresada : null;

            string? error = modelo.AgregarProducto(comboprodretirar.SelectedItem as Producto, cantidad);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listordenact.Items.Clear();
            foreach (ItemOrden item in modelo.ItemsOrden)
            {
                listordenact.Items.Add(new ListViewItem(new[] { item.Producto.Sku, item.Producto.Nombre, $"{item.Cantidad} {modelo.UnidadDeCantidad}" }));
            }

            textcantretirar.Clear();
        }

        private void btnconfirmar_Click(object? sender, EventArgs e)
        {
            int? dni = int.TryParse(textDNI.Text, out int dniIngresado) ? dniIngresado : null;

            modelo.FechaRetiro = datefecha.Value;
            modelo.NombreDestinatario = textnombre.Text;
            modelo.DireccionDestino = textdireccion.Text;
            modelo.NombreTransportista = textnomtrans.Text;
            modelo.DniTransportista = dni;
            modelo.EmpresaTransportista = textempresatrans.Text;

            string? error = modelo.ValidarOrden();
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string mensaje in modelo.RegistrarOrden())
            {
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            combodepo.SelectedIndex = -1;
            comboprodretirar.Items.Clear();
            comboprodretirar.SelectedIndex = -1;
            radiocerrado.Checked = false;
            radiofullfilment.Checked = false;
            textcantretirar.Clear();
            datefecha.Value = modelo.FechaRetiro;
            textnombre.Clear();
            textdireccion.Clear();
            textnomtrans.Clear();
            textDNI.Clear();
            textempresatrans.Clear();
        }

        private void btncancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void textsolonumeros_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textsolonumeros_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            string filtrado = new string(textBox.Text.Where(char.IsDigit).ToArray());
            if (filtrado != textBox.Text)
            {
                textBox.Text = filtrado;
                textBox.SelectionStart = filtrado.Length;
            }
        }

        private void textsololetras_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !modelo.EsCaracterValidoParaNombre(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textsololetras_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            string filtrado = modelo.QuitarCaracteresInvalidosDeNombre(textBox.Text);
            if (filtrado != textBox.Text)
            {
                textBox.Text = filtrado;
                textBox.SelectionStart = filtrado.Length;
            }
        }
    }
}
