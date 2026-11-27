# Pampazon · Grupo E

Prototipo del sistema de gestión de depósito de **Pampazon**, desarrollado para el Trabajo Práctico de CAI. Es una aplicación de escritorio en **C# / Windows Forms (.NET 10)** que cubre el circuito de salida de la mercadería: desde que el cliente pide un retiro hasta que el transportista se la lleva.

## Alcance de esta entrega

La entrega se concentra en cuatro pantallas, que forman el circuito de salida:

| # | Pantalla | Qué resuelve |
|---|---|---|
| 1 | **Registrar Orden de Preparación** | El cliente pide retirar mercadería de un depósito en una de dos modalidades: **Palet cerrado** (pallets completos, tal como están guardados) o **Fulfillment** (unidades). La orden queda `Pendiente` y compromete el stock disponible. |
| 2 | **Cumplir Orden de Selección** | El sistema genera las órdenes de selección en cada corte (08:00 y 14:00), juntando por depósito las órdenes de preparación `Pendiente`. Almacenamiento baja de los racks lo que pide cada orden y lo entrega a Preparación: la mercadería sale de su posición y las órdenes de preparación quedan listas para empaquetar. |
| 3 | **Empaquetar pedido y generar orden de entrega** | Preparación empaqueta la orden, que pasa a `Preparada`, se genera la orden de entrega `Pendiente` y se registra el servicio a facturar (derecho a cobro de Pampazon). |
| 4 | **Entregar mercadería al transportista** | Despacho valida el DNI del transportista autorizado, emite el remito (original y copia) y, con la copia firmada, confirma el despacho. |

Estados de una orden de preparación a lo largo del circuito: `Pendiente` → `EnPreparacion` (al generarse su orden de selección) → `Preparada` → `Cumplida`.

### Corte: generación de órdenes de selección

El corte corre al abrir **Cumplir Orden de Selección**, antes de buscar la orden pendiente, si ya pasó un horario de corte (08:00 o 14:00) que todavía no se ejecutó. Por cada depósito arma una orden de selección con todas las órdenes de preparación `Pendiente`: las de palet cerrado primero y después por fecha de retiro. Para cada renglón elige las posiciones en el orden del recorrido y descuenta lo que ya reservan otras órdenes de selección pendientes. Si una orden de preparación no tiene stock en posiciones, queda `Pendiente` para el próximo corte.

### Pallets

Todo se guarda en pallets, en posiciones de los racks. El stock se lleva por cliente + SKU + posición + cantidad, y cada ubicación guarda el `CodigoPallet` del pallet que ocupa esa posición.

- **ID:** `PAL-000001`, `PAL-000002`, … (el último usado + 1).
- **Origen:** armado por el cliente (se marca en Recepción con la casilla *Pallet armado por el cliente*; un pallet puede tener varios SKU del mismo cliente) o armado por Pampazon en la descarga (la carga suelta recibe su ID en Ingreso a Stock).
- **Estado:** con código, el pallet está completo; sin código (`null`), ya se le sacaron unidades y quedó desarmado.
- **Salida a palet cerrado:** solo pallets completos y de un solo SKU, que salen enteros. El renglón de la orden guarda las unidades de esos pallets.
- **Salida a fulfillment:** toma unidades, primero de los pallets ya desarmados. Si a un pallet se le sacan unidades y algo queda en la posición, todo el pallet queda desarmado y ya no puede salir a palet cerrado.

### Servicios a facturar

Al confirmar el empaquetado se registra el derecho a cobro (`ServicioAFacturarEntidad`, `ServiciosAFacturarAlmacen`, `Datos/ServiciosAFacturar.json`) con la orden de preparación, el cliente, el depósito, la modalidad, el detalle por SKU y el estado `PendienteDeFacturar`.

## Arquitectura

```
Formulario (pantalla)  ⇄  Modelo  ⇄  Almacenes  ⇄  Datos/*.json
```

- **Formularios y modelos** (una carpeta por pantalla): el formulario solo muestra y captura datos; toda la lógica está en el modelo.
- **`Almacenes/`**: las entidades (`xxxxEntidad`), sus clases auxiliares, los enums y un almacén por entidad (`xxxxAlmacen`). Cada almacén carga su archivo de `Datos/` al primer uso y lo escribe con `Guardar()`.
- **`Datos/`**: los datos de prueba en JSON. Los enums se guardan por nombre (por ejemplo, `"Cumplida"`).

Reglas de diseño que respeta el código:
1. Los formularios hablan solo con su modelo: nunca acceden a los almacenes ni a las entidades.
2. Las entidades nunca llegan a los formularios: el modelo las convierte a las clases de cada pantalla.
3. Las búsquedas en los almacenes las hace el modelo; las entidades no tienen métodos de búsqueda.
4. Los modelos conservan los métodos del prototipo. Los métodos nuevos (pallets en Recepción, elección de cliente en Registrar Orden y el corte en Cumplir Orden de Selección) se agregaron sin cambiar los existentes.

## Cómo ejecutarlo

1. Abrir `GrupoE.slnx` en Visual Studio y compilar.
2. Los archivos de `Datos/` se copian siempre al directorio de salida (`bin/Debug/net10.0-windows/Datos/`). La aplicación lee y guarda **esa copia**: cada compilación la vuelve a dejar igual a la del repositorio.
3. Al cerrar la aplicación, `Program.cs` guarda todos los almacenes.

Hay clientes en los cuatro depósitos: Bazar del Sol solo en Mendoza, Limpieza Serrana solo en Córdoba, Jardinería Litoral en Rosario y Córdoba, y Ferretería Federal en Gran Buenos Aires, Rosario y Mendoza. El `CodigoDeposito` del cliente es su depósito principal (donde se recibe mercadería); los demás depósitos surgen de dónde tiene stock. El stock de su depósito principal está respaldado por una orden de recepción `Cumplida` (1057 a 1063) con su remito.

Los datos de prueba incluyen selecciones pendientes en los cuatro depósitos, órdenes listas para empaquetar y transportistas con órdenes listas para retirar (por ejemplo, DNI `31555777`, `33222444`, `29444666`, `35111222` y `32777888`).

Pallets completos y libres para probar **Palet cerrado** sin cargar nada antes (recepciones 1061 a 1063):

| Cliente / depósito | Pallets |
|---|---|
| Bazar del Sol / Mendoza | `PAL-000013` BDS-6001 (24 u.) · `PAL-000014` BDS-6002 (12 u.) |
| Limpieza Serrana / Córdoba | `PAL-000015` LSE-7001 (30 u.) · `PAL-000016` LSE-7002 (20 u.) |
| Jardinería Litoral / Rosario | `PAL-000017` y `PAL-000018` AGL-8001 (40 u. cada uno) · `PAL-000019` AGL-8002 (25 u.) |

Los demás pallets del stock (`PAL-000001` a `PAL-000012`) tienen unidades reservadas por órdenes de selección pendientes, así que no se ofrecen a palet cerrado. La próxima orden de recepción es la 1064 y el próximo pallet, `PAL-000020`.

En Registrar Orden de Preparación el cliente se elige de una lista desplegable (al abrir la pantalla queda elegido uno al azar).

## Documentación

Cada pantalla de la entrega tiene en su carpeta su caso de uso y su diagrama de secuencia (en Mermaid, que GitHub muestra como diagrama):

| Pantalla | Caso de uso | Diagrama de secuencia |
|---|---|---|
| Registrar Orden de Preparación | `RegistrarOrdenDePreparacion/Caso de uso RegistrarOrdenDePreparacion.docx` | `RegistrarOrdenDePreparacion/Diagrama_de_secuencia_RegistrarOrdenDePreparacion.md` |
| Cumplir Orden de Selección | `CumplirOrdenSeleccion/Caso de uso orden seleccion.docx` | `CumplirOrdenSeleccion/Diagrama_de_secuencia_CumplirOrdenSeleccion.md` |
| Empaquetar pedido | `EmpaquetarPedido/Caso de uso EmpaquetarPedido.docx` | `EmpaquetarPedido/Diagrama_de_secuencia_EmpaquetarPedido.md` |
| Entregar mercadería al transportista | `EntregaATransportista/Caso de uso - Entregar mercadería al transportista.docx` | `EntregaATransportista/Diagrama_de_secuencia_EntregaATransportista.md` |

El diagrama de clases se entrega aparte. Los diagramas de secuencia siguen el código; las diferencias que quedan figuran en la sección siguiente.

## Pendientes de implementación

Funcionalidad que los casos de uso y los diagramas ya describen, pero que el código todavía no tiene:

| Pantalla | Pendiente | Estado actual del código |
|---|---|---|
| General | **Inicio de sesión.** El usuario de cada operación es el operario identificado en el sistema. | Se usa el usuario de Windows (`Environment.UserName`). |
| Registrar Orden de Preparación | **Cliente logueado.** La orden se registra para el cliente que inició sesión. | El cliente se elige de una lista desplegable con los clientes con contrato vigente y stock disponible. |
| Empaquetar | **Orden tomada por un puesto.** La marca `TomadaPor` se guarda en `OrdenPreparacionEntidad` para que otro puesto no tome la misma orden. | La marca vive solo en memoria de cada pantalla. |

Diagramas de secuencia por actualizar:
- **Registrar Orden de Preparación:** falta la lista de clientes (`Clientes`, `SeleccionarCliente`) y el manejo de pallets (pallets libres por SKU y unidades comprometidas por pallet).
- **Cumplir Orden de Selección:** muestra el corte, pero no las reglas de pallets (palet cerrado primero, solo pallets completos y fulfillment empezando por los desarmados) ni la desconsolidación al cumplir.

Mejoras identificadas, sin implementar:
- **Entrega a transportista:** en las órdenes a palet cerrado, la columna *Paquetes* muestra unidades (un pallet de 12 unidades figura como 12 paquetes). Se resuelve guardando un renglón por pallet en la orden de preparación.
- **Corte:** el último corte ejecutado se recuerda solo mientras el programa está abierto; si se vuelve a abrir, el último corte corre de nuevo.
- **Cumplir Orden de Selección:** el modelo arma y descuenta un inventario en memoria (`Inventario`) que no se usa; el stock real se actualiza en `MercaderiasAlmacen`. Se puede simplificar.
- **Datos de prueba:** el remito `5001` quedó `Emitido`, por lo que su orden de entrega `3002` no se ofrece para un nuevo remito.

Fuera del alcance de esta entrega:
- Las pantallas **Recepción de Mercadería** e **Ingreso de Stock**: están conectadas a los almacenes y manejan los pallets (alta de pallets del cliente e ID de los pallets que arma Pampazon), pero no forman parte de la documentación de esta entrega.
