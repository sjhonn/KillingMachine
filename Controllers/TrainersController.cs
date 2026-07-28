using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class TrainersController : CrudController<Trainer>
{
    public TrainersController(AppDbContext db) : base(db) { }
}
