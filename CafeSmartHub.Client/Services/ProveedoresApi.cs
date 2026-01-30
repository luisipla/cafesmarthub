using System.Net.Http.Json;
using CafeSmartHub.Client.Models;

namespace CafeSmartHub.Client.Services;

public class ProveedoresApi
{
    private readonly HttpClient _http;
    public ProveedoresApi(HttpClient http) => _http = http;

    public Task<List<ProveedorReadDto>?> GetAll(bool? activo = null)
    {
        var url = "api/Proveedores";
        if (activo.HasValue)
            url += $"?activo={activo.Value.ToString().ToLower()}";

        return _http.GetFromJsonAsync<List<ProveedorReadDto>>(url);
    }

    public Task<ProveedorReadDto?> GetById(int id)
        => _http.GetFromJsonAsync<ProveedorReadDto>($"api/Proveedores/{id}");

    public async Task<ProveedorReadDto> Create(ProveedorCreateDto dto)
    {
        var resp = await _http.PostAsJsonAsync("api/Proveedores", dto);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());

        return (await resp.Content.ReadFromJsonAsync<ProveedorReadDto>())!;
    }

    public async Task Update(int id, ProveedorUpdateDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/Proveedores/{id}", dto);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());
    }

    public async Task Delete(int id)
    {
        var resp = await _http.DeleteAsync($"api/Proveedores/{id}");
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());
    }
}
