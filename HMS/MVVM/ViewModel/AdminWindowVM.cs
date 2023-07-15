using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.ViewModel
{
	public partial class AdminWindowVM : ObservableObject
	{
		public UserDashboardVM U_Dashboard_VM { get; set; }
		public AdminDoctorsVM A_Doctors_VM { get; set; }
		public AdminDrugsVM A_Drugs_VM { get; set; }
		public AdminTestsVM A_Tests_VM { get; set; }
		public AdminUsersVM A_Users_VM { get; set; }
		

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
		public void AdminDoctorsView()
		{
			A_Doctors_VM = new AdminDoctorsVM();
			CurrentView = A_Doctors_VM;
			CurrentViewName = "Doctors";
		}
		[RelayCommand]
		public void AdminDrugsView()
		{
			A_Drugs_VM = new AdminDrugsVM();
			CurrentView = A_Drugs_VM;
			CurrentViewName = "Drugs";
		}
		[RelayCommand]
		public void AdminTestsView()
		{
			A_Tests_VM = new AdminTestsVM();
			CurrentView = A_Tests_VM;
			CurrentViewName = "Tests";
		}
		[RelayCommand]
		public void AdminUsersView()
		{
			A_Users_VM = new AdminUsersVM();
			CurrentView = A_Users_VM;
			CurrentViewName = "Users";
		}

		public AdminWindowVM()
		{

			U_Dashboard_VM = new UserDashboardVM();
			CurrentView = U_Dashboard_VM;
			CurrentViewName = "Dashboard";

		}
	}
}
