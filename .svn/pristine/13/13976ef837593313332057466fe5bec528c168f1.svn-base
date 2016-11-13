using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NakedObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace KhelaGhar.AMS.Model.Domain
{
    [DisplayName("আসর")]
    public class Asar
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        #region Primitive Properties

        [Key, Hidden]
        public virtual int Id { get; set; }

        [MemberOrder(10), Title]
        [DisplayName("নাম")]
        [MaxLength(250),Required]
        public virtual string Name { get; set; }

        [DisplayName("প্রতিষ্ঠার তারিখ"), Mask("d")]
        [MemberOrder(20), Required]
        public virtual DateTime DateOfEstablishment { get; set; }

        [DisplayName("ভাই-বোনের সংখ্যা")]
        [MemberOrder(30)]
        public virtual int TotalMembers { get; set; }

        [MemberOrder(40), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("ঠিকানা")]
        [MaxLength(350)]
        public virtual string AddressLine { get; set; }

        [MemberOrder(50)]
        [DisplayName("বিভাগ"), Required]
        public virtual Division Division { get; set; }

        [MemberOrder(60)]
        [DisplayName("জেলা"), Required]
        public virtual District District { get; set; }

        public IList<District> ChoicesDistrict(Division division)
        {
            IList<District> districts = new List<District>();

            if (division != null)
            {
                districts = (from d in Container.Instances<District>()
                             where d.Division.Id == division.Id
                             select d).ToList();
            }

            return districts;
        }

        [MemberOrder(70)]
        [DisplayName("উপজেলা"), Required]
        public virtual SubDistrict SubDistrict { get; set; }       

        public IList<SubDistrict> ChoicesSubDistrict(District district)
        {
            IList<SubDistrict> subdistricts = new List<SubDistrict>();

            if (district != null)
            {
                subdistricts = (from d in Container.Instances<SubDistrict>()
                                where d.District.Id == district.Id
                                select d).ToList();
            }

            return subdistricts;
        }

        [DisplayName("আসরের অবস্থা"), MemberOrder(80), Required]
        [EnumDataType(typeof(AllEnums.AsarStatus))]
        public virtual int AsarStatus { get; set; }

        [MemberOrder(90), NotMapped]
        [DisplayName("কমিটির ধরণ")]
        public string CommitteeType
        {
            get
            {
                Committee com = GetLatestCommittee();

                if (com != null)
                {
                    AllEnums.CommitteeType c = (AllEnums.CommitteeType)com.CommitteeType;
                    return c.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public bool HideCommitteeType()
        {
            //Committee com = GetLatestCommittee();

            //if (com != null)
            if(this.CommitteeType != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [MemberOrder(100), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কার্যক্রম")]
        public virtual IList<Activity> AllActivities
        {
            get
            {
                IList<Activity> allActivities = (from activity in Container.Instances<AsarActivity>()
                                                 where activity.Asar.Id == this.Id
                                                 select activity.Activity).ToList();
                return allActivities;
            }
        }

        public bool HideAllActivities()
        {
            if (this.AllActivities.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        public string ValidateName(string asarname)
        {
            var rb = new ReasonBuilder();

            Asar asar = (from obj in Container.Instances<Asar>()
                         where obj.Name == asarname                             
                         select obj).FirstOrDefault();

            if (asar != null)
            {
                if (this.Id != asar.Id)
                {
                    rb.AppendOnCondition(true, "Duplicate Asar Name");
                }
            }
            return rb.Reason;
        }

        #region AddKormi (Action)

        [MemberOrder(Sequence = "1", Name = "খ. কর্মী")]
        [DisplayName("নতুন কর্মী")]
        public void AddKormi( [MaxLength(250), Required] string name,[MaxLength(11), Required] string mobileNo)
        {
            Kormi kormi = Container.NewTransientInstance<Kormi>();

            kormi.Name = name;
            kormi.MobileNo = mobileNo;

            kormi.Division = this.Division;
            kormi.District = this.District;
            kormi.SubDistrict = this.SubDistrict;
            kormi.Asar = this;

            Container.Persist(ref kormi);
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ShowAllKormi (Action)

        [MemberOrder(Sequence = "2", Name = "খ. কর্মী")]
        [DisplayName("সকল কর্মী")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IList<Kormi> ShowAllKormi( )
        {
            IList<Kormi> kormis = (from c in Container.Instances<Kormi>()
                                    where c.Asar.Id == this.Id
                                    select c).ToList();

            return kormis;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region AddActivity (Action)

        [MemberOrder(Sequence = "1", Name = "ক. কার্যক্রম")]
        [DisplayName("নতুন কার্যক্রম")]
        public void AddActivity(IEnumerable<Activity> activities)
        {
            foreach (Activity activity in activities)
            {
                AsarActivity newactivity = Container.NewTransientInstance<AsarActivity>();
                newactivity.Asar = this;
                newactivity.Activity = activity;

                Container.Persist(ref newactivity);
            }
        }

        public Activity[] Choices0AddActivity()
        {
            IList<int> oldActivities = (from activity in Container.Instances<AsarActivity>()
                                             where activity.Asar.Id == this.Id
                                             select activity.Activity.ActivityId).ToList();

            return (from activity in Container.Instances<Activity>()
                    where (!oldActivities.Contains(activity.ActivityId))
                    select activity).ToArray();
        }
        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region RemoveActivity (Action)

        [MemberOrder(Sequence = "2", Name = "ক. কার্যক্রম")]
        [DisplayName("কার্যক্রম অপসারণ")]
        public void RemoveActivity( IEnumerable<Activity> activities)
        {
            foreach (Activity activity in activities)
            {
                AsarActivity asaractivity = (from act in Container.Instances<AsarActivity>()
                                             where act.Asar.Id == this.Id
                                             && act.Activity.ActivityId == activity.ActivityId
                                             select act).First();

                Container.DisposeInstance(asaractivity);
            }
        }

        public Activity[] Choices0RemoveActivity()
        {
            
            return (from activity in Container.Instances<AsarActivity>()
                    where activity.Asar.Id == this.Id
                    select activity.Activity).ToArray();
        }
        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region NewCommittee (Action)

        [MemberOrder(Sequence = "3", Name = "গ. কমিটি")]
        [DisplayName("নতুন কমিটি")]
        public Committee NewCommittee([EnumDataType(typeof(AllEnums.CommitteeType))]int CommitteeType, int TotalMembers, DateTime DateOfFormation)
        {
            Committee com = Container.NewTransientInstance<Committee>();
            AsarCommittee asarcom = Container.NewTransientInstance<AsarCommittee>();

            com.CommitteeType = CommitteeType;
            com.TotalMembers = TotalMembers;
            com.DateOfFormation = DateOfFormation;

            Container.Persist(ref com);

            asarcom.Asar = this;
            asarcom.Committee = com;

            Container.Persist(ref asarcom);

            com.CustomeTitle = ((AllEnums.CommitteeType)com.CommitteeType).ToString() + " কমিটি, " + this.Name;
            return com;
        }
  
        public string Validate2NewCommittee(DateTime DateOfFormation)
        {
            var rb = new ReasonBuilder();

            Committee com = GetLatestCommittee();

            if (com != null)
            {
                if (com.DateOfFormation <= DateOfFormation)
                {
                    rb.AppendOnCondition(true, "Committee already exist");
                }
            }
            return rb.Reason;
        }
      
        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ShowCurrentCommittee (Action)

        [MemberOrder(Sequence = "4", Name = "গ. কমিটি")]
        [DisplayName("বর্তমান কমিটি")]
        public Committee ShowCurrentCommittee( )
        {
            return GetLatestCommittee();
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        //#region ShowCommitteeMembers (Action)

        //[MemberOrder(Sequence = "5", Name = "কমিটি মেম্বার")]
        //[DisplayName("কমিটির মেম্বার")]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        //public IList<CommitteeMember> ShowCommitteeMembers()
        //{
        //    Committee com = GetLatestCommittee();

        //    IList<CommitteeMember> member = (from c in Container.Instances<CommitteeMember>()
        //                                     where c.Committee.CommitteeId == com.CommitteeId
        //                                     select c).ToList();
        //    return member;
        //}

        //// Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        //#endregion

        #region ShowAllCommittees (Action)

        [MemberOrder(Sequence = "6", Name = "গ. কমিটি")]
        [DisplayName("সকল কমিটি")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IList<Committee> ShowAllCommittees( )
        {
            IList<Committee> com = (from c in Container.Instances<AsarCommittee>()
                                    where c.Asar.Id == this.Id
                                    select c.Committee).OrderByDescending(o => o.DateOfFormation).ToList();

            return com;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        private Committee GetLatestCommittee()
        {
            Committee com = (from c in Container.Instances<AsarCommittee>()
                             where c.Asar.Id == this.Id
                             select c.Committee).OrderByDescending(o => o.DateOfFormation).FirstOrDefault();

            if (com != null)
            {
                com.CustomeTitle = ((AllEnums.CommitteeType)com.CommitteeType).ToString() + " কমিটি, " + this.Name;
            }

            return com;
        }
    }
}
