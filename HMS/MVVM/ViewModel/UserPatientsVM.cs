using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.View.MessageWindow;
using HMS.MVVM.View.Patients;
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
	public class UserPatientsVM : ObservableObject
	{
		private ObservableCollection<Patient> _patientData = new ObservableCollection<Patient>();


		public ObservableCollection<Patient> PatientData
		{
			get => _patientData;
			set
			{
				if (_patientData != value)
				{
					_patientData = value;
					OnPropertyChanged();
				}
			}
		}

		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
			var messageWindow = new MessageWindow("Patient list has been refreshed 🔃");
			messageWindow.ShowDialog();
			Read();
		}

		// Delete patient command using prism core package
		private DelegateCommand<Patient> _deletePatientCommand;
		public DelegateCommand<Patient> DeletePatientCommand =>
			_deletePatientCommand ?? (_deletePatientCommand = new DelegateCommand<Patient>(ExecuteDeletePatientCommand));

		void ExecuteDeletePatientCommand(Patient parameter)
		{
			string deletedPatientName = "";
			using (DataContext context = new DataContext())
			{
				Patient selectedPatient = parameter;
				if (selectedPatient != null)
				{
					Patient pat = context.Patients.Single(x => x.Id == selectedPatient.Id);
					deletedPatientName = pat.FullName;
					context.Patients.Remove(pat);
					context.SaveChanges();
				}
			}
			var messageWindow = new WarningMessageWindow($"Paitent '{deletedPatientName}' has been deleted !");
			messageWindow.ShowDialog();

			Read();
		}

		// Profile patient command using prism core package
		private DelegateCommand<Patient> _profilePatientCommand;
		public DelegateCommand<Patient> ProfilePatientCommand =>
			_profilePatientCommand ?? (_profilePatientCommand = new DelegateCommand<Patient>(ExecuteProfilePatientCommand));

		void ExecuteProfilePatientCommand(Patient parameter)
		{
			

			using (DataContext context = new DataContext())
			{
				foreach (var _pa in context.Patients)
				{
					_pa.IsPatientSelected = false;
				}
				context.SaveChanges();

				context.Patients.Single(x => x.Id == parameter.Id).IsPatientSelected = true;
				context.SaveChanges();
			}
			var window = new PatientProfileWindow();
			window.Show();

			Read();
		}



		// Edit patient command using prism core package
		private DelegateCommand<Patient> _editPatientCommand;
		public DelegateCommand<Patient> EditPatientCommand =>
			_editPatientCommand ?? (_editPatientCommand = new DelegateCommand<Patient>(ExecuteEditPatientCommand));

		void ExecuteEditPatientCommand(Patient parameter)
		{
			

			using (DataContext context = new DataContext())
			{
				context.Patients.Single(x => x.Id == parameter.Id).IsPatientSelected = true;
				context.SaveChanges();
				
				var window = new EditPatientWindow();
				window.Show();
			}

			Read();
		}


		public UserPatientsVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var pat_ in context.Patients) pat_.IsPatientSelected = false;
				context.SaveChanges();
			}

			
			Read();
		}

		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				
				_patientData.Clear();
				foreach (var std in context.Patients)
				{
					_patientData.Add(std);
				}
			}
		}
	}
}
