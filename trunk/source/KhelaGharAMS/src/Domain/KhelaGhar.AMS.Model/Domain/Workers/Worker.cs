using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Shared;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Workers
{
  [DisplayName("কর্মী")]
  public class Worker
  {
    #region Injected Services
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

    public string Title()
    {
      var t = Container.NewTitleBuilder();
      t.Append(this.Name).Append(" - ").Append(this.MobileNo); //.Append(" (").Append(this.Asar.Name).Append(")");
      return t.ToString();
    }

    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual int WorkerId { get; set; }
    [MemberOrder(10), Title]
    [DisplayName("নাম")]
    [StringLength(250), Required]
    public virtual string Name { get; set; }

    [MemberOrder(20)]
    [DisplayName("মোবাইল নং")]
    [MaxLength(25), Optionally]
    [Description("Example: +8801523456789")]
    [RegEx(Validation = @"^(?:\+88|01)?\d{11}\r?$", Message = "Not a valid mobile no")]
    public virtual string MobileNo { get; set; }

    [MemberOrder(30)]
    [DisplayName("ইমেইল"), Optionally]
    [Description("Example: ssdk@gmail.com")]
    [RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")]
    public virtual string Email { get; set; }
    [MemberOrder(40)]
    [DisplayName("মাতার নাম")]
    [StringLength(250), Optionally]
    public virtual string MotherName { get; set; }
    [MemberOrder(50)]
    [DisplayName("পিতার নাম")]
    [StringLength(250), Optionally]
    public virtual string FatherName { get; set; }
    [MemberOrder(60), Mask("d")]
    [DisplayName("জন্ম তারিখ"), Optionally]
    public virtual DateTime? DOB { get; set; }
    [MemberOrder(70), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
    [DisplayName("ঠিকানা")]
    [MaxLength(350)]
    public virtual string AddressLine { get; set; }

    [DisplayName("কর্মীর অবস্থা"), MemberOrder(80), Required]
    [DefaultValue(WorkerStatus.সক্রিয়)]
    public virtual WorkerStatus Status { get; set; }
    public enum WorkerStatus
    {
      সক্রিয় = 1, //Active
      নিষ্ক্রিয় = 2 //Inactive
    }
    #endregion

    #region Complex Properties
    #region AuditFields (AuditFields)

    private AuditFields _auditFields = new AuditFields();

    [MemberOrder(250)]
    [Required, Hidden]
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
    [Required, Disabled]
    public virtual Asar Asar { get; set; }

    #endregion
  }
}
