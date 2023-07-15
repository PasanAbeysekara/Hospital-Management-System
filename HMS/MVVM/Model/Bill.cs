using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model
{
	public class Bill
	{
		[Key]
		public int Id { get; set; }
		public double BillAmount { get; set; }
		public string? PaymentMode { get; set; }
		public bool Status { get; set; }
		public DateTime PaymentDate { get; set; }
		public bool? IsBillSelected { get; set; }

		// Foreign key property for the related Patient object
		public int PatientId { get; set; }

		// Navigation property for the related Patient object
		public virtual Patient? Patient { get; set; }

		public Bill()
		{
			IsBillSelected = false;
		}
	}
}
