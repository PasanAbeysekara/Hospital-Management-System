using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HMS.MVVM.View.UserControls
{
	public partial class InfoCard : UserControl
	{
		public InfoCard()
		{
			InitializeComponent();
		}

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }

		}
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(InfoCard));

		public string Number
		{
			get { return (string)GetValue(NumberProperty); }
			set { SetValue(NumberProperty, value); }

		}
		public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(InfoCard));

		public FontAwesome.Sharp.IconChar Icon
		{
			get { return (FontAwesome.Sharp.IconChar)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }

		}
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.Sharp.IconChar), typeof(InfoCard));

		public string Background1
		{
			get { return (string)GetValue(Background1Property); }
			set { SetValue(Background1Property, value); }

		}
		public static readonly DependencyProperty Background1Property = DependencyProperty.Register("Background1", typeof(string), typeof(InfoCard));

		public string Background2
		{
			get { return (string)GetValue(Background2Property); }
			set { SetValue(Background2Property, value); }

		}
		public static readonly DependencyProperty Background2Property = DependencyProperty.Register("Background2", typeof(string), typeof(InfoCard));

		public string EllipseBackground1
		{
			get { return (string)GetValue(EllipseBackground1Property); }
			set { SetValue(EllipseBackground1Property, value); }

		}
		public static readonly DependencyProperty EllipseBackground1Property = DependencyProperty.Register("EllipseBackground1", typeof(string), typeof(InfoCard));

		public string EllipseBackground2
		{
			get { return (string)GetValue(EllipseBackground2Property); }
			set { SetValue(EllipseBackground2Property, value); }

		}
		public static readonly DependencyProperty EllipseBackground2Property = DependencyProperty.Register("EllipseBackground2", typeof(string), typeof(InfoCard));

	}
}
