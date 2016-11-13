using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using NakedObjects;
using KhelaGhar.AMS.Model.Domain;

namespace KhelaGhar.AMS.Model.KhelaGharAMSAudit
{
    public class AuditProperties
    {
        [Key, NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [NakedObjectsIgnore, MemberOrder(10), EnumDataType(typeof(AllEnums.ActionTypes))]
        public virtual int ActionType { get; set; }

        [NakedObjectsIgnore, MemberOrder(20)]
        public virtual string User { get; set; }

        [NakedObjectsIgnore, MemberOrder(30)]
        public virtual Nullable<DateTime> Date { get; set; }

        [NakedObjectsIgnore, MemberOrder(40)]
        public virtual string DomainID { get; set; }
    }
}
