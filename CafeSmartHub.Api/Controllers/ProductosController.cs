using CafeSmartHub.Api.DTOs.Productos;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeSmartHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Productos")]
[Authorize(Roles = "Admin,User")] // ✅ Por defecto: ambos pueden acceder
public class ProductosController : ControllerBase
{
    private readonly IProductosService _svc;

    public ProductosController(IProductosService svc)
    {
        _svc = svc;
    }

    // GET: api/Productos
    [HttpGet]
    public async Task<ActionResult<List<ProductoReadDto>>> GetAll()
        => Ok(await _svc.GetAll());

    // GET: api/Productos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoReadDto>> GetById(int id)
    {
        var prod = await _svc.GetById(id);
        return prod is null ? NotFound() : Ok(prod);
    }

    // POST: api/Productos
    [HttpPost]
    [Authorize(Roles = "Admin")] // ✅ Solo Admin
    public async Task<ActionResult<ProductoReadDto>> Create([FromBody] ProductoCreateDto dto)
    {
        var created = await _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Productos
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")] // ✅ Solo Admin
    public async Task<IActionResult> Update(int id, [FromBody] ProductoUpdateDto dto)
    {
        var ok = await _svc.Update(id, dto);
        return ok ? NoContent() : NotFound();
    }

    // DELETE: api/Productos/5 (borrado lógico)
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] // ✅ Solo Admin
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.Delete(id);
        return ok ? NoContent() : NotFound();
    }
}
