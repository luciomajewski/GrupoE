using System.Text.Json.Serialization;

namespace GrupoE.Almacenes;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum EstadoOrdenEntregaEnum
{
    Pendiente,
    Despachada
}
