using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Domain.UserAccounts;
using KhelaGhar.AMS.Utility;
using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Repository
{
  [DisplayName("User Accounts")]
  public class UserAccountRepository : AbstractFactoryAndRepository
  {
    private const int UserCodeLength = 6;

    #region Injected Services

    public LoggedInUserInfoRepository LoggedInUserInfoRepository { set; protected get; }

    #endregion

    public static void Menu(IMenu menu)
    {
      menu.AddAction("ChangePassword");
      menu.CreateSubMenu("Users")
          .AddAction("AddUser")
          .AddAction("SearchUserByCode")
          .AddAction("ShowAllUsers");

      menu.CreateSubMenu("Role")
          .AddAction("AddRole")
          .AddAction("ShowAllRoles");
      //menu.CreateSubMenu("FeatureType")
      //    .AddAction("AddFeatureType")
      //    .AddAction("ShowAllFeatureTypes");
      menu.CreateSubMenu("Feature")
          .AddAction("ShowFeatures");
    }

    #region ASP DOT NET MEMBERSHIP

    #region ROLE

    public Role AddRole([RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string name)
    {
      Role role = Container.NewTransientInstance<Role>();

      role.Id = Guid.NewGuid().ToString();
      role.Name = name;

      Container.Persist(ref role);

      return role;
    }

    public string Validate0AddRole(string name)
    {
      Role role = (from r in Container.Instances<Role>()
                   where r.Name.ToLower() == name.ToLower()
                   select r).FirstOrDefault();

      if (role != null)
      {
        return "Duplicate Role";
      }

      return null;
    }

    public bool HideAddRole()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.AddRole
                              && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Name")]
    public IQueryable<Role> ShowAllRoles()
    {
      return Container.Instances<Role>();
    }

    public bool HideShowAllRoles()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.ShowAllRoles
                              && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    #endregion

    #region USERS

    public User AddUser([RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string firstName, [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string lastName, DateTime dateOfBirth, [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string nationalId,
        [Description("Example: +8801523456789")] [RegEx(Validation = @"^(?:\+88|01)?\d{11}\r?$", Message = "Not a valid mobile no")] string mobileNo,
        [RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")] string email)
    {
      User user = Container.NewTransientInstance<User>();

      user.UserCode = GetUserCode();
      user.FirstName = firstName;
      user.LastName = lastName;
      user.DOB = dateOfBirth;
      user.NationalId = nationalId;
      user.MobileNo = mobileNo;
      user.Email = email;

      Container.Persist(ref user);

      return user;
    }

    private string GetUserCode()
    {
      string key = StringGenerator.GenerateUniqueNumberKey(UserCodeLength);

      while (IsUserCodeExist(key))
      {
        key = StringGenerator.GenerateUniqueNumberKey(UserCodeLength);
      }
      return key;
    }

    private bool IsUserCodeExist(string code)
    {
      User user = Container.Instances<User>().Where(w => w.UserCode == code).FirstOrDefault();

      if (user != null)
      {
        return true;
      }
      return false;
    }
    public bool HideAddUser()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.AddLoginUser
                              && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    public string ValidateAddUser(string firstName, string lastName, DateTime dateOfBirth, string nationalId, string mobileNo, string email)
    {
      if (dateOfBirth > DateTime.Today)
      {
        return "Invalid Birth Date";
      }

      User user = (from u in Container.Instances<User>()
                   where u.NationalId == nationalId
                   select u).FirstOrDefault();
      if (user != null)
      {
        return "Duplicate National Id";
      }

      user = (from u in Container.Instances<User>()
              where u.Email.ToLower() == email.ToLower()
              select u).FirstOrDefault();

      if (user != null)
      {
        return "Duplicate User Email Address";
      }
      string digit11Mobile = mobileNo.Right(11);
      user = (from u in Container.Instances<User>()
              where u.MobileNo.Contains(digit11Mobile)
              select u).FirstOrDefault();

      if (user != null)
      {
        return "Duplicate Mobile No";
      }

      return null;
    }

    public User SearchUserByCode(string code)
    {
      User user = Container.Instances<User>().Where(w => w.UserCode == code).FirstOrDefault();

      if (user == null)
        Container.WarnUser("User not Found");
      return user;
    }
    public bool HideSearchUserByCode()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.ShowAllUsers
                                                  && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "FirstName", "LastName", "DOB", "NationalId", "MobileNo", "Email")]
    public IQueryable<User> ShowAllUsers()
    {
      return Container.Instances<User>();
    }

    public bool HideShowAllUsers()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.ShowAllUsers
                              && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    #endregion

    #region Feature Type

    //public void AddFeatureType(string typeName)
    //{
    //    FeatureType feature = Container.NewTransientInstance<FeatureType>();

    //    feature.FeatureTypeName = typeName;

    //    Container.Persist(ref feature);
    //}
    //public bool HideAddFeatureType()
    //{
    //    IList<Feature> features = LoggedInUserInfoDomainRepository.GetFeatureListByLoginUser();

    //    Feature feature =
    //        features.Where(w => w.FeatureCode == (int)Feature.UserAccountsFeatureEnums.AddFeatureType
    //        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnums.UserAccount.ToString()).FirstOrDefault();

    //    if (feature == null)
    //        return true;
    //    return false;
    //}
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    //[TableView(true, "FeatureTypeName")]
    //public IQueryable<FeatureType> ShowAllFeatureTypes()
    //{
    //    return Container.Instances<FeatureType>();
    //}
    //public bool HideShowAllFeatureTypes()
    //{
    //    IList<Feature> features = LoggedInUserInfoDomainRepository.GetFeatureListByLoginUser();

    //    Feature feature =
    //        features.Where(w => w.FeatureCode == (int)Feature.UserAccountsFeatureEnums.ShowAllFeatureTypes
    //        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnums.UserAccount.ToString()).FirstOrDefault();

    //    if (feature == null)
    //        return true;
    //    return false;
    //}

    #endregion

    #region Feature

    //public void AddFeature(string featureName, FeatureType featureType)
    //{
    //    Feature feature = Container.NewTransientInstance<Feature>();

    //    feature.FeatureName = featureName;
    //    feature.FeatureType = featureType;

    //    Container.Persist(ref feature);
    //}
    //public bool HideAddFeature()
    //{
    //    IList<Feature> features = LoggedInUserInfoDomainRepository.GetFeatureListByLoginUser();

    //    Feature feature =
    //        features.Where(w => w.FeatureCode == (int)Feature.UserAccountsFeatureEnums.AddFeature
    //        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnums.UserAccount.ToString()).FirstOrDefault();

    //    if (feature == null)
    //        return true;
    //    return false;
    //}

    public IList<Feature> ShowFeatures(FeatureType featureType)
    {
      IList<Feature> features =
          Container.Instances<Feature>().Where(w => w.FeatureType.FeatureTypeId == featureType.FeatureTypeId).ToList();

      return features;
    }

    public IList<FeatureType> Choices0ShowFeatures()
    {
      return Container.Instances<FeatureType>().ToList();
    }
    public bool HideShowFeatures()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.ShowFeatures
                                                  && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }
    #endregion

    #region Change Password
    public void ChangePassword([DataType(DataType.Password)] string oldPassword, [DataType(DataType.Password)] string newPassword,
            [DataType(DataType.Password)] string confirmNewPassword)
    {
      LoginUser user = GetLoggedinUser();
      user.PasswordHash = PasswordHash.HashPassword(newPassword);
      Container.InformUser("Password has been changed.");
    }
    public string ValidateChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
    {
      LoginUser user = GetLoggedinUser();
      //string enteredOldPassword = PasswordHash.HashPassword(oldPassword);

      if (!PasswordHash.VerifyHashedPassword(user.PasswordHash, oldPassword)) return "Old Password Does Not Match";
      if (newPassword != confirmNewPassword)
      {
        return "Password does not match";
      }
      return null;
    }
    public bool HideChangePassword()
    {
      IList<Feature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.ChangePassword
                              && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }
    private LoginUser GetLoggedinUser()
    {
      LoginUser user = Container.Instances<LoginUser>().Where(w => w.Email == Container.Principal.Identity.Name).FirstOrDefault();

      return user;
    }
    #endregion

    #endregion
  }
}
