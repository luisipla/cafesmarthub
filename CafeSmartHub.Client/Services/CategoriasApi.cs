using System.Net.Http.Json;
using CafeSmartHub.Client.Models;

namespace CafeSmartHub.Client.Services;

public class CategoriasApi
{
    private readonly HttpClient _http;
    public CategoriasApi(HttpClient http) => _http = http;

    public Task<List<CategoriaReadDto>?> GetAll(bool? activa = null)
    {
        var url = "api/Categorias";
        if (activa.HasValue)
            url += $"?activa={activa.Value.ToString().ToLower()}";

        return _http.GetFromJsonAsync<List<CategoriaReadDto>>(url);
    }

    public Task<CategoriaReadDto?> GetById(int id)
        => _http.GetFromJsonAsync<CategoriaReadDto>($"api/Categorias/{id}");

    public async Task<CategoriaReadDto> Create(CategoriaCreateDto dto)
    {
        var resp = await _http.PostAsJsonAsync("api/Categorias", dto);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());

        return (await resp.Content.ReadFromJsonAsync<CategoriaReadDto>())!;
    }

    public async Task Update(int id, CategoriaUpdateDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/Categorias/{id}", dto);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());
    }

    public async Task Delete(int id)
    {
        var resp = await _http.DeleteAsync($"api/Categorias/{id}");
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException(await resp.Content.ReadAsStringAsync());
    }
}
