using HMS.MVVM.Model;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public class EditTestWindowVM : INotifyPropertyChanged, ICloseWindows
	{
		// #begin INotifyPropertyChanged Interface 
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		// #end

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		private string _name;

		public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }

		private string _fee;

		public string Fee { get { return _fee; } set { _fee = value; OnPropertyChanged(nameof(Fee)); } }


		private string _description;

		public string Description { get { return _description; } set { _description = value; OnPropertyChanged(nameof(Description)); } }


		// Close Button Command
		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			using (DataContext context = new DataContext())
			{
				var tst_ = context.Tests.Single(x => x.IsTestSelected == true);
				tst_.IsTestSelected = false;
				context.SaveChanges();
			}

			Close?.Invoke();
		}

		// Create Button Command
		private DelegateCommand _createCommand;
		public DelegateCommand CreateCommand =>
			_createCommand ?? (_createCommand = new DelegateCommand(ExecuteCreateCommand));


		void ExecuteCreateCommand()
		{
			using (DataContext context = new DataContext())
			{
				Test tmp = context.Tests.Single(x => x.IsTestSelected == true);
				if (tmp != null)
				{
					tmp.TestName = _name;
					tmp.Fee = Convert.ToDouble(_fee);
					tmp.Description = _description;
					context.SaveChanges();
				}
				else
				{
                    var messageWindow2 = new WarningMessageWindow("Please select a test again");
                    messageWindow2.ShowDialog();
                }
			}
			var messageWindow = new MessageWindow("Refresh the Test records to see the changes 😊");
            messageWindow.ShowDialog();
            Close?.Invoke();
		}

		public EditTestWindowVM()
		{
			using (DataContext context = new DataContext())
			{
				Test tst_ = context.Tests.Single(x => x.IsTestSelected == true);
				_name = tst_.TestName;
				_fee = tst_.Fee.ToString();
				_description = tst_.Description;
			}
		}

	}
}
