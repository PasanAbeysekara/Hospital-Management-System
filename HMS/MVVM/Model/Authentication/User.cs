using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.Model.Authentication
{
	public class User
	{
		
		public User(string userName, string password, bool isSuperUser) // User constructor with a random id
		{
			Random r = new Random();
			this.UserId = r.Next(0, 10000);
			this.UserName = userName;
			this.Password = password;
			IsSuperUser = isSuperUser;
			IsSelectedUser = false;
		}

		[Key]
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool IsSuperUser { get; set; }
		public bool IsSelectedUser { get; set; }
	}
}
