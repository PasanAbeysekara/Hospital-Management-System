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

namespace HMS
{
	public partial class AddPatientWindow : Window
	{
		public AddPatientWindow()
		{
			DataContext = new AddPatientWindowVM();
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

		private void buttonSelectImage_Click(object sender, RoutedEventArgs e)
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".png";
			dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = dlg.ShowDialog();


			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				// Open document 
				string filename = dlg.FileName;
				//textBox1.Text = filename;
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
