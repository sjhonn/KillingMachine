using KillingMachine.Models;

namespace KillingMachine.ViewModels;

public class HomeViewModel
{
    public IReadOnlyList<Service> Services { get; set; } = Array.Empty<Service>();
    public IReadOnlyList<MembershipPlan> Plans { get; set; } = Array.Empty<MembershipPlan>();
    public IReadOnlyList<Trainer> Trainers { get; set; } = Array.Empty<Trainer>();
    public IReadOnlyList<GalleryItem> Gallery { get; set; } = Array.Empty<GalleryItem>();
    public ContactMessage Contact { get; set; } = new();
    public TrialRequest Trial { get; set; } = new();
}
