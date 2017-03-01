using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.MasterData;
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
using static KhelaGhar.AMS.Model.Domain.MasterData.Designation;

namespace KhelaGhar.AMS.Model.Domain.Committees
{
    [DisplayName("কমিটি")]
    public class Committee
    {
        public string Title ()
        {
            string title = String.Empty;
            if (this.Asar is ShakhaAsar)
            {
                title = this.Asar.Name + " কমিটি - " + this.DateOfFormation.Year;
            }
            else
            {
                title = this.Asar.Area.Name + " কমিটি - " + this.DateOfFormation.Year;
            }

            return title;
        }

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
        public virtual int CommitteeId { get; set; }

        [DisplayName("কমিটি গঠনের তারিখ"), MemberOrder(10), Required]
        [Mask("d")]
        public virtual DateTime DateOfFormation { get; set; }
        [DisplayName("মেয়াদ শেষের তারিখ"), MemberOrder(15), Optionally]
        [Mask("d"), Disabled]
        public virtual DateTime? DateOfExpiration { get; set; }
        [DisplayName("কমিটির ধরণ"), MemberOrder(20), Required]
        public virtual TypeOfCommittee CommitteeType { get; set; }

        public enum TypeOfCommittee
        {
            পূর্ণাঙ্গ = 1,
            আহ্বায়ক = 2,
            কমিটিবিহীন = 3
        }

        #endregion

        #region Get Properties

        #region Committee Members
        [MemberOrder(30), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কমিটির মেম্বরগণ")]
        [TableView(false, "Worker", "Designation")]
        public IList<CommitteeMember> Members
        {
            get
            {
                return Container.Instances<CommitteeMember>().Where(w => w.Committee.CommitteeId == this.CommitteeId).OrderBy(o => o.Designation.DesignationOrder).ToList();
            }
        }
        #endregion

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

        [MemberOrder(100), NakedObjectsIgnore]
        [DisplayName("আসর"), Required]
        public virtual Asar Asar { get; set; }

        #endregion

        #region Behavior
        #region Add Committee Member
        public void AddCommitteeMember (Worker কর্মী, Designation পদবী)
        {
            CommitteeMember member = Container.NewTransientInstance<CommitteeMember>();
            member.Worker = কর্মী;
            member.Designation = পদবী;
            member.Committee = this;
            Container.Persist(ref member);
        }

        [PageSize(10)]
        public IQueryable<Worker> AutoComplete0AddCommitteeMember ([MinLength(1)] string name)
        {
            return Container.Instances<Worker>().Where(w => w.Name.Contains(name) && w.Asar.AsarId == this.Asar.AsarId).OrderBy(o => o.Name);
        }

        [PageSize(10)]
        public IQueryable<Designation> AutoComplete1AddCommitteeMember ([MinLength(1)] string নাম)
        {
            if (this.CommitteeType == TypeOfCommittee.আহ্বায়ক)
            {
                return Container.Instances<Designation>().Where(w => w.Name.StartsWith(নাম) && w.DesignationType == TypeOfDesignation.আহ্বায়ক_কমিটি).OrderBy(o => o.DesignationOrder);
            }
            else
            {
                if (this.Asar is KendrioAsar)
                {
                    return Container.Instances<Designation>().Where(w => w.Name.StartsWith(নাম) && w.DesignationType == TypeOfDesignation.কেন্দ্রীয়_কমিটি).OrderBy(o => o.DesignationOrder);
                }
                if (this.Asar is UpojelaAsar || this.Asar is MohanagarAsar || this.Asar is JelaAsar)
                {
                    return Container.Instances<Designation>().Where(w => w.Name.StartsWith(নাম) && w.DesignationType == TypeOfDesignation.জেলা_মহানগর_উপজেলা_কমিটি).OrderBy(o => o.DesignationOrder);
                }

                return Container.Instances<Designation>().Where(w => w.Name.StartsWith(নাম) && w.DesignationType == TypeOfDesignation.শাখা_আসর_কমিটি).OrderBy(o => o.DesignationOrder);
            }
        }
        #endregion

        #region Remove Committee Member
        public void RemoveCommitteeMember (CommitteeMember কর্মী)
        {
            Container.DisposeInstance(কর্মী);
        }
        public IList<CommitteeMember> Choices0RemoveCommitteeMember ()
        {
            return Container.Instances<CommitteeMember>().Where(w => w.Committee.CommitteeId == this.CommitteeId).OrderBy(o => o.Worker.Name).ToList();
        }
        #endregion

        #endregion
    }
}
