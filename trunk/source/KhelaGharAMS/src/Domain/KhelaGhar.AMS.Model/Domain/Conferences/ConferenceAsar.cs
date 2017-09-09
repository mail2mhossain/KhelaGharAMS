using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Shared;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KhelaGhar.AMS.Model.Domain.Committees.Committee;
using static KhelaGhar.AMS.Model.Domain.Conferences.Conference;

namespace KhelaGhar.AMS.Model.Domain.Conferences
{
    [DisplayName("আসর")]
    public class ConferenceAsar
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.

        public virtual void Persisting ()
        {
            AuditFields.InsertedBy = Container.Principal.Identity.Name;
            AuditFields.InsertedDateTime = DateTime.Now;
            AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
            AuditFields.LastUpdatedDateTime = DateTime.Now;
        }
        public virtual void Updating ()
        {
            AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
            AuditFields.LastUpdatedDateTime = DateTime.Now;
        }
        #endregion

        #region Primitive Properties

        [Key, NakedObjectsIgnore]
        public virtual int ConferenceAsarId { get; set; }
        [DisplayName("প্রতিনিধি/পর্যবেক্ষক"), MemberOrder(40), Required]
        public virtual TypeOfDeletegate DelegateType { get; set; }
        public virtual decimal RegistrationFee { get; set; }
        
        [StringLength(150), Optionally]
        public virtual string ReceiptNo { get; set; }
        [Mask("d"), Optionally]
        public virtual DateTime? ReceiptDate { get; set; }
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

        #region Get Properties
        [MemberOrder(60), NotMapped]
        [DisplayName("নাম")]
        public string Name
        {
            get
            {
                return Asar.Name;
            }
        }
        [MemberOrder(60), NotMapped]
        [DisplayName("কমিটির ধরণ")]
        public TypeOfCommittee CommitteeType
        {
            get
            {
                return Asar.CommitteeType;
            }
        }
        #endregion

        #region Navigation Properties
        [DisplayName("আসর"), MemberOrder(10), Required]
        public virtual Asar Asar { get; set; }

        [MemberOrder(100), NakedObjectsIgnore]
        [DisplayName("সম্মেলন"), Required]
        public virtual Conference Conference { get; set; }

        #endregion
    }
}
