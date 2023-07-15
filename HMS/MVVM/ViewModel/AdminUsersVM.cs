using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.Model.Authentication;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.View.Doctors;
using HMS.MVVM.View.MessageWindow;
using HMS.MVVM.View.Users;
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
	public partial class AdminUsersVM : ObservableObject
	{
		private ObservableCollection<User> _usersData = new ObservableCollection<User>();

		public ObservableCollection<User> UsersData
		{
			get => _usersData;
			set
			{
				if (_usersData != value)
				{
					_usersData = value;
					OnPropertyChanged();
				}
			}
		}


		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
            var messageWindow = new MessageWindow("Users list has been refreshed 🔃");
            messageWindow.ShowDialog();
            Read();
		}


		// Delete doctor command using prism core package
		private DelegateCommand<User> _deleteUserCommand;
		public DelegateCommand<User> DeleteUserCommand =>
			_deleteUserCommand ?? (_deleteUserCommand = new DelegateCommand<User>(ExecuteDeleteUserCommand));

		void ExecuteDeleteUserCommand(User parameter)
		{
			string deletedUserName = "";
			using (DataContext context = new DataContext())
			{
				User selectedUser = parameter;
				if (selectedUser != null)
				{
					User usr = context.Users.Single(x => x.UserId == selectedUser.UserId);
					deletedUserName = usr.UserName;
					context.Users.Remove(usr);
					context.SaveChanges();
				}
			}
            var messageWindow = new WarningMessageWindow($"User '{deletedUserName}' deleted!");
            messageWindow.ShowDialog();
            Read();
		}

		// Edit doctor command using prism core package
		private DelegateCommand<User> _editUserCommand;
		public DelegateCommand<User> EditUserCommand =>
			_editUserCommand ?? (_editUserCommand = new DelegateCommand<User>(ExecuteEditUserCommand));

		void ExecuteEditUserCommand(User parameter)
		{
			using (DataContext context = new DataContext())
			{
				context.Users.Single(x => x.UserId == parameter.UserId).IsSelectedUser = true;
				context.SaveChanges();
				var window = new EditUserWindow();
				window.Show();
			}
			Read();
		}

		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				_usersData.Clear();
				foreach (var usr in context.Users)
				{
					_usersData.Add(usr);
				}
			}

		}

		public AdminUsersVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var usr_ in context.Users) usr_.IsSelectedUser = false;
				context.SaveChanges();
			}
			Read();
		}
	}
}
