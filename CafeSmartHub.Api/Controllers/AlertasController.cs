using CafeSmartHub.Api.DTOs.Alertas;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeSmartHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Alertas")]
[Authorize(Roles = "Admin,User")] //  ver alertas
public class AlertasController : ControllerBase
{
    private readonly IAlertasService _svc;

    public AlertasController(IAlertasService svc)
        => _svc = svc;

    [HttpGet("stock-minimo")]
    public async Task<ActionResult<List<AlertaStockDto>>> GetStockMinimo()
        => Ok(await _svc.GetAlertasStockMinimo());
}
