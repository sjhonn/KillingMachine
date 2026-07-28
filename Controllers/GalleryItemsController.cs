using KillingMachine.Data;
using KillingMachine.Models;

namespace KillingMachine.Controllers;

public class GalleryItemsController : CrudController<GalleryItem>
{
    public GalleryItemsController(AppDbContext db) : base(db) { }
}
