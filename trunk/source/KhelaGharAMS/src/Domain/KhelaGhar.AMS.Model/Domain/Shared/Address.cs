using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Repository;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Shared
{
  [Table("Addresses")]
  public class Address
  {
    #region Injected Services

    public IDomainObjectContainer Container { set; protected get; }
    public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }

    #endregion

    #region Life Cycle Methods

    // This region should contain any of the 'life cycle' convention methods (such as
    // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.

    public virtual void Persisting()
    {
      AuditFields.InsertedBy = Container.Principal.Identity.Name;
      AuditFields.InsertedDateTime = DateTime.Now;
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }

    public virtual void Updating()
    {
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }

    #endregion

    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual long AddressId { get; set; }
    [MemberOrder(10), Required, Title]
    [StringLength(250)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Street 1")]
    public virtual string Street1 { get; set; }
    [MemberOrder(20), Optionally]
    [StringLength(250)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Street 2")]
    public virtual string Street2 { get; set; }
    [MemberOrder(30), Optionally]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Postal Code")]
    [StringLength(100)]
    public virtual string PostalCode { get; set; }
    [MemberOrder(40), Optionally]
    [StringLength(150)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid City")]
    public virtual string City { get; set; }

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

    public bool HideAuditFields()
    {
      return true;
    }

    #endregion

    #endregion

    #region Edit Address Enable Disable

    public string DisablePropertyDefault()
    {
      IList<Feature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

      Feature feature =
          features.Where(w => w.FeatureCode == (int)Feature.UserManagementFeatureEnums.EditAddress
                              && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnum.UserManagement.ToString())
              .FirstOrDefault();

      if (feature == null)
      {
        return "You do not have permission to Edit";
      }

      return null;
    }

    #endregion
  }
}
