using System.Net.Http.Json;
using CafeSmartHub.Client.Models;

namespace CafeSmartHub.Client.Services;

public class ProductosApi
{
    private readonly HttpClient _http;
    public ProductosApi(HttpClient http) => _http = http;

    public Task<List<ProductoReadDto>?> GetAll(bool? activo = null, bool bajoStock = false)
    {
        var qs = new List<string>();
        if (activo.HasValue) qs.Add($"activo={activo.Value.ToString().ToLower()}");
        if (bajoStock) qs.Add("bajoStock=true");

        var url = "api/Productos" + (qs.Count > 0 ? "?" + string.Join("&", qs) : "");
        return _http.GetFromJsonAsync<List<ProductoReadDto>>(url);
    }

    public Task<ProductoReadDto?> GetById(int id)
        => _http.GetFromJsonAsync<ProductoReadDto>($"api/Productos/{id}");

    public async Task<ProductoReadDto> Create(ProductoCreateDto dto)
    {
        var resp = await _http.PostAsJsonAsync("api/Productos", dto);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());

        return (await resp.Content.ReadFromJsonAsync<ProductoReadDto>())!;
    }

    public async Task Update(int id, ProductoUpdateDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/Productos/{id}", dto);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());
    }

    public async Task Delete(int id)
    {
        var resp = await _http.DeleteAsync($"api/Productos/{id}");
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());
    }
}
