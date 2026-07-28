using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillingMachine.Models;

public class ClientMembership
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Cliente")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    [Required]
    [Display(Name = "Plan")]
    public int MembershipPlanId { get; set; }
    public MembershipPlan? MembershipPlan { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha inicio")]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha fin")]
    public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(1);

    [Required, StringLength(30)]
    public string Status { get; set; } = "Activa";

    [Range(0, 100000)]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Monto pagado")]
    public decimal AmountPaid { get; set; }
}
