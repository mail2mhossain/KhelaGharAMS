using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NakedObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using KhelaGhar.AMS.Model.Domain;
using System.Runtime.InteropServices;

namespace KhelaGhar.AMS.Model.KhelaGharAMSAudit
{
    public class CommitteeAudit : AuditProperties
    {
        [DisplayName("কমিটির ধরণ"), MemberOrder(20), Required]
        [EnumDataType(typeof(AllEnums.CommitteeType))]
        public virtual int CommitteeType { get; set; }

        [DisplayName("কমিটির সদস্য সংখ্যা"), MemberOrder(30), Required]
        public virtual int TotalMembers { get; set; }

        [DisplayName("কমিটি গঠনের তারিখ"), MemberOrder(40), Required]
        [Mask("d")]
        public virtual DateTime DateOfFormation { get; set; }
    }
}
