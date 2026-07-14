using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class Exercise
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(80)]
    [Display(Name = "Grupo muscular")]
    public string MuscleGroup { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(120)]
    public string? Equipment { get; set; }

    [Required, StringLength(30)]
    public string Difficulty { get; set; } = "Intermedio";

    [Range(0, 3000)]
    [Display(Name = "Calorias por hora")]
    public int CaloriesPerHour { get; set; }

    public ICollection<WorkoutLog> WorkoutLogs { get; set; } = new List<WorkoutLog>();
}
