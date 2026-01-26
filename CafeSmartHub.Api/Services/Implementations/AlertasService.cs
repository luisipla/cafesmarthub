using CafeSmartHub.Api.Data;
using CafeSmartHub.Api.DTOs.Alertas;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeSmartHub.Api.Services.Implementations;

public class AlertasService : IAlertasService
{
    private readonly AppDbContext _db;
    public AlertasService(AppDbContext db) => _db = db;

    public async Task<List<AlertaStockDto>> GetAlertasStockMinimo()
    {
        return await _db.Productos
            .AsNoTracking()
            .Where(p => p.Activo && p.StockActual <= p.StockMinimo)
            .OrderBy(p => p.StockActual)
            .Select(p => new AlertaStockDto
            {
                ProductoId = p.Id,
                ProductoNombre = p.Nombre,
                StockActual = p.StockActual,
                StockMinimo = p.StockMinimo,
                Diferencia = p.StockMinimo - p.StockActual
            })
            .ToListAsync();
    }
}
