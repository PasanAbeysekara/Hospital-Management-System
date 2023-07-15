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

namespace HMS.MVVM.View.UserGuides
{
    /// <summary>
    /// Interaction logic for NormalUserGuideWindow.xaml
    /// </summary>
    public partial class NormalUserGuideWindow : Window
    {
        public NormalUserGuideWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            var normalUserGuideWindow2 = new NormalUserGuideWindow2();
            normalUserGuideWindow2.Show();
        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
