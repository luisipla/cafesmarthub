using CafeSmartHub.Api.Data;
using CafeSmartHub.Api.DTOs.Productos;
using CafeSmartHub.Api.Models;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeSmartHub.Api.Services.Implementations;

public class ProductosService : IProductosService
{
    private readonly AppDbContext _db;
    public ProductosService(AppDbContext db) => _db = db;

    public async Task<List<ProductoReadDto>> GetAll(bool? activo = null, bool? bajoStock = null)
    {
        var q = _db.Productos
            .AsNoTracking()
            .Include(p => p.CategoriaProducto)
            .AsQueryable();

        if (activo.HasValue)
            q = q.Where(p => p.Activo == activo.Value);

        if (bajoStock.HasValue && bajoStock.Value)
            q = q.Where(p => p.StockActual <= p.StockMinimo);

        return await q
            .OrderBy(p => p.Nombre)
            .Select(p => new ProductoReadDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                UnidadMedida = p.UnidadMedida,
                Precio = p.Precio,
                StockActual = p.StockActual,
                StockMinimo = p.StockMinimo,
                Activo = p.Activo,
                CategoriaProductoId = p.CategoriaProductoId,
                CategoriaNombre = p.CategoriaProducto!.Nombre
            })
            .ToListAsync();
    }

    public async Task<ProductoReadDto?> GetById(int id)
    {
        var p = await _db.Productos
            .AsNoTracking()
            .Include(x => x.CategoriaProducto)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (p is null) return null;

        return new ProductoReadDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            UnidadMedida = p.UnidadMedida,
            Precio = p.Precio,
            StockActual = p.StockActual,
            StockMinimo = p.StockMinimo,
            Activo = p.Activo,
            CategoriaProductoId = p.CategoriaProductoId,
            CategoriaNombre = p.CategoriaProducto!.Nombre
        };
    }

    public async Task<ProductoReadDto> Create(ProductoCreateDto dto)
    {
        dto.Nombre = dto.Nombre.Trim();

        var categoriaExiste = await _db.CategoriasProducto.AnyAsync(c => c.Id == dto.CategoriaProductoId && c.Activa);
        if (!categoriaExiste) throw new InvalidOperationException("La categoría indicada no existe o está inactiva.");

        var entity = new Producto
        {
            Nombre = dto.Nombre,
            UnidadMedida = dto.UnidadMedida,
            Precio = dto.Precio,
            StockActual = dto.StockActual,
            StockMinimo = dto.StockMinimo,
            Activo = dto.Activo,
            CategoriaProductoId = dto.CategoriaProductoId
        };

        _db.Productos.Add(entity);
        await _db.SaveChangesAsync();

        var read = await _db.Productos.AsNoTracking()
            .Include(p => p.CategoriaProducto)
            .Where(p => p.Id == entity.Id)
            .Select(p => new ProductoReadDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                UnidadMedida = p.UnidadMedida,
                Precio = p.Precio,
                StockActual = p.StockActual,
                StockMinimo = p.StockMinimo,
                Activo = p.Activo,
                CategoriaProductoId = p.CategoriaProductoId,
                CategoriaNombre = p.CategoriaProducto!.Nombre
            }).FirstAsync();

        return read;
    }

    public async Task<bool> Update(int id, ProductoUpdateDto dto)
    {
        var entity = await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null) return false;

        dto.Nombre = dto.Nombre.Trim();

        var categoriaExiste = await _db.CategoriasProducto.AnyAsync(c => c.Id == dto.CategoriaProductoId && c.Activa);
        if (!categoriaExiste) throw new InvalidOperationException("La categoría indicada no existe o está inactiva.");

        entity.Nombre = dto.Nombre;
        entity.UnidadMedida = dto.UnidadMedida;
        entity.Precio = dto.Precio;
        entity.StockActual = dto.StockActual;
        entity.StockMinimo = dto.StockMinimo;
        entity.Activo = dto.Activo;
        entity.CategoriaProductoId = dto.CategoriaProductoId;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        // Buscamos el producto por su ID
        var entity = await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);

        // Si el producto no existe en la base de datos, retornamos false
        if (entity is null) return false;

        // ELIMINACIÓN FÍSICA: Quitamos el registro de la base de datos definitivamente
        _db.Productos.Remove(entity);

        // Guardamos los cambios para que se aplique en Aiven Cloud
        await _db.SaveChangesAsync();

        return true;
    }
}