using HMS.MVVM.View.Drugs;
using HMS.MVVM.View.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HMS.MVVM.View
{
	/// <summary>
	/// Interaction logic for AdminUsers.xaml
	/// </summary>
	public partial class AdminUsers : UserControl
	{
		public AdminUsers()
		{
			InitializeComponent();
		}

		private void AddMemberButton_Click(object sender, RoutedEventArgs e) // When AddUser button clicked
        {

			var window = new AddUserWindow();
			window.Show();
		}
    }
}
