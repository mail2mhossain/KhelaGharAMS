using NakedObjects;
using NakedObjects.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Areas
{
	[DisplayName("জেলা")]
	public class District : Area
	{
		#region Get Properties
		[MemberOrder(70), NotMapped]
		[DisplayName("উপজেলাসমূহ")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(true, "Name", "Description")]
		public IList<SubDistrict> SubDistricts
		{
			get
			{
				return Container.Instances<SubDistrict>().Where(w => w.Parent.AreaId == this.AreaId).ToList();
			}
		}

		[MemberOrder(100), NotMapped]
		[DisplayName("বিভাগ")]
		public Division Division
		{
			get
			{
				return Container.Instances<Division>().Where(w => w.AreaId == this.Parent.AreaId).FirstOrDefault();
			}
		}
		#endregion

		#region Behaviour
		[DisplayName("নতুন উপজেলা")]
		public SubDistrict AddSubDistrict (string উপজেলারনাম)
		{
			SubDistrict subdistrict = Container.NewTransientInstance<SubDistrict>();
			subdistrict.Name = উপজেলারনাম;
			subdistrict.Parent = this;

			Container.Persist(ref subdistrict);
			return subdistrict;
		}
		#endregion

		#region Menu
		public static void Menu (IMenu menu)
		{
			menu.AddAction("AddSubDistrict");
		}
		#endregion
	}
}
