using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class Client
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    [Display(Name = "Nombre completo")]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(20)]
    [Display(Name = "Documento")]
    public string DocumentNumber { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    [Display(Name = "Telefono")]
    public string Phone { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de nacimiento")]
    public DateTime BirthDate { get; set; } = DateTime.Today.AddYears(-18);

    [Range(80, 250)]
    [Display(Name = "Altura (cm)")]
    public double HeightCm { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de ingreso")]
    public DateTime JoinDate { get; set; } = DateTime.Today;

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;

    [StringLength(1000)]
    public string? Notes { get; set; }

    public ICollection<WorkoutLog> WorkoutLogs { get; set; } = new List<WorkoutLog>();
    public ICollection<BodyMeasurement> BodyMeasurements { get; set; } = new List<BodyMeasurement>();
    public ICollection<WeeklySchedule> WeeklySchedules { get; set; } = new List<WeeklySchedule>();
    public ICollection<ClientMembership> Memberships { get; set; } = new List<ClientMembership>();
}
