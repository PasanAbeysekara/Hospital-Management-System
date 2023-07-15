using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.Model.InsidePrescription.insideDrug;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class AddPrescriptionWindowVM : ObservableObject, ICloseWindows
	{


		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		// Properties
		public int prescripId = new int();

		public int patId = new int();

		public bool isPrescriptionCreated = false;

		[ObservableProperty]
		public string caption;

		// Observable Properties
		[ObservableProperty]
		public DateTime prescribedDate;

		[ObservableProperty]
		public string drugType;

		[ObservableProperty]
		public List<string> drugTypes;

		[ObservableProperty]
		public string drugName;

		[ObservableProperty]
		public string comments;

		[ObservableProperty]
		public List<string> drugNames;

		[ObservableProperty]
		public string dose;

		[ObservableProperty]
		public string duration_;

		[ObservableProperty]
		public string testName;

		[ObservableProperty]
		public List<string> testNames;

		[ObservableProperty]
		public string testDescription;


		// Observable collections
		private ObservableCollection<Dosage> _dosages = new ObservableCollection<Dosage>();
		public ObservableCollection<Dosage> Dosages
		{
			get => _dosages;
			set
			{
				if (_dosages != value)
				{
					_dosages = value;
					OnPropertyChanged();
				}
			}
		}


		private ObservableCollection<MedicalTest> _medicalTests = new ObservableCollection<MedicalTest>();
		public ObservableCollection<MedicalTest> MedicalTests
		{
			get => _medicalTests;
			set
			{
				if (_medicalTests != value)
				{
					_medicalTests = value;
					OnPropertyChanged();
				}
			}
		}


		// Commands
		private DelegateCommand _createDrugCommand;
		public DelegateCommand CreateDrugCommand =>
			_createDrugCommand ?? (_createDrugCommand = new DelegateCommand(ExecuteCreateDrugCommand));

		void ExecuteCreateDrugCommand()
		{
			Random random = new Random();
			if (!isPrescriptionCreated)
			{
				isPrescriptionCreated = true;
				prescripId = random.Next(100, 10000);
				using (DataContext context = new DataContext())
				{
					var _pat = context.Patients.Single(x => x.IsPatientSelected == true);
					patId = _pat.Id;
					context.Prescriptions.Add(new Model.Prescription { Id = prescripId, PrescribedDate = PrescribedDate, PatientId = patId });
					context.SaveChanges();
				}
			}

			using (DataContext context = new DataContext())
			{
                double tmp;
                var drg = context.Drugs.Single(x => x.TradeName == drugName);

				//Exception handling

				if (String.IsNullOrWhiteSpace(DrugType) || String.IsNullOrWhiteSpace(Dose) || String.IsNullOrWhiteSpace(Duration_) || !Double.TryParse(Duration_, out tmp) || !Double.TryParse(Dose, out tmp))
				{
					if (String.IsNullOrWhiteSpace(DrugType) && String.IsNullOrWhiteSpace(Dose) && String.IsNullOrWhiteSpace(Duration_))
					{
						var msgWindow = new WarningMessageWindow("Please Enter Valid Prescription Details.\n Make Sure to fill all the fields!");
						msgWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(DrugType))
					{
						var msgWindow = new WarningMessageWindow("Please Enter Valid Drug Type!");
						msgWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(Dose) || !Double.TryParse(Dose, out tmp))
					{
						var msgWindow = new WarningMessageWindow("Please How many Dose at a time as a number \n \t\t(like 1,2,... ) ");
						msgWindow.ShowDialog();
					}
					else if (String.IsNullOrWhiteSpace(Duration_) || !Double.TryParse(Duration_, out tmp))
					{
						var msgWindow = new WarningMessageWindow("Please Enter Valid Duration in days (like 1,2,...) ");
						msgWindow.ShowDialog();
					}
					else
					{
						var messageWindow = new WarningMessageWindow("Please Enter Valid Prescription Details in all the fields!");
						messageWindow.ShowDialog();
					}
				}
				else
				{
					Dosage dsg = new Dosage { DrugId = drg.Id, DrugType = DrugType, Dose = Convert.ToDouble(Dose), Duration = Convert.ToDouble(Duration_), PrescriptionId = prescripId, Comments = Comments };
					context.Dosages.Add(dsg);

					var messageWindow = new MessageWindow($"Drug Id: \t{drg.Id}\nDrug Name: \t{DrugName}\nDose: \t{Dose}\nPresc ID: \t{prescripId}\nComments: \t{Comments}");
					messageWindow.ShowDialog();

					context.SaveChanges();



				}
                
                       
				
				Dosages.Clear();
				foreach (var _dsg in context.Dosages.Where(x => x.PrescriptionId == prescripId))
				{
					Dosages.Add(_dsg);
				}

			}
		}



		private DelegateCommand _createTestCommand;
		public DelegateCommand CreateTestCommand =>
			_createTestCommand ?? (_createTestCommand = new DelegateCommand(ExecuteCreateTestCommand));

		void ExecuteCreateTestCommand()
		{
			Random random = new Random();
			if (!isPrescriptionCreated)
			{
				isPrescriptionCreated = true;
				prescripId = random.Next(100, 10000);
				using (DataContext context = new DataContext())
				{
					var _pat = context.Patients.Single(x => x.IsPatientSelected == true);
					patId = _pat.Id;
					context.Prescriptions.Add(new Model.Prescription { Id = prescripId, PrescribedDate = PrescribedDate, PatientId = patId });
					context.SaveChanges();
				}
			}

			using (DataContext context = new DataContext())
			{

				var _tst = context.Tests.Single(x => x.TestName == TestName);
				MedicalTest mtst = new MedicalTest { TestId = _tst.Id, PrescriptionId = prescripId, Description = TestDescription };
				context.MedicalTests.Add(mtst);
				context.SaveChanges();

				MedicalTests.Clear();
				foreach (var _mtst in context.MedicalTests.Where(x => x.PrescriptionId == prescripId))
				{
					MedicalTests.Add(_mtst);
				}
			}


		}

		private DelegateCommand _doneCommand;
		public DelegateCommand DoneCommand =>
			_doneCommand ?? (_doneCommand = new DelegateCommand(ExecuteDoneCommand));

		void ExecuteDoneCommand()
		{
			using (DataContext context = new DataContext())
			{
				context.Patients.Single(x => x.IsPatientSelected == true).IsPatientSelected = false;
				context.SaveChanges();
			}

            var messageWindow = new MessageWindow("Please click 'Refresh' to see the updated Prescription list 😊");
            messageWindow.ShowDialog();
         	Close?.Invoke();

		}

		private DelegateCommand _cancelCommand;
		public DelegateCommand CancelCommand =>
			_cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

		void ExecuteCancelCommand()
		{
			if (isPrescriptionCreated)
			{
				using (DataContext context = new DataContext())
				{
					foreach (var _dsg in context.Dosages.Where(x => x.PrescriptionId == prescripId))
					{
						context.Dosages.Remove(_dsg);
						context.SaveChanges();
					}
					foreach (var _mtst in context.MedicalTests.Where(x => x.PrescriptionId == prescripId))
					{
						context.MedicalTests.Remove(_mtst);
						context.SaveChanges();
					}
					context.Prescriptions.Remove(context.Prescriptions.Single(x => x.Id == prescripId));
					context.SaveChanges();
				}
			}

			Close?.Invoke();

		}


		public AddPrescriptionWindowVM()
		{
			string dateString = "2023-04-14";
			DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			PrescribedDate = date;

			DrugTypes = new List<string> { "Liquid", "Tablet", "Capsules", "Injections", "Others" };
			DrugType = "Others";

			// DOSAGES
			using (DataContext context = new DataContext())
			{
				DrugNames = new List<string> { };
				foreach (var drg in context.Drugs)
				{
					DrugNames.Add(drg.TradeName);
				}
				DrugName = DrugNames[0];
			}

			// MEDICAL TESTS
			using (DataContext context = new DataContext())
			{
				TestNames = new List<string> { };
				foreach (var tst in context.Tests)
				{
					TestNames.Add(tst.TestName);
				}
				TestName = TestNames[0];
			}

		}

	}
}
