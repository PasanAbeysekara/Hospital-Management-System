using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMS.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.ViewModel.Profiles
{
	public partial class AppointmentProfile:ObservableObject
	{
		[ObservableProperty]
		public string appointmentId;

		[ObservableProperty]
		public string appointmentDate;

		[ObservableProperty]
		public string doctorId;

		[ObservableProperty]
		public string doctorName;

		[ObservableProperty]
		public string patientId;

		[ObservableProperty]
		public string patientName;

		public AppointmentProfile()
		{
			using(DataContext context = new DataContext())
			{
				Appointment tmp = context.Appointments.Single(x => x.IsAppointmentSelected == true);
				foreach (var app_ in context.Appointments) app_.IsAppointmentSelected = false;
				context.SaveChanges();

				AppointmentId = tmp.Id.ToString();
				AppointmentDate = tmp.AppointedDate.ToShortDateString();
				DoctorId = tmp.DoctorId.ToString();
				DoctorName = context.Doctors.Single(x => x.Id == tmp.DoctorId).Name;
				PatientId = tmp.PatientId.ToString();
				PatientName = context.Patients.Single(x => x.Id == tmp.PatientId).FullName;
			}
		}

	}
}
