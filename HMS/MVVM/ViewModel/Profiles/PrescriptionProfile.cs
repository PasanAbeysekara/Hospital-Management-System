using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.Model.InsidePrescription.insideDrug;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel.Profiles
{
	public partial class PrescriptionProfile:ObservableObject
	{

		private ObservableCollection<Dosage> dosages = new ObservableCollection<Dosage>();


		public ObservableCollection<Dosage> Dosages
		{
			get => dosages;
			set
			{
				if (dosages != value)
				{
					dosages = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<MedicalTest> medicalTests = new ObservableCollection<MedicalTest>();


		public ObservableCollection<MedicalTest> MedicalTests
		{
			get => medicalTests;
			set
			{
				if (medicalTests != value)
				{
					medicalTests = value;
					OnPropertyChanged();
				}
			}
		}


		[ObservableProperty]
		public string prescribedDate;

		public PrescriptionProfile()
		{

			using (DataContext context = new DataContext())
			{
				var pres = context.Prescriptions.Single(x => x.IsPrescriptionSelected == true);
				foreach (var pred_ in context.Prescriptions) pred_.IsPrescriptionSelected = false;
				context.SaveChanges();

				PrescribedDate = pres.PrescribedDate.ToShortDateString();

				foreach(var dsg in context.Dosages.Where(x => x.PrescriptionId == pres.Id))
				{
					Dosages.Add(dsg);
				}

				foreach (var mtst in context.MedicalTests.Where(x => x.PrescriptionId == pres.Id))
				{
					MedicalTests.Add(mtst);
				}

				dosages.Clear();
				var dsgs = context.Dosages.Where(x => x.PrescriptionId == pres.Id).ToList();
				if (dsgs != null) dsgs.ForEach(y => { dosages.Add(y); });
				else MessageBox.Show("This Prescription have no Dosages");
				medicalTests.Clear();
				var mtsts = context.MedicalTests.Where(x => x.PrescriptionId == pres.Id).ToList();
				if (mtsts != null) mtsts.ForEach(p => { medicalTests.Add(p); });
				else MessageBox.Show("This Prescription have no Medical Tests");

			}
		}
	}
}
