using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.View.MessageWindow;
using HMS.MVVM.View.Patients;
using HMS.MVVM.View.Prescriptions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class UserPrescriptionsVM : ObservableObject
	{
		private ObservableCollection<Prescription> _prescriptionsData = new ObservableCollection<Prescription>();

		public ObservableCollection<Prescription> PrescriptionsData
		{
			get => _prescriptionsData;
			set
			{
				if (_prescriptionsData != value)
				{
					_prescriptionsData = value;
					OnPropertyChanged();
				}
			}
		}

		[ObservableProperty]
		public string tmperar;

		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
			
			var messageWindow = new MessageWindow("You clicked refresh 🔃");
			messageWindow.ShowDialog();

			Read();
		}

		// Delete patient command using prism core package
		private DelegateCommand<Prescription> _deletePrescriptionommand;
		public DelegateCommand<Prescription> DeletePrescriptionCommand =>
			_deletePrescriptionommand ?? (_deletePrescriptionommand = new DelegateCommand<Prescription>(ExecutePrescriptionPatientCommand));

		void ExecutePrescriptionPatientCommand(Prescription parameter)
		{
			string deletedPrescriptionName = "";
			using (DataContext context = new DataContext())
			{
				Prescription selectedPatient = parameter;
				if (selectedPatient != null)
				{
					Prescription pres = context.Prescriptions.Single(x => x.Id == selectedPatient.Id);
					deletedPrescriptionName = pres.Id.ToString();
					context.Prescriptions.Remove(pres);
					context.SaveChanges();
				}
			}
			
			var messageWindow = new WarningMessageWindow($"Prescription with ID '{deletedPrescriptionName}' has been deleted!");
			messageWindow.ShowDialog();

			Read();
		}


		// Profile patient command using prism core package
		private DelegateCommand<Prescription> _profilePrescriptionCommand;
		public DelegateCommand<Prescription> ProfilePrescriptionCommand =>
			_profilePrescriptionCommand ?? (_profilePrescriptionCommand = new DelegateCommand<Prescription>(ExecuteProfilePrescriptionCommand));

		void ExecuteProfilePrescriptionCommand(Prescription parameter)
		{
			using (DataContext context = new DataContext())
			{
				foreach (var _pa in context.Prescriptions)
				{
					_pa.IsPrescriptionSelected = false;
				}
				context.SaveChanges();

				context.Prescriptions.Single(x => x.Id == parameter.Id).IsPrescriptionSelected = true;
				context.SaveChanges();
			}
			var window = new PrescriptionProfileWindow();
			window.Show();

			Read();
		}

		public UserPrescriptionsVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var pat_ in context.Prescriptions) pat_.IsPrescriptionSelected = false;
				context.SaveChanges();
			}
			Read();
		}


		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				
				_prescriptionsData.Clear();
				foreach (var pres in context.Prescriptions)
				{
					_prescriptionsData.Add(pres);
				}
			}

		}
	}
}
