using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NakedObjects;
using NakedObjects.Services;
using NakedObjects.Security;
using KhelaGhar.AMS.Model.Domain;
using NakedObjects.Menu;
using KhelaGhar.AMS.Model.Domain.Asars;
using static KhelaGhar.AMS.Model.Domain.AllEnums;
using static KhelaGhar.AMS.Model.Domain.Asars.ShakhaAsar;
using KhelaGhar.AMS.Model.Domain.Areas;
using KhelaGhar.AMS.Model.Domain.MasterData;
using static KhelaGhar.AMS.Model.Domain.Committees.Committee;

namespace KhelaGhar.AMS.Model.Repository
{
	[DisplayName("আসর")]
	public class AsarRepository : AbstractFactoryAndRepository
	{
		#region Injected Services
		// This region should contain properties to hold references to any services required by the
		// object.  Use the 'injs' shortcut to add a new service.

		#endregion

		// 'fact' shortcut to add a factory method, 
		// 'alli' for an all-instances method
		// 'find' for a method to find a single object by query
		// 'list' for a method to return a list of objects matching a query

		public static void Menu (IMenu menu)
		{
			//IMenu centralAsar = menu.CreateSubMenu("কেন্দ্রীয় খেলাঘর আসর");
			menu.AddAction("ShowCentralAsar");
			menu.AddAction("JelaAsar");
			menu.AddAction("MohanagarAsar");
			menu.AddAction("UpojelaAsar");

			IMenu shakhaAsar = menu.CreateSubMenu("শাখা আসর");
			shakhaAsar.AddAction("CreateAsar");
			shakhaAsar.CreateSubMenu("খোঁজ")
					.AddAction("ByAsarName")
					.AddAction("ByStatus")
					.AddAction("ByCommitteeType")
					.AddAction("ShowAll");

			//menu.AddAction("CustomerDashboard");
		}

		#region Central KhelaGhar Asar
		[DisplayName("কেন্দ্রীয় খেলাঘর আসর")]
		public KendrioAsar ShowCentralAsar()
		{
			Division dhaka = Container.Instances<Division>().Where(w => w.AreaId == 1).FirstOrDefault();
			KendrioAsar kendrioAsar = Container.Instances<KendrioAsar>().FirstOrDefault();

			if(kendrioAsar == null)
			{
				if (dhaka != null)
				{
					kendrioAsar = Container.NewTransientInstance<KendrioAsar>();
					kendrioAsar.Name = "কেন্দ্রীয় খেলাঘর আসর";
					kendrioAsar.CommitteeType = TypeOfCommittee.পূর্ণাঙ্গ;
					kendrioAsar.AddressLine = "২/২২ বাবর রোড, মোহাম্মদপুর, ঢাকা";
					kendrioAsar.Area = dhaka;

					Container.Persist(ref kendrioAsar);
				}
			}
			return kendrioAsar;
		}
		#endregion

		#region Jela Asar
		[DisplayName("জেলা আসর")]
		public JelaAsar JelaAsar(District district)
		{
			JelaAsar jelaAsar = Container.Instances<JelaAsar>().Where(w => w.Area.AreaId == district.AreaId).FirstOrDefault();

			if(jelaAsar == null)
			{
				jelaAsar = Container.NewTransientInstance<JelaAsar>();
				jelaAsar.Name = district.Name + " খেলাঘর আসর";
				jelaAsar.CommitteeType = TypeOfCommittee.কমিটিবিহীন;
				jelaAsar.Area = district;

				Container.Persist(ref jelaAsar);
			}
			return jelaAsar;
		}

		[PageSize(10)]
		public IQueryable<District> AutoComplete0JelaAsar ([MinLength(1)] string name)
		{
			IQueryable<District> areas = Container.Instances<District>().Where(w => w.Name.StartsWith(name));

			return areas;
		}
		#endregion

		#region Mohanagar Asar
		[DisplayName("মহানগর আসর")]
		public MohanagarAsar MohanagarAsar (MetropolitanCity city)
		{
			MohanagarAsar mohanagarAsar = Container.Instances<MohanagarAsar>().Where(w => w.Area.AreaId == city.AreaId).FirstOrDefault();

			if (mohanagarAsar == null)
			{
				mohanagarAsar = Container.NewTransientInstance<MohanagarAsar>();
				mohanagarAsar.Name = city.Name + " খেলাঘর আসর";
				mohanagarAsar.CommitteeType = TypeOfCommittee.কমিটিবিহীন;
				mohanagarAsar.Area = city;

				Container.Persist(ref mohanagarAsar);
			}
			return mohanagarAsar;
		}

		[PageSize(10)]
		public IQueryable<MetropolitanCity> AutoComplete0MohanagarAsar ([MinLength(1)] string name)
		{
			IQueryable<MetropolitanCity> areas = Container.Instances<MetropolitanCity>().Where(w => w.Name.StartsWith(name));

			return areas;
		}
		#endregion

		#region Upojela Asar
		[DisplayName("উপজেলা আসর")]
		public UpojelaAsar UpojelaAsar (SubDistrict subdistrict)
		{
			UpojelaAsar upojelaAsar = Container.Instances<UpojelaAsar>().Where(w => w.Area.AreaId == subdistrict.AreaId).FirstOrDefault();

			if (upojelaAsar == null)
			{
				upojelaAsar = Container.NewTransientInstance<UpojelaAsar>();
				upojelaAsar.Name = subdistrict.Name + " খেলাঘর আসর";
				upojelaAsar.CommitteeType = TypeOfCommittee.কমিটিবিহীন;
				upojelaAsar.Area = subdistrict;

				Container.Persist(ref upojelaAsar);
			}
			return upojelaAsar;
		}

		[PageSize(10)]
		public IQueryable<SubDistrict> AutoComplete0UpojelaAsar ([MinLength(1)] string name)
		{
			IQueryable<SubDistrict> areas = Container.Instances<SubDistrict>().Where(w => w.Name.StartsWith(name));

			return areas;
		}
		#endregion

		#region Shakha Asar
		[MemberOrder(Sequence = "20")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("নতুন শাখা আসর")]
		public ShakhaAsar CreateAsar ([MaxLength(250)]string নাম, [Optionally]DateTime? প্রতিষ্ঠার_তারিখ, StatusOfAsar আসরের_অবস্থা, TypeOfCommittee কমিটির_ধরণ, [MultiLine(NumberOfLines = 3, Width = 50), Optionally]string ঠিকানা, Area উপজেলা_মহানগর)
		{
			ShakhaAsar asar = Container.NewTransientInstance<ShakhaAsar>();
			asar.Name = নাম;
			asar.DateOfEstablishment = প্রতিষ্ঠার_তারিখ;
			asar.AsarStatus = আসরের_অবস্থা;
			asar.CommitteeType = কমিটির_ধরণ;
			asar.AddressLine = ঠিকানা;
			asar.Area = উপজেলা_মহানগর;

			Container.Persist(ref asar);
			return asar;
		}

		[PageSize(10)]
		public IQueryable<Area> AutoComplete5CreateAsar ([MinLength(1)] string name)
		{
			List<int> areaIds = (from area in Container.Instances<SubDistrict>()
													 select area.AreaId).ToList();
			IList<int> metroIds = ((from area in Container.Instances<MetropolitanCity>()
															select area.AreaId).ToList());
			areaIds.AddRange(metroIds);
			IQueryable<Area> areas = Container.Instances<Area>().Where(w => w.Name.StartsWith(name) && areaIds.Contains(w.AreaId));

			return areas;
		}
		public string Validate0CreateAsar (string নাম)
		{
			Asar asar = Container.Instances<Asar>().Where(w => w.Name == নাম).FirstOrDefault();

			if (asar != null)
			{
				if (asar.Area != null)
				{
					return asar.Area + " তে এই নামে শাখা আসর আছে।";
				}
				else
				{
					return "এই নামে শাখা আসর আছে।";
				}
			}
			return null;
		}

		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[DisplayName("সকল আসর")]
		[TableView(true, "DateOfEstablishment", "LastConferenceDate", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
		public IQueryable<ShakhaAsar> ShowAll ()
		{
			return Container.Instances<ShakhaAsar>().OrderBy(o => o.Name).AsQueryable();
		}

		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[DisplayName("আসরের অবস্থা অনুযায়ী")]
		[TableView(true, "DateOfEstablishment", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
		public IQueryable<ShakhaAsar> ByStatus ([EnumDataType(typeof(ShakhaAsar.StatusOfAsar))] int status)
		{
			return Container.Instances<ShakhaAsar>().Where(w => (int)w.AsarStatus == status).OrderBy(o => o.Name).AsQueryable();
		}

		[DisplayName("নাম দিয়ে খোঁজ")]
		[TableView(true, "DateOfEstablishment", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
		public ShakhaAsar ByAsarName (ShakhaAsar আসর)
		{
			return আসর;
			//return Container.Instances<Asar>().Where(w => w.Name.StartsWith(আসর.Name));
		}

		[PageSize(10)]
		public IQueryable<ShakhaAsar> AutoComplete0ByAsarName ([MinLength(1)] string name)
		{
			return Container.Instances<ShakhaAsar>().Where(w => w.Name.StartsWith(name));
		}

		#region ByCommitteeType (Action)

		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[DisplayName("কমিটির ধরণ অনুযায়ী")]
		[TableView(true, "DateOfEstablishment", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
		public IList<Asar> ByCommitteeType (string committeeType)
		{
			IList<Asar> asars = new List<Asar>();



			return asars;
		}

		public IList<string> Choices0ByCommitteeType ()
		{
			return AllEnums.GetCommitteeType();
		}

		// Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
		#endregion
		#endregion

		public IList<string> GetWeeklyActivities ()
		{
			IList<string> activities = Container.Instances<MasterActivity>().OrderBy(o => o.ActivityName).Select(s => s.ActivityName).ToList();
			return activities;
		}
		public List<int> GetChildrenIds (Area area)
		{
			List<int> ids = new List<int>();
			IList<int> upojelaIds = new List<int>();

			if(area is Division)
			{
				IList<int> jelaIds = GetAllJelaIds(area);
				IList<int> mohanagarIds = GetAllMohanagarIds(area);
				ids = ids.Union(mohanagarIds).ToList();
				upojelaIds = GetAllUpojelaIds(jelaIds);
				ids = ids.Union(upojelaIds).ToList();
				return ids;
			}
			if(area is District)
			{
				IList<int> jelaIds = new List<int>();
				jelaIds.Add(area.AreaId);
				upojelaIds = GetAllUpojelaIds(jelaIds);
				ids = ids.Union(upojelaIds).ToList();
				return ids;
			}
			if(area is SubDistrict)
			{
				ids.Add(area.AreaId);
				return ids;
			}
			if(area is MetropolitanCity)
			{
				ids.Add(area.AreaId);
				return ids;
			}
			//ids.Add(area.AreaId);
			//IList<Area> areas = GetChildren(area);

			//foreach (Area a in areas)
			//{
			//	ids = ids.Union(GetChildrenIds(a)).ToList();
			//}

			return ids;
		}
		private IList<Area> GetChildren (Area area)
		{
			IList<Area> areas = (from a in Container.Instances<Area>()
													 where a.Parent.AreaId == area.AreaId
													 select a).ToList();

			return areas;
		}
		private IList<int> GetAllJelaIds (Area area)
		{
			IList<int> areas = (from a in Container.Instances<District>()
													 where a.Parent.AreaId == area.AreaId
													 select a.AreaId).ToList();

			return areas;
		}
		private IList<int> GetAllMohanagarIds (Area area)
		{
			IList<int> areas = (from a in Container.Instances<MetropolitanCity>()
													where a.Parent.AreaId == area.AreaId
													select a.AreaId).ToList();

			return areas;
		}
		private IList<int> GetAllUpojelaIds(IList<int> jelaIds)
		{
			IList<int> upojelaIds = (from u in Container.Instances<SubDistrict>()
															 where jelaIds.Contains(u.Parent.AreaId)
															 select u.AreaId).ToList();

			return upojelaIds;
		}
		public List<int> GetAllChildrenIds ()
		{
			List<int> ids = new List<int>();
			IList<int> upojelaIds = new List<int>();
			IList<Division> divisions = Container.Instances<Division>().ToList();


			foreach (Division area in divisions)
			{
				IList<int> jelaIds = GetAllJelaIds(area);
				IList<int> mohanagarIds = GetAllMohanagarIds(area);
				ids = ids.Union(mohanagarIds).ToList();
				upojelaIds = GetAllUpojelaIds(jelaIds);
				ids = ids.Union(upojelaIds).ToList();
			}
			
			return ids;
		}
	}
}
