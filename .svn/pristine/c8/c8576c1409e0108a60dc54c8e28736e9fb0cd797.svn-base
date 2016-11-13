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
    [DisplayName("জেলা")]
    //[Bounded]
    public class District
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public string Title()
        {
            var t = new TitleBuilder();
            t.Append(this.Name).Append(" ").Append("জেলা");
            return t.ToString();
        }

        #region Primitive Properties

        [Key, Hidden]
        public virtual int Id { get; set; }

        [MemberOrder(20)]
        [DisplayName("জেলা")]
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
                return Container.Instances<Asar>().Where(d => d.District.Id == this.Id && d.AsarStatus == (int)AllEnums.AsarStatus.সচল).ToList();
            }
        }

        [MemberOrder(60), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("নিষ্ক্রিয় আসর সংখ্যা")]
        public virtual IList<Asar> AllInactiveAsars
        {
            get
            {
                return Container.Instances<Asar>().Where(d => d.District.Id == this.Id && d.AsarStatus == (int)AllEnums.AsarStatus.নিষ্ক্রিয়).ToList();
            }
        }

        [MemberOrder(70), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("উপজেলাসমূহ")]
        public virtual IList<SubDistrict> Districts
        {
            get
            {
                return Container.Instances<SubDistrict>().Where(s => s.District.Id == this.Id).ToList();
            }
        }

        #endregion

        #region Navigation Properties

        [MemberOrder(40)]
        [DisplayName("বিভাগ")]
        public virtual Division Division { get; set; }

        #endregion

        public string ValidateName(string districtname)
        {
            var rb = new ReasonBuilder();

            if (IsNameDuplicate(districtname))
            {
                rb.AppendOnCondition(true, "   Duplicate District Name");
            }
            
            return rb.Reason;
        }

        #region AddSubDistrict (Action)

        [MemberOrder(Sequence = "1", Name = "উপজেলা")]
        [DisplayName("নতুন উপজেলা")]
        public SubDistrict AddSubDistrict(string name, [Optionally]string description)
        {
            SubDistrict subdistrict = Container.NewTransientInstance<SubDistrict>();
            subdistrict.District = this;
            subdistrict.Name = name;
            subdistrict.Description = description;

            Container.Persist(ref subdistrict);

            return subdistrict;
        }

        public string Validate0AddSubDistrict(string subdistrictname)
        {
            SubDistrict district = Container.NewTransientInstance<SubDistrict>();

            district.Name = subdistrictname;
            district.District = this;
            if (district.IsNameDuplicate(subdistrictname))
            {
                return "Duplicate Sub-District Name";
            }
            return null;
        }
        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region NewCommittee (Action)

        [MemberOrder(Sequence = "3", Name = "কমিটি")]
        [DisplayName("নতুন কমিটি")]
        public Committee NewCommittee([EnumDataType(typeof(AllEnums.CommitteeType))]int CommitteeType, int TotalMembers, DateTime DateOfFormation)
        {
            Committee com = Container.NewTransientInstance<Committee>();
            DistrictCommittee distcom = Container.NewTransientInstance<DistrictCommittee>();

            com.CommitteeType = CommitteeType;
            com.TotalMembers = TotalMembers;
            com.DateOfFormation = DateOfFormation;

            distcom.District = this;
            distcom.Committee = com;

            Container.Persist(ref distcom);

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
            IList<Committee> com = (from c in Container.Instances<DistrictCommittee>()
                                    where c.District.Id == this.Id
                                    select c.Committee).OrderByDescending(o => o.DateOfFormation).ToList();

            return com;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ByCommitteeType (Action)

        [MemberOrder(Sequence = "350", Name = "উপজেলা")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কমিটির ধরণ অনুযায়ী")]
        public IList<SubDistrict> ByCommitteeType(string committeeType)
        {
            IList<SubDistrict> subDists = new List<SubDistrict>();

            if (committeeType == AllEnums.CommitteeType.পূর্ণাঙ্গ.ToString())
            {
                subDists = (from a in Container.Instances<SubDistrictCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.পূর্ণাঙ্গ
                         && a.SubDistrict.District.Id==this.Id
                         select a.SubDistrict).ToList();
            }
            else if (committeeType == AllEnums.CommitteeType.আহ্বায়ক.ToString())
            {
                subDists = (from a in Container.Instances<SubDistrictCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.আহ্বায়ক
                         && a.SubDistrict.District.Id == this.Id
                         select a.SubDistrict).ToList();
            }
            else
            {
                IList<int> subdistids = (from a in Container.Instances<SubDistrictCommittee>()
                                         where a.SubDistrict.District.Id == this.Id
                                         select a.SubDistrict.Id).ToList();

                subDists = (from a in Container.Instances<SubDistrict>()
                            where (!subdistids.Contains(a.Id))
                            && a.District.Id == this.Id
                            select a).ToList();
            }

            return subDists;
        }

        public IList<string> Choices0ByCommitteeType()
        {
            return AllEnums.GetCommitteeType();
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        private Committee GetLatestCommittee()
        {
            Committee com = (from c in Container.Instances<DistrictCommittee>()
                             where c.District.Id == this.Id
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

            District district = (from obj in Container.Instances<District>()
                                 where obj.Name == name
                                 && obj.Division.Id == this.Division.Id
                                 select obj).FirstOrDefault();
            if (district != null)
            {
                if (this.Id != district.Id)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
