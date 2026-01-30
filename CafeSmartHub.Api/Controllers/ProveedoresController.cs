using CafeSmartHub.Api.DTOs.Proveedores;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeSmartHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Proveedores")]
[Authorize(Roles = "Admin,User")] //  acceder los dos
public class ProveedoresController : ControllerBase
{
    private readonly IProveedoresService _svc;

    public ProveedoresController(IProveedoresService svc)
    {
        _svc = svc;
    }

    // GET: api/Proveedores?activo=true/false
    [HttpGet]
    public async Task<ActionResult<List<ProveedorReadDto>>> GetAll([FromQuery] bool? activo = null)
        => Ok(await _svc.GetAll(activo));

    // GET: api/Proveedores/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProveedorReadDto>> GetById(int id)
    {
        var prov = await _svc.GetById(id);
        return prov is null ? NotFound() : Ok(prov);
    }

    // POST: api/Proveedores
    [HttpPost]
    [Authorize(Roles = "Admin")] // Solo Admin
    public async Task<ActionResult<ProveedorReadDto>> Create([FromBody] ProveedorCreateDto dto)
    {
        var created = await _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Proveedores/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")] // Solo Admin
    public async Task<IActionResult> Update(int id, [FromBody] ProveedorUpdateDto dto)
    {
        var ok = await _svc.Update(id, dto);
        return ok ? NoContent() : NotFound();
    }

    // DELETE: api/Proveedores/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] // Solo Admin
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.Delete(id);
        return ok ? NoContent() : NotFound();
    }
}
