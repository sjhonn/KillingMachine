using KillingMachine.Models;

namespace KillingMachine.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext db)
    {
        if (db.Services.Any()) return;

        var services = new[]
        {
            new Service { Name = "Sala de musculacion", Description = "Equipamiento para fuerza, hipertrofia y acondicionamiento general.", Price = 20, DurationMinutes = 90 },
            new Service { Name = "Entrenamiento funcional", Description = "Sesiones grupales de movilidad, potencia y resistencia.", Price = 25, DurationMinutes = 60 },
            new Service { Name = "Evaluacion fisica", Description = "Control inicial de peso, medidas, objetivos y condicion fisica.", Price = 30, DurationMinutes = 45 }
        };
        db.Services.AddRange(services);

        var plans = new[]
        {
            new MembershipPlan { Name = "Basico", Description = "Acceso libre a sala de musculacion.", MonthlyPrice = 79.90m, DurationMonths = 1, Benefits = "Acceso de lunes a sabado; evaluacion inicial" },
            new MembershipPlan { Name = "Machine", Description = "Plan completo con clases grupales.", MonthlyPrice = 119.90m, DurationMonths = 1, Benefits = "Sala de musculacion; clases funcionales; seguimiento mensual" },
            new MembershipPlan { Name = "Elite", Description = "Plan intensivo con seguimiento personalizado.", MonthlyPrice = 179.90m, DurationMonths = 1, Benefits = "Acceso total; entrenador asignado; control quincenal" }
        };
        db.MembershipPlans.AddRange(plans);

        var trainers = new[]
        {
            new Trainer { FullName = "Carlos Mendoza", Specialty = "Fuerza e hipertrofia", Email = "carlos@killingmachine.pe", Phone = "999111222", Biography = "Entrenador certificado con experiencia en progresion de fuerza.", Schedule = "Lunes a viernes 06:00-14:00" },
            new Trainer { FullName = "Andrea Torres", Specialty = "Entrenamiento funcional", Email = "andrea@killingmachine.pe", Phone = "999333444", Biography = "Especialista en movilidad, acondicionamiento y entrenamiento grupal.", Schedule = "Lunes a sabado 14:00-22:00" }
        };
        db.Trainers.AddRange(trainers);

        var exercises = new[]
        {
            new Exercise { Name = "Sentadilla", MuscleGroup = "Piernas", Description = "Movimiento compuesto para tren inferior.", Equipment = "Barra", Difficulty = "Intermedio", CaloriesPerHour = 450 },
            new Exercise { Name = "Press de banca", MuscleGroup = "Pecho", Description = "Empuje horizontal con barra o mancuernas.", Equipment = "Banco y barra", Difficulty = "Intermedio", CaloriesPerHour = 380 },
            new Exercise { Name = "Remo con barra", MuscleGroup = "Espalda", Description = "Traccion horizontal para espalda y brazos.", Equipment = "Barra", Difficulty = "Intermedio", CaloriesPerHour = 400 }
        };
        db.Exercises.AddRange(exercises);

        db.WorkoutPlans.AddRange(
            new WorkoutPlan { Name = "Inicio total", Objective = "Acondicionamiento general", Level = "Principiante", DurationWeeks = 8, SessionsPerWeek = 3, Description = "Rutina de cuerpo completo con progresion semanal." },
            new WorkoutPlan { Name = "Fuerza Machine", Objective = "Incrementar fuerza", Level = "Intermedio", DurationWeeks = 12, SessionsPerWeek = 4, Description = "Plan basado en movimientos compuestos y cargas progresivas." }
        );

        var client = new Client
        {
            FullName = "María López",
            DocumentNumber = "00000001",
            Email = "maria@example.com",
            Phone = "999555666",
            BirthDate = new DateTime(1995, 5, 15),
            HeightCm = 175,
            JoinDate = DateTime.Today.AddMonths(-2),
            Notes = "Cliente activa con seguimiento de entrenamiento y medidas."
        };
        db.Clients.Add(client);
        db.SaveChanges();

        db.ClientMemberships.Add(new ClientMembership
        {
            ClientId = client.Id,
            MembershipPlanId = plans[1].Id,
            StartDate = DateTime.Today.AddDays(-10),
            EndDate = DateTime.Today.AddMonths(1),
            Status = "Activa",
            AmountPaid = plans[1].MonthlyPrice
        });

        db.BodyMeasurements.AddRange(
            new BodyMeasurement { ClientId = client.Id, MeasurementDate = DateTime.Today.AddDays(-42), WeightKg = 84, WaistCm = 94, ChestCm = 103, ArmCm = 35, LegCm = 58, BodyFatPercentage = 24 },
            new BodyMeasurement { ClientId = client.Id, MeasurementDate = DateTime.Today.AddDays(-28), WeightKg = 82.5, WaistCm = 92, ChestCm = 103, ArmCm = 35.5, LegCm = 58.5, BodyFatPercentage = 22.8 },
            new BodyMeasurement { ClientId = client.Id, MeasurementDate = DateTime.Today.AddDays(-14), WeightKg = 81.2, WaistCm = 90.5, ChestCm = 104, ArmCm = 36, LegCm = 59, BodyFatPercentage = 21.9 },
            new BodyMeasurement { ClientId = client.Id, MeasurementDate = DateTime.Today, WeightKg = 80.4, WaistCm = 89, ChestCm = 104, ArmCm = 36.2, LegCm = 59.2, BodyFatPercentage = 21.2 }
        );

        for (var i = 0; i < 8; i++)
        {
            db.WorkoutLogs.Add(new WorkoutLog
            {
                ClientId = client.Id,
                ExerciseId = exercises[i % exercises.Length].Id,
                WorkoutDate = DateTime.Today.AddDays(-i * 2),
                Sets = 4,
                Repetitions = 10,
                WeightKg = 40 + i,
                DurationMinutes = 50 + (i % 3) * 10,
                Notes = "Sesión completada"
            });
        }

        db.WeeklySchedules.AddRange(
            new WeeklySchedule { ClientId = client.Id, DayOfWeek = "Lunes", StartTime = "18:00", EndTime = "19:00", Activity = "Fuerza tren inferior", TrainerId = trainers[0].Id },
            new WeeklySchedule { ClientId = client.Id, DayOfWeek = "Miercoles", StartTime = "18:00", EndTime = "19:00", Activity = "Fuerza tren superior", TrainerId = trainers[0].Id },
            new WeeklySchedule { ClientId = client.Id, DayOfWeek = "Viernes", StartTime = "19:00", EndTime = "20:00", Activity = "Funcional", TrainerId = trainers[1].Id }
        );

        db.GalleryItems.AddRange(
            new GalleryItem { Title = "Comunidad Killing Machine", ImageUrl = "/images/killing-machine-logo.png", Description = "Disciplina, constancia y progreso.", DisplayOrder = 1 },
            new GalleryItem { Title = "Entrenamiento de fuerza", ImageUrl = "/images/killing-machine-logo.png", Description = "Rutinas para todos los niveles.", DisplayOrder = 2 },
            new GalleryItem { Title = "Seguimiento real", ImageUrl = "/images/killing-machine-logo.png", Description = "Control de peso, medidas y entrenamientos.", DisplayOrder = 3 }
        );

        db.SaveChanges();
    }
}
