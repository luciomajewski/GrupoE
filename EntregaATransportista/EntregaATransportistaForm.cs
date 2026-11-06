#nullable enable
using System.Drawing.Printing;

namespace GrupoE.EntregaATransportista
{
    public partial class EntregaATransportistaForm : Form
    {
        private readonly EntregaATransportistaModelo modelo = new();

        public EntregaATransportistaForm()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            int? dni = int.TryParse(txtDni.Text, out int dniIngresado) ? dniIngresado : null;

            string? error = modelo.Buscar(dni);

            // Si el transportista está autorizado pero sin órdenes, el nombre queda a la vista.
            txtTransportista.Text = modelo.TransportistaNombre ?? "";
            lstDetalle.Items.Clear();
            foreach (OrdenEntrega orden in modelo.OrdenesAEntregar)
            {
                lstDetalle.Items.Add(new ListViewItem(new[] { orden.NumeroOrdenPreparacion.ToString(), orden.CantidadPaquetes.ToString() }));
            }
            btnGenerarRemito.Enabled = modelo.PuedeGenerarRemito;

            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtDni_TextChanged(object? sender, EventArgs e)
        {
            // El DNI solo admite números: se quita cualquier otro carácter, también si se pega texto.
            string filtrado = new string(txtDni.Text.Where(char.IsAsciiDigit).ToArray());
            if (filtrado != txtDni.Text)
            {
                // Al reasignar el texto el evento se vuelve a disparar con el valor ya filtrado.
                txtDni.Text = filtrado;
                txtDni.SelectionStart = filtrado.Length;
                return;
            }

            // Si cambia el DNI después de buscar, el nombre y la grilla dejan de corresponder.
            if (modelo.Remito == null && (modelo.TransportistaNombre != null || lstDetalle.Items.Count > 0))
            {
                modelo.Reiniciar();
                txtTransportista.Clear();
                lstDetalle.Items.Clear();
                btnGenerarRemito.Enabled = modelo.PuedeGenerarRemito;
            }
        }

        private void btnGenerarRemito_Click(object? sender, EventArgs e)
        {
            RemitoEntrega remito = modelo.GenerarRemito();

            // Impresión del remito: página 1 original, página 2 copia para firmar.
            using var documento = new PrintDocument { DocumentName = $"Remito {remito.Numero}" };
            int pagina = 0;
            documento.PrintPage += (s, ev) =>
            {
                var g = ev.Graphics!;
                using var fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);
                using var fuente = new Font("Segoe UI", 11);
                float x = ev.MarginBounds.Left;
                float y = ev.MarginBounds.Top;
                float renglon = fuente.GetHeight(g) + 4;

                g.DrawString($"REMITO DE ENTREGA N° {remito.Numero} - {(pagina == 0 ? "ORIGINAL" : "COPIA")}", fuenteTitulo, Brushes.Black, x, y);
                y += fuenteTitulo.GetHeight(g) + 12;

                g.DrawString($"Fecha y hora: {remito.FechaHora:dd/MM/yyyy HH:mm}", fuente, Brushes.Black, x, y); y += renglon;
                g.DrawString($"Depósito: {remito.CodigoDeposito}", fuente, Brushes.Black, x, y); y += renglon;
                g.DrawString($"Transportista: {remito.TransportistaNombre} - DNI {remito.TransportistaDni}", fuente, Brushes.Black, x, y); y += renglon;
                g.DrawString($"Usuario: {remito.CodigoUsuario}", fuente, Brushes.Black, x, y); y += renglon * 2;

                g.DrawString("Orden de preparación", fuente, Brushes.Black, x, y);
                g.DrawString("Paquetes", fuente, Brushes.Black, x + 250, y);
                y += renglon;
                foreach (OrdenEntrega orden in modelo.OrdenesAEntregar)
                {
                    g.DrawString(orden.NumeroOrdenPreparacion.ToString(), fuente, Brushes.Black, x, y);
                    g.DrawString(orden.CantidadPaquetes.ToString(), fuente, Brushes.Black, x + 250, y);
                    y += renglon;
                }
                y += renglon;
                g.DrawString($"Total de paquetes: {modelo.OrdenesAEntregar.Sum(o => o.CantidadPaquetes)}", fuente, Brushes.Black, x, y);

                if (pagina == 1)
                {
                    y += renglon * 4;
                    g.DrawString("Firma del transportista: ______________________________", fuente, Brushes.Black, x, y);
                }

                pagina++;
                ev.HasMorePages = pagina < 2;
                if (!ev.HasMorePages)
                {
                    pagina = 0;
                }
            };
            using var vistaPrevia = new PrintPreviewDialog { Document = documento, Width = 800, Height = 900 };
            vistaPrevia.ShowDialog(this);

            MessageBox.Show($"Remito N° {remito.Numero} generado. Entregue el original y solicite la firma de la copia",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtDni.Enabled = modelo.PuedeBuscar;
            btnBuscar.Enabled = modelo.PuedeBuscar;
            btnGenerarRemito.Enabled = modelo.PuedeGenerarRemito;
            chkRemitoFirmado.Enabled = modelo.PuedeMarcarRemitoFirmado;
            btnConfirmarDespacho.Enabled = modelo.PuedeConfirmarDespacho;
        }

        private void chkRemitoFirmado_CheckedChanged(object? sender, EventArgs e)
        {
            modelo.RemitoFirmado = chkRemitoFirmado.Checked;
            btnConfirmarDespacho.Enabled = modelo.PuedeConfirmarDespacho;
        }

        private void btnConfirmarDespacho_Click(object? sender, EventArgs e)
        {
            RemitoEntrega remito = modelo.Remito!;
            DialogResult respuesta = MessageBox.Show(
                $"¿Confirma el despacho de {modelo.OrdenesAEntregar.Count} órdenes al transportista {remito.TransportistaNombre} con el remito N° {remito.Numero}?",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            modelo.ConfirmarDespacho();
            MessageBox.Show($"Despacho confirmado. Remito N° {remito.Numero}", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Pantalla lista para el siguiente transportista.
            modelo.Reiniciar();
            txtTransportista.Clear();
            txtDni.Clear();
            lstDetalle.Items.Clear();
            chkRemitoFirmado.Checked = false;
            txtDni.Enabled = modelo.PuedeBuscar;
            btnBuscar.Enabled = modelo.PuedeBuscar;
            btnGenerarRemito.Enabled = modelo.PuedeGenerarRemito;
            chkRemitoFirmado.Enabled = modelo.PuedeMarcarRemitoFirmado;
            btnConfirmarDespacho.Enabled = modelo.PuedeConfirmarDespacho;
            txtDni.Focus();
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea cancelar la entrega? No se registrará el despacho",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            // Anula el remito si se había generado; después de esto el cierre no vuelve a preguntar.
            modelo.Cancelar();
            Close();
        }

        private void EntregaATransportistaForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Cerrar la pantalla con un remito emitido equivale a cancelar la entrega.
            if (modelo.Remito?.Estado != EstadoRemito.Emitido)
            {
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Desea cancelar la entrega? No se registrará el despacho",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                modelo.Cancelar();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
