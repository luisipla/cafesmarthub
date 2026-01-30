using CafeSmartHub.Api.DTOs.Categorias;
using CafeSmartHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeSmartHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Categorias")]
[Authorize(Roles = "Admin,User")] // acceder a categorias user y admin
public class CategoriasController : ControllerBase
{
    private readonly ICategoriasService _svc;

    public CategoriasController(ICategoriasService svc)
    {
        _svc = svc;
    }

    // GET: api/Categorias?activa=true/false
    [HttpGet]
    public async Task<ActionResult<List<CategoriaReadDto>>> GetAll([FromQuery] bool? activa = null)
        => Ok(await _svc.GetAll(activa));

    // GET: api/Categorias/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaReadDto>> GetById(int id)
    {
        var cat = await _svc.GetById(id);
        return cat is null ? NotFound() : Ok(cat);
    }

    // POST: api/Categorias
    [HttpPost]
    [Authorize(Roles = "Admin")] // Solo Admin
    public async Task<ActionResult<CategoriaReadDto>> Create([FromBody] CategoriaCreateDto dto)
    {
        var created = await _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Categorias/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")] //  Solo Admin
    public async Task<IActionResult> Update(int id, [FromBody] CategoriaUpdateDto dto)
    {
        var ok = await _svc.Update(id, dto);
        return ok ? NoContent() : NotFound();
    }

    // DELETE: api/Categorias/5  (borrado lógico)
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] // Solo Admin
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.Delete(id);
        return ok ? NoContent() : NotFound();
    }
}
