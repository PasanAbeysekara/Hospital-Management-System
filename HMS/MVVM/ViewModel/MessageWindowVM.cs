using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HMS.MVVM.ViewModel
{
	public class MessageWindowVM
	{
		public string Message { get; }

		public MessageWindowVM(string message)
		{
			Message = message;
		}
		
	}
}
