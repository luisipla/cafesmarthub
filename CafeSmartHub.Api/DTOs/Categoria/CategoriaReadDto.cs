namespace CafeSmartHub.Api.DTOs.Categorias;

public class CategoriaReadDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activa { get; set; }
}
