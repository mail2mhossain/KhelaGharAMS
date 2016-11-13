using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
    public class AsarActivity
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container

        #endregion

        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


        #endregion

        [Key, NakedObjectsIgnore]
        public virtual int AsarActivityId { get; set; }

        [MemberOrder(10)]
        [DisplayName("আসর"), Required]
        public virtual Asar Asar { get; set; }

        [MemberOrder(20)]
        [DisplayName("কার্যক্রম"), Required]
        public virtual Activity Activity { get; set; }
        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

    }
}

