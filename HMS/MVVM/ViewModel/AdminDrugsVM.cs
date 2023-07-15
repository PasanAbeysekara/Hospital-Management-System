using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class AdminDrugsVM : ObservableObject
	{
		private ObservableCollection<Drug> _drugsData = new ObservableCollection<Drug>();

		public ObservableCollection<Drug> DrugsData
		{
			get => _drugsData;
			set
			{
				if (_drugsData != value)
				{
					_drugsData = value;
					OnPropertyChanged();
				}
			}
		}


		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
			var messageWindow = new MessageWindow("Drugs list has been refreshed 🔃");
            messageWindow.ShowDialog();
            Read();
		}

		// Delete d rugcommand using prism core package
		private DelegateCommand<Drug> _deleteDrugCommand;
		public DelegateCommand<Drug> DeleteDrugCommand =>
			_deleteDrugCommand ?? (_deleteDrugCommand = new DelegateCommand<Drug>(ExecuteDeleteDrugCommand));

		void ExecuteDeleteDrugCommand(Drug parameter)
		{
			string deletedDrugName = "";
			using (DataContext context = new DataContext())
			{
				Drug selectedDrug = parameter;
				if (selectedDrug != null)
				{
					Drug drg = context.Drugs.Single(x => x.Id == selectedDrug.Id);
					deletedDrugName = drg.TradeName;
					context.Drugs.Remove(drg);
					context.SaveChanges();
				}
			}
            var messageWindow = new WarningMessageWindow($"Drug '{deletedDrugName}' deleted!");
            messageWindow.ShowDialog();
            Read();
		}


		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				
				_drugsData.Clear();
				foreach (var drg in context.Drugs)
				{
					_drugsData.Add(drg);
				}
			}

		}

		public AdminDrugsVM()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var drg_ in context.Drugs) drg_.IsDrugSelected = false;
				context.SaveChanges();
			}
			Read();
		}
	}
}
