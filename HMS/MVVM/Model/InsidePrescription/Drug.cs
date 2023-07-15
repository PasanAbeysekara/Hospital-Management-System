using HMS.MVVM.Model.InsidePrescription.insideDrug;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model.InsidePrescription
{
	public class Drug // Drug constructor
	{
		[Key]
		public int Id { get; set; }
		public string? TradeName { get; set; }
		public string? GenericName { get; set; }
		public bool IsDrugSelected { get; set; }

		// Navigation property for the related Dosages objects
		public virtual ICollection<Dosage>? Dosages { get; set; }

		public Drug()
		{
			IsDrugSelected = false;
		}
	}
}
