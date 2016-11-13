using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhelaGhar.AMS.Model.Domain
{
    public class CommitteeMember
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container
        public IDomainObjectContainer Container { set; protected get; }

        #endregion

        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.

        public virtual void Persisting()
        {
          AuditFields.InsertedBy = Container.Principal.Identity.Name;
          AuditFields.InsertedDateTime = DateTime.Now;
          AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
          AuditFields.LastUpdatedDateTime = DateTime.Now;
        }
        public virtual void Updating()
        {
          AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
          AuditFields.LastUpdatedDateTime = DateTime.Now;
        }
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

        #region Primitive Properties

        [Key, NakedObjectsIgnore]
        public virtual int CommitteeMemberId { get; set; }

        [DisplayName("মেম্বার"), MemberOrder(10), Required]
        public virtual Kormi Kormi { get; set; }
      
        [DisplayName("পদবী"), MemberOrder(20), Required]
        [NakedObjectsIgnore]
        public virtual Designation Designation { get; set; }

        [DisplayName("পদবী"), MemberOrder(20)]
        [NotMapped]
        public string DesigString
        {
          get
          {
            return this.Designation.Name;
          }
        }

        [MemberOrder(30), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("মোবাইল নং")]
        public virtual string MobileNo
        {
          get
          {
            return this.Kormi.MobileNo;
          }
        }

        [DisplayName("কমিটি"), MemberOrder(40), Required]
        [NakedObjectsIgnore]
        public virtual Committee Committee { get; set; }

        #endregion

        #region Complex Properties
        #region AuditFields (AuditFields)

        private AuditFields _auditFields = new AuditFields();

        [MemberOrder(250)]
        [Required, Hidden]
        public virtual AuditFields AuditFields
        {
          get
          {
            return _auditFields;
          }
          set
          {
            _auditFields = value;
          }
        }

        #endregion

        #endregion
    }
}
