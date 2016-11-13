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
	[DisplayName("উপজেলা")]
	public class SubDistrict : Area
	{
		#region Get Properties
		[MemberOrder(80), NotMapped]
		[DisplayName("জেলা")]
		public District District
		{
			get
			{
				return Container.Instances<District>().Where(w => w.AreaId == this.Parent.AreaId).FirstOrDefault();
			}
		}
		#endregion
	}
}
