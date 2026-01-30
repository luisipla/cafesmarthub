using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSmartHub.Api.Models;

public class Proveedor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    // En BD es longtext NOT NULL
    [Required]
    public string Contacto { get; set; } = string.Empty;

    // En BD es longtext NOT NULL
    [Required]
    public string ProductosSuministrados { get; set; } = string.Empty;

    // En BD es longtext NOT NULL
    [Required]
    public string CondicionesEntrega { get; set; } = string.Empty;

    // En BD es int NOT NULL
    public int DiasDespacho { get; set; }

    // En BD es tinyint(1) NOT NULL
    public bool Activo { get; set; } = true;
}
