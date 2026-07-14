using KillingMachine.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KillingMachine.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260714000000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
CREATE TABLE "Clients" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Clients" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "DocumentNumber" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "BirthDate" TEXT NOT NULL,
    "HeightCm" REAL NOT NULL,
    "JoinDate" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL,
    "Notes" TEXT NULL
);
""");
        migrationBuilder.Sql("""
CREATE UNIQUE INDEX "IX_Clients_DocumentNumber" ON "Clients" ("DocumentNumber");
""");
        migrationBuilder.Sql("""
CREATE UNIQUE INDEX "IX_Clients_Email" ON "Clients" ("Email");
""");
        migrationBuilder.Sql("""
CREATE TABLE "Trainers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Trainers" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "Specialty" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "Biography" TEXT NOT NULL,
    "Schedule" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "Services" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Services" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "Price" TEXT NOT NULL,
    "DurationMinutes" INTEGER NOT NULL,
    "IsActive" INTEGER NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "MembershipPlans" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_MembershipPlans" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "MonthlyPrice" TEXT NOT NULL,
    "DurationMonths" INTEGER NOT NULL,
    "Benefits" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "Exercises" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Exercises" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "MuscleGroup" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "Equipment" TEXT NULL,
    "Difficulty" TEXT NOT NULL,
    "CaloriesPerHour" INTEGER NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "WorkoutPlans" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_WorkoutPlans" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Objective" TEXT NOT NULL,
    "Level" TEXT NOT NULL,
    "DurationWeeks" INTEGER NOT NULL,
    "SessionsPerWeek" INTEGER NOT NULL,
    "Description" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "ContactMessages" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ContactMessages" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Phone" TEXT NULL,
    "Subject" TEXT NOT NULL,
    "Message" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "Status" TEXT NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "TrialRequests" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_TrialRequests" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "PreferredDate" TEXT NOT NULL,
    "PreferredTime" TEXT NOT NULL,
    "Objective" TEXT NOT NULL,
    "Status" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "GalleryItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_GalleryItems" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "ImageUrl" TEXT NOT NULL,
    "Description" TEXT NULL,
    "DisplayOrder" INTEGER NOT NULL,
    "IsActive" INTEGER NOT NULL
);
""");
        migrationBuilder.Sql("""
CREATE TABLE "WorkoutLogs" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_WorkoutLogs" PRIMARY KEY AUTOINCREMENT,
    "ClientId" INTEGER NOT NULL,
    "ExerciseId" INTEGER NOT NULL,
    "WorkoutDate" TEXT NOT NULL,
    "Sets" INTEGER NOT NULL,
    "Repetitions" INTEGER NOT NULL,
    "WeightKg" REAL NOT NULL,
    "DurationMinutes" INTEGER NOT NULL,
    "Notes" TEXT NULL,
    CONSTRAINT "FK_WorkoutLogs_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_WorkoutLogs_Exercises_ExerciseId" FOREIGN KEY ("ExerciseId") REFERENCES "Exercises" ("Id") ON DELETE RESTRICT
);
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_WorkoutLogs_ClientId" ON "WorkoutLogs" ("ClientId");
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_WorkoutLogs_ExerciseId" ON "WorkoutLogs" ("ExerciseId");
""");
        migrationBuilder.Sql("""
CREATE TABLE "BodyMeasurements" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BodyMeasurements" PRIMARY KEY AUTOINCREMENT,
    "ClientId" INTEGER NOT NULL,
    "MeasurementDate" TEXT NOT NULL,
    "WeightKg" REAL NOT NULL,
    "WaistCm" REAL NOT NULL,
    "ChestCm" REAL NOT NULL,
    "ArmCm" REAL NOT NULL,
    "LegCm" REAL NOT NULL,
    "BodyFatPercentage" REAL NOT NULL,
    CONSTRAINT "FK_BodyMeasurements_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_BodyMeasurements_ClientId" ON "BodyMeasurements" ("ClientId");
""");
        migrationBuilder.Sql("""
CREATE TABLE "WeeklySchedules" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_WeeklySchedules" PRIMARY KEY AUTOINCREMENT,
    "ClientId" INTEGER NOT NULL,
    "DayOfWeek" TEXT NOT NULL,
    "StartTime" TEXT NOT NULL,
    "EndTime" TEXT NOT NULL,
    "Activity" TEXT NOT NULL,
    "TrainerId" INTEGER NULL,
    "Notes" TEXT NULL,
    CONSTRAINT "FK_WeeklySchedules_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_WeeklySchedules_Trainers_TrainerId" FOREIGN KEY ("TrainerId") REFERENCES "Trainers" ("Id") ON DELETE SET NULL
);
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_WeeklySchedules_ClientId" ON "WeeklySchedules" ("ClientId");
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_WeeklySchedules_TrainerId" ON "WeeklySchedules" ("TrainerId");
""");
        migrationBuilder.Sql("""
CREATE TABLE "ClientMemberships" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ClientMemberships" PRIMARY KEY AUTOINCREMENT,
    "ClientId" INTEGER NOT NULL,
    "MembershipPlanId" INTEGER NOT NULL,
    "StartDate" TEXT NOT NULL,
    "EndDate" TEXT NOT NULL,
    "Status" TEXT NOT NULL,
    "AmountPaid" TEXT NOT NULL,
    CONSTRAINT "FK_ClientMemberships_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ClientMemberships_MembershipPlans_MembershipPlanId" FOREIGN KEY ("MembershipPlanId") REFERENCES "MembershipPlans" ("Id") ON DELETE RESTRICT
);
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_ClientMemberships_ClientId" ON "ClientMemberships" ("ClientId");
""");
        migrationBuilder.Sql("""
CREATE INDEX "IX_ClientMemberships_MembershipPlanId" ON "ClientMemberships" ("MembershipPlanId");
""");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "ClientMemberships";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "WeeklySchedules";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "BodyMeasurements";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "WorkoutLogs";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "GalleryItems";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "TrialRequests";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "ContactMessages";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "WorkoutPlans";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "Exercises";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "MembershipPlans";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "Services";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "Trainers";
""");
        migrationBuilder.Sql("""
DROP TABLE IF EXISTS "Clients";
""");
    }
}
