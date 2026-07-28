using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class TrialRequest
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    [Display(Name = "Nombre completo")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    [Display(Name = "Telefono")]
    public string Phone { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha preferida")]
    public DateTime PreferredDate { get; set; } = DateTime.Today.AddDays(1);

    [Required, StringLength(5)]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Use el formato HH:mm")]
    [Display(Name = "Hora preferida")]
    public string PreferredTime { get; set; } = "18:00";

    [Required, StringLength(1000)]
    public string Objective { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Status { get; set; } = "Pendiente";

    [Display(Name = "Fecha de registro")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
