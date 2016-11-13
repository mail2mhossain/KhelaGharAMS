using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhelaGhar.AMS.Model.Domain
{
  [DisplayName("টীকা")]
  public class Note
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
    public virtual int NoteId { get; set; }

    [MemberOrder(20), Required]
    [DisplayName("তারিখ"), Mask("d"), Hidden]
    public virtual DateTime NoteDate { get; set; }

    [MemberOrder(30), Required]
    [DisplayName("বিষয়")]
    [MaxLength(350), Title]
    public virtual string Subject { get; set; }

    [MemberOrder(40), Required]
    [DisplayName("বর্ণনা")]
    [MaxLength(350), MultiLine(NumberOfLines = 4, Width = 100)]
    public virtual string Description { get; set; }

    [MemberOrder(50), Optionally]
    [DisplayName("সিদ্ধান্তসমূহ")]
    [MaxLength(350), MultiLine(NumberOfLines = 4, Width = 100)]
    public virtual string Decision { get; set; }

    #endregion

    #region Get Only Properties
    [MemberOrder(10), NotMapped]
    [DisplayName("টীকা"), Disabled]
    public virtual string NoteTypeString
    {
      get
      {
        if (this.NoteType != null)
        {
          return this.NoteType.Name;
        }

        return String.Empty;
      }
    }

    public bool HideNoteTypeString()
    {
      if (Container.IsPersistent(this))
      {
        return false;
      }

      return true;
    }

    [MemberOrder(20), NotMapped]
    [DisplayName("তারিখ"), Disabled]
    public virtual string NoteDateString
    {
      get
      {
        return this.NoteDate.ToString("MMMM dd, yyyy");
      }
    }

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
    [MemberOrder(10), Required]
    [DisplayName("টীকা"), Hidden]
    public virtual NoteType NoteType { get; set; }

    //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts
    #endregion

    //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

  }
}

