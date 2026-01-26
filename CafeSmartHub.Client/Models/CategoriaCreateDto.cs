using System.ComponentModel.DataAnnotations;

namespace CafeSmartHub.Client.Models;

public class CategoriaCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(120)]
    public string Nombre { get; set; } = string.Empty;

    public bool Activa { get; set; } = true;
}
