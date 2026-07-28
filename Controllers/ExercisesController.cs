using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class ExercisesController : CrudController<Exercise>
{
    public ExercisesController(AppDbContext db) : base(db) { }
}
