using NakedObjects.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Activities
	{
		[Table("Notes")]
		public class Note : Activity
		{
			#region Behavior
			#endregion

			#region Menu
			public static void Menu( IMenu menu )
				{
				menu.AddAction("AddOrChangeAttachment");
				menu.AddAction("RemoveAttachment");
				}
			#endregion
		}
	}
