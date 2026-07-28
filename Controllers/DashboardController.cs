using KillingMachine.Data;
using KillingMachine.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public class DashboardController : Controller
{
    private readonly AppDbContext _db;
    public DashboardController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(int? clientId)
    {
        var clients = await _db.Clients.AsNoTracking().OrderBy(x => x.FullName).ToListAsync();
        var selected = clientId.HasValue
            ? clients.FirstOrDefault(x => x.Id == clientId.Value)
            : clients.FirstOrDefault();

        var vm = new DashboardViewModel
        {
            ClientCount = await _db.Clients.CountAsync(),
            ActiveMembershipCount = await _db.ClientMemberships.CountAsync(x => x.Status == "Activa" && x.EndDate >= DateTime.Today),
            TrainerCount = await _db.Trainers.CountAsync(x => x.IsActive),
            WorkoutCount = await _db.WorkoutLogs.CountAsync(),
            PendingTrialCount = await _db.TrialRequests.CountAsync(x => x.Status == "Pendiente"),
            SelectedClientId = selected?.Id,
            SelectedClientName = selected?.FullName,
            Clients = clients.Select(x => new SelectListItem(x.FullName, x.Id.ToString(), x.Id == selected?.Id)).ToList()
        };

        if (selected is not null)
        {
            var measurements = await _db.BodyMeasurements.AsNoTracking()
                .Where(x => x.ClientId == selected.Id)
                .OrderBy(x => x.MeasurementDate)
                .Take(12)
                .ToListAsync();

            vm.WeightLabels = measurements.Select(x => x.MeasurementDate.ToString("dd/MM")).ToList();
            vm.WeightValues = measurements.Select(x => x.WeightKg).ToList();
            var latest = measurements.LastOrDefault();
            vm.LatestBmi = latest?.CalculateBmi(selected.HeightCm) ?? 0;

            var start = DateTime.Today.AddDays(-6);
            var logs = await _db.WorkoutLogs.AsNoTracking()
                .Where(x => x.ClientId == selected.Id && x.WorkoutDate.Date >= start)
                .ToListAsync();

            for (var day = start; day <= DateTime.Today; day = day.AddDays(1))
            {
                vm.WorkoutLabels.Add(day.ToString("ddd"));
                vm.WorkoutMinutes.Add(logs.Where(x => x.WorkoutDate.Date == day.Date).Sum(x => x.DurationMinutes));
            }
            vm.CompletedThisWeek = logs.Count;
        }

        return View(vm);
    }
}
