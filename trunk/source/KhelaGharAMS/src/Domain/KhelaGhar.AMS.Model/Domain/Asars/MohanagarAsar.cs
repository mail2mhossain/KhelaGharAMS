using KhelaGhar.AMS.Model.Domain.Conferences;
using KhelaGhar.AMS.Model.Domain.Workers;
using NakedObjects;
using NakedObjects.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Asars
{
  [DisplayName("মহানগর আসর")]
  public class MohanagarAsar : Asar
  {
    #region Get Properties

    #region Workers
    [MemberOrder(60), NotMapped]
    [DisplayName("কর্মী সংখ্যা")]
    public int Workers
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

        return Container.Instances<Worker>().Where(d => allchildrenids.Contains(d.Asar.Area.AreaId)).Count();
      }
    }
    #endregion

    #region সক্রিয় আসরসমূহ
    [MemberOrder(110), NotMapped]
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("সক্রিয় আসরসমূহ")]
    [TableView(true, "Name", "DateOfEstablishment", "TotalMembers", "Workers", "CommitteeType")]
    public IList<ShakhaAsar> AllActiveAsars
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

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
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

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
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

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
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

        return Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId) && (int)d.AsarStatus == (int)ShakhaAsar.StatusOfAsar.নিষ্ক্রিয়).OrderBy(o => o.Name).ToList();
      }
    }
    #endregion

    #region সভ্য সংখ্যা
    [MemberOrder(50), NotMapped]
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("সভ্য সংখ্যা")]
    public int Members
    {
      get
      {
        List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

        IList<ShakhaAsar> asars = Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId)).ToList();

        if (asars.Count > 0)
          return asars.Sum(s => s.TotalMembers);

        return 0;
      }
    }
    #endregion

    #endregion
    public static void Menu(IMenu menu)
    {
      IMenu sub = menu.CreateSubMenu("কর্মী");
      sub.AddAction("AddWorker");
      sub.AddAction("ShowAllWorkers");

      sub = menu.CreateSubMenu("সম্মেলন");
      sub.AddAction("NewConference");
      sub.AddAction("ShowAllConferences");
    }
  }
}
