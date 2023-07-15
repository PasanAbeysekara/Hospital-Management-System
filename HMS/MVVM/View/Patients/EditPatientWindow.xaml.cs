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

namespace HMS.MVVM.View.Patients
{
	/// <summary>
	/// Interaction logic for EditPatientWindow.xaml
	/// </summary>
	public partial class EditPatientWindow : Window
	{
		public EditPatientWindow()
		{
			DataContext = new EditPatientWindowVM();
			InitializeComponent();
			Loaded += EditPatientWindow_Loaded;
		}
		private void EditPatientWindow_Loaded(object sender, RoutedEventArgs e)
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
