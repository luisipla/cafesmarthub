using System.ComponentModel.DataAnnotations;

namespace CafeSmartHub.Client.Models;

public class ProductoUpdateDto
{
    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int StockActual { get; set; }

    [Range(0, int.MaxValue)]
    public int StockMinimo { get; set; }

    public bool Activo { get; set; } = true;

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
    public int CategoriaProductoId { get; set; }

    [MaxLength(60)]
    public string? UnidadMedida { get; set; }
}