namespace CafeSmartHub.Api.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? UnidadMedida { get; set; }
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public bool Activo { get; set; } = true;

    public int CategoriaProductoId { get; set; }
    public CategoriaProducto? CategoriaProducto { get; set; }
}
