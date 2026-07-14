using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class WorkoutPlan
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Objective { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Level { get; set; } = "Intermedio";

    [Range(1, 52)]
    [Display(Name = "Duracion (semanas)")]
    public int DurationWeeks { get; set; }

    [Range(1, 14)]
    [Display(Name = "Sesiones por semana")]
    public int SessionsPerWeek { get; set; }

    [Required, StringLength(3000)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;
}
