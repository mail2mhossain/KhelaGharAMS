using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NakedObjects;
using NakedObjects.Services;
using NakedObjects.Security;
using KhelaGhar.AMS.Model.Domain;


namespace KhelaGhar.AMS.Model.Repository
{
    [DisplayName("আসর")]
    public class AsarRepository : AbstractFactoryAndRepository
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service.

        #endregion

        // 'fact' shortcut to add a factory method, 
        // 'alli' for an all-instances method
        // 'find' for a method to find a single object by query
        // 'list' for a method to return a list of objects matching a query

        #region Asar
        [MemberOrder(Sequence = "20")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নতুন আসর")]
        public Asar CreateAsar()
        {
            return Container.NewTransientInstance<Asar>();
        }

        //[MemberOrder(Sequence = "20")]
        ////[AuthorizeAction(Roles = "AMSAdmin")]
        //[DisplayName("নতুন আসর")]
        //public void CreateAsar(string Name, DateTime DateOfEstablishment, int TotalMembers, Division Division, District District, SubDistrict SubDistrict)
        //{
        //    Asar asar = Container.NewTransientInstance<Asar>();
        //    asar.Name = Name;
        //    asar.DateOfEstablishment = DateOfEstablishment;
        //    asar.TotalMembers = TotalMembers;
        //    asar.Division = Division;
        //    asar.District = District;
        //    asar.SubDistrict = SubDistrict;

        //    Container.Persist(ref asar);
        //}
        //public IList<District> Choices4CreateAsar(Division division)
        //{
        //    IList<District> districts = new List<District>();

        //    if (division != null)
        //    {
        //        districts = (from d in Container.Instances<District>()
        //                     where d.Division.Id == division.Id
        //                     select d).ToList();
        //    }

        //    return districts;
        //}

        //public IList<SubDistrict> Choices5CreateAsar(District district)
        //{
        //    IList<SubDistrict> subdistricts = new List<SubDistrict>();

        //    if (district != null)
        //    {
        //        subdistricts = (from d in Container.Instances<SubDistrict>()
        //                        where d.District.Id == district.Id
        //                        select d).ToList();
        //    }

        //    return subdistricts;
        //}

        [MemberOrder(Sequence = "30", Name = "খোঁজ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("সকল আসর")]
        [TableView(true, "DateOfEstablishment", "TotalMembers","AddressLine","SubDistrict","AsarStatus","CommitteeType","AllActivities")]
        public IQueryable<Asar> ShowAll()
        {
            return Container.Instances<Asar>().OrderBy(o=>o.Name).AsQueryable();
        }

        [MemberOrder(Sequence = "35", Name = "খোঁজ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("আসরের অবস্থা")]
        [TableView(true, "DateOfEstablishment", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
        public IQueryable<Asar> ByStatus([EnumDataType(typeof(AllEnums.AsarStatus))] int status)
        {
          return Container.Instances<Asar>().Where(w=>w.AsarStatus==status).OrderBy(o => o.Name).AsQueryable();
        }

        [MemberOrder(Sequence = "40", Name = "খোঁজ")]
        //[AuthorizeAction(Roles = "AMSAdmin")]
        [DisplayName("নাম দিয়ে খোঁজ")]
        [TableView(true, "DateOfEstablishment", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
        public Asar ByAsarName(Asar আসর)
        {
            return আসর;
            //return Container.Instances<Asar>().Where(w => w.Name.StartsWith(আসর.Name));
        }
        [PageSize(10)]
        [Hidden]
        public IQueryable<Asar> AutoComplete0ByAsarName([MinLength(1)] string name)
        {
            return Container.Instances<Asar>().Where(w => w.Name.StartsWith(name));
        }

        #region ByCommitteeType (Action)

        [MemberOrder(Sequence = "50", Name = "খোঁজ")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কমিটির ধরণ অনুযায়ী")]
        [TableView(true, "DateOfEstablishment", "TotalMembers", "AddressLine", "SubDistrict", "AsarStatus", "CommitteeType", "AllActivities")]
        public IList<Asar> ByCommitteeType(string committeeType)
        {
            IList<Asar> asars = new List<Asar>();

            if (committeeType == AllEnums.CommitteeType.পূর্ণাঙ্গ.ToString())
            {
                asars = (from a in Container.Instances<AsarCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.পূর্ণাঙ্গ
                         select a.Asar).ToList();
            }
            else if (committeeType == AllEnums.CommitteeType.আহ্বায়ক.ToString())
            {
                asars = (from a in Container.Instances<AsarCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.আহ্বায়ক
                         select a.Asar).ToList();
            }
            else
            {
                IList<int> asarids = (from a in Container.Instances<AsarCommittee>()
                                      select a.Asar.Id).ToList();

                asars = (from a in Container.Instances<Asar>()
                         where (!asarids.Contains(a.Id))
                         select a).ToList();
            }
           
            return asars;
        }

        public IList<string> Choices0ByCommitteeType()
        {
            return AllEnums.GetCommitteeType();
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion
        #endregion

    }
}
