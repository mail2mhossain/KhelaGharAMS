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
    [DisplayName("বিভাগ")]
    [Bounded]
    public class Division
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public string Title()
        {
            var t = new TitleBuilder();
            t.Append(this.Name).Append(" ").Append("বিভাগ");
            return t.ToString();
        }

        #region Primitive Properties

        [Key, Hidden]
        public virtual int Id { get; set; }

        [MemberOrder(20)]
        [DisplayName("নাম")]
        [MaxLength(150)]
        public virtual string Name { get; set; }

        [MemberOrder(30), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("বর্ণনা")]
        [MaxLength(250)]
        public virtual string Description { get; set; }

        [MemberOrder(40), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("সচল আসর সংখ্যা")]
        public virtual IList<Asar> AllActiveAsars
        {
            get
            {
                return Container.Instances<Asar>().Where(d => d.Division.Id == this.Id && d.AsarStatus== (int)AllEnums.AsarStatus.সচল).ToList();
            }
        }

        [MemberOrder(50), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("নিষ্ক্রিয় আসর সংখ্যা")]
        public virtual IList<Asar> AllInactiveAsars
        {
            get
            {
                return Container.Instances<Asar>().Where(d => d.Division.Id == this.Id && d.AsarStatus == (int)AllEnums.AsarStatus.নিষ্ক্রিয়).ToList();
            }
        }

        [MemberOrder(50), NotMapped]
        //[Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("জেলাসমূহ")]
        public virtual IList<District> Districts
        {
            get
            {
                return Container.Instances<District>().Where(d => d.Division.Id == this.Id).ToList();
            }
        }
        #endregion

        public string ValidateName(string divisionname)
        {
            var rb = new ReasonBuilder();
            Division division = (from obj in Container.Instances<Division>()
                                 where obj.Name == divisionname
                                 select obj).FirstOrDefault();
            if (division != null)
            {
                if (this.Id != division.Id)
                {
                    rb.AppendOnCondition(true, "Duplicate Division Name");
                }
            }
            return rb.Reason;
        }

        #region AddDistrict (Action)

        [MemberOrder(Sequence = "1", Name = "জেলা")]
        [DisplayName("নতুন জেলা")]
        public District AddDistrict(string name, [Optionally]string description)
        {
            District district = Container.NewTransientInstance<District>();
            district.Division = this;
            district.Name = name;
            district.Description = description;

            Container.Persist(ref district);
            return district;
        }
        
        public string Validate0AddDistrict(string districtname)
        {
            District district = Container.NewTransientInstance<District>();

            district.Name = districtname;
            district.Division = this;
            if (district.IsNameDuplicate(districtname))
            {
                return "Duplicate District Name";
            }
            return null;
        }
      

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        #region ByCommitteeType (Action)

        [MemberOrder(Sequence = "2", Name = "জেলা")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [DisplayName("কমিটির ধরণ অনুযায়ী")]
        public IList<District> ByCommitteeType(string committeeType)
        {
            IList<District> dists = new List<District>();

            if (committeeType == AllEnums.CommitteeType.পূর্ণাঙ্গ.ToString())
            {
                dists = (from a in Container.Instances<DistrictCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.পূর্ণাঙ্গ
                         && a.District.Division.Id == this.Id
                         select a.District).ToList();
            }
            else if (committeeType == AllEnums.CommitteeType.আহ্বায়ক.ToString())
            {
                dists = (from a in Container.Instances<DistrictCommittee>()
                         where a.Committee.CommitteeType == (int)AllEnums.CommitteeType.আহ্বায়ক
                         && a.District.Division.Id == this.Id
                         select a.District).ToList();
            }
            else
            {
                IList<int> distids = (from a in Container.Instances<DistrictCommittee>()
                                      where a.District.Division.Id == this.Id
                                      select a.District.Id).ToList();

                dists = (from a in Container.Instances<District>()
                            where (!distids.Contains(a.Id))
                            && a.Division.Id == this.Id
                            select a).ToList();
            }

            return dists;
        }

        public IList<string> Choices0ByCommitteeType()
        {
            return AllEnums.GetCommitteeType();
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion
    }
}
