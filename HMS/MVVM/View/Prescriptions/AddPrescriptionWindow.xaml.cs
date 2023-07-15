using HMS.MVVM.ViewModel;
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

namespace HMS.MVVM.View.Prescriptions
{
	/// <summary>
	/// Interaction logic for AddPrescriptionWindow.xaml
	/// </summary>
	public partial class AddPrescriptionWindow : Window
	{
		public AddPrescriptionWindow()
		{
			DataContext = new AddPrescriptionWindowVM();
			InitializeComponent();
			Loaded += AddPatientWindow_Loaded;
		}

		private void AddPatientWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (DataContext is ICloseWindows vm)
			{
				vm.Close += () =>
				{
					this.Close();
				};
			}
		}

		private void buttonCreate_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}
	}
}
