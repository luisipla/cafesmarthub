using CafeSmartHub.Api.DTOs.Alertas;

namespace CafeSmartHub.Api.Services.Interfaces;

public interface IAlertasService
{
    Task<List<AlertaStockDto>> GetAlertasStockMinimo();
}
