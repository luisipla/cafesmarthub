using CafeSmartHub.Api.DTOs.Proveedores;

namespace CafeSmartHub.Api.Services.Interfaces;

public interface IProveedoresService
{
    Task<List<ProveedorReadDto>> GetAll(bool? activo = null);
    Task<ProveedorReadDto?> GetById(int id);
    Task<ProveedorReadDto> Create(ProveedorCreateDto dto);
    Task<bool> Update(int id, ProveedorUpdateDto dto);
    Task<bool> Delete(int id);
}
