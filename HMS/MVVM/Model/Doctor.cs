using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model
{
	public class Doctor
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
		public double Fee { get; set; }

		public bool? IsDoctorSelected { get; set; }

		public Doctor()
		{
			IsDoctorSelected = false;
		}

		// Navigation property for the related Appointments objects
		public virtual ICollection<Appointment>? Appointments { get; set; }
	}
}
