using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.View.Appointments;
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
	public partial class UserAppointmentsVM : ObservableObject
	{
		private ObservableCollection<Appointment> _appointmentsData = new ObservableCollection<Appointment>();

		public ObservableCollection<Appointment> AppointmentsData
		{
			get => _appointmentsData;
			set
			{
				if (_appointmentsData != value)
				{
					_appointmentsData = value;
					OnPropertyChanged();
				}
			}
		}


		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
			
			var messageWindow = new MessageWindow("You clicked refresh!");
			messageWindow.ShowDialog();

			Read();
		}

		// Delete prescription command using prism core package
		private DelegateCommand<Appointment> _deleteAppointmentCommand;
		public DelegateCommand<Appointment> DeleteAppointmentCommand =>
			_deleteAppointmentCommand ?? (_deleteAppointmentCommand = new DelegateCommand<Appointment>(ExecuteDeleteAppointmentCommand));

		void ExecuteDeleteAppointmentCommand(Appointment parameter)
		{
			string deletedAppName = "";
			using (DataContext context = new DataContext())
			{
				Appointment selectedAppointment = parameter;
				if (selectedAppointment != null)
				{
					Appointment app = context.Appointments.Single(x => x.Id == selectedAppointment.Id);
					deletedAppName = app.Id.ToString();
					context.Appointments.Remove(app);
					context.SaveChanges();
				}
			}
			
			var messageWindow = new WarningMessageWindow($"Appointment with ID of '{deletedAppName}' has been deleted!");
			messageWindow.ShowDialog();


			Read();
		}

		// Profile patient command using prism core package
		private DelegateCommand<Appointment> _profileAppointmnetCommand;
		public DelegateCommand<Appointment> ProfileAppointmentCommand =>
			_profileAppointmnetCommand ?? (_profileAppointmnetCommand = new DelegateCommand<Appointment>(ExecuteProfileAppointmentCommand));

		void ExecuteProfileAppointmentCommand(Appointment parameter)
		{
			using (DataContext context = new DataContext())
			{
				foreach (var _ap in context.Appointments)
				{
					_ap.IsAppointmentSelected = false;
				}
				context.SaveChanges();

				context.Appointments.Single(x => x.Id == parameter.Id).IsAppointmentSelected = true;
				context.SaveChanges();
			}
			var window = new AppointmentProfileWindow();
			window.Show();

			Read();
		}

		public UserAppointmentsVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var pat_ in context.Appointments) pat_.IsAppointmentSelected = false;
				context.SaveChanges();
			}
			Read();
		}


		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				
				_appointmentsData.Clear();
				foreach (var app in context.Appointments)
				{
					_appointmentsData.Add(app);
				}
			}

		}
	}
}
