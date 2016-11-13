using KhelaGhar.AMS.Model.Domain.Shared;
using NakedObjects;
using NakedObjects.Value;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Activities
{
    public class Attachment
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        #region Life Cycle Methods
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

        #region Primitive Properties
        [Key, NakedObjectsIgnore]
        public virtual long FileAttachmentId { get; set; }
        [NakedObjectsIgnore]
        public virtual byte[] FileContent { get; set; }
        [Required, StringLength(200), NakedObjectsIgnore]
        [MemberOrder(10)]
        public virtual string FileName { get; set; }
        [NakedObjectsIgnore]
        public virtual string FileMime { get; set; }

        #endregion
        [Title]
        public virtual FileAttachment FileAttachment
        {
            get
            {
                if (FileContent == null) return null;
                return new FileAttachment(FileContent, FileName, FileMime) { DispositionType = "inline" };
            }
        }

        #region Complex Properties
        #region AuditFields (AuditFields)

        private AuditFields _auditFields = new AuditFields();

        [MemberOrder(250)]
        [Required]
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
        public bool HideAuditFields()
        {
            return true;
        }
        #endregion
        #endregion

        #region  Navigation Properties
        [Required, NakedObjectsIgnore]
        [MemberOrder(200)]
        public virtual Activity Activity { get; set; }
        #endregion

        #region Edit Meter Class Enable Disable
        public string DisablePropertyDefault()
        {
            return "You do not have permission to Edit";
        }
        #endregion
    }
}
