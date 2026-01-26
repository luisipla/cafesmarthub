using CafeSmartHub.Api.Data;
using CafeSmartHub.Api.DTOs.Categorias;
using CafeSmartHub.Api.Models;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeSmartHub.Api.Services.Implementations;

public class CategoriasService : ICategoriasService
{
    private readonly AppDbContext _db;

    public CategoriasService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CategoriaReadDto>> GetAll(bool? activa = null)
    {
        var q = _db.CategoriasProducto.AsNoTracking().AsQueryable();

        if (activa.HasValue)
            q = q.Where(x => x.Activa == activa.Value);

        return await q
            .OrderBy(x => x.Nombre)
            .Select(x => new CategoriaReadDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Activa = x.Activa
            })
            .ToListAsync();
    }

    public async Task<CategoriaReadDto?> GetById(int id)
    {
        var c = await _db.CategoriasProducto.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (c is null) return null;

        return new CategoriaReadDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Activa = c.Activa
        };
    }

    public async Task<CategoriaReadDto> Create(CategoriaCreateDto dto)
    {
        dto.Nombre = dto.Nombre.Trim();

        var entity = new CategoriaProducto
        {
            Nombre = dto.Nombre,
            Activa = dto.Activa
        };

        _db.CategoriasProducto.Add(entity);
        await _db.SaveChangesAsync();

        return new CategoriaReadDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Activa = entity.Activa
        };
    }

    public async Task<bool> Update(int id, CategoriaUpdateDto dto)
    {
        var entity = await _db.CategoriasProducto.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null) return false;

        dto.Nombre = dto.Nombre.Trim();
        entity.Nombre = dto.Nombre;
        entity.Activa = dto.Activa;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        // categoría 
        var entity = await _db.CategoriasProducto.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null) return false;

        // elimina la fila de la tabla)
        _db.CategoriasProducto.Remove(entity);

        await _db.SaveChangesAsync();
        return true;
    }
}