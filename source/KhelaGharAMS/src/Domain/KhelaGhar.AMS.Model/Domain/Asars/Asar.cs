using KhelaGhar.AMS.Model.Domain.Areas;
using KhelaGhar.AMS.Model.Domain.Committees;
using KhelaGhar.AMS.Model.Domain.Conferences;
using KhelaGhar.AMS.Model.Domain.Members;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Domain.Workers;
using KhelaGhar.AMS.Model.Repository;
using KhelaGhar.AMS.Utility;
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

namespace KhelaGhar.AMS.Model.Domain.Asars
{
  [DisplayName("আসর")]
  public class Asar
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    public AsarRepository AsarRepository { set; protected get; }
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

    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual int AsarId { get; set; }

    [MemberOrder(10), Title]
    [DisplayName("নাম")]
    [MaxLength(250), Required]
    public virtual string Name { get; set; }

    [DisplayName("কমিটির ধরণ"), MemberOrder(40), Required]
    public virtual TypeOfCommittee CommitteeType { get; set; }

    [MemberOrder(250), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
    [DisplayName("ঠিকানা")]
    [MaxLength(350)]
    public virtual string AddressLine { get; set; }
    [Optionally, NakedObjectsIgnore]
    public virtual decimal? Latitude { get; set; }
    [Optionally, NakedObjectsIgnore]
    public virtual decimal? Longitude { get; set; }

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
    [MemberOrder(100), Required, NakedObjectsIgnore]
    [DisplayName("উপজেলা/মহানগর")]
    public virtual Area Area { get; set; }

    [PageSize(10)]
    public IQueryable<Area> AutoCompleteArea([MinLength(3)] string matching)
    {
      IQueryable<Area> areas = (from a in Container.Instances<Area>()
                                where a.Name.StartsWith(matching)

                                select a);
      return areas;
    }

    //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts
    #endregion

    #region Get Properties
    #region Last Conference Date
    [MemberOrder(20), NotMapped]
    [DisplayName("সর্বশেষ সম্মেলনের তারিখ")]
    [Mask("d")]
    public DateTime? LastConferenceDate
    {
      get
      {
        Conference conference = Container.Instances<Conference>().Where(w => w.Asar.AsarId == this.AsarId).OrderByDescending(o => o.StartDate).FirstOrDefault();

        if (conference != null)
        {
          return conference.StartDate;
        }
        return null;
      }
    }

    #endregion
    #endregion

    #region Behavior

    #region Add Worker (Action)
    [DisplayName("নতুন কর্মী")]
    public void AddWorker(string নাম, [RegEx(Validation = @"^(?:\+88|01)?\d{11}\r?$", Message = "Not a valid mobile no"), Optionally]string মোবাইল, [RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address"), Optionally]string ইমেইল, [Mask("d"), Optionally] DateTime? জন্মতারিখ)
    {
      Worker worker = Container.NewTransientInstance<Worker>();
      worker.Name = নাম;
      worker.MobileNo = মোবাইল;
      worker.Email = ইমেইল;
      worker.DOB = জন্মতারিখ;
      worker.Asar = this;
      Container.Persist(ref worker);
    }
    public string Validate1AddWorker(string মোবাইল)
    {
      //string mobile = মোবাইল.Right(11);

      //Worker worker = Container.Instances<Worker>().Where(w => w.MobileNo.Contains(mobile)).FirstOrDefault();
      //if (worker != null)
      //{
      //	return "এই কর্মী " + worker.Asar.Name + " এ কাজ করছেন।";
      //}
      return null;
    }
    public string Validate2AddWorker(string ইমেইল)
    {
      //if (!String.IsNullOrEmpty(ইমেইল))
      //{
      //	Worker worker = Container.Instances<Worker>().Where(w => w.Email.Contains(ইমেইল)).FirstOrDefault();
      //	if (worker != null)
      //	{
      //		return "এই কর্মী " + worker.Asar.Name + " এ কাজ করছেন।";
      //	}
      //}
      return null;
    }
    #endregion

    #region ShowAllWorkers
    [DisplayName("কর্মীবৃন্দ")]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Name", "MobileNo", "Email", "Status", "Asar")]
    public IList<Worker> ShowAllWorkers()
    {
      return Container.Instances<Worker>().Where(w => w.Asar.AsarId == this.AsarId).OrderBy(o => o.Name).ToList();
    }
    #endregion

    #region Create Convener Committee
    [DisplayName("আহ্বায়ক কমিটি গঠন")]
    public ConvenerCommittee CreateConvenerCommittee(DateTime dateOfFormation)
    {
      ConvenerCommittee committee = Container.NewTransientInstance<ConvenerCommittee>();
      committee.DateOfExpiration = dateOfFormation;
      committee.CommitteeType = TypeOfCommittee.আহ্বায়ক;
      committee.Asar = this;
      Container.Persist(ref committee);

      this.CommitteeType = TypeOfCommittee.আহ্বায়ক;
      Committee lastCommittee = GetCurrentCommittee();

      if (lastCommittee != null)
      {
        lastCommittee.DateOfExpiration = dateOfFormation.AddDays(-1);
      }
      return committee;
    }
    //public bool HideConvenerCommittee()
    //{
    //  IList<Committee> committees = Container.Instances<Committee>().Where(w => w.Asar.AsarId == this.AsarId).ToList();

    //  if(committees.Count > 0)
    //  {
    //    return true;
    //  }
    //  return false;
    //}
    #endregion

    #region Show Current Committee Members
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("কমিটির মেম্বরগণ")]
    [TableView(false, "Worker", "Designation")]
    public IList<CommitteeMember> ShowCommitteeMembers()
    {
      Committee committee = GetCurrentCommittee();
      return Container.Instances<CommitteeMember>().Where(w => w.Committee.CommitteeId == committee.CommitteeId).OrderBy(o => o.Designation.DesignationOrder).ToList();
    }
    #endregion

    #region New Conference
    [DisplayName("সম্মেলন")]
    public Conference NewConference(DateTime startDate, DateTime endDate, [Optionally] string slogan)
    {
      Conference conference = Container.NewTransientInstance<Conference>();
      conference.StartDate = startDate;
      conference.EndDate = endDate;
      conference.Slogan = slogan;
      conference.Asar = this;
      Container.Persist(ref conference);
      return conference;
    }
    #endregion

    #region Show All Conferences
    public IList<Conference> ShowAllConferences()
    {
      IList<Conference> conferences = Container.Instances<Conference>().Where(w => w.Asar.AsarId == this.AsarId).OrderBy(o => o.StartDate).ToList();
      return conferences;

    }
    #endregion
    private Committee GetCurrentCommittee()
    {
      Committee committee = Container.Instances<Committee>().Where(w => w.Asar.AsarId == this.AsarId && w.DateOfExpiration != null).FirstOrDefault();

      return committee;
    }
    #endregion
  }
}