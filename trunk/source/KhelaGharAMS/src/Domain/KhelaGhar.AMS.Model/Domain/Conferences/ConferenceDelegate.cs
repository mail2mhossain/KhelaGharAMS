using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Members;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Domain.Workers;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Conferences
{
    //[DisplayName("প্রতিনিধি")]
    [DisplayName("Delegate")]
    public class ConferenceDelegate
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
        public virtual int DelegateId { get; set; }
        [DisplayName("প্রতিনিধি/পর্যবেক্ষক"), MemberOrder(40), Required]
        public virtual TypeOfDeletegate DelegateType { get; set; }
        public virtual decimal DelegateFee { get; set; }
        public enum TypeOfDeletegate
        {
            প্রতিনিধি = 1,
            পর্যবেক্ষক = 2
        }
        [StringLength(150), Optionally]
        public virtual string ReceiptNo { get; set; }
        [Mask("d"), Optionally]
        public virtual DateTime? ReceiptDate { get; set; }
        #endregion

        #region Get Properties
        [MemberOrder(20), NotMapped]
        [DisplayName("আসর")]
        public Asar Asar
        {
            get
            {
                return this.Worker.Asar;
            }
        }
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

        #region Navigation Properties
        [DisplayName("মেম্বার"), MemberOrder(10), Required]
        public virtual Worker Worker { get; set; }

        [MemberOrder(100), NakedObjectsIgnore]
        [DisplayName("সম্মেলন"), Required]
        public virtual Conference Conference { get; set; }

        #endregion
    }
}
