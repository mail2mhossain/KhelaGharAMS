using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
    public class CommitteeMember
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container

        #endregion
        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


        #endregion

        public string Title()
        {
            var t = new TitleBuilder();
            if (this.Kormi != null)
            {
                t.Append(this.Kormi.Name).Append(", ").Append(this.Designation.Name);
            }
            else
            {
                t.Append("");
            }
            return t.ToString();
        }

        [Key, Hidden]
        public virtual int CommitteeMemberId { get; set; }

        [DisplayName("মেম্বার"), MemberOrder(10), Required]
        public virtual Kormi Kormi { get; set; }
      
        [DisplayName("পদবী"), MemberOrder(20), Required]
        public virtual Designation Designation { get; set; }

        [DisplayName("কমিটি"), MemberOrder(40), Required]
        [Hidden]
        public virtual Committee Committee { get; set; }
    }
}
