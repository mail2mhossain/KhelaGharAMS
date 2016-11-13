using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.KhelaGharAMSAudit
{
  public class AreaAudit : AuditProperties
  {
    #region Injected Services
    // This region should contain properties to hold references to any services required by the
    // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container

    #endregion
    #region Life Cycle Methods
    // This region should contain any of the 'life cycle' convention methods (such as
    // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


    #endregion

    #region Primitive Properties
    [MemberOrder(20)]
    [DisplayName("নাম"), Required]
    [MaxLength(150)]
    public virtual string Name { get; set; }

    [MemberOrder(30)]
    [DisplayName("বর্ণনা")]
    [MaxLength(150)]
    public virtual string Description { get; set; }

    [MemberOrder(40), NakedObjectsIgnore]
    [DisplayName("এলাকার ধরণ"), Required]
    public virtual int AreaTypeID { get; set; }

    [MemberOrder(50)]
    [Optionally, NakedObjectsIgnore]
    public virtual int ParentID { get; set; }
    #endregion
  }
}

