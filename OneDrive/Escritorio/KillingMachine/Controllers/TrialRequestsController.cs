using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class TrialRequestsController : CrudController<TrialRequest>
{
    public TrialRequestsController(AppDbContext db) : base(db) { }
}
