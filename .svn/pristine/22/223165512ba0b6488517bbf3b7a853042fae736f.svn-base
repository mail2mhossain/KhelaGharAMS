using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NakedObjects;
using System.ComponentModel.DataAnnotations;

namespace KhelaGhar.AMS.Model.KhelaGharAMSAudit
{
    public class SubDistrictAudit : AuditProperties
    {
        #region Primitive Properties

        [MemberOrder(20)]
        public virtual string Name { get; set; }

        [MemberOrder(30), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        public virtual string Description { get; set; }

        #endregion
    }
}
