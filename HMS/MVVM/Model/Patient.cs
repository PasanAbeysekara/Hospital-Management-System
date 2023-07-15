using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model
{
	public class Patient
	{
		[Key]
		public int Id { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? BirthDay { get; set; }
		public string? Phone { get; set; }
		public char Gender { get; set; }
		public string? BloodGroup { get; set; }
		public string? Address { get; set; }
		public double Weight { get; set; }
		public double Height { get; set; }
		public DateTime AdmittedDate { get; set; }

		// Navigation property for the related Prescriptions objects
		public virtual ICollection<Prescription>? Prescriptions { get; set; }

		// Navigation property for the related Appointments objects
		public virtual ICollection<Appointment>? Appointments { get; set; }

		// Navigation property for the related Bill object
		public virtual Bill? Bill { get; set; }
		public bool? IsPatientSelected { get; set; }

		public Patient()
		{
			IsPatientSelected = false;
		}
	}
}
