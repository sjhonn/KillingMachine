using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class WorkoutLog
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Cliente")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    [Required]
    [Display(Name = "Ejercicio")]
    public int ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    public DateTime WorkoutDate { get; set; } = DateTime.Today;

    [Range(1, 100)]
    [Display(Name = "Series")]
    public int Sets { get; set; }

    [Range(1, 1000)]
    [Display(Name = "Repeticiones")]
    public int Repetitions { get; set; }

    [Range(0, 1000)]
    [Display(Name = "Peso usado (kg)")]
    public double WeightKg { get; set; }

    [Range(1, 1440)]
    [Display(Name = "Duracion (minutos)")]
    public int DurationMinutes { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }
}
