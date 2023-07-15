using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.View.Doctors;
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
	public partial class AdminDoctorsVM : ObservableObject
	{
		private ObservableCollection<Doctor> _doctorsData = new ObservableCollection<Doctor>();

		public ObservableCollection<Doctor> DoctorsData
		{
			get => _doctorsData;
			set
			{
				if (_doctorsData != value)
				{
					_doctorsData = value;
					OnPropertyChanged();
				}
			}
		}


		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
            var messageWindow = new MessageWindow("Doctors list has been refreshed 🔃");
            messageWindow.ShowDialog();
			Read();
		}

		// Delete doctor command using prism core package
		private DelegateCommand<Doctor> _deleteDoctorCommand;
		public DelegateCommand<Doctor> DeleteDoctorCommand =>
			_deleteDoctorCommand ?? (_deleteDoctorCommand = new DelegateCommand<Doctor>(ExecuteDeleteDoctorCommand));

		void ExecuteDeleteDoctorCommand(Doctor parameter)
		{
			string deletedDoctorName = "";
			using (DataContext context = new DataContext())
			{
				Doctor selectedDoctor = parameter;
				if (selectedDoctor != null)
				{
					Doctor doc = context.Doctors.Single(x => x.Id == selectedDoctor.Id);
					deletedDoctorName = doc.Name;
					context.Doctors.Remove(doc);
					context.SaveChanges();
				}
			}
            var messageWindow = new WarningMessageWindow($"Doctor '{deletedDoctorName}' deleted!");
            messageWindow.ShowDialog();
            Read();
		}


		// Edit doctor command using prism core package
		private DelegateCommand<Doctor> _editDoctorCommand;
		public DelegateCommand<Doctor> EditDoctorCommand =>
			_editDoctorCommand ?? (_editDoctorCommand = new DelegateCommand<Doctor>(ExecuteEditDoctorCommand));

		void ExecuteEditDoctorCommand(Doctor parameter)
		{
			using (DataContext context = new DataContext())
			{
				context.Doctors.ToList().ForEach(x => x.IsDoctorSelected = false);
				context.SaveChanges();
				context.Doctors.Single(x => x.Id == parameter.Id).IsDoctorSelected = true;
				context.SaveChanges();
				var window = new EditDoctorWindow();
				window.Show();
			}
			Read();
		}


		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				
				_doctorsData.Clear();
				foreach (var doc in context.Doctors)
				{
					_doctorsData.Add(doc);
				}
			}

		}

		public AdminDoctorsVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var doc_ in context.Doctors) doc_.IsDoctorSelected = false;
				context.SaveChanges();
			}
			Read();
		}
	}
}
