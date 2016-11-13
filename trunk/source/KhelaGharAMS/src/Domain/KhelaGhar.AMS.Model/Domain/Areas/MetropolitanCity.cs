using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Areas
{
	[DisplayName("মহানগর")]
	public class MetropolitanCity : Area
	{
		#region Get Properties
		[MemberOrder(80), NotMapped]
		[DisplayName("বিভাগ")]
		public Division Division
		{
			get
			{
				return Container.Instances<Division>().Where(w => w.AreaId == this.Parent.AreaId).FirstOrDefault();
			}
		}
		#endregion
	}
}
