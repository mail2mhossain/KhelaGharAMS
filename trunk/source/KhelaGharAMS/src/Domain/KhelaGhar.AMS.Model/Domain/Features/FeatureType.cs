using KhelaGhar.AMS.Model.Repository;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Features
{
  [Bounded]
  public class FeatureType
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }
    #endregion

    #region Primitive Properties
    [Key, NakedObjectsIgnore]
    public virtual int FeatureTypeId { get; set; }
    [Title, Required]
    [MemberOrder(10)]
    [StringLength(50)]
    public virtual string FeatureTypeName { get; set; }
    [MemberOrder(10)]
    public virtual int FeatureTypeCode { get; set; }

    #endregion

    #region Get Properties
    #region Features
    [MemberOrder(50), NotMapped]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("Features")]
    [TableView(false, "FeatureName")]
    public IList<Feature> Features
    {
      get
      {
        IList<Feature> features = (from r in Container.Instances<Feature>()
                                   where r.FeatureType.FeatureTypeId == this.FeatureTypeId
                                   select r).ToList();
        return features;
      }
    }
    #endregion
    #endregion

    #region Feature Type Enums
    public enum FeatureTypeEnum
    {
      UserManagement = 1,
      Area = 2,
      Asar = 3,
      Committe = 4,
      Setup = 5
    }
    #endregion

    #region Behavior
    #region Edit Feature Type Enable Disable
    public string DisablePropertyDefault()
    {
      return "You do not have permission to Edit";
    }
    #endregion
    #endregion
  }
}
