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
    /// Interaction logic for NormalUserGuideWindow2.xaml
    /// </summary>
    public partial class NormalUserGuideWindow2 : Window
    {
        public NormalUserGuideWindow2()
        {
            InitializeComponent();
        }
        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var normalUserGuideWindow3 = new NormalUserGuideWindow3();
            normalUserGuideWindow3.Show();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var normalUserGuideWindow = new NormalUserGuideWindow();
            normalUserGuideWindow.Show();
        }


        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
