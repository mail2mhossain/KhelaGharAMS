using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Repository;
using NakedObjects;
using NakedObjects.Value;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Activities
{
	//[Table("Activities")]
	public class Activity
	{
		#region Injected Services
		public IDomainObjectContainer Container { set; protected get; }
		public AsarRepository AsarRepository { set; protected get; }
		#endregion

		#region Life Cycle Methods
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
		public virtual long ActivityId { get; set; }
		#endregion

		#region Get Properties
		[NotMapped]
		[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(false, "FileAttachment")]
		public IList<Attachment> Attachments
		{
			get
			{
				//List<FileAttachment> files = new List<FileAttachment>();
				IList<Attachment> attachments =
						Container.Instances<Attachment>().Where(w => w.Activity.ActivityId == this.ActivityId).ToList();
				//foreach (Attachment att in attachments)
				//{
				//    if (att.FileContent != null)
				//        files.Add (new FileAttachment(att.FileContent,att.FileName,att.FileMime){ DispositionType = "inline" });
				//}
				return attachments;
			}
		}
		#endregion

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
		public bool HideAuditFields ()
		{
			return true;
		}
		#endregion
		#endregion

		#region Behavior
		public void AddOrChangeAttachment (FileAttachment newAttachment)
		{
			Attachment oldAtt =
					Container.Instances<Attachment>()
							.Where(w => w.FileName == newAttachment.Name && w.FileMime == newAttachment.MimeType)
							.FirstOrDefault();

			if (oldAtt != null)
			{
				oldAtt.FileContent = newAttachment.GetResourceAsByteArray();
				oldAtt.FileName = newAttachment.Name;
				oldAtt.FileMime = newAttachment.MimeType;
			}
			else
			{
				Attachment att = Container.NewTransientInstance<Attachment>();
				att.FileContent = newAttachment.GetResourceAsByteArray();
				att.FileName = newAttachment.Name;
				att.FileMime = newAttachment.MimeType;
				att.Activity = this;
				Container.Persist(ref att);
			}
		}
		//public bool HideAddOrChangeAttachment()
		//{
		//    if (this is Email)
		//    {
		//        Email email = (Email)this;
		//        if (email.IsEmailSent)
		//            return true;
		//    }
		//    return false;
		//}
		public void RemoveAttachment (Attachment attachment)
		{
			Container.DisposeInstance(attachment);
		}
		public IList<Attachment> Choices0RemoveAttachment ()
		{
			IList<Attachment> attachments =
					Container.Instances<Attachment>().Where(w => w.Activity.ActivityId == this.ActivityId).ToList();

			return attachments;
		}
		//public bool HideRemoveAttachment()
		//{
		//    if (this is Email)
		//    {
		//        Email email = (Email)this;
		//        if (email.IsEmailSent)
		//            return true;
		//    }
		//    return false;
		//}
		#endregion

		#region Edit Meter Class Enable Disable
		public string DisablePropertyDefault ()
		{
			//if (this is Email)
			//{
			//    Email email = (Email)this;
			//    if (email.IsEmailSent)
			//        return "You do not have permission to Edit";
			//}
			return null;
		}
		#endregion
	}
}
