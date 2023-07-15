using HMS.MVVM.Model;
using HMS.MVVM.Model.Authentication;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{

	public class EditUserWindowVM : INotifyPropertyChanged, ICloseWindows
	{
		// #begin INotifyPropertyChanged Interface 
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		// #end

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		private string _userName;

		public string UserName { get { return _userName; } set { _userName = value; OnPropertyChanged(nameof(UserName)); } }

		private string _userPassword;

		public string UserPassword { get { return _userPassword; } set { _userPassword = value; OnPropertyChanged(nameof(UserPassword)); } }

		private bool[] _modeArray = new bool[] { true, false, false };
		public bool[] ModeArray
		{
			get { return _modeArray; }
		}
		public int SelectedMode
		{
			get { return Array.IndexOf(_modeArray, true); }
		}

		// Close Button Command
		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			using (DataContext context = new DataContext())
			{
				var usr_ = context.Users.Single(x => x.IsSelectedUser == true);
				usr_.IsSelectedUser = false;
				context.SaveChanges();
			}

			Close?.Invoke();
		}

		// Create Button Command
		private DelegateCommand _createCommand;
		public DelegateCommand CreateCommand =>
			_createCommand ?? (_createCommand = new DelegateCommand(ExecuteCreateCommand));


		void ExecuteCreateCommand()
		{
			using (DataContext context = new DataContext())
			{
				User tmp = context.Users.Single(x => x.IsSelectedUser == true);
				if (tmp != null)
				{
					tmp.UserName = _userName;
					tmp.Password = _userPassword;
					tmp.IsSuperUser = ModeArray[0];
					context.SaveChanges();
				}
				else
				{
                    var messageWindow2 = new WarningMessageWindow("Please select a user again!");
                    messageWindow2.ShowDialog();
                }
			}
            var messageWindow = new MessageWindow("Refresh the Users records to see the changes 😊");
            messageWindow.ShowDialog();
            Close?.Invoke();
		}

		public EditUserWindowVM()
		{
			using (DataContext context = new DataContext())
			{
				User usr_ = context.Users.Single(x => x.IsSelectedUser == true);
				_userName = usr_.UserName;
				_userPassword = usr_.Password;
				ModeArray[0] = usr_.IsSuperUser;
				ModeArray[1] = usr_.IsSuperUser ? false : true;

			}
		}

	}
}
