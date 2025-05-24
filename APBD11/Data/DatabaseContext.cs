using APBD11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");
            entity.HasKey(e => e.IdPatient);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Birthdate).IsRequired();
            entity.HasMany(e => e.Prescriptions).WithOne(e => e.Patient).HasForeignKey(e => e.IdPatient);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");
            entity.HasKey(e => e.IdDoctor);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasMany(e => e.Prescriptions).WithOne(e => e.Doctor).HasForeignKey(e => e.IdDoctor);
        });

        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.ToTable("Medicament");
            entity.HasKey(e => e.IdMedicament);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            entity.HasMany(e => e.PrescriptionMedicaments).WithOne(e => e.Medicament).HasForeignKey(e => e.IdMedicament);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");
            entity.HasKey(e => e.IdPrescription);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.DueDate).IsRequired();
            entity.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
            entity.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);
            entity.HasMany(e => e.PrescriptionMedicaments).WithOne(e => e.Prescription).HasForeignKey(e => e.IdPrescription);
        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.ToTable("PrescriptionMedicament");
            entity.HasKey(e => new { e.IdPrescription, e.IdMedicament });
            entity.Property(e => e.Dose);
            entity.Property(e => e.Details).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Medicament).WithMany(e => e.PrescriptionMedicaments).HasForeignKey(e => e.IdMedicament);
            entity.HasOne(e => e.Prescription).WithMany(e => e.PrescriptionMedicaments).HasForeignKey(e => e.IdPrescription);
        });

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "Emily", LastName = "Johnson", Email = "emily.johnson@example.com" }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Alice", LastName = "Brown", Birthdate = new DateOnly(1990, 5, 15) },
            new Patient { IdPatient = 2, FirstName = "Bob", LastName = "Davis", Birthdate = new DateOnly(1985, 9, 23) }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Paracetamol", Description = "Pain reliever", Type = "Tablet" },
            new Medicament { IdMedicament = 2, Name = "Amoxicillin", Description = "Antibiotic", Type = "Capsule" }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { IdPrescription = 1, Date = new DateTime(2025, 5, 1), DueDate = new DateTime(2025, 5, 10), IdPatient = 1, IdDoctor = 1 },
            new Prescription { IdPrescription = 2, Date = new DateTime(2025, 5, 2), DueDate = new DateTime(2025, 5, 12), IdPatient = 2, IdDoctor = 2 }
        );

        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 500, Details = "Take twice daily after meals" },
            new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 2, Dose = 250, Details = "Take once daily before bedtime" }
        );
    }
}