using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class ClientsController : CrudController<Client>
{
    public ClientsController(AppDbContext db) : base(db) { }
}
