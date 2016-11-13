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
    public class AsarAudit : AuditProperties
    {
        [MemberOrder(0), Title]
        [DisplayName("নাম")]
        [MaxLength(250)]
        public virtual string Name { get; set; }

        [DisplayName("প্রতিষ্ঠার তারিখ")]
        [MemberOrder(20)]
        public virtual DateTime DateOfEstablishment { get; set; }

        [DisplayName("ভাই-বোনের সংখ্যা")]
        [MemberOrder(30)]
        public virtual int TotalMembers { get; set; }

        [MemberOrder(30), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("ঠিকানা")]
        [MaxLength(350)]
        public virtual string AddressLine { get; set; }

        [MemberOrder(40)]
        [DisplayName("এলাকা")]
        public virtual int AreaId { get; set; }

        [DisplayName("আসরের অবস্থা"), MemberOrder(100), Required]
        [EnumDataType(typeof(AllEnums.AsarStatus))]
        public virtual int AsarStatus { get; set; }
    }
}
