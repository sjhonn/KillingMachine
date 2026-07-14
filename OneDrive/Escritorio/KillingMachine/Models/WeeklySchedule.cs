using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class WeeklySchedule
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Cliente")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "Dia")]
    public string DayOfWeek { get; set; } = "Lunes";

    [Required, StringLength(5)]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Use el formato HH:mm")]
    [Display(Name = "Hora inicio")]
    public string StartTime { get; set; } = "08:00";

    [Required, StringLength(5)]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Use el formato HH:mm")]
    [Display(Name = "Hora fin")]
    public string EndTime { get; set; } = "09:00";

    [Required, StringLength(120)]
    public string Activity { get; set; } = string.Empty;

    [Display(Name = "Entrenador")]
    public int? TrainerId { get; set; }
    public Trainer? Trainer { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }
}
