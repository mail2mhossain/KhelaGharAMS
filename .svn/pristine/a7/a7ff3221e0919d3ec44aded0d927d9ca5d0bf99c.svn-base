using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
    [DisplayName("কর্মী")]
    public class Kormi
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container
        public IDomainObjectContainer Container { set; protected get; }
        #endregion
        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


        #endregion

        [Key, Hidden]
        public virtual int KormiId { get; set; }

        [MemberOrder(10), Title]
        [DisplayName("নাম")]
        [MaxLength(250), Required]
        public virtual string Name { get; set; }

        [MemberOrder(20)]
        [DisplayName("মোবাইল নং")]
        [MaxLength(11), Required]
        public virtual string MobileNo { get; set; }

        [MemberOrder(40), MultiLine(NumberOfLines = 3, Width = 50), Optionally]
        [DisplayName("ঠিকানা")]
        [MaxLength(350)]
        public virtual string AddressLine { get; set; }

        [MemberOrder(50)]
        [DisplayName("বিভাগ"), Optionally]
        public virtual Division Division { get; set; }

        [MemberOrder(60)]
        [DisplayName("জেলা"), Optionally]
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
        [DisplayName("উপজেলা"), Optionally]
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
        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

        [MemberOrder(80)]
        [DisplayName("আসর"), Optionally]
        public virtual Asar Asar { get; set; }
      
        public string ValidateMobileNo(string mobileNo)
        {
            var rb = new ReasonBuilder();

            Kormi kormi = (from obj in Container.Instances<Kormi>()
                         where obj.MobileNo == mobileNo
                         select obj).FirstOrDefault();

            if (kormi != null)
            {
                if (this.KormiId != kormi.KormiId)
                {
                    rb.AppendOnCondition(true, "Duplicate Kormi");
                }
            }
            return rb.Reason;
        }
    }
}

