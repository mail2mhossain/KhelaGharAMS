using KhelaGhar.AMS.Model.Domain.Activities;
using KhelaGhar.AMS.Model.Domain.Areas;
using KhelaGhar.AMS.Model.Domain.Conferences;
using KhelaGhar.AMS.Model.Domain.Members;
using KhelaGhar.AMS.Model.Domain.Workers;
using NakedObjects;
using NakedObjects.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KhelaGhar.AMS.Model.Domain.Committees.Committee;
using KhelaGhar.AMS.Utility;

namespace KhelaGhar.AMS.Model.Domain.Asars
{
  [DisplayName("শাখা আসর")]
  public class ShakhaAsar : Asar
  {
    #region Primitive Properties
    [DisplayName("প্রতিষ্ঠার তারিখ"), Mask("d")]
    [MemberOrder(20), Optionally]
    public virtual DateTime? DateOfEstablishment { get; set; }

    [DisplayName("সভ্য")]
    [MemberOrder(50), Optionally]
    public virtual int TotalMembers { get; set; }

    [DisplayName("আসরের অবস্থা"), MemberOrder(30), Required]
    public virtual StatusOfAsar AsarStatus { get; set; }

    public enum StatusOfAsar
    {
      সচল = 1, //Active
      নিষ্ক্রিয় = 2, //Inactive
      নতুন = 3, //New
      পুনর্জাগরণ = 4 //Old But 
    }
    #endregion

    #region Get Only Properties

    #region Workers
    [MemberOrder(60), NotMapped]
    [DisplayName("কর্মী সংখ্যা")]
    public int Workers
    {
      get
      {
        return Container.Instances<Worker>().Where(w => w.Asar.AsarId == this.AsarId).Count();
      }
    }
    #endregion

    #region Weekly Activities
    [MemberOrder(65), NotMapped]
    [DisplayName("সাপ্তাহিক কার্যক্রম")]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Day", "StartTime", "EndTime")]
    public virtual IList<WeeklyActivity> WeeklyActivities
    {
      get
      {
        IList<WeeklyActivity> activities = Container.Instances<WeeklyActivity>().Where(w => w.ShakhaAsar.AsarId == this.AsarId).ToList();
        activities = activities.OrderBy(o => o.Day).OrderBy(o => Convert.ToDateTime(DateTime.Today.ToShortDateString() + " " + o.StartTime)).ToList();
        return activities;
      }
    }
    #endregion

    #region Area
    [MemberOrder(100), NotMapped]
    [DisplayName("উপজেলা/মহানগর")]
    public Area DisplayArea
    {
      get
      {
        return this.Area;
      }
    }

    #endregion

    #endregion

    #region Behavior
    #region Routine
    #region Add Weekly Activity (Action)

    [DisplayName("আসর বসার দিন")]
    public void AddWeeklyActivity(string কার্যক্রম, WeeklyActivity.WeekDays দিন, [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time."), Optionally] string শুরুর_সময়, [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time."), Optionally] string শেষ_সময়)
    {
      WeeklyActivity routine = Container.NewTransientInstance<WeeklyActivity>();
      routine.ActivityName = কার্যক্রম;
      routine.Day = দিন;
      routine.StartTime = শুরুর_সময়;
      routine.EndTime = শেষ_সময়;

      routine.ShakhaAsar = this;

      Container.Persist(ref routine);
    }

    public IList<string> Choices0AddWeeklyActivity()
    {
      return AsarRepository.GetWeeklyActivities();
    }
    public string ValidateAddWeeklyActivity(string কার্যক্রম, WeeklyActivity.WeekDays দিন, string শুরুর_সময়, string শেষ_সময়)
    {
      DateTime startDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + শুরুর_সময়);
      DateTime endDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + শেষ_সময়);

      if (startDate > endDate)
      {
        return "End time must be greater than start time";
      }

      return null;
    }

    #endregion

    #region RemoveActivity (Action)

    //[MemberOrder(Sequence = "2", Name = "ক. কার্যক্রম")]
    [DisplayName("কার্যক্রম অপসারণ")]
    public void RemoveWeeklyActivity(IEnumerable<WeeklyActivity> কার্যক্রমসমূহ)
    {
      foreach (WeeklyActivity activity in কার্যক্রমসমূহ)
      {
        Container.DisposeInstance(activity);
      }
    }

    public IQueryable<WeeklyActivity> Choices0RemoveWeeklyActivity()
    {
      return (from activity in Container.Instances<WeeklyActivity>()
              where activity.ShakhaAsar.AsarId == this.AsarId
              select activity);
    }
    // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
    #endregion
    #endregion

    #region Member (Shobbho)

    #region AddMember (Action)
    [DisplayName("নতুন সভ্য")]
    public Member AddMember(string নাম, DateTime জন্ম_তারিখ, string অভিভাবক১, [Optionally]string অভিভাবক২, string মোবাইল, [Optionally]string শিক্ষা_প্রতিষ্ঠানের_নাম, [Optionally]string শ্রেণী, string রক্তের_গ্রুপ, string ঠিকানা)
    {
      Member member = Container.NewTransientInstance<Member>();
      member.Name = নাম;
      member.DOB = জন্ম_তারিখ;
      member.Guardian1 = অভিভাবক১;
      member.Guardian2 = অভিভাবক২;
      member.MobileNo = মোবাইল;
      member.SchoolName = শিক্ষা_প্রতিষ্ঠানের_নাম;
      member.SchoolStandard = শ্রেণী;
      member.BloodGroup = রক্তের_গ্রুপ;
      member.PresentAddress = ঠিকানা;
      member.Asar = this;
      Container.Persist(ref member);
      return member;
    }
    public IList<string> Choices7AddMember()
    {
      return AllEnums.GetBloodGroup();
    }
    #endregion

    #region Search Members (Action)
    [DisplayName("মোবাইল নং দিয়ে খোঁজ")]
    public Member SearchByMobile(string মোবাইল)
    {
      string mobileNo = মোবাইল.Right(11);

      return Container.Instances<Member>().Where(w => w.MobileNo.Contains(mobileNo)).FirstOrDefault();
    }
    #endregion

    #endregion

    #region Note (Action)
    #region NewNote (Action)

    #endregion

    #region ShowActivities (Action)

    #endregion

    #endregion
    #endregion

    #region Menu
    public static void Menu(IMenu menu)
    {
      var sub = menu.CreateSubMenu("কার্যক্রম");
      sub.AddAction("AddWeeklyActivity");
      sub.AddAction("RemoveWeeklyActivity");

      sub = menu.CreateSubMenu("সভ্য");
      sub.AddAction("AddMember");
      sub.AddAction("SearchByMobile");

      sub = menu.CreateSubMenu("কর্মী");
      sub.AddAction("AddWorker");
      sub.AddAction("ShowAllWorkers");

      sub = menu.CreateSubMenu("কমিটি");
      sub.AddAction("CreateConvenerCommittee");
      sub.AddAction("ShowCommitteeMembers");
      //sub.AddAction("ShowAllCommittees");

      sub = menu.CreateSubMenu("সম্মেলন");
      sub.AddAction("NewConference");
      sub.AddAction("ShowAllConferences");
    }
    #endregion
  }
}
