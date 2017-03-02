using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using KhelaGhar.AMS.Model.Domain;
using KhelaGhar.AMS.Model.Domain.Areas;
using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Activities;
using KhelaGhar.AMS.Model.Domain.UserAccounts;
using KhelaGhar.AMS.Utility;
using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Domain.MasterData;

namespace KhelaGhar.AMS.Model.DbAccess
{
	public class KhelaGharAMSDbInitialiser : CreateDatabaseIfNotExists<KhelaGharAMSDbContext>
	{
		private KhelaGharAMSDbContext _context;

		protected override void Seed (KhelaGharAMSDbContext context)
		{
			this._context = context;

			PopulateDbWithRequiredConfig();

		}

		private void PopulateDbWithRequiredConfig ()
		{
			//CreateCentralKhelaGhar();

			AddActivities();
			//AddDivisions();
			CreateFeatureList();
		}

		private void CreateFeatureList ()
		{
			Role role = CreateRole("Admin");
			LoginUser loginUser = CreateAdminUser(role);
			AssignRoleToAdminUser(loginUser, role);

            FeatureType userManagementFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.UserManagement);
			CreateUserAccountFeature(userManagementFeatureType, role);

			FeatureType areaFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Area);
			CreateAreaFeature(areaFeatureType, role);

			FeatureType setupFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Setup);
			CreateSetupFeature(setupFeatureType, role);

		}
		private void CreateSetupFeature (FeatureType setupFeatureType, Role role)
		{
			var values = Enum.GetValues(typeof(Feature.SetupFeatureEnum));
			foreach (int value in values)
			{
				Feature feature = new Feature();
				feature.FeatureType = setupFeatureType;
				feature.FeatureName = Enum.GetName(typeof(Feature.SetupFeatureEnum), value);
				feature.FeatureCode = value;

				_context.Features.Add(feature);
				CreateRoleFeatures(feature, role);
			}
		}
		private void CreateAreaFeature (FeatureType areaFeatureType, Role role)
		{
			var values = Enum.GetValues(typeof(Feature.AreaSetupFeatureEnum));
			foreach (int value in values)
			{
				Feature feature = new Feature();
				feature.FeatureType = areaFeatureType;
				feature.FeatureName = Enum.GetName(typeof(Feature.AreaSetupFeatureEnum), value);
				feature.FeatureCode = value;

				_context.Features.Add(feature);
				CreateRoleFeatures(feature, role);
			}
		}
		private void CreateUserAccountFeature (FeatureType userManagementFeatureType, Role role)
		{
			var values = Enum.GetValues(typeof(Feature.UserManagementFeatureEnums));
			foreach (int value in values)
			{
				Feature feature = new Feature();
				feature.FeatureType = userManagementFeatureType;
				feature.FeatureName = Enum.GetName(typeof(Feature.UserManagementFeatureEnums), value);
				feature.FeatureCode = value;

				_context.Features.Add(feature);
				CreateRoleFeatures(feature, role);
			}
		}
		private Role CreateRole (string roleName)
		{
			Role role = new Role();
			role.Id = Guid.NewGuid().ToString();
			role.Name = roleName;

			_context.Roles.Add(role);

			return role;
		}
		private LoginUser CreateAdminUser (Role role)
		{
            string userCode = "762854";

            User user = new User();
            user.UserCode = userCode;
            user.FirstName = "Mosharraf";
            user.LastName = "Hossain";
            user.MobileNo = "+8801713032885";
            user.Email = "mail2mhossain@gmail.com";
            user.AuditFields.InsertedBy = "Automated";
            user.AuditFields.InsertedDateTime = DateTime.Now;
            user.AuditFields.LastUpdatedBy = "Automated";
            user.AuditFields.LastUpdatedDateTime = DateTime.Now;
            _context.Users.Add(user);

            LoginUser loginUser = CreateLoginUser(userCode, "123456");

            user.LoginUser = loginUser;

            return loginUser;
		}
        private LoginUser CreateLoginUser (string userCode, string password)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.Id = Guid.NewGuid().ToString();
            loginUser.UserName = userCode;
            loginUser.Email = userCode;
            loginUser.EmailConfirmed = false;
            loginUser.PasswordHash = PasswordHash.HashPassword("123456");
            loginUser.SecurityStamp = Guid.NewGuid().ToString();
            loginUser.PhoneNumberConfirmed = false;
            loginUser.TwoFactorEnabled = false;
            loginUser.LockoutEnabled = false;
            loginUser.AccessFailedCount = 0;

            _context.LoginUsers.Add(loginUser);

            return loginUser;
        }
        private void AssignRoleToAdminUser (LoginUser adminUser, Role role)
		{
			UserRoles userRole = new UserRoles();
			userRole.LoginUser = adminUser;
			userRole.Role = role;

			_context.UserRoles.Add(userRole);
		}
		private void CreateRoleFeatures (Feature feature, Role role)
		{
			RoleFeatures roleFeature = new RoleFeatures();
			roleFeature.Feature = feature;
			roleFeature.Role = role;

			_context.RoleFeatures.Add(roleFeature);
		}
		private FeatureType CreateFeatureType (FeatureType.FeatureTypeEnum featureTypeEnum)
		{
			FeatureType featureType = new FeatureType();
			featureType.FeatureTypeName = Enum.GetName(typeof(FeatureType.FeatureTypeEnum), featureTypeEnum);
			featureType.FeatureTypeCode = featureTypeEnum.GetHashCode();

			_context.FeatureTypes.Add(featureType);
			return featureType;

		}

		private void CreateCentralKhelaGhar ()
		{
			KendrioAsar khelaghar = new KendrioAsar
			{
				Name = "কেন্দ্রীয় খেলাঘর আসর",
				CommitteeType = Domain.Committees.Committee.TypeOfCommittee.পূর্ণাঙ্গ,
				AddressLine = "২/২২ বাবর রোড, মোহাম্মদপুর, ঢাকা"
			};

			_context.Asars.Add(khelaghar);
		}

		private void AddActivities ()
		{
			AddActivity("সংঙ্গীত");
			AddActivity("নৃত্য");
			AddActivity("আবৃত্তি");
			AddActivity("চিত্রাংকন");
			AddActivity("নাটক");
			AddActivity("গীটার");
			AddActivity("পাঠ চক্র");
		}

		private MasterActivity AddActivity (string activityname)
		{
			MasterActivity activity = new MasterActivity
			{
				ActivityName = activityname
			};

			_context.MasterActivities.Add(activity);
			return activity;
		}

		private void AddDivisions ()
		{
			AddDivision("ঢাকা");
			AddDivision("ময়মনসিংহ");
			AddDivision("চট্টগ্রাম");
			AddDivision("রাজশাহী");
			AddDivision("খুলনা");
			AddDivision("বরিশাল");
			AddDivision("সিলেট");
			AddDivision("রংপুর");
		}

		private void AddDivision (string name)
		{
			Division area = new Division
			{
				Name = name,
				Description = "",
				Parent = null
			};

			_context.Areas.Add(area);
		}
	}
}
