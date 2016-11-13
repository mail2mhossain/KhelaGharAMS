using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
  [DisplayName("সভ্য")]
  public class Member
  {
    #region Injected Services
    // This region should contain properties to hold references to any services required by the
    // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container
    public IDomainObjectContainer Container { set; protected get; }

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

    [Key, Hidden]
    public virtual int MemberId { get; set; }

    [MemberOrder(10), Title]
    [DisplayName("নাম")]
    [MaxLength(250), Required]
    public virtual string Name { get; set; }

    [MemberOrder(20)]
    [DisplayName("অভিভাবক ১")]
    [MaxLength(250), Required]
    public virtual string Guardian1 { get; set; }

    [MemberOrder(30)]
    [DisplayName("অভিভাবক ২")]
    [MaxLength(250), Optionally]
    public virtual string Guardian2 { get; set; }

    [MemberOrder(40)]
    [DisplayName("মোবাইল নং")]
    [MaxLength(25), Optionally]
    public virtual string MobileNo { get; set; }
      
    [MemberOrder(50)]
    [DisplayName("শিক্ষা প্রতিষ্ঠানের নাম")]
    [MaxLength(250), Optionally]
    public virtual string SchoolName { get; set; }

    [DisplayName("জন্ম তারিখ"), MemberOrder(60), Required]
    [Mask("d")]
    public virtual DateTime DOB { get; set; }

    [MemberOrder(70)]
    [DisplayName("শ্রেণী")]
    [MaxLength(50), Optionally]
    public virtual string SchoolStandard { get; set; }

    [MemberOrder(80), Optionally]
    [DisplayName("রক্তের গ্রুপ")]
    [MaxLength(10)]
    public virtual string BloodGroup { get; set; }
    
    public IList<string> ChoicesBloodGroup()
    {
      return AllEnums.GetBloodGroup();
    }

    [MemberOrder(90), Optionally]
    [DisplayName("বর্তমান ঠিকানা")]
    [MaxLength(250), MultiLine(NumberOfLines = 3, Width = 50)]
    public virtual string PresentAddress { get; set; }
      
    #endregion

    #region Complex Properties
    #region AuditFields (AuditFields)

    private AuditFields _auditFields = new AuditFields();

    [MemberOrder(250)]
    [Hidden, Required]
    public virtual AuditFields AuditFields
    {
      get
      {
        return _auditFields;
      }
      set
      {
        _auditFields = value;
      }
    }

    #endregion

    #endregion

    #region Navigation Properties
    [MemberOrder(100)]
    [DisplayName("আসর")]
    [Required]
    public virtual Asar Asar { get; set; }
      
    #endregion

    //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

  }
}

