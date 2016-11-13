using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NakedObjects;
using NakedObjects.Services;
using KhelaGhar.AMS.Model.Domain;


namespace KhelaGhar.AMS.Model.Repository
{
    [DisplayName("কেন্দ্রীয় খেলাঘর")]
    public class CentralKhelaGharRepository : AbstractFactoryAndRepository
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service.

        #endregion      
        
        #region NewCommittee (Action)

        [MemberOrder(Sequence = "3", Name = "কমিটি")]
        [DisplayName("নতুন কমিটি")]
        public Committee NewCommittee([EnumDataType(typeof(AllEnums.CommitteeType))]int CommitteeType, int TotalMembers, DateTime DateOfFormation)
        {
            CentralKhelaGhar centralkg = GetCentralKhelaGhar();

            Committee com = Container.NewTransientInstance<Committee>();
            CentralCommittee central = Container.NewTransientInstance<CentralCommittee>();

            com.CommitteeType = CommitteeType;
            com.TotalMembers = TotalMembers;
            com.DateOfFormation = DateOfFormation;

            central.CentralKhelaGhar = centralkg;
            central.Committee = com;

            Container.Persist(ref central);

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
            CentralKhelaGhar centralkg = GetCentralKhelaGhar();
            IList<Committee> com = (from c in Container.Instances<CentralCommittee>()
                                    where c.CentralKhelaGhar.CentralKhelaGharId == centralkg.CentralKhelaGharId
                                    select c.Committee).OrderByDescending(o => o.DateOfFormation).ToList();

            return com;
        }

        // Use 'hide', 'dis', 'val', 'actdef', 'actcho' shortcuts to add supporting methods here.
        #endregion

        private Committee GetLatestCommittee()
        {
            CentralKhelaGhar central = GetCentralKhelaGhar();

            Committee com = new Committee();

            if (central != null)
            {
                com = (from c in Container.Instances<CentralCommittee>()
                       where c.CentralCommitteeId == central.CentralKhelaGharId
                       select c.Committee).OrderByDescending(o => o.DateOfFormation).FirstOrDefault();
            }
            
            return com;
        }

        private CentralKhelaGhar GetCentralKhelaGhar()
        {
            CentralKhelaGhar central = (from c in Container.Instances<CentralKhelaGhar>()
                                        select c).FirstOrDefault();

            return central;
        }

        
        // 'fact' shortcut to add a factory method, 
        // 'alli' for an all-instances method
        // 'find' for a method to find a single object by query
        // 'list' for a method to return a list of objects matching a query

    }
}
