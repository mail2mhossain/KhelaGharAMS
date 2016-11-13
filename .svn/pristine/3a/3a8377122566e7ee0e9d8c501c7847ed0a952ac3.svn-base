using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using KhelaGhar.AMS.Model.KhelaGharAMSAudit;

namespace KhelaGhar.AMS.Model.DbAccess
{
    public class KhelaGharAMSAuditContext : DbContext
    {
        public KhelaGharAMSAuditContext(string name) : base(name) { }
        public KhelaGharAMSAuditContext() { }

        public DbSet<AreaAudit> AreaAudits { get; set; }
        public DbSet<AsarAudit> AsarAudits { get; set; }
        public DbSet<AsarActivityAudit> AsarActivityAudits { get; set; }
        public DbSet<CommitteeAudit> CommitteeAudits { get; set; }
        public DbSet<CommitteeMemberAudit> CommitteeMemberAudits { get; set; }
        public DbSet<DistrictCommitteeAudit> DistrictCommitteeAudits { get; set; }
        public DbSet<AsarCommitteeAudit> AsarCommitteeAudits { get; set; }
        public DbSet<SubDistrictCommitteeAudit> SubDistrictCommitteeAudits { get; set; }
        public DbSet<CentralCommitteeAudit> CentralCommitteeAudits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Initialisation
            //Use the Naked Objects > DbInitialiser template to add a custom initialiser, then reference thus:
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KhelaGharAMSAuditContext>());

            //Mappings
            //Use the Naked Objects > Mapping template to add mapping classes & reference them thus:
            //modelBuilder.Configurations.Add(new Employee_Mapping());
        }
    }
}
