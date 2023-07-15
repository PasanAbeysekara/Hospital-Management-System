using HMS.MVVM.Model.InsidePrescription.insideDrug;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model
{
	public class Prescription
	{
		[Key]
		public int Id { get; set; }
		public DateTime PrescribedDate { get; set; }

		public bool? IsPrescriptionSelected { get; set; }

		// Navigation property for the related Dosages objects
		public virtual ICollection<Dosage>? Dosages { get; set; }

		// Navigation property for the related MedicalTests objects
		public virtual ICollection<MedicalTest>? MedicalTests { get; set; }

		// Foreign key property for the related Patient object
		public int PatientId { get; set; }

		// Navigation property for the related Patient object
		public virtual Patient? Patient { get; set; }

		public Prescription()
		{
			IsPrescriptionSelected = false;
		}
	}
}
