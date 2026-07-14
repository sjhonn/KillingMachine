using KillingMachine.Data;
using KillingMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public class WorkoutLogsController : Controller
{
    private readonly AppDbContext _db;
    public WorkoutLogsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.WorkoutLogs.AsNoTracking().Include(x => x.Client).Include(x => x.Exercise).OrderByDescending(x => x.WorkoutDate).ToListAsync());
    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : (await _db.WorkoutLogs.AsNoTracking().Include(x => x.Client).Include(x => x.Exercise).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());

    public async Task<IActionResult> Create() { await LoadLists(); return View(new WorkoutLog()); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WorkoutLog item)
    {
        if (!ModelState.IsValid) { await LoadLists(item.ClientId, item.ExerciseId); return View(item); }
        _db.Add(item); await _db.SaveChangesAsync(); TempData["Success"] = "Entrenamiento registrado."; return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound(); var item = await _db.WorkoutLogs.FindAsync(id); if (item is null) return NotFound(); await LoadLists(item.ClientId, item.ExerciseId); return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WorkoutLog item)
    {
        if (id != item.Id) return NotFound(); if (!ModelState.IsValid) { await LoadLists(item.ClientId, item.ExerciseId); return View(item); }
        _db.Update(item); await _db.SaveChangesAsync(); TempData["Success"] = "Entrenamiento actualizado."; return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : (await _db.WorkoutLogs.AsNoTracking().Include(x => x.Client).Include(x => x.Exercise).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) { var item = await _db.WorkoutLogs.FindAsync(id); if (item is not null) { _db.Remove(item); await _db.SaveChangesAsync(); } TempData["Success"] = "Entrenamiento eliminado."; return RedirectToAction(nameof(Index)); }

    private async Task LoadLists(int? clientId = null, int? exerciseId = null)
    {
        ViewData["ClientId"] = new SelectList(await _db.Clients.OrderBy(x => x.FullName).ToListAsync(), "Id", "FullName", clientId);
        ViewData["ExerciseId"] = new SelectList(await _db.Exercises.OrderBy(x => x.Name).ToListAsync(), "Id", "Name", exerciseId);
    }
}
