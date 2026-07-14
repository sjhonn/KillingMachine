using KillingMachine.Data;
using KillingMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public class WeeklySchedulesController : Controller
{
    private readonly AppDbContext _db;
    public WeeklySchedulesController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.WeeklySchedules.AsNoTracking().Include(x => x.Client).Include(x => x.Trainer).OrderBy(x => x.Client!.FullName).ThenBy(x => x.DayOfWeek).ToListAsync());
    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : (await _db.WeeklySchedules.AsNoTracking().Include(x => x.Client).Include(x => x.Trainer).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());
    public async Task<IActionResult> Create() { await LoadLists(); return View(new WeeklySchedule()); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WeeklySchedule item) { ValidateTimes(item); if (!ModelState.IsValid) { await LoadLists(item.ClientId, item.TrainerId); return View(item); } _db.Add(item); await _db.SaveChangesAsync(); TempData["Success"] = "Horario registrado."; return RedirectToAction(nameof(Index)); }

    public async Task<IActionResult> Edit(int? id) { if (id is null) return NotFound(); var item = await _db.WeeklySchedules.FindAsync(id); if (item is null) return NotFound(); await LoadLists(item.ClientId, item.TrainerId); return View(item); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WeeklySchedule item) { if (id != item.Id) return NotFound(); ValidateTimes(item); if (!ModelState.IsValid) { await LoadLists(item.ClientId, item.TrainerId); return View(item); } _db.Update(item); await _db.SaveChangesAsync(); TempData["Success"] = "Horario actualizado."; return RedirectToAction(nameof(Index)); }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : (await _db.WeeklySchedules.AsNoTracking().Include(x => x.Client).Include(x => x.Trainer).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) { var item = await _db.WeeklySchedules.FindAsync(id); if (item is not null) { _db.Remove(item); await _db.SaveChangesAsync(); } TempData["Success"] = "Horario eliminado."; return RedirectToAction(nameof(Index)); }

    private void ValidateTimes(WeeklySchedule item) { if (TimeOnly.TryParse(item.StartTime, out var start) && TimeOnly.TryParse(item.EndTime, out var end) && end <= start) ModelState.AddModelError(nameof(item.EndTime), "La hora fin debe ser posterior a la hora inicio."); }
    private async Task LoadLists(int? clientId = null, int? trainerId = null) { ViewData["ClientId"] = new SelectList(await _db.Clients.OrderBy(x => x.FullName).ToListAsync(), "Id", "FullName", clientId); ViewData["TrainerId"] = new SelectList(await _db.Trainers.OrderBy(x => x.FullName).ToListAsync(), "Id", "FullName", trainerId); }
}
