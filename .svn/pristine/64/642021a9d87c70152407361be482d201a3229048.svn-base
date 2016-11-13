using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
  [DisplayName("এলাকা ধরণ")]
  [Bounded]
  public class AreaType
  {
    #region Injected Services
    // This region should contain properties to hold references to any services required by the
    // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container

    #endregion

    #region Life Cycle Methods
    // This region should contain any of the 'life cycle' convention methods (such as
    // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


    #endregion

    [Key, Hidden]
    public virtual int AreaTypeId { get; set; }

    [MemberOrder(20)]
    [DisplayName("এলাকা ধরণ")]
    [MaxLength(150), Title]
    public virtual string Name { get; set; }

    [MemberOrder(30), Optionally]
    //[DisplayName("সদস্যের নাম")]
    public virtual AreaType Parent { get; set; }
      
    
    //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

  }
}

