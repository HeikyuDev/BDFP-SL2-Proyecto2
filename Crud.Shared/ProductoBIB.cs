using System.ComponentModel.DataAnnotations;

public class ProductoBIB
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string Nombre { get; set; } = null!;

    [Required]
    [Range(0.01, float.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0.")]
    public float Precio { get; set; }
}
