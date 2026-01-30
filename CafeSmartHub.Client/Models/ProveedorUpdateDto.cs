using System.ComponentModel.DataAnnotations;

namespace CafeSmartHub.Client.Models;

public class ProveedorUpdateDto
{
    [Required, MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Contacto { get; set; } = string.Empty;

    [Required]
    public string ProductosSuministrados { get; set; } = string.Empty;

    [Required]
    public string CondicionesEntrega { get; set; } = string.Empty;

    [Range(0, 365)]
    public int DiasDespacho { get; set; }

    public bool Activo { get; set; } = true;
}
