using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Features
{
	public class Feature
	{
		#region Primitive Properties
		[Key, NakedObjectsIgnore]
		public virtual int FeatureId { get; set; }
		[Title, Required]
		[MemberOrder(20)]
		[StringLength(100)]
		public virtual string FeatureName { get; set; }
		[MemberOrder(10)]
		public virtual int FeatureCode { get; set; }
		#endregion

		#region Navigation Properties
		[MemberOrder(10), Required, Disabled]
		public virtual FeatureType FeatureType { get; set; }
		#endregion

		#region Feature Enums
		public enum AreaSetupFeatureEnum
		{
			AddDivision = 1,
			EditDivision = 2,
			ShowAllDivisions = 3,
			AddDistrict = 4,
			EditDistrict = 5,
			ShowAllDistrict = 6,
			ByDistrictName = 7,
			AddMetropolitaCity = 8,
			EditMetropolitaCity = 9,
			ShowMetropolitaCities = 10,
			AddSubdistrict = 11,
			EditSubdistrict = 12,
			ShowAllSubdistricts = 13,
			BySubDistrictName = 14
		}
		public enum UserManagementFeatureEnums
		{
			AddLoginUser = 1,
			ShowAllUsers = 2,
			AddRole = 3,
			EditRole = 4,
			ShowAllRoles = 5,
			AddFeatures = 6,
			RemoveFeatures = 7,
			AssignRoleToUser = 8,
			ChangePassword = 9,
			ShowAllFeatureTypes = 10,
            AddAddress = 11,
            EditAddress = 12,
            AddUser = 13,
            EditUser = 14,
            ShowFeatures = 15
        }
		public enum SetupFeatureEnum
		{
			CreateDesignation = 1,
			AllDesignation = 2
		}
		#endregion
	}
}
