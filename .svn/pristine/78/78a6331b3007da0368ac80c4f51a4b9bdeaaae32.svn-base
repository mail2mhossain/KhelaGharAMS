using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NakedObjects;
using NakedObjects.Services;
using NakedObjects.Security;
using WebMatrix.WebData;
using System.Web.Security;
using KhelaGhar.AMS.Model.Domain;

namespace KhelaGhar.AMS.Model.Repository
{
    //[AuthorizeAction(Roles = "AMSAdmin")]
    [DisplayName("সেটআপ")]
    public class SetupRepository : AbstractFactoryAndRepository
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service.

        #endregion

        #region Authorization

        #region Change Password
        [MemberOrder(Sequence = "205", Name = "Authorization")]
        [AuthorizeAction(Roles = "AMSAdmin")]
        public void ChangePassword([DataType(DataType.Password)]string CurrentPassword, [DataType(DataType.Password)]string NewPassword, [DataType(DataType.Password)]string ConfirmNewPassword)
        {
        }

        public string ValidateChangePassword(string currentPassword, string newPassword, string confirmNewPassword)
        {
            var rb = new ReasonBuilder();
            if (newPassword.CompareTo(confirmNewPassword) != 0)
            {
                rb.AppendOnCondition(true, "New passwords don\'t match.");
            }
            else if (!WebSecurity.ChangePassword(WebSecurity.CurrentUserName, currentPassword, newPassword))
            {
                rb.AppendOnCondition(true, "Current password doesn\'t match.");
            }
            return rb.Reason;
        }
        #endregion

        #region Create Role

        [AuthorizeAction(Roles = "AMSAdmin")]
        [MemberOrder(Sequence = "210", Name = "Authorization")]
        public IList<string> CreateRole(string RoleName)
        {
            try
            {
                Roles.CreateRole(RoleName.Trim());
            }
            catch (Exception ex) { }
            return Roles.GetAllRoles().ToList();
        }

        public string ValidateCreateRole(string roleName)
        {
            var rb = new ReasonBuilder();
            if (Roles.RoleExists(roleName))
            {
                rb.AppendOnCondition(true, "Role already exists.");
            }
            return rb.Reason;
        }
        #endregion

        #region Show All Roles
        [AuthorizeAction(Roles = "AMSAdmin")]
        [MemberOrder(Sequence = "215", Name = "Authorization")]
        public IList<string> ShowAllRoles()
        {
            return this.getAllRoles();
        }
        #endregion

        #region Create User

        [AuthorizeAction(Roles = "AMSAdmin")]
        [MemberOrder(Sequence = "220", Name = "Authorization")]
        public IList<string> CreateUser(string UserName, [DataType(DataType.Password)]string Password, [DataType(DataType.Password)]string ConfirmPassword, string Role)
        {
            try
            {
                WebSecurity.CreateUserAndAccount(UserName, Password);
                Roles.AddUserToRole(UserName, Role);
            }
            catch (Exception ex) { }
            return this.getMembershipUsers(new string[] { Role });
        }

        public IList<string> Choices3CreateUser()
        {
            return this.getAllRoles();
        }

        public string ValidateCreateUser(string userName, string password, string confirmPassword, string role)
        {
            var rb = new ReasonBuilder();
            if (WebSecurity.GetUserId(userName) != -1)
            {
                rb.AppendOnCondition(true, "User already exists.");
            }
            if (password.CompareTo(confirmPassword) != 0)
            {
                rb.AppendOnCondition(true, "Password and Confirm Password doesn\'t match.");
            }
            return rb.Reason;
        }
        #endregion

        #region Show All Users
        [AuthorizeAction(Roles = "AMSAdmin")]
        [MemberOrder(Sequence = "225", Name = "Authorization")]
        public IList<string> ShowAllUsers()
        {
            IList<string> us = this.getFilteredMemebershipUsers();
            return this.getFilteredMemebershipUsers();
        }
        #endregion

        #endregion

        #region Activity
        [MemberOrder(Sequence = "20", Name = "কার্যক্রম")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নতুন কার্যক্রম")]
        public Activity CreateActivity()
        {
            return Container.NewTransientInstance<Activity>();
        }

        [MemberOrder(Sequence = "30", Name = "কার্যক্রম")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("সকল কার্যক্রম")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IQueryable<Activity> AllActivity()
        {
            return Container.Instances<Activity>();
        }

        #endregion

        #region Division
        [MemberOrder(Sequence = "120", Name = "বিভাগ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নতুন বিভাগ")]
        public Division CreateDivision()
        {
            return Container.NewTransientInstance<Division>();
        }

        [MemberOrder(Sequence = "130", Name = "বিভাগ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("সকল বিভাগ")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IQueryable<Division> AllDivision()
        {
            return Container.Instances<Division>();
        }

        #endregion

        #region District
        [MemberOrder(Sequence = "220", Name = "জেলা")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নতুন জেলা")]
        public District CreateDistrict()
        {
            return Container.NewTransientInstance<District>();
        }

        [MemberOrder(Sequence = "230", Name = "জেলা")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("সকল জেলা")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IQueryable<District> AllDistrict()
        {
            return Container.Instances<District>();
        }

        [MemberOrder(Sequence = "240", Name = "জেলা")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নাম দিয়ে খোঁজ")]
        public District ByDistrictName(District জেলা)
        {
            return জেলা;
            //return Container.Instances<District>().Where(w => w.Name.StartsWith(dist.Name));
        }

        [PageSize(10)]
        public IQueryable<District> AutoComplete0ByDistrictName([MinLength(1)] string name)
        {
            IQueryable<District> dists=Container.Instances<District>().Where(w => w.Name.StartsWith(name));

            return dists;
        }

        #region ByCommitteeType (Action)

        [MemberOrder(Sequence = "250", Name = "জেলা")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কমিটির ধরণ অনুযায়ী")]
        public IList<District> ByCommitteeType(string committeeType)
        {
            IList<District> asars = new List<District>();

            if (committeeType == AllEnums.CommitteeType.পূর্ণাঙ্গ.ToString())
            {
                asars = (from a in Container.Instances<DistrictCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.পূর্ণাঙ্গ
                         select a.District).ToList();
            }
            else if (committeeType == AllEnums.CommitteeType.আহ্বায়ক.ToString())
            {
                asars = (from a in Container.Instances<DistrictCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.আহ্বায়ক
                         select a.District).ToList();
            }
            else
            {
                IList<int> distids = (from a in Container.Instances<DistrictCommittee>()
                                      select a.District.Id).ToList();

                asars = (from a in Container.Instances<District>()
                         where (!distids.Contains(a.Id))
                         select a).ToList();
            }

            return asars;
        }

        public IList<string> Choices0ByCommitteeType()
        {
            return AllEnums.GetCommitteeType();
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion
        #endregion      

        #region SubDistrict
        [MemberOrder(Sequence = "320", Name = "উপজেলা")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নতুন উপজেলা")]
        public SubDistrict CreateSubDistrict()
        {
            return Container.NewTransientInstance<SubDistrict>();
        }

        [MemberOrder(Sequence = "330", Name = "উপজেলা")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("সকল উপজেলা")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IQueryable<SubDistrict> AllSubDistrict()
        {
            return Container.Instances<SubDistrict>();
        }

        [MemberOrder(Sequence = "340", Name = "উপজেলা")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নাম দিয়ে খোঁজ")]
        public SubDistrict BySubDistrictName(SubDistrict উপজেলা)
        {
            return উপজেলা;
            //return Container.Instances<District>().Where(w => w.Name.StartsWith(dist.Name));
        }

        [PageSize(10)]
        [Hidden]
        public IQueryable<SubDistrict> AutoComplete0BySubDistrictName([MinLength(1)] string name)
        {
            return Container.Instances<SubDistrict>().Where(w => w.Name.StartsWith(name));
        }

        #endregion

        #region Designation
        [MemberOrder(Sequence = "420", Name = "পদবী ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নতুন পদবী")]
        public Designation CreateDesignation()
        {
            return Container.NewTransientInstance<Designation>();
        }

        [MemberOrder(Sequence = "430", Name = "পদবী ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("সকল পদবী")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IQueryable<Designation> AllDesignation([EnumDataType(typeof(AllEnums.DesignationType))] int comitteType)
        {
            return Container.Instances<Designation>().Where(w => w.DesignationType == comitteType || w.DesignationType ==(int) AllEnums.DesignationType.উভয়_কমিটি).OrderBy(O=>O.DesignationOrder);
        }

        #endregion

        #region Authorization PRIVATE METHODS

        [Hidden]
        private List<string> getFilteredMemebershipUsers()
        {
            if ((Roles.IsUserInRole(AllEnums.ADMIN)))
            {
                return this.getMembershipUsers(Roles.GetAllRoles());
            }
            //return this.getMembershipUsers(Roles.GetAllRoles().Where(x => (!x.Contains(AllEnums.ADMIN))).ToArray<string>());
            return this.getMembershipUsers(Roles.GetAllRoles().Where(x => (!x.Equals(AllEnums.ADMIN))).ToArray<string>());
        }

        [Hidden]
        private List<string> getMembershipUsers(string[] roleNames)
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

        //[Hidden]
        //public IList<string> getAllRoles()
        //{
        //    if ((Roles.IsUserInRole(AllEnums.FMS_ADMIN)) || (Roles.IsUserInRole(AllEnums.ADMIN)))
        //    {
        //        return Roles.GetAllRoles().ToList();
        //    }
        //    return Roles.GetAllRoles().Where(x => (!x.Contains(AllEnums.FMS_ADMIN)) && (!x.Contains(AllEnums.MANAGEMENT)) && (!x.Contains(AllEnums.ADMIN))).ToList();
        //}

        [Hidden]
        public IList<string> getAllRoles()
        {
            if (Roles.IsUserInRole(AllEnums.ADMIN))
            {
                return Roles.GetAllRoles().ToList();
            }
            return Roles.GetAllRoles().Where(x => (!x.Equals(AllEnums.ADMIN))).ToList();
        }

        #endregion

        // 'fact' shortcut to add a factory method, 
        // 'alli' for an all-instances method
        // 'find' for a method to find a single object by query
        // 'list' for a method to return a list of objects matching a query

    }
}
