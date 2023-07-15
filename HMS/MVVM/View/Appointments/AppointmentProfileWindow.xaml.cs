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
using System.Windows.Shapes;

namespace HMS.MVVM.View.Appointments
{
	/// <summary>
	/// Interaction logic for AppointmentProfileWindow.xaml
	/// </summary>
	public partial class AppointmentProfileWindow : Window
	{
		public AppointmentProfileWindow()
		{
			InitializeComponent();
		}

		private void CloseButton_Clicked(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
