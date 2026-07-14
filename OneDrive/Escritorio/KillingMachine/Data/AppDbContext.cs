using KillingMachine.Models;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<WorkoutLog> WorkoutLogs => Set<WorkoutLog>();
    public DbSet<BodyMeasurement> BodyMeasurements => Set<BodyMeasurement>();
    public DbSet<WeeklySchedule> WeeklySchedules => Set<WeeklySchedule>();
    public DbSet<ClientMembership> ClientMemberships => Set<ClientMembership>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<TrialRequest> TrialRequests => Set<TrialRequest>();
    public DbSet<GalleryItem> GalleryItems => Set<GalleryItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().HasIndex(x => x.DocumentNumber).IsUnique();
        modelBuilder.Entity<Client>().HasIndex(x => x.Email).IsUnique();

        modelBuilder.Entity<WorkoutLog>()
            .HasOne(x => x.Client)
            .WithMany(x => x.WorkoutLogs)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkoutLog>()
            .HasOne(x => x.Exercise)
            .WithMany(x => x.WorkoutLogs)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BodyMeasurement>()
            .HasOne(x => x.Client)
            .WithMany(x => x.BodyMeasurements)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeeklySchedule>()
            .HasOne(x => x.Client)
            .WithMany(x => x.WeeklySchedules)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeeklySchedule>()
            .HasOne(x => x.Trainer)
            .WithMany(x => x.WeeklySchedules)
            .HasForeignKey(x => x.TrainerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ClientMembership>()
            .HasOne(x => x.Client)
            .WithMany(x => x.Memberships)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientMembership>()
            .HasOne(x => x.MembershipPlan)
            .WithMany(x => x.ClientMemberships)
            .HasForeignKey(x => x.MembershipPlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
