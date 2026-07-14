using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class ServicesController : CrudController<Service>
{
    public ServicesController(AppDbContext db) : base(db) { }
}
