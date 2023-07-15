using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.View.MessageWindow;
using HMS.MVVM.View.Patients;
using HMS.MVVM.View.Tests;
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
	public partial class AdminTestsVM : ObservableObject
	{
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


		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
            var messageWindow = new MessageWindow("Tests list has been refreshed 🔃");
            messageWindow.ShowDialog();
            Read();
		}

		// Delete doctor command using prism core package
		private DelegateCommand<Test> _deleteTestCommand;
		public DelegateCommand<Test> DeleteTestCommand =>
			_deleteTestCommand ?? (_deleteTestCommand = new DelegateCommand<Test>(ExecuteDeleteTestCommand));

		void ExecuteDeleteTestCommand(Test parameter)
		{
			string deletedTestName = "";
			using (DataContext context = new DataContext())
			{
				Test selectedTest = parameter;
				if (selectedTest != null)
				{
					Test tst = context.Tests.Single(x => x.Id == selectedTest.Id);
					deletedTestName = tst.TestName;
					context.Tests.Remove(tst);
					context.SaveChanges();
				}
			}
            var messageWindow = new WarningMessageWindow($"Test '{deletedTestName}' deleted!");
            messageWindow.ShowDialog();
            Read();
		}


		// Edit doctor command using prism core package
		private DelegateCommand<Test> _editTestCommand;
		public DelegateCommand<Test> EditTestCommand =>
			_editTestCommand ?? (_editTestCommand = new DelegateCommand<Test>(ExecuteEditTestCommand));

		void ExecuteEditTestCommand(Test parameter)
		{
			using (DataContext context = new DataContext())
			{
				context.Tests.Single(x => x.Id == parameter.Id).IsTestSelected = true;
				context.SaveChanges();
				var window = new EditTestWindow();
				window.Show();
			}
			Read();
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
			}

		}

		public AdminTestsVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var tst_ in context.Tests) tst_.IsTestSelected = false;
				context.SaveChanges();
			}
			Read();
		}
	}
}
