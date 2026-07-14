using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillingMachine.Models;

public class MembershipPlan
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 100000)]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Precio mensual")]
    public decimal MonthlyPrice { get; set; }

    [Range(1, 36)]
    [Display(Name = "Duracion (meses)")]
    public int DurationMonths { get; set; } = 1;

    [Required, StringLength(1000)]
    public string Benefits { get; set; } = string.Empty;

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;

    public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
}
