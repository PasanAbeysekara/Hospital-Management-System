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
    /// Interaction logic for AdminUserGuide1.xaml
    /// </summary>
    public partial class AdminUserGuide1 : Window
    {
        public AdminUserGuide1()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            var adminUserGuide2 = new AdminUserGuide2();
            adminUserGuide2.Show();
        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
