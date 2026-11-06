#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class RecepcionMercaderiaModelo
{
    public List<Cliente> Clientes { get; }
    public List<Posicion> Posiciones { get; }
    public List<OrdenRecepcion> OrdenesRegistradas { get; } = new();
    public List<ItemRecepcion> Items { get; } = new();
    public Cliente? ClienteSeleccionado { get; private set; }

    public string NumeroRemito { get; set; } = "";
    public DateTime FechaRemito { get; set; } = DateTime.Today;
    public string Transportista { get; set; } = "";
    public string Chofer { get; set; } = "";
    public string Patente { get; set; } = "";
    public string Observaciones { get; set; } = "";

    // Los informa AgregarItem para que la pantalla sepa qué campos limpiar (excepciones 10.1 y 10.2).
    public bool CodigoInvalido { get; private set; }
    public bool CantidadesInvalidas { get; private set; }

    public int UltimoNumeroOrden { get; private set; } = 1047;

    public RecepcionMercaderiaModelo()
    {
        var yerba = new Producto("AND-1001", "Yerba mate 1 kg", "PAL-AND-1001", 120);
        var aceite = new Producto("AND-1002", "Aceite girasol 1,5 L", "PAL-AND-1002", 96);
        var arroz = new Producto("AND-1003", "Arroz largo fino 1 kg", null, 0);
        var licuadora = new Producto("ELS-2001", "Licuadora 600 W", "PAL-ELS-2001", 40);
        var pava = new Producto("ELS-2002", "Pava eléctrica 1,7 L", null, 0);
        var remera = new Producto("TXP-3001", "Remera algodón talle M", null, 0);
        var jean = new Producto("TXP-3002", "Jean clásico talle 42", "PAL-TXP-3002", 200);
        var pelota = new Producto("JMF-5001", "Pelota de fútbol N°5", null, 0);

        Clientes = new List<Cliente>
        {
            new Cliente("Distribuidora Andina", true, new List<Producto> { yerba, aceite, arroz },
                new List<string> { "00001-00004521", "00001-00004522" }),
            new Cliente("ElectroSur", true, new List<Producto> { licuadora, pava },
                new List<string> { "00003-00001187" }),
            new Cliente("Textil Pampa", true, new List<Producto> { remera, jean },
                new List<string> { "00002-00000915" }),
            // Contrato vencido: no se muestra en la lista de clientes (precondición del caso de uso).
            new Cliente("Juguetería Mundo Feliz", false, new List<Producto> { pelota },
                new List<string>())
        };

        // Posiciones del depósito (Sector-Posición-Fila); las primeras ya están ocupadas.
        Posiciones = new List<Posicion>
        {
            new Posicion("A-01-1", true),
            new Posicion("A-01-2", true),
            new Posicion("A-01-3", true),
            new Posicion("A-01-4", false),
            new Posicion("A-02-1", false),
            new Posicion("A-02-2", false),
            new Posicion("A-02-3", false),
            new Posicion("A-02-4", false),
            new Posicion("B-01-1", false),
            new Posicion("B-01-2", false),
            new Posicion("B-01-3", false),
            new Posicion("B-01-4", false)
        };
    }

    public List<Cliente> ClientesConContratoVigente => Clientes.Where(c => c.ContratoVigente).ToList();

    // Excepción paso 3.
    public string? ValidarHayClientes()
    {
        if (ClientesConContratoVigente.Count == 0)
        {
            return "No hay clientes con contrato vigente para registrar recepciones";
        }
        return null;
    }

    // Busca un cliente escrito a mano en el campo Cliente (sin distinguir mayúsculas).
    public Cliente? BuscarCliente(string razonSocial)
    {
        return ClientesConContratoVigente.Find(c => c.RazonSocial.ToUpper() == razonSocial.Trim().ToUpper());
    }

    // El cliente escrito debe coincidir con uno de la lista.
    public string? ValidarClienteEscrito(string razonSocial)
    {
        if (razonSocial.Trim() == "" || BuscarCliente(razonSocial) != null)
        {
            return null;
        }

        return $"El cliente {razonSocial.Trim()} no figura en el sistema con contrato vigente. Elija uno de los clientes de la lista";
    }

    // Devuelve la pregunta a hacer antes de cambiar de cliente, o null si se puede cambiar sin preguntar.
    public string? PreguntarCambioDeCliente(Cliente? nuevo)
    {
        if (nuevo == ClienteSeleccionado || ClienteSeleccionado == null || Items.Count == 0)
        {
            return null;
        }

        return "Si cambia el cliente, se quitarán los ítems cargados del detalle. ¿Desea cambiar el cliente?";
    }

    // Los SKU se validan contra el cliente: si cambia, los ítems cargados dejan de valer.
    public void SeleccionarCliente(Cliente? cliente)
    {
        if (cliente == ClienteSeleccionado)
        {
            return;
        }

        ClienteSeleccionado = cliente;
        foreach (ItemRecepcion item in Items)
        {
            LiberarPosicion(item);
        }
        Items.Clear();
    }

    // Cliente, Transportista y Nombre y Apellido: solo admiten letras (y espacios entre palabras).
    public bool AdmiteSoloLetras(char caracter)
    {
        return char.IsControl(caracter) || char.IsLetter(caracter) || caracter == ' ';
    }

    // N° Remito, Cant. Contada y Cant. Remito: solo admiten números.
    public bool AdmiteSoloNumeros(char caracter)
    {
        return char.IsControl(caracter) || char.IsDigit(caracter);
    }

    // Deja solo números (máximo 13) y agrega el guion después del 5°: 00001-00012345.
    public string FormatearNumeroRemito(string numero)
    {
        string digitos = "";
        foreach (char caracter in numero)
        {
            if (char.IsDigit(caracter) && digitos.Length < 13)
            {
                digitos += caracter;
            }
        }

        if (digitos.Length <= 5)
        {
            return digitos;
        }
        return digitos.Substring(0, 5) + "-" + digitos.Substring(5);
    }

    // Excepción paso 6.
    public string? ValidarRemitoNoRegistrado(string numero)
    {
        string formateado = FormatearNumeroRemito(numero);
        if (ClienteSeleccionado == null || !ClienteSeleccionado.RemitosRegistrados.Contains(formateado))
        {
            return null;
        }

        return $"El remito N° {formateado} ya fue registrado para el cliente {ClienteSeleccionado.RazonSocial}";
    }

    // Pasos 8 a 10. El código puede ser el SKU (recepción por ítem) o el código de pallet del producto
    // (recepción en pallet cerrado: las cantidades se cuentan en pallets).
    // Devuelve el error a mostrar, o null si el ítem se agregó al detalle.
    public string? AgregarItem(string codigo, string textoContada, string textoRemito)
    {
        CodigoInvalido = false;
        CantidadesInvalidas = false;

        if (ClienteSeleccionado == null)
        {
            return "Seleccione el cliente antes de cargar la mercadería: cada SKU se valida contra los dados de alta para ese cliente.";
        }

        codigo = codigo.Trim().ToUpper();
        if (codigo == "" || textoContada.Trim() == "" || textoRemito.Trim() == "")
        {
            return "Complete SKU/ Código, Cant. Contada y Cant. Remito para agregar el ítem.";
        }

        // Excepción 10.1.
        Producto? producto = ClienteSeleccionado.Productos.Find(p => p.Sku == codigo || p.CodigoPallet == codigo);
        if (producto == null)
        {
            CodigoInvalido = true;
            return $"El SKU {codigo} no está dado de alta para el cliente {ClienteSeleccionado.RazonSocial}";
        }

        // Excepción 10.2.
        if (!int.TryParse(textoContada, out int contada) || !int.TryParse(textoRemito, out int remito)
            || remito <= 0 || contada < 0 || remito > 999999 || contada > 999999)
        {
            CantidadesInvalidas = true;
            return "Ingrese cantidades enteras: la del remito mayor a cero y la contada mayor o igual a cero";
        }

        // Excepción 10.3.
        if (Items.Find(i => i.Producto == producto) != null)
        {
            return $"El SKU {producto.Sku} ya fue agregado. Quítelo para volver a cargarlo";
        }

        // Cada ítem con unidades contadas recibe una posición libre; con cantidad contada 0 queda sin posición.
        Posicion? posicion = null;
        if (contada > 0)
        {
            posicion = Posiciones.Find(p => !p.Ocupada);
            if (posicion == null)
            {
                return "No hay posiciones libres en el depósito para ubicar esta mercadería. Avise al jefe de Almacenamiento.";
            }
            posicion.Ocupada = true;
        }

        Items.Add(new ItemRecepcion(producto, producto.CodigoPallet == codigo, contada, remito, posicion));
        return null;
    }

    // Excepción paso 11: quita el ítem seleccionado y libera la posición propuesta.
    public string? QuitarItem(int indice)
    {
        if (Items.Count == 0)
        {
            return "No hay mercadería disponible para quitar.";
        }

        if (indice < 0)
        {
            return "Seleccione el ítem que desea quitar";
        }

        LiberarPosicion(Items[indice]);
        Items.RemoveAt(indice);
        return null;
    }

    private void LiberarPosicion(ItemRecepcion item)
    {
        if (item.Posicion != null)
        {
            item.Posicion.Ocupada = false;
        }
    }

    // Excepción paso 15: devuelve el error a mostrar, o null si la recepción se puede confirmar.
    public string? ValidarRecepcion()
    {
        var faltantes = new List<string>();
        if (ClienteSeleccionado == null) faltantes.Add("Cliente");
        if (NumeroRemito.Trim() == "") faltantes.Add("N° Remito");
        if (Transportista.Trim() == "") faltantes.Add("Transportista");
        if (Chofer.Trim() == "") faltantes.Add("Nombre y Apellido");
        if (Patente.Trim() == "") faltantes.Add("Patente");
        if (Items.Count == 0) faltantes.Add("Detalle de mercadería recibida");

        if (faltantes.Count > 0)
        {
            return "Complete los datos del remito y del transportista y cargue al menos un ítem." +
                   Environment.NewLine + "Falta completar: " + string.Join(", ", faltantes);
        }

        if (FormatearNumeroRemito(NumeroRemito).Length != 14)
        {
            return "El N° de remito debe tener 13 números: 5 del punto de venta, un guion y 8 del comprobante. Ejemplo: 00001-00012345.";
        }

        string? errorRemito = ValidarRemitoNoRegistrado(NumeroRemito);
        if (errorRemito != null)
        {
            return errorRemito;
        }

        if (FechaRemito.Date > DateTime.Today)
        {
            return "La fecha del remito no puede ser posterior a la fecha actual.";
        }

        if (!Transportista.All(c => char.IsLetter(c) || c == ' '))
        {
            return "El transportista solo admite letras.";
        }

        if (!Chofer.All(c => char.IsLetter(c) || c == ' ') || !Chofer.Trim().Contains(' '))
        {
            return "Ingrese el nombre y el apellido del chofer, solo con letras. Por ejemplo: Juan Pérez.";
        }

        // Patente: AAA123 o AA123AA.
        string patente = Patente.Trim().ToUpper();
        bool formatoViejo = patente.Length == 6
            && patente.Substring(0, 3).All(char.IsLetter) && patente.Substring(3).All(char.IsDigit);
        bool formatoNuevo = patente.Length == 7
            && patente.Substring(0, 2).All(char.IsLetter) && patente.Substring(2, 3).All(char.IsDigit)
            && patente.Substring(5).All(char.IsLetter);
        if (!formatoViejo && !formatoNuevo)
        {
            return "La patente debe tener el formato AAA123 o AA123AA, por ejemplo AB123CD.";
        }

        return null;
    }

    // Paso 16: pregunta de confirmación antes de guardar.
    public string PreguntaConfirmacion =>
        $"¿Confirma el registro de la recepción del remito N° {NumeroRemito} del cliente {ClienteSeleccionado?.RazonSocial}?";

    // Pasos 18 a 20: registra la orden de recepción y deja el modelo listo para otra recepción.
    // Devuelve los mensajes a mostrar, en orden.
    public List<string> RegistrarRecepcion()
    {
        var mensajes = new List<string>();
        string estado = "Pendiente de almacenamiento";

        // Excepción paso 18: cantidad contada menor que la del remito.
        if (Items.Find(i => i.Diferencia < 0) != null)
        {
            estado = "Recibida con diferencias";
            mensajes.Add("Se registraron diferencias con el remito. Entregue al transportista la nota de diferencias");
        }

        // Excepción paso 19: cantidad contada mayor que la del remito.
        if (Items.Find(i => i.Diferencia > 0) != null)
        {
            estado = "Pendiente - Excedente";
            mensajes.Add("Se recibió más mercadería que la indicada en el remito. Haga firmar la orden de espacio insuficiente al transportista");
        }

        UltimoNumeroOrden++;
        var orden = new OrdenRecepcion(UltimoNumeroOrden, ClienteSeleccionado!, NumeroRemito, FechaRemito.Date,
            Transportista.Trim(), Chofer.Trim(), Patente.Trim().ToUpper(), Observaciones.Trim(), Items.ToList(), estado);
        OrdenesRegistradas.Add(orden);
        orden.Cliente.RemitosRegistrados.Add(orden.NumeroRemito);

        mensajes.Add($"Orden de recepción N° {orden.Numero} registrada con éxito. Estado: {orden.Estado}" + Environment.NewLine +
                     $"Cliente: {orden.Cliente.RazonSocial} - Remito N° {orden.NumeroRemito}" + Environment.NewLine +
                     $"Transportista: {orden.Transportista} - Chofer: {orden.Chofer} - Patente: {orden.Patente}");

        // Las posiciones quedan ocupadas; se limpia la recepción para cargar la próxima.
        ClienteSeleccionado = null;
        Items.Clear();
        NumeroRemito = "";
        FechaRemito = DateTime.Today;
        Transportista = "";
        Chofer = "";
        Patente = "";
        Observaciones = "";

        return mensajes;
    }

    // Pasos 2 a 16: el operario presiona Cancelar.
    public string MensajeCancelacion => "Si selecciona cancelar, los cambios no se guardarán. ¿Desea cancelar?";
}
