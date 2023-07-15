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
    /// Interaction logic for AdminUserGuide2.xaml
    /// </summary>
    public partial class AdminUserGuide2 : Window
    {
        public AdminUserGuide2()
        {
            InitializeComponent();
        }
        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var adminUserGuide1 = new AdminUserGuide1();
            adminUserGuide1.Show();
        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
