using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
    [DisplayName("আসর কমিটি")]
    public class AsarCommittee
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
        public virtual int AsarCommitteeId { get; set; }

        [Required]
        public virtual Asar Asar { get; set; }

        [Required]
        public virtual Committee Committee { get; set; }
      
        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts
    }
}

