# Pampazon · Grupo E

Prototipo del sistema de gestión de depósito de **Pampazon**, desarrollado para el Trabajo Práctico de CAI. Es una aplicación de escritorio en **C# / Windows Forms (.NET 10)** que cubre el circuito de salida de la mercadería: desde que el cliente pide un retiro hasta que el transportista se la lleva.

## Alcance de esta entrega

La entrega se concentra en cuatro pantallas, que forman el circuito de salida:

| # | Pantalla | Qué resuelve |
|---|---|---|
| 1 | **Registrar Orden de Preparación** | El cliente pide retirar mercadería de un depósito. La orden queda `Pendiente` y compromete el stock disponible. |
| 2 | **Cumplir Orden de Selección** | Almacenamiento baja de los racks lo que pide la orden de selección y lo entrega a Preparación. La mercadería sale de su posición y las órdenes de preparación quedan listas para empaquetar. |
| 3 | **Empaquetar pedido y generar orden de entrega** | Preparación empaqueta la orden, que pasa a `Preparada`, y se genera la orden de entrega `Pendiente`. |
| 4 | **Entregar mercadería al transportista** | Despacho valida el DNI del transportista autorizado, emite el remito (original y copia) y, con la copia firmada, confirma el despacho. |

Estados de una orden de preparación a lo largo del circuito: `Pendiente` → `EnPreparacion` → `Preparada` → `Cumplida`.

## Arquitectura

```
Formulario (pantalla)  ⇄  Modelo  ⇄  Almacenes  ⇄  Datos/*.json
```

- **Formularios y modelos** (una carpeta por pantalla): el formulario solo muestra y captura datos; toda la lógica está en el modelo.
- **`Almacenes/`**: las entidades (`xxxxEntidad`), sus clases auxiliares, los enums y un almacén por entidad (`xxxxAlmacen`). Cada almacén carga su archivo de `Datos/` al primer uso y lo escribe con `Guardar()`.
- **`Datos/`**: los datos de prueba en JSON. Los enums se guardan por nombre (por ejemplo, `"Cumplida"`).

Reglas de diseño que respeta el código:
1. La firma de los modelos no cambia: los métodos devuelven las mismas clases de pantalla que el prototipo.
2. Las entidades nunca llegan a los formularios: el modelo las convierte a las clases de cada pantalla.
3. Las búsquedas en los almacenes las hace el modelo; las entidades no tienen métodos de búsqueda.

## Cómo ejecutarlo

1. Abrir `GrupoE.slnx` en Visual Studio y compilar.
2. Los archivos de `Datos/` se copian siempre al directorio de salida (`bin/Debug/net10.0-windows/Datos/`). La aplicación lee y guarda **esa copia**: cada compilación la vuelve a dejar igual a la del repositorio.
3. Al cerrar la aplicación, `Program.cs` guarda todos los almacenes.

Hay clientes en los cuatro depósitos: Bodegas del Sol solo en Mendoza, Lácteos Serranos solo en Córdoba, Agro Litoral en Rosario y Córdoba, y Ferretería Federal en Gran Buenos Aires, Rosario y Mendoza. El `CodigoDeposito` del cliente es su depósito principal (donde se recibe mercadería); los demás depósitos surgen de dónde tiene stock. El stock de su depósito principal está respaldado por una orden de recepción `Cumplida` (1057 a 1060) con su remito.

Los datos de prueba incluyen selecciones pendientes en los cuatro depósitos, órdenes listas para empaquetar y transportistas con órdenes listas para retirar (por ejemplo, DNI `31555777`, `33222444`, `29444666`, `35111222` y `32777888`).

## Documentación

Cada pantalla de la entrega tiene en su carpeta su caso de uso y su diagrama de secuencia (en Mermaid, que GitHub muestra como diagrama):

| Pantalla | Caso de uso | Diagrama de secuencia |
|---|---|---|
| Registrar Orden de Preparación | `RegistrarOrdenDePreparacion/Caso de uso RegistrarOrdenDePreparacion.docx` | `RegistrarOrdenDePreparacion/Diagrama_de_secuencia_RegistrarOrdenDePreparacion.md` |
| Cumplir Orden de Selección | `CumplirOrdenSeleccion/Caso de uso orden seleccion.docx` | `CumplirOrdenSeleccion/Diagrama_de_secuencia_CumplirOrdenSeleccion.md` |
| Empaquetar pedido | `EmpaquetarPedido/Caso de uso EmpaquetarPedido.docx` | `EmpaquetarPedido/Diagrama_de_secuencia_EmpaquetarPedido.md` |
| Entregar mercadería al transportista | `EntregaATransportista/Caso de uso - Entregar mercadería al transportista.docx` | `EntregaATransportista/Diagrama_de_secuencia_EntregaATransportista.md` |

El diagrama de clases se entrega aparte. Los diagramas de secuencia siguen el código; cuando muestran algo que todavía no está programado, figura en la sección siguiente.

## Pendientes de implementación

Funcionalidad que los casos de uso y los diagramas ya describen, pero que el código todavía no tiene:

| Pantalla | Pendiente | Estado actual del código |
|---|---|---|
| General | **Inicio de sesión.** El usuario de cada operación es el operario identificado en el sistema. | Se usa el usuario de Windows (`Environment.UserName`). |
| Registrar Orden de Preparación | **Cliente logueado.** La orden se registra para el cliente que inició sesión. | Cada vez que se abre la pantalla toma al azar uno de los clientes con contrato vigente y stock disponible. |
| Registrar Orden de Preparación | **Modalidad Palet cerrado.** Pedido y control de stock en palets. | Responde "La funcionalidad de pallets aún no está implementada". Solo funciona Fulfillment. |
| Cumplir Orden de Selección | **Generación de órdenes de selección.** El sistema las arma en cada corte a partir de las órdenes de preparación `Pendiente`. | No hay ningún proceso que las genere; se usan las de `Datos/OrdenesSeleccion.json`. |
| Empaquetar | **Orden tomada por un puesto.** La marca `TomadaPor` se guarda en `OrdenPreparacionEntidad` para que otro puesto no tome la misma orden. | La marca vive solo en memoria de cada pantalla. |
| Empaquetar | **Servicios facturados.** Al confirmar el empaquetado se registra el derecho a cobro (`ServicioFacturadoEntidad`, `ServiciosFacturadosAlmacen`, `Datos/ServiciosFacturados.json`). | La entidad, el almacén y el JSON no existen todavía. |

Mejoras identificadas, sin implementar:
- **Cumplir Orden de Selección:** el modelo arma y descuenta un inventario en memoria (`Inventario`) que no se usa; el stock real se actualiza en `MercaderiasAlmacen`. Se puede simplificar.
- **Datos de prueba:** el remito `5001` quedó `Emitido`, por lo que su orden de entrega `3002` no se ofrece para un nuevo remito.

Fuera del alcance de esta entrega:
- Las pantallas **Recepción de Mercadería** e **Ingreso de Stock**: están conectadas a los almacenes, pero no forman parte de la documentación de esta entrega.
- El manejo de pallets en la recepción y el almacenamiento (rama `pallet_includ`, sin integrar).
