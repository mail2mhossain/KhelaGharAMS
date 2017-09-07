using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NakedObjects;
using NakedObjects.Services;
using NakedObjects.Security;
//using WebMatrix.WebData;
using System.Web.Security;
using KhelaGhar.AMS.Model.Domain;
using NakedObjects.Menu;
using KhelaGhar.AMS.Model.Domain.Areas;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Domain.Activities;
using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Domain.MasterData;
using static KhelaGhar.AMS.Model.Domain.MasterData.Designation;

namespace KhelaGhar.AMS.Model.Repository
{
	//[AuthorizeAction(Roles = "AMSAdmin")]
	[DisplayName("সেটআপ")]
	public class SetupRepository : AbstractFactoryAndRepository
	{
		#region Injected Services
		// This region should contain properties to hold references to any services required by the
		// object.  Use the 'injs' shortcut to add a new service.
		public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }
		#endregion

		public static void Menu (IMenu menu)
		{
			//menu.CreateSubMenu("পদবী")
			//		.AddAction("CreateDesignation")
			//		.AddAction("AllDesignation");
			menu.CreateSubMenu("মেট্রোপলিটন শহর")
					.AddAction("AddMetropolitanCity")
					.AddAction("AllCities");
			menu.CreateSubMenu("উপজেলা")
					.AddAction("AddSubDistrict")
					.AddAction("BySubDistrictName")
					.AddAction("AllSubDistricts");
			menu.CreateSubMenu("জেলা")
					.AddAction("AddDistrict")
					.AddAction("ByDistrictName")
					.AddAction("AllDistrict");
			menu.CreateSubMenu("বিভাগ")
					.AddAction("AllDivisions");
			//menu.AddAction("CustomerDashboard");
		}

		#region Designation
		[DisplayName("নতুন পদবী")]
		public Designation CreateDesignation (string পদবী, TypeOfDesignation পদবীরধরণ, int ক্রম)
		{
			Designation designation = Container.NewTransientInstance<Designation>();
			designation.Name = পদবী;
			designation.DesignationType = পদবীরধরণ;
			designation.DesignationOrder = ক্রম;
			Container.Persist(ref designation);
			return designation;
		}
		public bool HideCreateDesignation ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.SetupFeatureEnum.CreateDesignation
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Setup.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}

		//[MemberOrder(Sequence = "430", Name = "পদবী ")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("সকল পদবী")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		public IQueryable<Designation> AllDesignation (TypeOfDesignation পদবীরধরণ)
		{
			return Container.Instances<Designation>().Where(w => w.DesignationType == পদবীরধরণ).OrderBy(O => O.DesignationOrder);
		}

		public bool HideAllDesignation ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.SetupFeatureEnum.AllDesignation
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Setup.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}
		#endregion

		#region Division
		//[MemberOrder(Sequence = "120", Name = "বিভাগ")]
		////[AuthorizeAction(Roles = "AMSAdmin")]
		//[DisplayName("নতুন বিভাগ")]
		//public Area CreateDivision([MaxLength(150), Required] string divisionName)
		//{
		//  AreaType type = (from t in Container.Instances<AreaType>()
		//                   where t.Parent == null
		//                   select t).First();

		//  Area area = Container.NewTransientInstance<Area>();

		//  area.Name = divisionName;
		//  area.AreaType = type;

		//  Container.Persist(ref area);

		//  return area;
		//}

		//[MemberOrder(Sequence = "130", Name = "বিভাগ")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("সকল বিভাগ")]
		[TableView(true, "Name", "Districts", "MetropolitanCity","AllActiveAsars", "AllNewAsars", "AllRevivedAsars", "AllInactiveAsars", "Members", "Workers")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		public IList<Division> AllDivisions ()
		{
			IList<Division> divisions = (from d in Container.Instances<Division>()
																	 select d).ToList();
			return divisions;
		}
		public bool HideAllDivisions ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.ShowAllDivisions 
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}
		#endregion

		#region District
		//[MemberOrder(Sequence = "220", Name = "জেলা")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("নতুন জেলা")]
		public District AddDistrict ([MaxLength(150), Required] string districtName, [Required] Division division)
		{
			District district = Container.NewTransientInstance<District>();
			district.Name = districtName;
			district.Parent = division;
			Container.Persist(ref district);
			return district;
		}
		
		public string ValidateAddDistrict (string districtName, Division division)
		{
			District district = Container.Instances<District>().Where(w => w.Name == districtName && w.Parent.AreaId == division.AreaId).FirstOrDefault();
			if (district != null)
			{
				return "Duplicate District Name";
			}

			return null;
		}

		public IList<Division> Choices1AddDistrict ()
		{
			return Container.Instances<Division>().ToList();
		}
		public bool HideAddDistrict ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.AddDistrict
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}

		//[MemberOrder(Sequence = "230", Name = "জেলা")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("সকল জেলা")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(true, "Name", "SubDistricts", "AllActiveAsars", "AllNewAsars", "AllRevivedAsars", "AllInactiveAsars", "Members", "Workers")]
		public IList<District> AllDistrict ()
		{
			IList<District> districts = (from d in Container.Instances<District>()
																	 select d).ToList();
			return districts;
		}
		public bool HideAllDistrict ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.ShowAllDistrict
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}

		//[MemberOrder(Sequence = "240", Name = "জেলা")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("নাম দিয়ে খোঁজ")]
		public District ByDistrictName (District জেলা)
		{
			return জেলা;
		}

		[PageSize(10)]
		public IQueryable<District> AutoComplete0ByDistrictName ([MinLength(1)] string name)
		{
			IQueryable<District> dists = Container.Instances<District>().Where(w => w.Name.StartsWith(name));

			return dists;
		}
		public bool HideByDistrictName ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.ByDistrictName
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}
		#endregion

		#region SubDistrict
		//[MemberOrder(Sequence = "320", Name = "উপজেলা")]
		[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("নতুন উপজেলা")]
		//[ExcludeFromFindMenu]
		public SubDistrict AddSubDistrict ([MaxLength(150), Required] string subdistrictName, [Required] District district)
		{
			SubDistrict subdistrict = Container.NewTransientInstance<SubDistrict>();
			subdistrict.Name = subdistrictName;
			subdistrict.Parent = district;
			Container.Persist(ref subdistrict);
			return subdistrict;
		}

		public IList<District> Choices1AddSubDistrict ()
		{
			return Container.Instances<District>().ToList();
		}

		public string ValidateAddSubDistrict (string subdistrictName, District district)
		{
			SubDistrict subdistrict = Container.Instances<SubDistrict>().Where(w => w.Name == subdistrictName && w.Parent.AreaId == district.AreaId).FirstOrDefault();
			if (subdistrict != null)
			{
				return "Duplicate Sub-District Name";
			}

			return null;
		}
		public bool HideAddSubDistrict ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.AddSubdistrict
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}

		//[MemberOrder(Sequence = "330", Name = "উপজেলা")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("সকল উপজেলা")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(true, "Name", "AllActiveAsars", "AllNewAsars", "AllRevivedAsars", "AllInactiveAsars", "Members", "Workers")]
		public IList<SubDistrict> AllSubDistricts ()
		{
			IList<SubDistrict> subdistricts = (from d in Container.Instances<SubDistrict>()
																				 select d).ToList();
			return subdistricts;
		}
		public bool HideAllSubDistricts ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.ShowAllSubdistricts
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}
		//[MemberOrder(Sequence = "340", Name = "উপজেলা")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("নাম দিয়ে খোঁজ")]
		public SubDistrict BySubDistrictName (SubDistrict উপজেলা)
		{
			return উপজেলা;
		}

		[PageSize(10)]
		public IQueryable<SubDistrict> AutoComplete0BySubDistrictName ([MinLength(1)] string name)
		{
			IQueryable<SubDistrict> subdists = Container.Instances<SubDistrict>().Where(w => w.Name.StartsWith(name));

			return subdists;
		}
		public bool HideBySubDistrictName ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.BySubDistrictName
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}
		#endregion

		#region AddMetropolitanCity (Action)

		//[MemberOrder(Sequence = "350", Name = "মেট্রোপলিটন শহর")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("নতুন মেট্রোপলিটন  শহর")]
		public MetropolitanCity AddMetropolitanCity ([MaxLength(150), Required] string cityName, [Required] Division division)
		{
			MetropolitanCity city = Container.NewTransientInstance<MetropolitanCity>();
			city.Name = cityName;
			city.Parent = division;
            Container.Persist(ref city);
			return city;
		}

		public IList<Division> Choices1AddMetropolitanCity ()
		{
			return Container.Instances<Division>().ToList();
		}

		public string ValidateAddMetropolitanCity (string cityName, Division division)
		{
			MetropolitanCity city = Container.Instances<MetropolitanCity>().Where(w => w.Name == cityName && w.Parent.AreaId == division.AreaId).FirstOrDefault();

			if (city != null)
			{
				return "Duplicate City Name";
			}

			return null;
		}
		public bool HideAddMetropolitanCity ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.AddMetropolitaCity
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}

		//[MemberOrder(Sequence = "360", Name = "মেট্রোপলিটন শহর")]
		//[AuthorizeAction(Roles = "AMSAdmin")]
		[DisplayName("সকল শহর")]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(true, "Name", "AllActiveAsars", "AllNewAsars", "AllRevivedAsars", "AllInactiveAsars", "Members", "Workers")]
		public IList<MetropolitanCity> AllCities ()
		{
			IList<MetropolitanCity> cities = (from d in Container.Instances<MetropolitanCity>()
																				select d).ToList();
			return cities;
		}
		public bool HideAllCities ()
		{
			IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

			Feature feature =
					features.Where(w => w.FeatureCode == (int)Feature.AreaSetupFeatureEnum.ShowMetropolitaCities
					&& w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.Area.ToString()).FirstOrDefault();

			if (feature == null)
				return true;
			return false;
		}
		// Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
		#endregion

		#region Authorization PRIVATE METHODS

		[NakedObjectsIgnore]
		private List<string> getFilteredMemebershipUsers ()
		{
			if ((Roles.IsUserInRole(AllEnums.ADMIN)))
			{
				return this.getMembershipUsers(Roles.GetAllRoles());
			}
			//return this.getMembershipUsers(Roles.GetAllRoles().Where(x => (!x.Contains(AllEnums.ADMIN))).ToArray<string>());
			return this.getMembershipUsers(Roles.GetAllRoles().Where(x => (!x.Equals(AllEnums.ADMIN))).ToArray<string>());
		}

		[NakedObjectsIgnore]
		private List<string> getMembershipUsers (string[] roleNames)
		{
			List<string> userNames = new List<string>();
			foreach (string roleName in roleNames)
			{
				string[] names = Roles.GetUsersInRole(roleName);
				userNames.AddRange(names);
			}
			userNames.Sort();
			return userNames;
		}

		//[NakedObjectsIgnore]
		//public IList<string> getAllRoles()
		//{
		//    if ((Roles.IsUserInRole(AllEnums.FMS_ADMIN)) || (Roles.IsUserInRole(AllEnums.ADMIN)))
		//    {
		//        return Roles.GetAllRoles().ToList();
		//    }
		//    return Roles.GetAllRoles().Where(x => (!x.Contains(AllEnums.FMS_ADMIN)) && (!x.Contains(AllEnums.MANAGEMENT)) && (!x.Contains(AllEnums.ADMIN))).ToList();
		//}

		[NakedObjectsIgnore]
		public IList<string> getAllRoles ()
		{
			if (Roles.IsUserInRole(AllEnums.ADMIN))
			{
				return Roles.GetAllRoles().ToList();
			}
			return Roles.GetAllRoles().Where(x => (!x.Equals(AllEnums.ADMIN))).ToList();
		}

		#endregion
	}
}
