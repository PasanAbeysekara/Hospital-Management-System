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
	public partial class AddDrugWindowVM : ObservableObject, ICloseWindows
	{

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		[ObservableProperty]
		public string tradeName;

		[ObservableProperty]
		public string genericName;

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
				if (String.IsNullOrWhiteSpace(TradeName) || String.IsNullOrWhiteSpace(GenericName))
				{
					if (String.IsNullOrWhiteSpace(TradeName) && String.IsNullOrWhiteSpace(GenericName))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Drug Details.\n Make Sure to fill all the fields!");
						messageWindow.ShowDialog();

					}
					else if (String.IsNullOrWhiteSpace(TradeName))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Trade Name!");
						messageWindow.ShowDialog();

					}
					else
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid GenericName!");
						messageWindow.ShowDialog();

					}
				}
				else
				{
					context.Drugs.Add(new Drug { TradeName = TradeName, GenericName = GenericName });
					context.SaveChanges();
					var messageWindow = new MessageWindow("Please click 'Refresh' to see the updated Drug list");
					messageWindow.ShowDialog();
					Close?.Invoke();
				}
			}
		}

		public AddDrugWindowVM()
		{
		}

	}
}
