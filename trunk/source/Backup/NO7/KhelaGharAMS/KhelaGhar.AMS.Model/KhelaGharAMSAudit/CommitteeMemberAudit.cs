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
    public class CommitteeMemberAudit : AuditProperties
    {
        [DisplayName("কর্মী"), MemberOrder(20), Required]
        public virtual int KormiId { get; set; }

        [DisplayName("পদবী"), MemberOrder(30), Required]
        public virtual int DesignationId { get; set; }

        [DisplayName("কমিটি"), MemberOrder(40), Required]
        [NakedObjectsIgnore]
        public virtual int CommitteeId { get; set; }
    }
}
