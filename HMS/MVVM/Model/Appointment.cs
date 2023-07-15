using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model
{
	public class Appointment
	{
		[Key]
		public int Id { get; set; }
		public DateTime AppointedDate { get; set; }
		public bool? IsAppointmentSelected { get; set; }

		// Foreign key property for the related Doctor object
		public int DoctorId { get; set; }

		// Navigation property for the related Doctor object
		public virtual Doctor? Doctor { get; set; }

		// Foreign key property for the related Patient object
		public int PatientId { get; set; }

		// Navigation property for the related Patient object
		public virtual Patient? Patient { get; set; }

		public Appointment()
		{
			IsAppointmentSelected = false;
		}
	}
}
