namespace CafeSmartHub.Client.Models;

public class ProductoReadDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    public int StockActual { get; set; }

    public int StockMinimo { get; set; }

    public bool Activo { get; set; }

    public int CategoriaProductoId { get; set; }

    public string? UnidadMedida { get; set; }
}
