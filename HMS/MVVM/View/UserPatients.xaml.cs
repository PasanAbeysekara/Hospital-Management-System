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
	/// Interaction logic for UserPatients.xaml
	/// </summary>
	public partial class UserPatients : UserControl
	{
		public UserPatients()
		{
			InitializeComponent();
		}

		private void AddMemberButton_Click(object sender, RoutedEventArgs e) // When AddPatient button clicked
        {
			var window = new AddPatientWindow();
			window.Show();
		}
	}
}
