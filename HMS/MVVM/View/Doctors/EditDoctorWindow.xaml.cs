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

namespace HMS.MVVM.View.Doctors
{
	/// <summary>
	/// Interaction logic for EditDoctorWindow.xaml
	/// </summary>
	public partial class EditDoctorWindow : Window
	{
		public EditDoctorWindow()
		{
			DataContext = new EditDoctorWindowVM();
			InitializeComponent();
			Loaded += AddDoctorWindow_Loaded;
		}
		private void AddDoctorWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (DataContext is ICloseWindows vm)
			{
				vm.Close += () =>
				{
					this.Close();
				};
			}
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
