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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS.MVVM.View.UserControls
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order2 : UserControl
    {
        public Order2()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }

        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Order2));

        public string Desc
        {
            get { return (string)GetValue(DescProperty); }
            set { SetValue(DescProperty, value); }

        }
        public static readonly DependencyProperty DescProperty = DependencyProperty.Register("Desc", typeof(string), typeof(Order2));

        public FontAwesome.Sharp.IconChar Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }

        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.Sharp.IconChar), typeof(Order2));


    }
}
