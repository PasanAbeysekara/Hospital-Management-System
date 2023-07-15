using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class AddDoctorWindowVM:ObservableObject, ICloseWindows
	{

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		[ObservableProperty]
		public string name;

		[ObservableProperty]
		public double fee;

		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			Close?.Invoke();
		}

		private DelegateCommand _createCommand;
		public DelegateCommand CreateCommand =>
			_createCommand ?? (_createCommand = new DelegateCommand(ExecuteCreateommand));

		void ExecuteCreateommand()
		{
			using (DataContext context = new DataContext())
			{
				if(String.IsNullOrWhiteSpace(Name))
				{
					if (String.IsNullOrWhiteSpace(Name))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Name of Doctor!");
						messageWindow.ShowDialog();

					}
				}
				else
				{
					context.Doctors.Add(new Doctor { Name = "Dr. " + Name, Fee = Fee });
					context.SaveChanges();
					var messageWindow = new MessageWindow("Please click 'Refresh' to see the updated Doctor list");
					messageWindow.ShowDialog();

					Close?.Invoke();
				}
			}
		}

		public AddDoctorWindowVM()
		{
		}

	}
}
