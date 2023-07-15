using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	interface ICloseWindows
	{
		Action Close { get; set; }
	}
	public class AddPatientWindowVM : INotifyPropertyChanged, ICloseWindows
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

		private string _fullName;

		public string FullName { get { return _fullName; } set { _fullName = value; OnPropertyChanged(nameof(FullName)); } }

		private string _email;

		public string Email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(Email)); } }

		private DateTime _dateOfBirth;

		public DateTime DateOfBirth { get { return _dateOfBirth; } set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); } }

		public List<string> _genders;
		public List<string> Genders { get { return _genders; } set { _genders = value; OnPropertyChanged(nameof(Genders)); } }

		private string _gender;

		public string Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }


		private string _phone;

		public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(nameof(Phone)); } }

		private string _blood;

		public string Blood { get { return _blood; } set { _blood = value; OnPropertyChanged(nameof(Blood)); } }

		private string _address;

		public string Address { get { return _address; } set { _address = value; OnPropertyChanged(nameof(Address)); } }

		private string _weight;

		public string Weight { get { return _weight; } set { _weight = value; OnPropertyChanged(nameof(Weight)); } }

		private string _height;

		public string Height { get { return _height; } set { _height = value; OnPropertyChanged(nameof(Height)); } }


		// Close Button Command
		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			Close?.Invoke();
		}

		// Create Button Command
		private DelegateCommand _createCommand;
		public DelegateCommand CreateCommand =>
			_createCommand ?? (_createCommand = new DelegateCommand(ExecuteCreateCommand));


		void ExecuteCreateCommand()
        {
            double tmp;
            using (DataContext context = new DataContext())
			{
				//Exception handling

				if (String.IsNullOrWhiteSpace(_fullName) || String.IsNullOrWhiteSpace(_email) || String.IsNullOrWhiteSpace(_gender) || String.IsNullOrWhiteSpace(_phone) || String.IsNullOrWhiteSpace(_blood) || String.IsNullOrWhiteSpace(_address) || String.IsNullOrWhiteSpace(_weight) || String.IsNullOrWhiteSpace(_height)||!Regex.Match(_phone, @"^\d{10}$").Success || !Double.TryParse(_weight, out tmp) || !Double.TryParse(_height, out tmp))
				{
				
					if (String.IsNullOrWhiteSpace(_fullName) && String.IsNullOrWhiteSpace(_email) && String.IsNullOrWhiteSpace(_gender) && String.IsNullOrWhiteSpace(_phone) && String.IsNullOrWhiteSpace(_blood) && String.IsNullOrWhiteSpace(_address) && String.IsNullOrWhiteSpace(_weight) && String.IsNullOrWhiteSpace(_height))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Patients Details.\n Make Sure to fill all the fields!");
						messageWindow.ShowDialog();

					}
					else if (String.IsNullOrWhiteSpace(_fullName))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Full Name!");
						messageWindow.ShowDialog();

					}
					else if (String.IsNullOrWhiteSpace(_email))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Email!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(_gender))
					{
						var messageWindow = new WarningMessageWindow("Please Select a Gender!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(_phone) || !Regex.Match(_phone, @"^\d{10}$").Success)
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Phone Number.\nIt Should consist of 10 numbers!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(_blood))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Blood!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(_address))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Address!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(_weight) || !Double.TryParse(_weight, out tmp))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Weight!");
						messageWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(_height) || !Double.TryParse(_height, out tmp))
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Height!");
						messageWindow.ShowDialog();
					}
					else
					{
                        var messageWindow = new WarningMessageWindow("Please Enter Valid Patients Details in all the fields!");
                        messageWindow.ShowDialog();
                    }
				}
				else
				{
					context.Patients.Add(new Model.Patient { FullName = _fullName, Email = _email, BirthDay = _dateOfBirth.ToShortDateString(), Gender = _gender[0], Phone = _phone, BloodGroup = _blood, Address = _address, Weight = Double.Parse(_weight), Height = Double.Parse(_height) });
					context.SaveChanges();
					var messageWindow = new MessageWindow("Refresh the student records to see the changes!");
					messageWindow.ShowDialog();

					Close?.Invoke();
				}
			}
		}

		public AddPatientWindowVM()
		{
			Genders = new List<string> { "Male", "Female" };
			string dateString = "2000-01-01";
			DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			_dateOfBirth = date;
		}

	}
}
