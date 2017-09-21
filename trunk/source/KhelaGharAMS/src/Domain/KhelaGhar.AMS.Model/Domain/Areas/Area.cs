using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Workers;
using KhelaGhar.AMS.Model.Repository;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KhelaGhar.AMS.Model.Domain.Asars.ShakhaAsar;
using static KhelaGhar.AMS.Model.Domain.Committees.Committee;

namespace KhelaGhar.AMS.Model.Domain.Areas
{
  public class Area
  {
    public string Title()
    {
      var t = Container.NewTitleBuilder();
      string title = GetTitle(this);
      t.Append(title);
      return t.ToString();
    }

    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    public AsarRepository AsarRepository { set; protected get; }
    #endregion

    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual int AreaId { get; set; }

    [MemberOrder(10)]
    [DisplayName("নাম"), Required]
    [MaxLength(150)]
    public virtual string Name { get; set; }

    [MemberOrder(20)]
    [DisplayName("বর্ণনা")]
    [MaxLength(150), Optionally]
    public virtual string Description { get; set; }

    #endregion

    #region Get Properties
    #region সক্রিয় আসরসমূহ
    [MemberOrder(110), NotMapped]
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("সক্রিয় আসরসমূহ")]
    [TableView(true, "Name", "DateOfEstablishment", "TotalMembers", "Workers", "CommitteeType")]
    public IList<ShakhaAsar> AllActiveAsars
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this);

        return Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId) && (int)d.AsarStatus == (int)ShakhaAsar.StatusOfAsar.সচল).OrderBy(o => o.Name).ToList();
      }
    }
    #endregion

    #region নতুন আসরসমূহ
    [MemberOrder(120), NotMapped]
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("নতুন আসরসমূহ")]
    [TableView(true, "Name", "DateOfEstablishment", "TotalMembers", "Kormies", "CommitteeType")]
    public IList<ShakhaAsar> AllNewAsars
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this);

        return Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId) && (int)d.AsarStatus == (int)ShakhaAsar.StatusOfAsar.নতুন).OrderBy(o => o.Name).ToList();
      }
    }
    #endregion

    #region পুনর্জাগরিত আসরসমূহ
    [MemberOrder(130), NotMapped]
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("পুনর্জাগরিত আসরসমূহ")]
    [TableView(true, "Name", "DateOfEstablishment", "TotalMembers", "Kormies", "CommitteeType")]
    public IList<ShakhaAsar> AllRevivedAsars
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this);

        return Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId) && (int)d.AsarStatus == (int)ShakhaAsar.StatusOfAsar.পুনর্জাগরণ).OrderBy(o => o.Name).ToList();
      }
    }
    #endregion

    #region নিষ্ক্রিয় আসরসমূহ
    [MemberOrder(140), NotMapped]
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("নিষ্ক্রিয় আসরসমূহ")]
    [TableView(true, "Name", "DateOfEstablishment", "TotalMembers", "Kormies", "CommitteeType")]
    public IList<ShakhaAsar> AllInactiveAsars
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this);

        return Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId) && (int)d.AsarStatus == (int)ShakhaAsar.StatusOfAsar.নিষ্ক্রিয়).OrderBy(o => o.Name).ToList();
      }
    }
    #endregion
    #endregion

    #region Navigation Properties

    [MemberOrder(100), NakedObjectsIgnore]
    [DisplayName("এলাকা"), Optionally]
    public virtual Area Parent { get; set; }

    //[PageSize(10)]
    //public IQueryable<Area> AutoCompleteParent ([MinLength(3)] string matching)
    //{
    //	List<Area> areas = new List<Area>();
    //	if (this.Parent != null)
    //	{
    //		areas = (from a in Container.Instances<Area>()
    //						 where a.Name.StartsWith(matching)
    //						 //&& a.AreaType.AreaTypeId == this.Parent.AreaType.AreaTypeId
    //						 select a).ToList();
    //	}
    //	return areas.AsQueryable();
    //}

    #endregion

    #region Private Methods
    private string GetTitle(Area area)
    {
      if (area == null)
      {
        return "";
      }
      if (area.Parent == null)
      {
        return area.Name + GetAreaType(area);
      }
      else
      {
        return area.Name + GetAreaType(area) + " - " + GetTitle(area.Parent);
      }
    }
    private string GetAreaType(Area area)
    {
      if (area is Division)
        return " বিভাগ";

      if (area is District)
        return " জেলা";

      if (area is MetropolitanCity)
        return "";

      if (area is SubDistrict)
        return " উপজেলা";

      return "";
    }
    #endregion

    #region Behavior
    #region New Asar
    [MemberOrder(Sequence = "20")]
    //[AuthorizeAction(Roles = "AMSAdmin")]
    [DisplayName("নতুন শাখা আসর")]
    public ShakhaAsar CreateAsar([MaxLength(250)]string নাম, [Optionally]DateTime? প্রতিষ্ঠার_তারিখ, StatusOfAsar আসরের_অবস্থা, TypeOfCommittee কমিটির_ধরণ, [MultiLine(NumberOfLines = 3, Width = 50), Optionally]string ঠিকানা)
    {
      ShakhaAsar asar = Container.NewTransientInstance<ShakhaAsar>();
      asar.Name = নাম;
      asar.DateOfEstablishment = প্রতিষ্ঠার_তারিখ;
      asar.AsarStatus = আসরের_অবস্থা;
      asar.CommitteeType = কমিটির_ধরণ;
      asar.AddressLine = ঠিকানা;
      asar.Area = this;

      Container.Persist(ref asar);
      return asar;
    }

    public string Validate0CreateAsar(string নাম)
    {
      return AsarRepository.Validate0CreateAsar(নাম);
    }
    #endregion
    #endregion
  }
}
