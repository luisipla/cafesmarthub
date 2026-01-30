namespace CafeSmartHub.Client.Models;

public class ProveedorReadDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Contacto { get; set; } = string.Empty;
    public string ProductosSuministrados { get; set; } = string.Empty;
    public string CondicionesEntrega { get; set; } = string.Empty;
    public int DiasDespacho { get; set; }
    public bool Activo { get; set; }
}
