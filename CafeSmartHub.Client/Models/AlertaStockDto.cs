namespace CafeSmartHub.Client.Models;

public class AlertaStockDto
{
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; } = string.Empty;
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty;
}
