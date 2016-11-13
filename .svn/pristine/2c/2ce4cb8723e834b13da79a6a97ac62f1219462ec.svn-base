using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NakedObjects.Audit;
using NakedObjects;
using KhelaGhar.AMS.Model.DbAccess;
using System.Security.Principal;
using KhelaGhar.AMS.Model.Domain;

namespace KhelaGhar.AMS.Model.KhelaGharAMSAudit
{
    public class KhelaGharAMSAuditor : IAuditor
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        KhelaGharAMSAuditContext context = new KhelaGharAMSAuditContext();

        public void ActionInvoked(IPrincipal byPrincipal, string actionName, string serviceName, bool queryOnly, object[] withParameters)
        {
            //throw new NotImplementedException();
        }

        public void ActionInvoked(IPrincipal byPrincipal, string actionName, object onObject, bool queryOnly, object[] withParameters)
        {
            //throw new NotImplementedException();
        }

        public void ObjectPersisted(IPrincipal byPrincipal, object updatedObject)
        {
            if (updatedObject is Division)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForDivision(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is District)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForDistrict(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is SubDistrict)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForSubDistrict(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is Asar)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForAsar(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is AsarActivity)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForAsarActivity(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is Committee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is CommitteeMember)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForCommitteeMember(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is DistrictCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForDistrictCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is AsarCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForAsarCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is SubDistrictCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForSubDistrictCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is CentralCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Insert);
                ObjectPersistUpdateForCentralCommittee(byPrincipal, updatedObject, actionType);
            }
        }

        public void ObjectUpdated(IPrincipal byPrincipal, object updatedObject)
        {
            if (updatedObject is Division)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForDivision(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is District)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForDistrict(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is SubDistrict)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForSubDistrict(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is Asar)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForAsar(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is AsarActivity)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForAsarActivity(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is Committee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is CommitteeMember)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForCommitteeMember(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is DistrictCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForDistrictCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is AsarCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForAsarCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is SubDistrictCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForSubDistrictCommittee(byPrincipal, updatedObject, actionType);
            }
            if (updatedObject is CentralCommittee)
            {
                var actionType = (int)(AllEnums.ActionTypes.Update);
                ObjectPersistUpdateForCentralCommittee(byPrincipal, updatedObject, actionType);
            }
        }

        private void ObjectPersistUpdateForDivision(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((Division)updatedObject).Id);

            var auditor = Container.NewTransientInstance<DivisionAudit>();

            auditor.Name = ((Division)updatedObject).Name;
            auditor.Description = ((Division)updatedObject).Description;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForDistrict(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((District)updatedObject).Id);

            var auditor = Container.NewTransientInstance<DistrictAudit>();

            auditor.Name = ((District)updatedObject).Name;
            auditor.Description = ((District)updatedObject).Description;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForSubDistrict(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((SubDistrict)updatedObject).Id);

            var auditor = Container.NewTransientInstance<SubDistrictAudit>();

            auditor.Name = ((SubDistrict)updatedObject).Name;
            auditor.Description = ((SubDistrict)updatedObject).Description;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForAsar(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((Asar)updatedObject).Id);

            var auditor = Container.NewTransientInstance<AsarAudit>();

            auditor.Name = ((Asar)updatedObject).Name;
            auditor.DateOfEstablishment = ((Asar)updatedObject).DateOfEstablishment;
            auditor.TotalMembers = ((Asar)updatedObject).TotalMembers;
            auditor.AddressLine = ((Asar)updatedObject).AddressLine;
            auditor.DivisionId = ((Asar)updatedObject).Division.Id;
            auditor.DistrictId = ((Asar)updatedObject).District.Id;
            auditor.SubDistrictId = ((Asar)updatedObject).SubDistrict.Id;
            auditor.AsarStatus = ((Asar)updatedObject).AsarStatus;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForAsarActivity(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((AsarActivity)updatedObject).AsarActivityId);

            var auditor = Container.NewTransientInstance<AsarActivityAudit>();

            auditor.AsarId = ((AsarActivity)updatedObject).Asar.Id;
            auditor.ActivityId = ((AsarActivity)updatedObject).Activity.ActivityId;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForCommittee(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((Committee)updatedObject).CommitteeId);

            var auditor = Container.NewTransientInstance<CommitteeAudit>();

            auditor.CommitteeType = ((Committee)updatedObject).CommitteeType;
            auditor.TotalMembers = ((Committee)updatedObject).TotalMembers;
            auditor.DateOfFormation = ((Committee)updatedObject).DateOfFormation;
            //auditor.AsarId = ((Committee)updatedObject).Asar.Id;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForCommitteeMember(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((CommitteeMember)updatedObject).CommitteeMemberId);

            var auditor = Container.NewTransientInstance<CommitteeMemberAudit>();

            auditor.KormiId = ((CommitteeMember)updatedObject).Kormi.KormiId;
            auditor.DesignationId = ((CommitteeMember)updatedObject).Designation.DesignationId;
            auditor.CommitteeId = ((CommitteeMember)updatedObject).Committee.CommitteeId;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForDistrictCommittee(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((DistrictCommittee)updatedObject).DistrictCommitteeId);

            var auditor = Container.NewTransientInstance<DistrictCommitteeAudit>();

            auditor.CommitteeId = ((DistrictCommittee)updatedObject).Committee.CommitteeId;
            auditor.DistrictId = ((DistrictCommittee)updatedObject).District.Id;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForAsarCommittee(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((AsarCommittee)updatedObject).AsarCommitteeId);

            var auditor = Container.NewTransientInstance<AsarCommitteeAudit>();

            auditor.CommitteeId = ((AsarCommittee)updatedObject).Committee.CommitteeId;
            auditor.AsarId = ((AsarCommittee)updatedObject).Asar.Id;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForSubDistrictCommittee(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((SubDistrictCommittee)updatedObject).SubDistrictCommitteeId);

            var auditor = Container.NewTransientInstance<SubDistrictCommitteeAudit>();

            auditor.CommitteeId = ((SubDistrictCommittee)updatedObject).Committee.CommitteeId;
            auditor.SubDistrictId = ((SubDistrictCommittee)updatedObject).SubDistrict.Id;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }

        private void ObjectPersistUpdateForCentralCommittee(IPrincipal byPrincipal, object updatedObject, int actionType)
        {
            var objectId = string.Format("{0}", ((CentralCommittee)updatedObject).CentralCommitteeId);

            var auditor = Container.NewTransientInstance<CentralCommitteeAudit>();

            auditor.CommitteeId = ((CentralCommittee)updatedObject).Committee.CommitteeId;
            auditor.CentralKhelaGharId = ((CentralCommittee)updatedObject).CentralKhelaGhar.CentralKhelaGharId;

            auditor.User = byPrincipal.Identity.Name;
            auditor.Date = DateTime.Now;
            auditor.ActionType = actionType;
            auditor.DomainID = objectId;

            Container.Persist(ref auditor);
        }
    }
}
