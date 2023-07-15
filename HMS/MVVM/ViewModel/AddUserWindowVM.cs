using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.Model.Authentication;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{


	public partial class AddUserWindowVM : ObservableObject, ICloseWindows
	{

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		[ObservableProperty]
		public string userName;

		[ObservableProperty]
		public string userPassword;


		private bool[] _modeArray = new bool[] { true, false, false };
		public bool[] ModeArray
		{
			get { return _modeArray; }
		}
		public int SelectedMode
		{
			get { return Array.IndexOf(_modeArray, true); }
		}

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
				if (String.IsNullOrWhiteSpace(UserName) || String.IsNullOrWhiteSpace(UserPassword))
				{
					if (String.IsNullOrWhiteSpace(UserName) && String.IsNullOrWhiteSpace(UserPassword))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Drug Details.\n Make Sure to fill all the fields!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(UserName))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Trade User Name!");
						messageWindow.ShowDialog();
					}
					else
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid User Password!");
						messageWindow.ShowDialog();
					}
				}
				else
				{
					context.Users.Add(new User(UserName, UserPassword, ModeArray[0]));
					context.SaveChanges();
					var messageWindow = new MessageWindow("Please click 'Refresh' to see the updated User list!");
					messageWindow.ShowDialog();
					Close?.Invoke();
				}
			}
		}

		public AddUserWindowVM()
		{
		}

	}
}
