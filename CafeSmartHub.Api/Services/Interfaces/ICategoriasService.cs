using CafeSmartHub.Api.DTOs.Categorias;

namespace CafeSmartHub.Api.Services.Interfaces;

public interface ICategoriasService
{
    Task<List<CategoriaReadDto>> GetAll(bool? activa = null);
    Task<CategoriaReadDto?> GetById(int id);
    Task<CategoriaReadDto> Create(CategoriaCreateDto dto);
    Task<bool> Update(int id, CategoriaUpdateDto dto);
    Task<bool> Delete(int id);
}
