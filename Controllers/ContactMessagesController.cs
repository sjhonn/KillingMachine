using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class ContactMessagesController : CrudController<ContactMessage>
{
    public ContactMessagesController(AppDbContext db) : base(db) { }
}
