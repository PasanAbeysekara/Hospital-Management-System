
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model.InsidePrescription.insideTest
{
	public class MedicalTest
	{
		[Key]
		public int Id { get; set; }
		public string? Description { get; set; }

		public bool IsMedicalTestSelected { get; set; }

		// Foreign key property for the related Patient object
		public int TestId { get; set; }

		// Navigation property for the related Bill object
		public virtual Test? Test { get; set; }

		// Foreign key property for the related Prescription object
		public int PrescriptionId { get; set; }

		// Navigation property for the related Prescription object
		public virtual Prescription? Prescription { get; set; }

		public MedicalTest()
		{
			IsMedicalTestSelected = false;
		}

	}
}
