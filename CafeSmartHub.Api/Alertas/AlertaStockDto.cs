namespace CafeSmartHub.Api.DTOs.Alertas;

public class AlertaStockDto
{
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; } = string.Empty;
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public int Diferencia { get; set; } // StockMinimo - StockActual (si > 0 hay alerta)
}
