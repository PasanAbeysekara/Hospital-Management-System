using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.ViewModel
{
	public partial class NormalUserWindowVM : ObservableObject
	{
		public UserDashboardVM U_Dashboard_VM { get; set; }
		public UserAppointmentsVM U_Appointments_VM { get; set; }
		public UserBillingVM U_Billing_VM { get; set; }
		public UserPatientsVM U_Patients_VM { get; set; }
		public UserPrescriptionsVM U_Prescriptions_VM { get; set; }


		// temp setter for maintainting current state
		private object _currentView;

		[ObservableProperty]
		public string currentViewName;

		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		[RelayCommand]
		public void UserDashboardView()
		{
			U_Dashboard_VM = new UserDashboardVM();
			CurrentView = U_Dashboard_VM;
			CurrentViewName = "Dashboard";
		}
		[RelayCommand]
		public void UserAppointmentsView()
		{
			U_Appointments_VM = new UserAppointmentsVM();
			CurrentView = U_Appointments_VM;
			CurrentViewName = "Appointments";
		}
		[RelayCommand]
		public void UserBillingView()
		{
			U_Billing_VM = new UserBillingVM();
			CurrentView = U_Billing_VM;
			CurrentViewName = "Billings";
		}
		[RelayCommand]
		public void UserPatientsView()
		{
			U_Patients_VM = new UserPatientsVM();
			CurrentView = U_Patients_VM;
			CurrentViewName = "Patients";
		}
		[RelayCommand]
		public void UserPrescriptionsView()
		{
			U_Prescriptions_VM = new UserPrescriptionsVM();
			CurrentView = U_Prescriptions_VM;
			CurrentViewName = "Prescriptions";
		}

		public NormalUserWindowVM()
		{

			U_Dashboard_VM = new UserDashboardVM();
			CurrentView = U_Dashboard_VM;
			CurrentViewName = "Dashboard";

		}

		
		

		
	}
}
