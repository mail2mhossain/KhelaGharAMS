using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Repository;
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
    [Table("AspNetRoles")]
    [Bounded]
    public class Role
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }
        #endregion

        #region Primitive Properties
        [Key, NakedObjectsIgnore]
        public virtual string Id { get; set; }
        [Title, Required]
        [MemberOrder(10)]
        public virtual string Name { get; set; }
        #endregion

        #region Get Properties    
        #region Users  
        [MemberOrder(70), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("Users")]
        [TableView(false, "Email")]
        public IList<LoginUser> Users
        {
            get
            {
                IList<LoginUser> users = (from r in Container.Instances<UserRoles>()
                                          where r.Role.Id == this.Id
                                          select r.LoginUser).OrderBy(o => o.UserName).ToList();
                return users;
            }
        }
        #endregion

        #region Features
        [MemberOrder(50), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("Features")]
        [TableView(false, "FeatureName", "FeatureType")]
        public IList<Feature> Features
        {
            get
            {
                IList<Feature> features = (from r in Container.Instances<RoleFeatures>()
                                           where r.Role.Id == this.Id
                                           select r.Feature).OrderBy(o => o.FeatureName).ToList();
                return features;
            }
        }
        #endregion
        #endregion

        #region Behavior
        #region Assign Feature To Role
        public void AssignFeatureToRole(FeatureType type, IEnumerable<Feature> features)
        {
            foreach (Feature f in features)
            {
                RoleFeatures roleFeatures = Container.NewTransientInstance<RoleFeatures>();

                roleFeatures.Role = this;
                roleFeatures.Feature = f;

                Container.Persist(ref roleFeatures);
            }
        }
        public bool HideAssignFeatureToRole()
        {
            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.AddFeatures
                && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString()).FirstOrDefault();

            if (feature == null)
                return true;
            return false;
        }

        public IList<Feature> Choices1AssignFeatureToRole(FeatureType type)
        {
            if (type == null) return new List<Feature>();
            IList<int> featureIds = (from f in this.Features
                                     where f.FeatureType.FeatureTypeId == type.FeatureTypeId
                                     select f.FeatureId).ToList();

            IList<Feature> features = (from f in Container.Instances<Feature>()
                                       where f.FeatureType.FeatureTypeId == type.FeatureTypeId
                                       && (!featureIds.Contains(f.FeatureId))
                                       select f).OrderBy(o => o.FeatureName).ToList();
            return features;
        }
        #endregion

        #region Remove Feature From Role

        public void RemoveFeatureFromRole(FeatureType type, IEnumerable<Feature> features)
        {
            foreach (Feature f in features)
            {
                RoleFeatures roleFeature = (from rf in Container.Instances<RoleFeatures>()
                                            where rf.Role.Id == this.Id
                                            && rf.Feature.FeatureId == f.FeatureId
                                            select rf).Single();
                Container.DisposeInstance(roleFeature);
            }
        }
        public bool HideRemoveFeatureFromRole()
        {
            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.RemoveFeatures
                && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString()).FirstOrDefault();

            if (feature == null)
                return true;
            return false;
        }
        public IList<Feature> Choices1RemoveFeatureFromRole(FeatureType type)
        {
            if (type == null) return new List<Feature>();
            IList<Feature> features = (from f in this.Features
                                       where f.FeatureType.FeatureTypeId == type.FeatureTypeId
                                       select f).OrderBy(o => o.FeatureName).ToList();
            return features;
        }
        #endregion

        #region Edit Role Enable Disable
        public string DisablePropertyDefault()
        {
            IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

            Feature feature =
                features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.EditRole
                && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString()).FirstOrDefault();

            if (feature == null)
            {
                return "You do not have permission to Edit";
            }

            return null;
        }
        #endregion
        #endregion

        #region Menu
        public static void Menu(IMenu menu)
        {
            menu.AddAction("AssignFeatureToRole");
            menu.AddAction("RemoveFeatureFromRole");
        }
        #endregion        
    }
}
