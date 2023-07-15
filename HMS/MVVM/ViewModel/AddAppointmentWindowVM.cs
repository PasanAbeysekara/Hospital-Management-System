using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class AddAppointmentWindowVM : ObservableObject, ICloseWindows
	{
		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		// observable properties

		[ObservableProperty]
		public string doctorName;

		[ObservableProperty]
		public List<string> doctorNames;

		[ObservableProperty]
		public string dope;

		[ObservableProperty]
		public DateTime appointmentDate;

		// commands

		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			Close?.Invoke();

		}


		private DelegateCommand _saveCommand;
		public DelegateCommand SaveCommand =>
			_saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));

		void ExecuteSaveCommand()
		{
			using (DataContext context = new DataContext())
			{
				context.Appointments.Add(new Model.Appointment
				{
					DoctorId = context.Doctors.Single(x => x.Name == doctorName).Id,
					PatientId = context.Patients.Single(x => x.IsPatientSelected == true).Id,
					AppointedDate = AppointmentDate
				});
				context.SaveChanges();

			}
			
			var messageWindow = new MessageWindow("Please click 'Refresh' to see the updated Appointment list 😊");
			messageWindow.ShowDialog();

			Close?.Invoke();

		}



		public AddAppointmentWindowVM()
		{
			string dateString = "2023-04-14";
			DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			AppointmentDate = date;

			DoctorNames = new List<string> { };
			using (DataContext context = new DataContext())
			{
				foreach (var doc in context.Doctors)
				{
					DoctorNames.Add(doc.Name);
				}
				DoctorName = DoctorNames[0];
			}

		}

	}
}
