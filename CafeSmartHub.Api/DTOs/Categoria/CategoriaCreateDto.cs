namespace CafeSmartHub.Api.DTOs.Categorias;

public class CategoriaCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public bool Activa { get; set; } = true;
}
