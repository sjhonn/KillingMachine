using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class Trainer
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    [Display(Name = "Nombre completo")]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Specialty { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    [Display(Name = "Telefono")]
    public string Phone { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Biography { get; set; } = string.Empty;

    [Required, StringLength(200)]
    [Display(Name = "Horario")]
    public string Schedule { get; set; } = string.Empty;

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;

    public ICollection<WeeklySchedule> WeeklySchedules { get; set; } = new List<WeeklySchedule>();
}
