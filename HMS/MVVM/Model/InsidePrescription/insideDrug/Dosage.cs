using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model.InsidePrescription.insideDrug
{
	public class Dosage
	{
		[Key]
		public int Id { get; set; }
		public string? DrugType { get; set; }
		public double Dose { get; set; }
		public double Duration { get; set; }
		public string? Comments { get; set; }
		public bool IsDosageSelected { get; set; }

		// Foreign key property for the related Doctor object
		public int DrugId { get; set; }

		// Navigation property for the related Doctor object
		public virtual Drug? Drug { get; set; }

		// Foreign key property for the related Prescription object
		public int PrescriptionId { get; set; }

		// Navigation property for the related Prescription object
		public virtual Prescription? Prescription { get; set; }

		public Dosage()
		{
			IsDosageSelected = false;
		}

	}
}
