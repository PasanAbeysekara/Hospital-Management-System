using HMS.MVVM.Model.Authentication;
using HMS.MVVM.Model.InsidePrescription.insideDrug;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows;
using System.IO;

namespace HMS
{
	public class DataContext : DbContext
	{
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Drug> Drugs { get; set; }
		public DbSet<Dosage> Dosages { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<MedicalTest> MedicalTests { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
           
            string databasePath = "testHospital.db"; // relative path to the database file
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string parentPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(basePath).FullName).FullName).FullName).FullName;


            string connectionString = $"Data Source={Path.Combine(parentPath, databasePath)}";
            
            optionsBuilder.UseSqlite(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Doctor)
				.WithMany(d => d.Appointments)
				.HasForeignKey(a => a.DoctorId);

			modelBuilder.Entity<Dosage>()
				.HasOne(a => a.Drug)
				.WithMany(d => d.Dosages)
				.HasForeignKey(a => a.DrugId);

			modelBuilder.Entity<MedicalTest>()
				.HasOne(b => b.Test)
				.WithMany(p => p.MedicalTests)
				.HasForeignKey(a => a.TestId);

			modelBuilder.Entity<Dosage>()
				.HasOne(a => a.Prescription)
				.WithMany(d => d.Dosages)
				.HasForeignKey(a => a.PrescriptionId);

			modelBuilder.Entity<MedicalTest>()
				.HasOne(a => a.Prescription)
				.WithMany(d => d.MedicalTests)
				.HasForeignKey(a => a.PrescriptionId);

			modelBuilder.Entity<Prescription>()
				.HasOne(a => a.Patient)
				.WithMany(d => d.Prescriptions)
				.HasForeignKey(a => a.PatientId);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Patient)
				.WithMany(d => d.Appointments)
				.HasForeignKey(a => a.PatientId);

			modelBuilder.Entity<Bill>()
				.HasOne(b => b.Patient)
				.WithOne(p => p.Bill)
				.HasForeignKey<Bill>(b => b.PatientId);

		}
	}
}
