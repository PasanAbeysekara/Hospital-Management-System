using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model.InsidePrescription;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.ViewModel
{
	public partial class UserDashboardVM : ObservableObject
	{
		[ObservableProperty]
		public string patientCount;

		[ObservableProperty]
		public string prescriptionCount;

		[ObservableProperty]
		public string testCount;

		[ObservableProperty]
		public string doctorCount;


		private ObservableCollection<Drug> _drugsData = new ObservableCollection<Drug>();

		public ObservableCollection<Drug> DrugsData
		{
			get => _drugsData;
			set
			{
				if (_drugsData != value)
				{
					_drugsData = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<Test> _testsData = new ObservableCollection<Test>();

		public ObservableCollection<Test> TestsData
		{
			get => _testsData;
			set
			{
				if (_testsData != value)
				{
					_testsData = value;
					OnPropertyChanged();
				}
			}
		}


		public void Read()
		{
			using (DataContext context = new DataContext())
			{
			
				_testsData.Clear();
				foreach (var tst in context.Tests)
				{
					_testsData.Add(tst);
				}
				_drugsData.Clear();
				foreach (var drg in context.Drugs)
				{
					_drugsData.Add(drg);
				}
			}

		}

		public UserDashboardVM()
		{
			
			using(DataContext context = new DataContext())
			{
				PatientCount = context.Patients.Count().ToString();
				PrescriptionCount = context.Prescriptions.Count().ToString();
				TestCount = context.Tests.Count().ToString();
				DoctorCount = context.Doctors.Count().ToString();
				
			}

			Read();
		}
	}
}
