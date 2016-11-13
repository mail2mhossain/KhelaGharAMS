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

        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.

        public virtual void Persisting()
        {
          AuditFields.InsertedBy = Container.Principal.Identity.Name;
          AuditFields.InsertedDateTime = DateTime.Now;
          AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
          AuditFields.LastUpdatedDateTime = DateTime.Now;

          if (this.DateOfEstablishment == null)
          {
              this.DateOfEstablishment = Convert.ToDateTime("1/1/1753");
          }
        }
        public virtual void Updating()
        {
          AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
          AuditFields.LastUpdatedDateTime = DateTime.Now;

          //if (this.DateOfEstablishment == null)
          //{
          //    DateTime? date = null;
          //    this.DateOfEstablishment = date;  //Convert.ToDateTime("1/1/1753");
          //}
        }
        #endregion

        #region Primitive Properties

        [Key, Hidden]
        public virtual int Id { get; set; }

        [MemberOrder(1), Title]
        [DisplayName("নাম")]
        [MaxLength(250),Required]
        public virtual string Name { get; set; }

        [DisplayName("প্রতিষ্ঠার তারিখ"), Mask("d")]
        [MemberOrder(10),Optionally]
        //[Column(TypeName = "datetime2")]
        public virtual DateTime? DateOfEstablishment { get; set; }

        //public DateTime? DefaultDateOfEstablishment()
        //{
        //    return Convert.ToDateTime("1/1/1753");
        //}


        [DisplayName("সভ্য")]
        [MemberOrder(50), Required]
        public virtual int TotalMembers { get; set; }

        [MemberOrder(90), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("ঠিকানা")]
        [MaxLength(350)]
        public virtual string AddressLine { get; set; }

        [DisplayName("আসরের অবস্থা"), MemberOrder(40), Required]
        [EnumDataType(typeof(AllEnums.AsarStatus))]
        public virtual int AsarStatus { get; set; }

        #endregion

        #region Get Only Properties

        [MemberOrder(30), NotMapped]
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
          if (this.CommitteeType != "")
          {
            return false;
          }
          else
          {
            return true;
          }
        }

        [MemberOrder(20), NotMapped]
        [DisplayName("সর্বশেষ সম্মেলনের তারিখ")]
        [Mask("d")]
        public DateTime? LastConferenceDate
        {
            get
            {
                Committee com = GetLatestCommittee();

                if (com != null)
                {
                    return com.DateOfFormation;
                    
                }
                else
                {
                    return null;
                }
            }
        }

        public bool HideLastConferenceDate()
        {
            //Committee com = GetLatestCommittee();

            //if (com != null)
            if (this.LastConferenceDate != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [MemberOrder(70), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("আসর বসে")]
        [TableView(false, "Day", "StartTime", "EndTime")]
        public IList<AsarRoutine> Routines
        {
          get
          {
            IList<AsarRoutine> routines = GetRoutines();
            return routines;
          }
        }

        public bool HideRoutines()
        {
          if (Container.IsPersistent(this))
          {
            return false;
          }
          else
          {
            return true;
          }
        }

        [MemberOrder(80), NotMapped]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কার্যক্রম")]
        [TableView(false, "ActivityName")]
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
          if (Container.IsPersistent(this))
          {
            return false;
          }
          else
          {
            return true;
          }
        }

        [MemberOrder(60), NotMapped]
        [DisplayName("কর্মী সংখ্যা")]
        public int Kormies
        {
          get
          {
            return this.ShowAllKormi().Count;
          }
        }

        public bool HideKormies()
        {
          if (Container.IsPersistent(this))
          {
            return false;
          }
          else
          {
            return true;
          }
        }
        #endregion

        #region Complex Properties
        #region AuditFields (AuditFields)

        private AuditFields _auditFields = new AuditFields();

        [MemberOrder(250)]
        [Hidden, Required]
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
        [MemberOrder(100), Required]
        [DisplayName("এলাকা")]
        public virtual Area Area { get; set; }

        [PageSize(10)]
        public IQueryable<Area> AutoCompleteArea([MinLength(3)] string matching)
        {
          IQueryable<Area> areas = (from a in Container.Instances<Area>()
                                    where a.Name.StartsWith(matching)
                                    && (a.AreaType.AreaTypeId == (int) AllEnums.AreaType.SubDistrict ||
                                    a.AreaType.AreaTypeId == (int) AllEnums.AreaType.City ||
                                    a.AreaType.AreaTypeId == (int) AllEnums.AreaType.Thana ||
                                    a.AreaType.AreaTypeId == (int) AllEnums.AreaType.Village ||
                                    a.AreaType.AreaTypeId == (int) AllEnums.AreaType.Union ||
                                    a.AreaType.AreaTypeId == (int) AllEnums.AreaType.Ward)
                                    select a);
          return areas;
        }

        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts
        #endregion

        #region Validations
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

        #endregion

        #region Action Methods
        
        #region Activity
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

        #endregion

        #region AddRoutine (Action)

        [MemberOrder(Sequence = "3", Name = "ক. কার্যক্রম")]
        [DisplayName("আসর বসার দিন")]
        public void AddRoutine([EnumDataType(typeof(AllEnums.WeekDays))] int day, [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")] string startTime, [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")] string endTime)
        {
          AsarRoutine routine = Container.NewTransientInstance<AsarRoutine>();
          routine.Day = day;
          routine.StartTime = startTime;
          routine.EndTime = endTime;
          
          routine.Asar = this;

          Container.Persist(ref routine);
        }

        public string ValidateAddRoutine(int day, string startTime, string endTime)
        {
          DateTime startDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + startTime);
          DateTime endDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + endTime);

          if (startDate > endDate)
          {
            return "End time must be greater than start time";
          }

          return null;
        }
      
        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region Member (Shobbho)

        #region AddMember (Action)

        [MemberOrder(Sequence = "1", Name = "খ. সভ্য")]
        [DisplayName("নতুন সভ্য")]
        public Member AddMember()
        {
          Member member = Container.NewTransientInstance<Member>();

          member.Asar = this;

          return member;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region AllMembers (Action)

        [MemberOrder(Sequence = "2", Name = "খ. সভ্য")]
        [DisplayName("সকল সভ্যগণ")]
        [TableView(true, "Name", "Guardian1", "MobileNo", "DOB", "BloodGroup")]
        public IList<Member> AllMembers()
        {
          IList<Member> members = (from m in Container.Instances<Member>()
                                   where m.Asar.Id == this.Id
                                   select m).OrderBy(o => o.Name).ToList();

          return members;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #endregion

        #region Kormi

        #region AddKormi (Action)

        [MemberOrder(Sequence = "1", Name = "গ. কর্মী")]
        [DisplayName("নতুন কর্মী")]
        public void AddKormi([MaxLength(250), Required] string name, [MaxLength(25), Required] string mobileNo)
        {
          Kormi kormi = Container.NewTransientInstance<Kormi>();

          kormi.Name = name;
          kormi.MobileNo = mobileNo;

          kormi.Area = this.Area;
          kormi.Asar = this;

          Container.Persist(ref kormi);
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ShowAllKormi (Action)

        [MemberOrder(Sequence = "2", Name = "গ. কর্মী")]
        [DisplayName("সকল কর্মী")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [TableView(true, "Name", "MobileNo","Email","Area","Asar")]
        public IList<Kormi> ShowAllKormi()
        {
          IList<Kormi> kormis = (from c in Container.Instances<Kormi>()
                                 where c.Asar.Id == this.Id
                                 select c).ToList();

          return kormis;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #endregion

        #region Committee

        #region NewCommittee (Action)

        [MemberOrder(Sequence = "3", Name = "ঘ. কমিটি")]
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

        [MemberOrder(Sequence = "4", Name = "ঘ. কমিটি")]
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

        [MemberOrder(Sequence = "6", Name = "ঘ. কমিটি")]
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

        #endregion

        #region Note (Action)
        #region NewNote (Action)

        [DisplayName("নতুন টীকা")]
        [MemberOrder(1, Name = "টীকা")]
        public void NewNote(NoteType type, DateTime date, [MaxLength(350)]string বিষয়, [MaxLength(350), MultiLine(NumberOfLines = 4, Width = 100)]string বর্ণনা, [MaxLength(350), MultiLine(NumberOfLines = 4, Width = 100), Optionally]string সিদ্ধান্তসমূহ)
        {
          Note note = Container.NewTransientInstance<Note>();
          AsarNote asarnote = Container.NewTransientInstance<AsarNote>();

          note.NoteType = type;
          note.NoteDate = date;
          note.Subject = বিষয়;
          note.Description = বর্ণনা;
          note.Decision = সিদ্ধান্তসমূহ;

          Container.Persist(ref note);

          asarnote.Asar = this;
          asarnote.Note = note;

          Container.Persist(ref asarnote);

          //return activity;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ShowActivities (Action)

        [DisplayName("টীকাসমূহ")]
        [MemberOrder(1, Name = "টীকা")]
        [TableView(true, "NoteType", "NoteDateString", "Subject")]
        public IList<Note> ShowActivities(DateTime fromDate, DateTime toDate)
        {
          IList<Note> notes = (from a in Container.Instances<AsarNote>()
                                        where a.Asar.Id == this.Id
                                        && a.Note.NoteDate >= fromDate
                                        && a.Note.NoteDate <= toDate
                                        select a.Note).ToList();
          return notes;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #endregion

        #endregion

        #region Private Methods
        private Committee GetLatestCommittee()
        {
            Committee com = (from c in Container.Instances<AsarCommittee>()
                             where c.Asar.Id == this.Id
                             select c.Committee).OrderByDescending(o => o.DateOfFormation).FirstOrDefault();

            //if (com != null)
            //{
            //    com.CustomeTitle = ((AllEnums.CommitteeType)com.CommitteeType).ToString() + " কমিটি, " + this.Name;
            //}

            return com;
        }

        private IList<AsarRoutine> GetRoutines()
        {
          IList<AsarRoutine> routines = (from r in Container.Instances<AsarRoutine>()
                                         where r.Asar.Id == this.Id
                                         select r).OrderBy(o => o.Day).ToList();
          return routines;
        }
        #endregion
    }
}
