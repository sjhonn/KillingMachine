using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class WorkoutPlansController : CrudController<WorkoutPlan>
{
    public WorkoutPlansController(AppDbContext db) : base(db) { }
}
