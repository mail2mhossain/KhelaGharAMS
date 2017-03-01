using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Repository;
using KhelaGhar.AMS.Utility;
using NakedObjects;
using NakedObjects.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.UserAccounts
{
    public class User
    {
        #region Injected Services

        public IDomainObjectContainer Container { set; protected get; }
        public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }

        #endregion

        #region Life Cycle Methods

        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.

        public virtual void Persisting ()
        {
            AuditFields.InsertedBy = Container.Principal.Identity.Name;
            AuditFields.InsertedDateTime = DateTime.Now;
            AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
            AuditFields.LastUpdatedDateTime = DateTime.Now;
        }

        public virtual void Updating ()
        {
            AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
            AuditFields.LastUpdatedDateTime = DateTime.Now;
        }

        #endregion

        public string Title ()
        {
            var t = Container.NewTitleBuilder();

            string title = this.FirstName + " " + this.LastName + " (" + this.MobileNo + ")";

            t.Append(title);

            return t.ToString();
        }

        #region Primitive Properties

        [Key, NakedObjectsIgnore]
        public virtual long UserId { get; set; }
        [Title, Required, MemberOrder(10), Disabled]
        [StringLength(6)]
        [Index("IX_User_UserCode", IsClustered = false, IsUnique = true)]
        public virtual string UserCode { get; set; }

        [Required, MemberOrder(20)]
        [StringLength(150)]
        [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid First Name")]
        public virtual string FirstName { get; set; }
        [Optionally, MemberOrder(30)]
        [StringLength(150)]
        [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Last Name")]
        public virtual string LastName { get; set; }
        [Optionally, Mask("d")]
        [MemberOrder(40)]
        public virtual DateTime? DOB { get; set; }
        public string ValidateDOB (DateTime? dateOfBirth)
        {
            if (dateOfBirth != null)
            {
                if (dateOfBirth > DateTime.Today)
                {
                    return "Invalid Birth Date";
                }
            }
            return null;
        }

        [MemberOrder(50), Optionally]
        [StringLength(50)]
        [Index("IX_User_NationalId", IsClustered = false, IsUnique = true)]
        [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid National Id")]
        public virtual string NationalId { get; set; }

        [MemberOrder(60), Optionally]
        [StringLength(15)]
        [Index("IX_User_MobileNo", IsClustered = false, IsUnique = true)]
        [Description("Example: +8801523456789")]
        [RegEx(Validation = @"^(?:\+88|01)?\d{11}\r?$", Message = "Not a valid mobile no")]
        public virtual string MobileNo { get; set; }

        [MemberOrder(70), Required]
        [StringLength(150)]
        [Disabled(WhenTo.OncePersisted)]
        [Index("IX_User_Email", IsClustered = false, IsUnique = true)]
        [RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")]
        public virtual string Email { get; set; }

        #endregion

        #region Complex Properties

        #region AuditFields (AuditFields)

        private AuditFields _auditFields = new AuditFields();

        [MemberOrder(250)]
        [Required]
        public virtual AuditFields AuditFields
        {
            get { return _auditFields; }
            set { _auditFields = value; }
        }

        public bool HideAuditFields ()
        {
            return true;
        }

        #endregion

        #endregion

        #region  Navigation Properties

        [MemberOrder(80), Optionally, Disabled]
        public virtual Address Address { get; set; }
        [MemberOrder(100), Optionally, Disabled]
        [NakedObjectsIgnore]
        public virtual LoginUser LoginUser { get; set; }

        #endregion

        #region Behavior

        #region Add Address (Action)

        [DisplayName("Add Address")]
        public void AddAddress ([Required, RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")] string street1, [Optionally, RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string street2, [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string zipCode, [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]string city)
        {
            Address address = Container.NewTransientInstance<Address>();
            address.Street1 = street1;
            address.Street2 = street2;
            address.PostalCode = zipCode;
            address.City = city;

            Container.Persist(ref address);

            this.Address = address;
        }

        public bool HideAddAddress ()
        {
            if (this.Address != null) return true;
            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.AddAddress
                                                        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
                    .FirstOrDefault();

            if (feature == null)
                return true;
            return false;
        }

        #endregion

        #region AddLoginUser

        [DisplayName("Add Login User")]
        public void AddLoginUser ([DataType(DataType.Password)] string password,
            [DataType(DataType.Password)] string confirmPassword, Role role)
        {
            LoginUser user = Container.NewTransientInstance<LoginUser>();

            user.Id = Guid.NewGuid().ToString();
            user.UserName = this.UserCode;
            user.Email = this.UserCode;
            user.EmailConfirmed = false;
            user.PasswordHash = PasswordHash.HashPassword(password);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PhoneNumberConfirmed = false;
            user.TwoFactorEnabled = false;
            user.LockoutEnabled = false;
            user.AccessFailedCount = 0;

            Container.Persist(ref user);

            UserRoles userRole = Container.NewTransientInstance<UserRoles>();
            userRole.LoginUser = user;
            userRole.Role = role;
            Container.Persist(ref userRole);

            this.LoginUser = user;
        }

        public IList<Role> Choices2AddLoginUser ()
        {
            IList<Role> roles = Container.Instances<RoleFeatures>().Select(s => s.Role).ToList();

            return roles.Distinct().OrderBy(o => o.Name).ToList();
        }
        public string ValidateAddLoginUser (string password, string confirmPassword, Role role)
        {
            if (password != confirmPassword)
            {
                return "Password does not match";
            }
            return null;
        }

        public bool HideAddLoginUser ()
        {
            if (String.IsNullOrEmpty(this.Email)) return true;

            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.AddLoginUser
                                                        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
                    .FirstOrDefault();

            if (feature == null) return true;

            LoginUser user =
                Container.Instances<LoginUser>().Where(w => w.Email == this.UserCode).FirstOrDefault();

            if (user != null) return true;

            return false;
        }

        #endregion

        #region Change Password
        public void ChangePassword ([DataType(DataType.Password)] string password,
                [DataType(DataType.Password)] string confirmPassword)
        {
            if (this.LoginUser != null)
            {
                this.LoginUser.PasswordHash = PasswordHash.HashPassword(password);
                Container.InformUser("Password has been changed.");
            }
        }
        public bool HideChangePassword ()
        {
            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                    features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.ChangePassword
                    && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString()).FirstOrDefault();

            if (feature == null)
                return true;
            if (this.LoginUser == null) return true;
            return false;
        }

        public string ValidateChangePassword (string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return "Password does not match";
            }
            return null;
        }
        #endregion

        #endregion

        #region Edit User Enable Disable

        public string DisablePropertyDefault ()
        {
            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.EditUser
                                                        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
                    .FirstOrDefault();

            if (feature == null)
            {
                return "You do not have permission to Edit";
            }

            return null;
        }

        #endregion

        #region Menu

        public static void Menu (IMenu menu)
        {
            menu.AddAction("AddAddress");
            menu.AddAction("AddLoginUser");
            menu.AddAction("ChangePassword");
        }

        #endregion
    }
}
