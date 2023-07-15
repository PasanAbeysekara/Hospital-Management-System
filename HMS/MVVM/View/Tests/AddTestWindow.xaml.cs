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

namespace HMS.MVVM.View.Tests
{
    /// <summary>
    /// Interaction logic for AddTestWindow.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {
        public AddTestWindow()
		{
			DataContext = new AddTestWindowVM();
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

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}

	}
}
