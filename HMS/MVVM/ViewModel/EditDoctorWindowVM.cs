using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
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
	public class EditDoctorWindowVM : INotifyPropertyChanged, ICloseWindows
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


		// Close Button Command
		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			using (DataContext context = new DataContext())
			{
				var doc_ = context.Doctors.Single(x => x.IsDoctorSelected == true);
				doc_.IsDoctorSelected = false;
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
				Doctor tmp = context.Doctors.Single(x => x.IsDoctorSelected == true);
				if (tmp != null)
				{
					tmp.Name = _name ;
					tmp.Fee = Convert.ToDouble(_fee); 
					context.SaveChanges();
				}
				else
				{
					MessageBox.Show("Please select a patient again");
                    var _messageWindow = new MessageWindow("Please select a patient again");
                    _messageWindow.ShowDialog();
                }
			}

            var messageWindow = new MessageWindow("Refresh the Doctor records to see the changes!");
            messageWindow.ShowDialog();
			Close?.Invoke();
		}

		public EditDoctorWindowVM()
		{
			using (DataContext context = new DataContext())
			{
				Doctor doc_ = context.Doctors.Single(x => x.IsDoctorSelected == true);
				_name = doc_.Name;
				_fee = doc_.Fee.ToString() ;
			}
		}

	}
}
