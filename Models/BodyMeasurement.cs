using System.ComponentModel.DataAnnotations;

namespace KillingMachine.Models;

public class BodyMeasurement
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Cliente")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    public DateTime MeasurementDate { get; set; } = DateTime.Today;

    [Range(20, 500)]
    [Display(Name = "Peso (kg)")]
    public double WeightKg { get; set; }

    [Range(20, 300)]
    [Display(Name = "Cintura (cm)")]
    public double WaistCm { get; set; }

    [Range(20, 300)]
    [Display(Name = "Pecho (cm)")]
    public double ChestCm { get; set; }

    [Range(10, 150)]
    [Display(Name = "Brazo (cm)")]
    public double ArmCm { get; set; }

    [Range(10, 200)]
    [Display(Name = "Pierna (cm)")]
    public double LegCm { get; set; }

    [Range(0, 80)]
    [Display(Name = "Grasa corporal (%)")]
    public double BodyFatPercentage { get; set; }

    public double CalculateBmi(double heightCm)
    {
        if (heightCm <= 0) return 0;
        var heightM = heightCm / 100d;
        return Math.Round(WeightKg / (heightM * heightM), 2);
    }
}
