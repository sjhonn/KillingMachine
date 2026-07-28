using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillingMachine.Models;

public class Service
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 100000)]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Precio")]
    public decimal Price { get; set; }

    [Range(1, 600)]
    [Display(Name = "Duracion (minutos)")]
    public int DurationMinutes { get; set; }

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;
}
