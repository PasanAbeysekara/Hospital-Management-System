using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class AddTestWindowVM : ObservableObject, ICloseWindows
	{

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		[ObservableProperty]
		public string testName;

		[ObservableProperty]
		public double testFee;

		[ObservableProperty]
		public string description;

		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			Close?.Invoke();
		}

		private DelegateCommand _createCommand;
		public DelegateCommand CreateCommand =>
			_createCommand ?? (_createCommand = new DelegateCommand(ExecuteCreateommand));

		void ExecuteCreateommand()
		{
			using (DataContext context = new DataContext())
			{
				if ( String.IsNullOrWhiteSpace(TestName) || String.IsNullOrWhiteSpace(Description) )
				{
					if (String.IsNullOrWhiteSpace(TestName) && String.IsNullOrWhiteSpace(Description))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Test Details.\n Make Sure to fill all the fields!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(TestName))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Name for Test!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(Description))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Description for Test!");
						messageWindow.ShowDialog();
					}
					else
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Fee!");
						messageWindow.ShowDialog();
					}
				}
				else
				{
					context.Tests.Add(new Test { TestName = TestName, Description = Description, Fee = TestFee });
					context.SaveChanges();
					var messageWindow = new MessageWindow("Please click 'Refresh' to see the updated Tests list!");
					messageWindow.ShowDialog();
					Close?.Invoke();
				}
			}
		}

		public AddTestWindowVM()
		{
		}

	}
}
