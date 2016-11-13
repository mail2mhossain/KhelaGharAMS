using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhelaGhar.AMS.Model.Domain
{
    [DisplayName("কমিটি")]
    public class Committee
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion
        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


        #endregion

        public string Title()
        {
            var t = new TitleBuilder();
            t.Append(this.CustomeTitle);
            
            return t.ToString();
        }

        [Key, Hidden]
        public virtual int CommitteeId { get; set; }

        [DisplayName("কমিটির ধরণ"), MemberOrder(20), Required]
        [EnumDataType(typeof(AllEnums.CommitteeType))]
        public virtual int CommitteeType { get; set; }

        [DisplayName("কমিটির সদস্য সংখ্যা"), MemberOrder(30), Required]
        public virtual int TotalMembers { get; set; }

        [DisplayName("কমিটি গঠনের তারিখ"), MemberOrder(40), Required]
        [Mask("d")]
        public virtual DateTime DateOfFormation { get; set; }

        [MemberOrder(60), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কমিটি মেম্বার")]
        public virtual IList<CommitteeMember> CommitteeMembers
        {
            get
            {
                IList<CommitteeMember> members = (from member in Container.Instances<CommitteeMember>()
                                                  where member.Committee.CommitteeId == this.CommitteeId
                                                  select member).ToList();
                return members;
            }
        }

        [Hidden, NotMapped]
        public string CustomeTitle { get; set; }
        
        public string ValidateTotalMembers(int TotalMembers)
        {
            var rb = new ReasonBuilder();

            if (TotalMembers < this.CommitteeMembers.Count())
            {
                rb.AppendOnCondition(true, "Invalid Number");
            }

            return rb.Reason;
        }
      
        #region AddCommitteeMember (Action)

        [MemberOrder(Sequence = "1")]
        [DisplayName("নতুন কমিটি মেম্বার")]
        public void AddCommitteeMember(Kormi kormi, Designation designation)
        {
            CommitteeMember member = Container.NewTransientInstance<CommitteeMember>();
            member.Kormi = kormi;
            member.Designation = designation;

            member.Committee = this;

            Container.Persist(ref member);
        }

        [PageSize(10)]
        public IQueryable<Kormi> AutoComplete0AddCommitteeMember([MinLength(1)] string name)
        {
            Asar asar = (from ac in Container.Instances<AsarCommittee>()
                         where ac.Committee.CommitteeId == this.CommitteeId
                         select ac.Asar).FirstOrDefault();

            if (asar != null)
            {
                return Container.Instances<Kormi>().Where(w => w.Name.StartsWith(name) && w.Asar.Id == asar.Id);
            }

            SubDistrict sub = (from ac in Container.Instances<SubDistrictCommittee>()
                               where ac.Committee.CommitteeId == this.CommitteeId
                               select ac.SubDistrict).FirstOrDefault();

            if (sub != null)
            {
                return Container.Instances<Kormi>().Where(w => w.Name.StartsWith(name) && w.SubDistrict.Id == sub.Id);
            }

            District dist = (from ac in Container.Instances<DistrictCommittee>()
                             where ac.Committee.CommitteeId == this.CommitteeId
                             select ac.District).FirstOrDefault();

            if (dist != null)
            {
                return Container.Instances<Kormi>().Where(w => w.Name.StartsWith(name) && w.District.Id == dist.Id);
            }

            CentralKhelaGhar khelaghar = (from ac in Container.Instances<CentralCommittee>()
                                          where ac.Committee.CommitteeId == this.CommitteeId
                                          select ac.CentralKhelaGhar).FirstOrDefault();

            return Container.Instances<Kormi>().Where(w => w.Name.StartsWith(name));
        }

        [PageSize(10)]
        public IQueryable<Designation> AutoComplete1AddCommitteeMember([MinLength(1)] string name)
        {
            return Container.Instances<Designation>().Where(w => w.Name.StartsWith(name) && (w.DesignationType == this.CommitteeType) || w.DesignationType == (int)AllEnums.DesignationType.উভয়_কমিটি);
        }

        public string DisableAddCommitteeMember()
        {
            var rb = new ReasonBuilder();
            if (this.TotalMembers == this.CommitteeMembers.Count())
            {
                rb.AppendOnCondition(true, "All Committee members are included");
            }
            return rb.Reason;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion
        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

    }
}

