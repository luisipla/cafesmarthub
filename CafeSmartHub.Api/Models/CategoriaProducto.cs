namespace CafeSmartHub.Api.Models;

public class CategoriaProducto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activa { get; set; } = true;

    public List<Producto> Productos { get; set; } = new();
}
