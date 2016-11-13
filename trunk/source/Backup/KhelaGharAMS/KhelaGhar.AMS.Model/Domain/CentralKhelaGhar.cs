using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
    public class CentralKhelaGhar
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
        public virtual int CentralKhelaGharId { get; set; }

        [MemberOrder(10), Title]
        [DisplayName("নাম")]
        [MaxLength(250), Required]
        public virtual string Name { get; set; }

        [MemberOrder(20), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("ঠিকানা")]
        [MaxLength(350)]
        public virtual string AddressLine { get; set; }
      
        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

    }
}

