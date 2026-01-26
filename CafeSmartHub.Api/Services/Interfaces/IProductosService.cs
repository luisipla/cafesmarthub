using CafeSmartHub.Api.DTOs.Productos;

namespace CafeSmartHub.Api.Services.Interfaces;

public interface IProductosService
{
    Task<List<ProductoReadDto>> GetAll(bool? activo = null, bool? bajoStock = null);
    Task<ProductoReadDto?> GetById(int id);
    Task<ProductoReadDto> Create(ProductoCreateDto dto);
    Task<bool> Update(int id, ProductoUpdateDto dto);
    Task<bool> Delete(int id); // borrado lógico (Activo=false)
}
