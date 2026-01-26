using System.ComponentModel.DataAnnotations;

namespace CafeSmartHub.Client.Models;

public class ProductoCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(160)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La unidad de medida es obligatoria")]
    [StringLength(50)]
    public string UnidadMedida { get; set; } = "Unidad";

    [Range(0, 999999999, ErrorMessage = "Precio inválido")]
    public decimal Precio { get; set; }

    [Range(0, 999999, ErrorMessage = "Stock actual inválido")]
    public int StockActual { get; set; }

    [Range(0, 999999, ErrorMessage = "Stock mínimo inválido")]
    public int StockMinimo { get; set; }

    public bool Activo { get; set; } = true;

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona una categoría")]
    public int CategoriaProductoId { get; set; }
}
