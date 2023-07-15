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
	/// Interaction logic for PrescriptionProfileWindow.xaml
	/// </summary>
	public partial class PrescriptionProfileWindow : Window
	{
		public PrescriptionProfileWindow()
		{
			InitializeComponent();
		}

		private void CloseButton_Clicked(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
