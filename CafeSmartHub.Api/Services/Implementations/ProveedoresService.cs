using CafeSmartHub.Api.Data;
using CafeSmartHub.Api.DTOs.Proveedores;
using CafeSmartHub.Api.Models;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeSmartHub.Api.Services.Implementations;

public class ProveedoresService : IProveedoresService
{
    private readonly AppDbContext _db;

    public ProveedoresService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ProveedorReadDto>> GetAll(bool? activo = null)
    {
        var q = _db.Proveedores.AsNoTracking().AsQueryable();

        if (activo.HasValue)
            q = q.Where(x => x.Activo == activo.Value);

        return await q
            .OrderBy(x => x.Nombre)
            .Select(x => new ProveedorReadDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Contacto = x.Contacto,
                ProductosSuministrados = x.ProductosSuministrados,
                CondicionesEntrega = x.CondicionesEntrega,
                DiasDespacho = x.DiasDespacho,
                Activo = x.Activo
            })
            .ToListAsync();
    }

    public async Task<ProveedorReadDto?> GetById(int id)
    {
        var p = await _db.Proveedores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return null;

        return new ProveedorReadDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Contacto = p.Contacto,
            ProductosSuministrados = p.ProductosSuministrados,
            CondicionesEntrega = p.CondicionesEntrega,
            DiasDespacho = p.DiasDespacho,
            Activo = p.Activo
        };
    }

    public async Task<ProveedorReadDto> Create(ProveedorCreateDto dto)
    {
        dto.Nombre = dto.Nombre.Trim();
        dto.Contacto = dto.Contacto.Trim();
        dto.ProductosSuministrados = dto.ProductosSuministrados.Trim();
        dto.CondicionesEntrega = dto.CondicionesEntrega.Trim();

        var entity = new Proveedor
        {
            Nombre = dto.Nombre,
            Contacto = dto.Contacto,
            ProductosSuministrados = dto.ProductosSuministrados,
            CondicionesEntrega = dto.CondicionesEntrega,
            DiasDespacho = dto.DiasDespacho,
            Activo = dto.Activo
        };

        _db.Proveedores.Add(entity);
        await _db.SaveChangesAsync();

        return new ProveedorReadDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Contacto = entity.Contacto,
            ProductosSuministrados = entity.ProductosSuministrados,
            CondicionesEntrega = entity.CondicionesEntrega,
            DiasDespacho = entity.DiasDespacho,
            Activo = entity.Activo
        };
    }

    public async Task<bool> Update(int id, ProveedorUpdateDto dto)
    {
        var entity = await _db.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) return false;

        dto.Nombre = dto.Nombre.Trim();
        dto.Contacto = dto.Contacto.Trim();
        dto.ProductosSuministrados = dto.ProductosSuministrados.Trim();
        dto.CondicionesEntrega = dto.CondicionesEntrega.Trim();

        entity.Nombre = dto.Nombre;
        entity.Contacto = dto.Contacto;
        entity.ProductosSuministrados = dto.ProductosSuministrados;
        entity.CondicionesEntrega = dto.CondicionesEntrega;
        entity.DiasDespacho = dto.DiasDespacho;
        entity.Activo = dto.Activo;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _db.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) return false;

        _db.Proveedores.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
