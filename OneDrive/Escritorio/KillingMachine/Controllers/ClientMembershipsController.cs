using KillingMachine.Data;
using KillingMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public class ClientMembershipsController : Controller
{
    private readonly AppDbContext _db;
    public ClientMembershipsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.ClientMemberships.AsNoTracking().Include(x => x.Client).Include(x => x.MembershipPlan).OrderByDescending(x => x.StartDate).ToListAsync());
    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : (await _db.ClientMemberships.AsNoTracking().Include(x => x.Client).Include(x => x.MembershipPlan).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());
    public async Task<IActionResult> Create() { await LoadLists(); return View(new ClientMembership()); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientMembership item) { ValidateDates(item); if (!ModelState.IsValid) { await LoadLists(item.ClientId, item.MembershipPlanId); return View(item); } _db.Add(item); await _db.SaveChangesAsync(); TempData["Success"] = "Membresia registrada."; return RedirectToAction(nameof(Index)); }

    public async Task<IActionResult> Edit(int? id) { if (id is null) return NotFound(); var item = await _db.ClientMemberships.FindAsync(id); if (item is null) return NotFound(); await LoadLists(item.ClientId, item.MembershipPlanId); return View(item); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClientMembership item) { if (id != item.Id) return NotFound(); ValidateDates(item); if (!ModelState.IsValid) { await LoadLists(item.ClientId, item.MembershipPlanId); return View(item); } _db.Update(item); await _db.SaveChangesAsync(); TempData["Success"] = "Membresia actualizada."; return RedirectToAction(nameof(Index)); }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : (await _db.ClientMemberships.AsNoTracking().Include(x => x.Client).Include(x => x.MembershipPlan).FirstOrDefaultAsync(x => x.Id == id) is { } item ? View(item) : NotFound());

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) { var item = await _db.ClientMemberships.FindAsync(id); if (item is not null) { _db.Remove(item); await _db.SaveChangesAsync(); } TempData["Success"] = "Membresia eliminada."; return RedirectToAction(nameof(Index)); }

    private void ValidateDates(ClientMembership item) { if (item.EndDate.Date <= item.StartDate.Date) ModelState.AddModelError(nameof(item.EndDate), "La fecha fin debe ser posterior a la fecha inicio."); }
    private async Task LoadLists(int? clientId = null, int? planId = null) { ViewData["ClientId"] = new SelectList(await _db.Clients.OrderBy(x => x.FullName).ToListAsync(), "Id", "FullName", clientId); ViewData["MembershipPlanId"] = new SelectList(await _db.MembershipPlans.OrderBy(x => x.Name).ToListAsync(), "Id", "Name", planId); }
}
