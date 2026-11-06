#nullable enable
namespace GrupoE.CumplirOrdenSeleccion
{
    public partial class CumplirOrdenSeleccionForm : Form
    {
        private readonly CumplirOrdenSeleccionModelo modelo = new();

        public CumplirOrdenSeleccionForm()
        {
            InitializeComponent();
        }

        private void CumplirOrdenSeleccionForm_Shown(object? sender, EventArgs e)
        {
            MostrarOrdenPendiente();
        }

        private void btnConfirmar_Click(object? sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(modelo.PreguntaConfirmacion, Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Excepción 8.1: no se guarda nada y la orden sigue en pantalla.
            if (respuesta == DialogResult.No)
            {
                return;
            }

            string mensaje = modelo.CumplirOrden();
            MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            MostrarOrdenPendiente();
        }

        private void MostrarOrdenPendiente()
        {
            string? mensaje = modelo.BuscarOrdenPendiente();

            lblOrden.Text = modelo.NumeroOrdenActual;
            lblFecha.Text = modelo.Fecha.ToString("dd/MM/yy");
            btnConfirmar.Enabled = modelo.HayOrdenPendiente;
            lvMercaderia.Items.Clear();

            // Excepción 2.1
            if (mensaje != null)
            {
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var renglon in modelo.RenglonesOrdenados)
            {
                var item = new ListViewItem(renglon.Posicion);
                item.SubItems.Add(renglon.Sku);
                item.SubItems.Add(renglon.Descripcion);
                item.SubItems.Add(renglon.Cantidad.ToString());
                lvMercaderia.Items.Add(item);
            }
        }
    }
}
