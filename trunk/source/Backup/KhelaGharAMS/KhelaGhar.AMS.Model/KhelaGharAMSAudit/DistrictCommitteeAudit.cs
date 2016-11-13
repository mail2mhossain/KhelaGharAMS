using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;

namespace KhelaGhar.AMS.Model.KhelaGharAMSAudit
{
    public class DistrictCommitteeAudit : AuditProperties
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container

        #endregion
        [Required]
        public virtual int DistrictId { get; set; }

        [Required]
        public virtual int CommitteeId { get; set; }

    }
}

