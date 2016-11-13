using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Workers;
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
	[DisplayName("বিভাগ")]
	public class Division : Area
	{
		#region Get Properties
		#region District
		[MemberOrder(80), NotMapped]
		[DisplayName("জেলাসমূহ")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(true, "Name", "SubDistricts", "Description")]
		public IList<District> Districts
		{
			get
			{
				return Container.Instances<District>().Where(w => w.Parent.AreaId == this.AreaId).ToList();
			}
		}
		#endregion

		#region MetropolitanCity
		[MemberOrder(70), NotMapped]
		[DisplayName("মহানগরসমূহ")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(true, "Name", "Description")]
		public IList<MetropolitanCity> MetropolitanCity
		{
			get
			{
				return Container.Instances<MetropolitanCity>().Where(w => w.Parent.AreaId == this.AreaId).ToList();
			}
		}
		#endregion

		#endregion

		#region Behaviour
		[DisplayName("নতুন জেলা")]
		public District AddDistrict (string জেলারনাম)
		{
			District district = Container.NewTransientInstance<District>();
			district.Name = জেলারনাম;
			district.Parent = this;

			Container.Persist(ref district);
			return district;
		}

		[DisplayName("নতুন মহানগর")]
		public MetropolitanCity AddMetropolitanCity (string মহানগরেরনাম)
		{
			MetropolitanCity city = Container.NewTransientInstance<MetropolitanCity>();
			city.Name = মহানগরেরনাম;
			city.Parent = this;

			Container.Persist(ref city);
			return city;
		}
		#endregion

		#region Menu
		public static void Menu (IMenu menu)
		{
			menu.AddAction("AddDistrict");
			menu.AddAction("AddMetropolitanCity");
		}
		#endregion
	}
}
