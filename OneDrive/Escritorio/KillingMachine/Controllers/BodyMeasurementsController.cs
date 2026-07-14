using KillingMachine.Data;
using KillingMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public class BodyMeasurementsController : Controller
{
    private readonly AppDbContext _db;
    public BodyMeasurementsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.BodyMeasurements.AsNoTracking().Include(x => x.Client).OrderByDescending(x => x.MeasurementDate).ToListAsync());
    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : (await _db.BodyMeasurements.AsNoTracking().Include(x => x.Client).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());
    public async Task<IActionResult> Create() { await LoadClients(); return View(new BodyMeasurement()); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BodyMeasurement item) { if (!ModelState.IsValid) { await LoadClients(item.ClientId); return View(item); } _db.Add(item); await _db.SaveChangesAsync(); TempData["Success"] = "Medicion registrada."; return RedirectToAction(nameof(Index)); }

    public async Task<IActionResult> Edit(int? id) { if (id is null) return NotFound(); var item = await _db.BodyMeasurements.FindAsync(id); if (item is null) return NotFound(); await LoadClients(item.ClientId); return View(item); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BodyMeasurement item) { if (id != item.Id) return NotFound(); if (!ModelState.IsValid) { await LoadClients(item.ClientId); return View(item); } _db.Update(item); await _db.SaveChangesAsync(); TempData["Success"] = "Medicion actualizada."; return RedirectToAction(nameof(Index)); }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : (await _db.BodyMeasurements.AsNoTracking().Include(x => x.Client).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) { var item = await _db.BodyMeasurements.FindAsync(id); if (item is not null) { _db.Remove(item); await _db.SaveChangesAsync(); } TempData["Success"] = "Medicion eliminada."; return RedirectToAction(nameof(Index)); }

    private async Task LoadClients(int? selected = null) => ViewData["ClientId"] = new SelectList(await _db.Clients.OrderBy(x => x.FullName).ToListAsync(), "Id", "FullName", selected);
}
