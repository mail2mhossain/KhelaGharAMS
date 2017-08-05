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
            IMenu jelaAsar = menu.CreateSubMenu("জেলা");
            jelaAsar.AddAction("NewJelaAsar");
            jelaAsar.AddAction("SearchJelaAsar");
            jelaAsar.AddAction("ShowAllJelaAsar");

            IMenu mohanaagarAsar = menu.CreateSubMenu("মহানগর");
            mohanaagarAsar.AddAction("NewMohanagarAsar");
            mohanaagarAsar.AddAction("SearchMohanagarAsar");
            mohanaagarAsar.AddAction("ShowAllMohanagarAsar");

            IMenu upojela = menu.CreateSubMenu("উপজেলা");
            upojela.AddAction("NewUpojelaAsar");
			upojela.AddAction("SearchUpojelaAsar");
            upojela.AddAction("ShowAllUpojelaAsar");

			IMenu shakhaAsar = menu.CreateSubMenu("শাখা আসর");
			shakhaAsar.AddAction("CreateAsar");
			shakhaAsar.CreateSubMenu("খোঁজ")
					.AddAction("ByAsarName")
					.AddAction("ByStatus")
					.AddAction("ByCommitteeType")
					.AddAction("ShowAllAsar");

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
        #region New Jela Asar
        [DisplayName("নতুন জেলা কমিটি")]
		public JelaAsar NewJelaAsar(District জেলা)
		{
			JelaAsar jelaAsar = Container.Instances<JelaAsar>().Where(w => w.Area.AreaId == জেলা.AreaId).FirstOrDefault();

			if(jelaAsar == null)
			{
				jelaAsar = Container.NewTransientInstance<JelaAsar>();
				jelaAsar.Name = জেলা.Name + " খেলাঘর আসর";
				jelaAsar.CommitteeType = TypeOfCommittee.কমিটিবিহীন;
				jelaAsar.Area = জেলা;

				Container.Persist(ref jelaAsar);
			}
			return jelaAsar;
		}

		[PageSize(10)]
		public IQueryable<District> AutoComplete0NewJelaAsar ([MinLength(1)] string name)
		{
			IQueryable<District> areas = Container.Instances<District>().Where(w => w.Name.StartsWith(name));

			return areas;
		}
        #endregion

        #region Search Jela Asar
        [DisplayName("জেলা কমিটি খোঁজ")]
        public JelaAsar SearchJelaAsar (District জেলা)
        {
            JelaAsar jelaAsar = Container.Instances<JelaAsar>().Where(w => w.Area.AreaId == জেলা.AreaId).FirstOrDefault();
            return jelaAsar;
        }

        [PageSize(10)]
        public IQueryable<District> AutoComplete0SearchJelaAsar ([MinLength(1)] string name)
        {
            IQueryable<District> areas = Container.Instances<District>().Where(w => w.Name.StartsWith(name));

            return areas;
        }
        #endregion

        #region Show All Jela Asar
        [DisplayName("সকল জেলা কমিটি")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [TableView(true, "DateOfEstablishment", "LastConferenceDate", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
        public IList<JelaAsar> ShowAllJelaAsar()
        {
            return Container.Instances<JelaAsar>().ToList();
        }
        #endregion

        #endregion

        #region Mohanagar Asar
        #region New Mohanagar Asar
        [DisplayName("নতুন মহানগর কমিটি")]
		public MohanagarAsar NewMohanagarAsar (MetropolitanCity city)
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
		public IQueryable<MetropolitanCity> AutoComplete0NewMohanagarAsar ([MinLength(1)] string name)
		{
			IQueryable<MetropolitanCity> areas = Container.Instances<MetropolitanCity>().Where(w => w.Name.StartsWith(name));

			return areas;
		}
        #endregion

        #region Search Mohanagar Asar
        [DisplayName("মহানগর কমিটি খোঁজ")]
        public MohanagarAsar SearchMohanagarAsar (MetropolitanCity city)
        {
            MohanagarAsar mohanagarAsar = Container.Instances<MohanagarAsar>().Where(w => w.Area.AreaId == city.AreaId).FirstOrDefault();
            
            return mohanagarAsar;
        }

        [PageSize(10)]
        public IQueryable<MetropolitanCity> AutoComplete0SearchMohanagarAsar ([MinLength(1)] string name)
        {
            IQueryable<MetropolitanCity> areas = Container.Instances<MetropolitanCity>().Where(w => w.Name.StartsWith(name));

            return areas;
        }
        #endregion

        #region Show All Mohanagar Asar
        [DisplayName("সকল মহানগর কমিটি")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [TableView(true, "DateOfEstablishment", "LastConferenceDate", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
        public IList<MohanagarAsar> ShowAllMohanagarAsar (MetropolitanCity city)
        {
            return Container.Instances<MohanagarAsar>().ToList();
        }
        #endregion
        #endregion

        #region Upojela Asar

        #region New Upojela Asar
        [DisplayName("নতুন উপজেলা কমিটি")]
        public UpojelaAsar NewUpojelaAsar (SubDistrict উপজেলা)
        {
            UpojelaAsar upojelaAsar = Container.Instances<UpojelaAsar>().Where(w => w.Area.AreaId == উপজেলা.AreaId).FirstOrDefault();

            if (upojelaAsar == null)
            {
                upojelaAsar = Container.NewTransientInstance<UpojelaAsar>();
                upojelaAsar.Name = উপজেলা.Name + " খেলাঘর আসর";
                upojelaAsar.CommitteeType = TypeOfCommittee.কমিটিবিহীন;
                upojelaAsar.Area = উপজেলা;

                Container.Persist(ref upojelaAsar);
            }
            return upojelaAsar;
        }
        [PageSize(10)]
        public IQueryable<SubDistrict> AutoComplete0NewUpojelaAsar ([MinLength(1)] string name)
        {
            IQueryable<SubDistrict> areas = Container.Instances<SubDistrict>().Where(w => w.Name.StartsWith(name));

            return areas;
        }
        #endregion

        #region Search Upojela Asar
        [DisplayName("উপজেলা কমিটি খোঁজা")]
		public UpojelaAsar SearchUpojelaAsar (SubDistrict উপজেলা)
		{
			UpojelaAsar upojelaAsar = Container.Instances<UpojelaAsar>().Where(w => w.Area.AreaId == উপজেলা.AreaId).FirstOrDefault();

			return upojelaAsar;
		}

		[PageSize(10)]
		public IQueryable<SubDistrict> AutoComplete0SearchUpojelaAsar ([MinLength(1)] string name)
		{
			IQueryable<SubDistrict> areas = Container.Instances<SubDistrict>().Where(w => w.Name.StartsWith(name));

			return areas;
		}
        #endregion

        #region Show All Upojela Asar
        [DisplayName("সকল উপজেলা কমিটি")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [TableView(true, "DateOfEstablishment", "LastConferenceDate", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
        public IList<UpojelaAsar> ShowAllUpojelaAsar()
        {
            return Container.Instances<UpojelaAsar>().ToList();
        }

        #endregion

        #endregion

        #region Shakha Asar

        #region New Shakha Asar
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
        #endregion

        #region Show All Shakha Asar
        [Eagerly(EagerlyAttribute.Do.Rendering)]
		[DisplayName("সকল আসর")]
		[TableView(true, "DateOfEstablishment", "LastConferenceDate", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
		public IQueryable<ShakhaAsar> ShowAllAsar ()
		{
			return Container.Instances<ShakhaAsar>().OrderBy(o => o.Name).AsQueryable();
		}
        #endregion

        #region Search By Status
        [Eagerly(EagerlyAttribute.Do.Rendering)]
		[DisplayName("আসরের অবস্থা অনুযায়ী")]
		[TableView(true, "DateOfEstablishment", "LastConferenceDate", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
		public IQueryable<ShakhaAsar> ByStatus ([EnumDataType(typeof(ShakhaAsar.StatusOfAsar))] int status)
		{
			return Container.Instances<ShakhaAsar>().Where(w => (int)w.AsarStatus == status).OrderBy(o => o.Name).AsQueryable();
		}
        #endregion

        #region Search By Name
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
        #endregion

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
