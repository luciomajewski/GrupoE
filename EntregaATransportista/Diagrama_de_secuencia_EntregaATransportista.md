# Diagrama de secuencia – Entregar mercadería al transportista

Pantalla: `EntregaATransportistaForm` · Modelo: `EntregaATransportistaModelo` · Caso de uso: Entregar mercadería al transportista.

Convenciones:
- El diagrama sigue el código: cada mensaje es una invocación real (evento del formulario, método o propiedad del modelo, consulta a un almacén o acceso a un objeto).
- Las búsquedas en los almacenes las hace **el modelo**: las entidades no tienen métodos de búsqueda.
- Todo objeto sobre el que se actúa aparece como participante, y cada acción sobre él es un mensaje que le envía el modelo (o el formulario, si es un objeto de la pantalla), con sus parámetros. Los objetos que se crean durante el caso de uso nacen con `create`.
- Cada transportista se atiende en una vuelta del ciclo de los pasos 2 a 16, que empieza siempre con el DNI y `Buscar(dni)`.
- `-->>` es el valor que devuelve la invocación.
- Los números de paso y de excepción son los del caso de uso.

```mermaid
sequenceDiagram
    autonumber
    actor O as Operario de Despacho
    actor T as Transportista
    participant F as EntregaATransportistaForm
    participant MB as MessageBox
    participant M as EntregaATransportistaModelo
    participant PA as OrdenesPreparacionAlmacen
    participant PE as preparacion : OrdenPreparacionEntidad
    participant OEA as OrdenesEntregaAlmacen
    participant EE as entrega : OrdenEntregaEntidad
    participant RA as RemitosEntregaAlmacen
    participant R0 as remitoGuardado : RemitoEntregaEntidad

    %% ---------- Paso 1: abrir la pantalla ----------
    Note over O,R0: Paso 1 · El operario abre la pantalla
    O->>F: Abre "Entrega a transportista"
    F->>M: new EntregaATransportistaModelo()
    M->>M: CodigoUsuario = Environment.UserName

    loop Pasos 2 a 16 · Por cada transportista que se presenta a retirar
        %% ---------- Paso 2: DNI ----------
        Note over O,R0: Paso 2 · El operario pide el DNI y lo escribe (el campo Transportista es de solo lectura)
        T->>O: Entrega su DNI
        O->>F: Escribe el DNI en txtDni
        F->>F: txtDni_TextChanged(sender, e): deja solo dígitos
        O->>F: Presiona "Buscar"
        F->>F: btnBuscar_Click(sender, e)
        F->>M: Buscar(dni)
        M->>M: Reiniciar()

        %% ---------- Paso 3: validar DNI ----------
        alt Excepción 3 · DNI vacío o que no tiene 8 dígitos
            M-->>F: "Ingrese un DNI válido"
        else Paso 4 · DNI válido: busca las órdenes del transportista
            M->>PA: OrdenesPreparacion
            PA-->>M: List<OrdenPreparacionEntidad>
            M->>OEA: OrdenesEntrega
            OEA-->>M: List<OrdenEntregaEntidad>
            M->>RA: RemitosEntrega
            RA-->>M: List<RemitoEntregaEntidad>
            create participant OP as op : OrdenPreparacion
            M->>OP: new OrdenPreparacion(Numero, CodigoCliente, CodigoDeposito, TransportistaNombre, TransportistaDni,<br/>Estado, NumeroRemito del remito Firmado que la incluye) por cada orden de preparación
            create participant OE as oe : OrdenEntrega
            M->>OE: new OrdenEntrega(NumeroOrdenPreparacion, CodigoDeposito, CantidadPaquetes, Estado, FechaHoraDespacho)<br/>por cada orden de entrega
            M->>M: RemitosEntrega = new RemitoEntrega(...) por cada remito guardado

            loop Autorización · Por cada orden de preparación
                M->>PE: DniTransportista
                PE-->>M: DNI autorizado por el cliente
                opt DniTransportista == dni
                    M->>M: preparacionesDelTransportista.Add(preparacion)
                end
            end

            alt 4.1 · El DNI no figura en ninguna orden de preparación
                M-->>F: "El transportista no está autorizado a retirar mercadería en este depósito"
            else El transportista está autorizado
                M->>PE: NombreTransportista
                PE-->>M: nombre registrado por el cliente
                M->>M: TransportistaNombre = nombre, TransportistaDni = dni
                loop Por cada remito guardado
                    M->>R0: Estado
                    R0-->>M: Emitido / Firmado / Anulado
                    opt Estado == Emitido
                        M->>R0: OrdenesEntrega
                        R0-->>M: números de orden de entrega ya incluidos en un remito en curso
                    end
                end
                loop Por cada preparación del transportista
                    M->>EE: NumeroOrdenPreparacion, Estado, Numero
                    EE-->>M: datos de su orden de entrega
                    opt Estado == Pendiente y no está en un remito Emitido
                        M->>M: listas.Add(preparacion)
                    end
                end
                alt 4.2 · No tiene órdenes listas para retirar
                    M-->>F: "El transportista no tiene órdenes listas para retirar"
                else Tiene órdenes listas
                    loop Por cada orden lista del depósito de la primera (un remito, un depósito)
                        M->>PE: CodigoDeposito
                        PE-->>M: depósito de la orden
                        M->>M: OrdenesAEntregar.Add(OrdenesEntrega.First(NumeroOrdenPreparacion == preparacion.Numero))
                    end
                    M->>PE: NombreTransportista
                    PE-->>M: nombre registrado en las órdenes a entregar
                    M-->>F: null
                end
            end
        end

        %% ---------- Paso 5: mostrar ----------
        Note over O,R0: Paso 5 · Muestra el transportista y las órdenes a entregar
        F->>M: TransportistaNombre
        M-->>F: nombre (o null)
        F->>M: OrdenesAEntregar
        M-->>F: List<OrdenEntrega>
        loop Por cada orden a entregar
            F->>OE: NumeroOrdenPreparacion
            OE-->>F: número de orden de preparación
            F->>OE: CantidadPaquetes
            OE-->>F: cantidad de paquetes
        end
        F->>M: PuedeGenerarRemito
        M-->>F: true / false
        F->>F: txtTransportista.Text = nombre, lstDetalle con las órdenes, btnGenerarRemito.Enabled

        alt Excepciones 3, 4.1 y 4.2 · Buscar devolvió un error
            F->>MB: Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            F-->>O: No se entrega nada. Vuelve al paso 2 (siguiente vuelta del ciclo)
        else Hay órdenes para entregar
            opt Excepción 5 · El operario cambia el DNI después de buscar (sin remito generado)
                O->>F: Modifica txtDni
                F->>F: txtDni_TextChanged(sender, e)
                F->>M: Remito
                M-->>F: null
                F->>M: TransportistaNombre
                M-->>F: nombre
                F->>M: Reiniciar()
                F->>M: PuedeGenerarRemito
                M-->>F: false
                F-->>O: Limpia Transportista y la grilla. Vuelve al paso 2 (siguiente vuelta del ciclo)
            end

            %% ---------- Pasos 6 a 9: remito ----------
            Note over O,R0: Paso 6 · El operario carga los paquetes controlando el número de orden de cada etiqueta
            O->>F: Paso 7 · Presiona "Generar Remito"
            F->>F: btnGenerarRemito_Click(sender, e)
            F->>M: GenerarRemito()
            M->>M: PuedeGenerarRemito
            Note right of M: Paso 8 · Registra el remito Emitido
            M->>RA: RemitosEntrega
            RA-->>M: List<RemitoEntregaEntidad> (para calcular Max(Numero) + 1)
            M->>M: fechaHora = DateTime.Now sin milisegundos
            M->>M: CodigoDeposito
            M->>PA: OrdenesPreparacion.First(Numero == OrdenesAEntregar[0].NumeroOrdenPreparacion)
            PA-->>M: preparacion
            M->>PE: CodigoDeposito
            PE-->>M: depósito del remito
            create participant RM as remito : RemitoEntrega
            M->>RM: new RemitoEntrega(Numero = Max(Numero) + 1, FechaHora = fechaHora, CodigoDeposito, TransportistaNombre,<br/>TransportistaDni, CodigoUsuario, Estado = Emitido)
            M->>M: RemitosEntrega.Add(remito)
            loop Por cada orden a entregar
                M->>OEA: OrdenesEntrega.First(NumeroOrdenPreparacion == oe.NumeroOrdenPreparacion)
                OEA-->>M: entrega
                M->>EE: Numero
                EE-->>M: número de orden de entrega
            end
            create participant RE as remitoEntidad : RemitoEntregaEntidad
            M->>RE: new RemitoEntregaEntidad(Numero, FechaHora = fechaHora, OrdenesEntrega = números de orden de entrega,<br/>CodigoUsuario, Estado = Emitido, CodigoDeposito, NombreTransportista, DniTransportista)
            M->>RA: RemitosEntrega.Add(remitoEntidad)
            M->>RA: Guardar()
            Note right of RA: Escribe Datos/RemitosEntrega.json
            loop Por cada orden a entregar
                M->>OE: NumeroOrdenPreparacion, CantidadPaquetes
                OE-->>M: datos de la orden
                create participant DR as detalle : DetalleRemitoEntrega
                M->>DR: new DetalleRemitoEntrega(NumeroRemito, NumeroOrdenPreparacion, CantidadPaquetes)
                M->>M: DetallesRemitoEntrega.Add(detalle)
            end
            M-->>F: remito

            Note over F,RM: Paso 9 · Imprime el remito en original y copia
            create participant PV as vistaPrevia : PrintPreviewDialog
            F->>PV: new PrintPreviewDialog(Document = new PrintDocument(DocumentName = "Remito [nro]"))
            F->>PV: ShowDialog(this)
            loop PrintPage · Página 1 original, página 2 copia con línea de firma
                PV->>F: PrintPage(sender, ev)
                F->>RM: Numero, FechaHora, CodigoDeposito, TransportistaNombre, TransportistaDni, CodigoUsuario
                RM-->>F: datos del remito
                F->>M: OrdenesAEntregar
                M-->>F: órdenes y paquetes del remito
            end
            O->>PV: Imprime original y copia
            F->>RM: Numero
            RM-->>F: número del remito
            F->>MB: Show("Remito N° [nro] generado. Entregue el original y solicite la firma de la copia", Text,<br/>MessageBoxButtons.OK, MessageBoxIcon.Information)
            F->>M: PuedeBuscar, PuedeGenerarRemito, PuedeMarcarRemitoFirmado, PuedeConfirmarDespacho
            M-->>F: false, false, true, false
            F->>F: Deshabilita DNI, Buscar y Generar Remito. Habilita la casilla Remito Firmado

            %% ---------- Paso 10: firma ----------
            Note over O,T: Paso 10 · Entrega el original y recibe la copia firmada<br/>(Excepción 10 · Si el transportista no firma, el operario presiona Cancelar)
            O->>T: Entrega el original del remito
            T->>O: Devuelve la copia firmada
            O->>F: Marca "Remito Firmado"
            F->>F: chkRemitoFirmado_CheckedChanged(sender, e)
            F->>M: RemitoFirmado = true
            F->>M: PuedeConfirmarDespacho
            M-->>F: true
            F->>F: btnConfirmarDespacho.Enabled = true

            %% ---------- Pasos 11 a 13: confirmar ----------
            O->>F: Paso 11 · Presiona "Confirmar Despacho"
            F->>F: btnConfirmarDespacho_Click(sender, e)
            F->>M: Remito
            M-->>F: remito
            F->>M: OrdenesAEntregar.Count
            M-->>F: cantidad de órdenes
            F->>RM: TransportistaNombre, Numero
            RM-->>F: nombre y número
            F->>MB: Show("¿Confirma el despacho de [cantidad] órdenes al transportista [nombre] con el remito N° [nro]?",<br/>Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Note right of MB: Paso 12
            alt Excepción 13 · El operario presiona No
                O->>MB: No
                MB-->>F: DialogResult.No
                F-->>O: No se guarda nada. Vuelve al paso 10
            else Paso 13 · El operario presiona Sí
                O->>MB: Sí
                MB-->>F: DialogResult.Yes
                F->>M: ConfirmarDespacho()
                M->>M: PuedeConfirmarDespacho
                M->>RM: Estado
                RM-->>M: Emitido

                %% ---------- Paso 14: cumplido ----------
                Note over M,RM: Paso 14 · El remito queda Firmado con fecha, hora y usuario (cumplido)
                M->>M: ahora = DateTime.Now sin milisegundos
                M->>RM: Estado = Firmado
                M->>RM: FechaHoraCumplido = ahora
                M->>RM: CodigoUsuarioCumplido = CodigoUsuario

                %% ---------- Paso 15: órdenes ----------
                Note over M,OE: Paso 15 · Las órdenes de preparación quedan Cumplidas y las de entrega Despachadas
                loop Por cada orden a entregar
                    M->>M: BuscarOrdenPreparacion(oe)
                    M->>OP: Estado = Cumplida
                    M->>OP: NumeroRemito = Remito.Numero
                    M->>OE: Estado = Despachada
                    M->>OE: FechaHoraDespacho = ahora
                    M->>OP: CodigoCliente, Numero
                    OP-->>M: cliente y número de la orden
                    create participant MV as movimiento : MovimientoMercaderia
                    M->>MV: new MovimientoMercaderia(CodigoCliente, NumeroOrdenPreparacion, NumeroRemito, Fecha = ahora.Date)
                    M->>M: MovimientosMercaderia.Add(movimiento)
                end

                Note over M,RE: Pasos 14 y 15 · Se guardan en los almacenes
                M->>RA: RemitosEntrega.First(Numero == Remito.Numero)
                RA-->>M: remitoEntidad
                M->>RE: Estado = Firmado
                M->>RE: FechaHoraCumplido = ahora
                M->>RE: CodigoUsuarioCumplido = CodigoUsuario
                M->>RE: OrdenesEntrega
                RE-->>M: números de orden de entrega del remito
                M->>OEA: OrdenesEntrega.Where(Numero contenido en remitoEntidad.OrdenesEntrega)
                OEA-->>M: órdenes de entrega del remito
                loop Por cada entrega del remito
                    M->>EE: Estado = Despachada
                    M->>EE: FechaHoraDespacho = ahora
                    M->>EE: NumeroOrdenPreparacion
                    EE-->>M: número de orden de preparación
                    M->>PA: OrdenesPreparacion.First(Numero == entrega.NumeroOrdenPreparacion)
                    PA-->>M: preparacion
                    M->>PE: Estado = Cumplida
                end
                M->>RA: Guardar()
                Note right of RA: Escribe Datos/RemitosEntrega.json
                M->>OEA: Guardar()
                Note right of OEA: Escribe Datos/OrdenesEntrega.json
                M->>PA: Guardar()
                Note right of PA: Escribe Datos/OrdenesPreparacion.json

                %% ---------- Paso 16 ----------
                F->>MB: Show("Despacho confirmado. Remito N° [nro]", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MB-->>O: Paso 16 · "Despacho confirmado. Remito N° [nro]"
                F->>M: Reiniciar()
                F->>F: Limpia DNI, Transportista, la grilla y la casilla
                F->>M: PuedeBuscar, PuedeGenerarRemito, PuedeMarcarRemitoFirmado, PuedeConfirmarDespacho
                M-->>F: true, false, false, false
                F-->>O: Pantalla lista: siguiente vuelta del ciclo con el próximo transportista (paso 2)
            end
        end
    end

    %% ---------- Cancelar ----------
    opt Excepciones 6, 10 y "2 a 12" · El operario presiona Cancelar
        O->>F: Presiona "Cancelar"
        F->>F: btnCancelar_Click(sender, e)
        F->>MB: Show("¿Desea cancelar la entrega? No se registrará el despacho", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        alt Presiona No
            MB-->>F: DialogResult.No
            F-->>O: Vuelve al paso en que estaba
        else Presiona Sí
            MB-->>F: DialogResult.Yes
            F->>M: Cancelar()
            opt Ya se había generado el remito (Remito.Estado == Emitido)
                M->>RM: Estado = Anulado
                M->>RA: RemitosEntrega.First(Numero == Remito.Numero)
                RA-->>M: remitoEntidad
                M->>RE: Estado = Anulado
                M->>RA: Guardar()
                Note right of RA: Escribe Datos/RemitosEntrega.json
            end
            M->>M: Reiniciar()
            F->>F: Close()
            F->>F: EntregaATransportistaForm_FormClosing(sender, e)
            F->>M: Remito
            M-->>F: null (no vuelve a preguntar)
            F-->>O: Las órdenes siguen Pendiente. Termina el caso de uso
        end
    end

    %% ---------- Salida cerrando la ventana ----------
    opt Salida · El operario cierra la ventana antes del paso 13
        O->>F: Cierra la ventana
        F->>F: EntregaATransportistaForm_FormClosing(sender, e)
        F->>M: Remito
        M-->>F: remito (o null)
        opt Hay un remito Emitido
            F->>RM: Estado
            RM-->>F: Emitido
            F->>MB: Show("¿Desea cancelar la entrega? No se registrará el despacho", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            alt Presiona Sí
                MB-->>F: DialogResult.Yes
                F->>M: Cancelar()
                M->>RM: Estado = Anulado
                M->>RA: RemitosEntrega.First(Numero == Remito.Numero)
                RA-->>M: remitoEntidad
                M->>RE: Estado = Anulado
                M->>RA: Guardar()
                M->>M: Reiniciar()
            else Presiona No
                MB-->>F: DialogResult.No
                F->>F: e.Cancel = true
                F-->>O: La pantalla sigue abierta en el paso en que estaba
            end
        end
    end
```

## Invocaciones por paso del caso de uso

| Paso / excepción | Evento del formulario | Invocaciones |
|---|---|---|
| 1 | Constructor | `new EntregaATransportistaModelo()` (`CodigoUsuario = Environment.UserName`) |
| 2 · 3 | `txtDni_TextChanged`, `btnBuscar_Click` | `Buscar(dni)` → `Reiniciar()`, validación de 8 dígitos |
| 4 · 4.1 · 4.2 | `btnBuscar_Click` | `Buscar(dni)` → `OrdenesPreparacionAlmacen`, `OrdenesEntregaAlmacen`, `RemitosEntregaAlmacen`, `new OrdenPreparacion` / `new OrdenEntrega` / `new RemitoEntrega`, `OrdenPreparacionEntidad` (DniTransportista, NombreTransportista, CodigoDeposito), `RemitoEntregaEntidad` (Estado, OrdenesEntrega), `OrdenEntregaEntidad` (NumeroOrdenPreparacion, Estado, Numero) |
| 5 · Excepción 5 | `btnBuscar_Click`, `txtDni_TextChanged` | `TransportistaNombre`, `OrdenesAEntregar`, `OrdenEntrega` (NumeroOrdenPreparacion, CantidadPaquetes), `PuedeGenerarRemito`, `Remito`, `Reiniciar()` |
| 6 | (acción del operario) | (ninguna) |
| 7 · 8 | `btnGenerarRemito_Click` | `GenerarRemito()` → `RemitosEntregaAlmacen`, `CodigoDeposito`, `new RemitoEntrega`, `OrdenEntregaEntidad.Numero`, `new RemitoEntregaEntidad`, `RemitosEntrega.Add`, `Guardar()`, `new DetalleRemitoEntrega` |
| 9 | `btnGenerarRemito_Click` | `PrintPreviewDialog.ShowDialog` (`PrintPage` lee `RemitoEntrega` y `OrdenesAEntregar`), `MessageBox.Show(...)`, `PuedeBuscar` / `PuedeGenerarRemito` / `PuedeMarcarRemitoFirmado` / `PuedeConfirmarDespacho` |
| 10 · Excepción 10 | `chkRemitoFirmado_CheckedChanged` | `RemitoFirmado = true`, `PuedeConfirmarDespacho` |
| 11 · 12 · 13 · Excepción 13 | `btnConfirmarDespacho_Click` | `Remito`, `OrdenesAEntregar.Count`, `RemitoEntrega` (TransportistaNombre, Numero), `MessageBox.Show(..., YesNo, Question)` |
| 14 | `btnConfirmarDespacho_Click` | `ConfirmarDespacho()` → `RemitoEntrega` (Estado, FechaHoraCumplido, CodigoUsuarioCumplido), `RemitoEntregaEntidad` (los mismos) |
| 15 | `btnConfirmarDespacho_Click` | `ConfirmarDespacho()` → `BuscarOrdenPreparacion(oe)`, `OrdenPreparacion` / `OrdenEntrega` de la pantalla, `new MovimientoMercaderia` (en memoria), `OrdenEntregaEntidad` (Estado, FechaHoraDespacho), `OrdenPreparacionEntidad.Estado`, `Guardar()` de los 3 almacenes |
| 16 | `btnConfirmarDespacho_Click` | `MessageBox.Show(...)`, `Reiniciar()`, `PuedeBuscar` / ... (siguiente vuelta del ciclo) |
| 6 · 10 · 2 a 12 (Cancelar) | `btnCancelar_Click`, `EntregaATransportistaForm_FormClosing` | `MessageBox.Show(..., YesNo, Question)`, `Cancelar()` → `RemitoEntrega.Estado`, `RemitoEntregaEntidad.Estado = Anulado`, `Guardar()`, `Reiniciar()`, `Close()` |
| Salida | `EntregaATransportistaForm_FormClosing` | `Remito`, `RemitoEntrega.Estado`, `MessageBox.Show(...)`, `Cancelar()` o `e.Cancel = true` |
