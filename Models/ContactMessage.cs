using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class ContactMessage
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    [Display(Name = "Nombre completo")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    [Display(Name = "Telefono")]
    public string? Phone { get; set; }

    [Required, StringLength(160)]
    public string Subject { get; set; } = string.Empty;

    [Required, StringLength(3000), MinLength(10)]
    public string Message { get; set; } = string.Empty;

    [Display(Name = "Fecha de registro")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required, StringLength(30)]
    public string Status { get; set; } = "Pendiente";
}
