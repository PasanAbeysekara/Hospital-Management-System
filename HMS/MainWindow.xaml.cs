using HMS.MVVM.Model.Authentication;
using HMS.MVVM.View.MessageWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS
{

	public partial class MainWindow : Window
	{
		public List<User> allUsers;

		public string isRegisteredUser(string enteredUserName, string enteredUserPassword)
		{
			string result = "not_a_user";
			foreach (var user in allUsers)
			{
				if (user.UserName == enteredUserName && user.Password == enteredUserPassword)
				{
					result = "normal_user";
					if (user.IsSuperUser) result = "super_user";
					return result;
				}
			}
			return result;
		}
		private void Border_Mousedown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}
		private bool isMaximized = false;
		private void Border_MouseLeftButtondown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				if (isMaximized)
				{
					this.WindowState = WindowState.Normal;
					this.Width = 1080;
					this.Height = 720;
					isMaximized = false;
				}
				else
				{
					this.WindowState = WindowState.Maximized;
					isMaximized = true;
				}
			}
		}
		private void CloseButton_Clicked(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		public MainWindow()
		{
			InitializeComponent();
			allUsers = new List<User> { };
			using (DataContext context = new DataContext())
			{
				foreach (var usr in context.Users)
				{
					allUsers.Add(usr);
				}
			}

		}

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Username")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E7CBCB")); ;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Username";
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E7CBCB"));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void loginClick(object sender, RoutedEventArgs e)
		{
			SecureString inputPassword = userPassword.SecurePassword;

			IntPtr unmanagedString = IntPtr.Zero;
			string password = null;

			try
			{
				unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(inputPassword);
				password = Marshal.PtrToStringUni(unmanagedString);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
			}

			string res = isRegisteredUser(userName.Text, password);
			switch (res)
			{
				case "not_a_user":
                    var messageWindow = new WarningMessageWindow("Please enter valid UserName & Password!");
                    messageWindow.ShowDialog();
					break;

				case "normal_user":
					var window = new NormalUserWindow();
					window.Show();
					this.Close();
					break;

				case "super_user":
					var window2 = new AdminWindow();
					window2.Show();
					this.Close();
					break;
			}

		}



	}
}
