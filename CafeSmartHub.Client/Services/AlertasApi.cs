using System.Net.Http.Json;
using CafeSmartHub.Client.Models;

namespace CafeSmartHub.Client.Services;

public class AlertasApi
{
    private readonly HttpClient _http;
    public AlertasApi(HttpClient http) => _http = http;

    public Task<List<AlertaStockDto>?> GetStockMinimo()
        => _http.GetFromJsonAsync<List<AlertaStockDto>>("api/Alertas/stock-minimo");
}
