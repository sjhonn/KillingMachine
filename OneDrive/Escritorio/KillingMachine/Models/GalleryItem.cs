using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class GalleryItem
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(500)]
    [Display(Name = "Ruta o URL de imagen")]
    public string ImageUrl { get; set; } = "/images/killing-machine-logo.png";

    [StringLength(1000)]
    public string? Description { get; set; }

    [Range(0, 1000)]
    [Display(Name = "Orden")]
    public int DisplayOrder { get; set; }

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;
}
