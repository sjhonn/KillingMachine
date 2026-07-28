using System.Diagnostics;
using KillingMachine.Data;
using KillingMachine.Models;
using KillingMachine.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        return View(new HomeViewModel
        {
            Services = await _db.Services.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Name).ToListAsync(),
            Plans = await _db.MembershipPlans.AsNoTracking().Where(x => x.IsActive).OrderBy(x => (double)x.MonthlyPrice).ToListAsync(),
            Trainers = await _db.Trainers.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.FullName).ToListAsync(),
            Gallery = await _db.GalleryItems.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).ToListAsync()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact([Bind(Prefix = "Contact")] ContactMessage contact)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Revise los datos del formulario de contacto.";
            return RedirectToAction(nameof(Index), new { section = "contacto" });
        }

        contact.CreatedAt = DateTime.Now;
        contact.Status = "Pendiente";
        _db.ContactMessages.Add(contact);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Mensaje registrado. El gimnasio se pondra en contacto.";
        return Redirect("/#contacto");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Trial([Bind(Prefix = "Trial")] TrialRequest trial)
    {
        if (trial.PreferredDate.Date < DateTime.Today)
            ModelState.AddModelError("Trial.PreferredDate", "La fecha no puede estar en el pasado.");

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Revise los datos de la solicitud de clase de prueba.";
            return Redirect("/#clase-prueba");
        }

        trial.CreatedAt = DateTime.Now;
        trial.Status = "Pendiente";
        _db.TrialRequests.Add(trial);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Solicitud registrada correctamente.";
        return Redirect("/#clase-prueba");
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
