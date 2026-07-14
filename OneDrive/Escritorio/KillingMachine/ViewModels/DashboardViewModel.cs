using Microsoft.AspNetCore.Mvc.Rendering;

namespace KillingMachine.ViewModels;

public class DashboardViewModel
{
    public int ClientCount { get; set; }
    public int ActiveMembershipCount { get; set; }
    public int TrainerCount { get; set; }
    public int WorkoutCount { get; set; }
    public int PendingTrialCount { get; set; }
    public int? SelectedClientId { get; set; }
    public string? SelectedClientName { get; set; }
    public List<SelectListItem> Clients { get; set; } = new();
    public List<string> WeightLabels { get; set; } = new();
    public List<double> WeightValues { get; set; } = new();
    public List<string> WorkoutLabels { get; set; } = new();
    public List<int> WorkoutMinutes { get; set; } = new();
    public double LatestBmi { get; set; }
    public int CompletedThisWeek { get; set; }
}
