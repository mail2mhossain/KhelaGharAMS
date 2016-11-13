using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NakedObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhelaGhar.AMS.Model.Domain
{
    [DisplayName("উপজেলা")]
    //[Bounded]
    public class SubDistrict
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public string Title()
        {
            var t = new TitleBuilder();
            t.Append(this.Name).Append(" ").Append("উপজেলা");
            return t.ToString();
        }

        #region Primitive Properties

        [Key, Hidden]
        public virtual int Id { get; set; }

        [MemberOrder(20)]
        [DisplayName("উপজেলা")]
        [MaxLength(150)]
        public virtual string Name { get; set; }

        [MemberOrder(30), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("বর্ণনা")]
        [MaxLength(250)]
        public virtual string Description { get; set; }

        [MemberOrder(40), NotMapped]
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
            if (this.CommitteeType != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [MemberOrder(50), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("সচল আসর সংখ্যা")]
        public virtual IList<Asar> AllActiveAsars
        {
            get
            {
                return Container.Instances<Asar>().Where(d => d.SubDistrict.Id == this.Id && d.AsarStatus == (int)AllEnums.AsarStatus.সচল).ToList();
            }
        }

        [MemberOrder(60), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("নিষ্ক্রিয় আসর সংখ্যা")]
        public virtual IList<Asar> AllInactiveAsars
        {
            get
            {
                return Container.Instances<Asar>().Where(d => d.SubDistrict.Id == this.Id && d.AsarStatus == (int)AllEnums.AsarStatus.নিষ্ক্রিয়).ToList();
            }
        }
        #endregion

        public string ValidateName(string subdistrictname)
        {
            var rb = new ReasonBuilder();

            if (IsNameDuplicate(subdistrictname))
            {
                rb.AppendOnCondition(true, "   Duplicate Sub-District Name");
            }

            return rb.Reason;
        }

        #region Navigation Properties

        [MemberOrder(40)]
        [DisplayName("জেলা")]
        public virtual District District { get; set; }

        [PageSize(10)]
        [Hidden]
        public IQueryable<District> AutoCompleteDistrict([MinLength(1)] string name)
        {
            return Container.Instances<District>().Where(w => w.Name.StartsWith(name));
        }

        //public IList<District> ChoicesDistrict()
        //{
        //    IList<District> districts = new List<District>();

        //       districts = (from d in Container.Instances<District>()
        //                     select d).ToList();

        //    return districts;
        //}
        #endregion

        #region NewCommittee (Action)

        [MemberOrder(Sequence = "3", Name = "কমিটি")]
        [DisplayName("নতুন কমিটি")]
        public Committee NewCommittee([EnumDataType(typeof(AllEnums.CommitteeType))]int CommitteeType, int TotalMembers, DateTime DateOfFormation)
        {
            Committee com = Container.NewTransientInstance<Committee>();
            SubDistrictCommittee subdistcom = Container.NewTransientInstance<SubDistrictCommittee>();

            com.CommitteeType = CommitteeType;
            com.TotalMembers = TotalMembers;
            com.DateOfFormation = DateOfFormation;

            subdistcom.SubDistrict = this;
            subdistcom.Committee = com;

            Container.Persist(ref subdistcom);
            com.CustomeTitle = ((AllEnums.CommitteeType)com.CommitteeType).ToString() + " কমিটি, " + this.Title();
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

        [MemberOrder(Sequence = "4", Name = "কমিটি")]
        [DisplayName("বর্তমান কমিটি")]
        public Committee ShowCurrentCommittee()
        {
            return GetLatestCommittee();
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ShowAllCommittees (Action)

        [MemberOrder(Sequence = "6", Name = "কমিটি")]
        [DisplayName("সকল কমিটি")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public IList<Committee> ShowAllCommittees()
        {
            IList<Committee> com = (from c in Container.Instances<SubDistrictCommittee>()
                                    where c.SubDistrict.Id == this.Id
                                    select c.Committee).OrderByDescending(o => o.DateOfFormation).ToList();

            return com;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        private Committee GetLatestCommittee()
        {
            Committee com = (from c in Container.Instances<SubDistrictCommittee>()
                             where c.SubDistrict.Id == this.Id
                             select c.Committee).OrderByDescending(o => o.DateOfFormation).FirstOrDefault();

            if (com != null)
            {
                com.CustomeTitle = ((AllEnums.CommitteeType)com.CommitteeType).ToString() + " কমিটি, " + this.Title();
            }

            return com;
        }

        [Hidden]
        public bool IsNameDuplicate(string name)
        {
            bool result = false;

            SubDistrict subdistrict = (from obj in Container.Instances<SubDistrict>()
                                       where obj.Name == name
                                       && obj.District.Id == this.District.Id
                                       select obj).FirstOrDefault();
            if (subdistrict != null)
            {
                if (this.Id != subdistrict.Id)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
