using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class MembershipPlansController : CrudController<MembershipPlan>
{
    public MembershipPlansController(AppDbContext db) : base(db) { }
}
