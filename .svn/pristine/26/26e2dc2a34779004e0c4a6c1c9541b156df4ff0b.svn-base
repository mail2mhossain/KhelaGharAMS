using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using KhelaGhar.AMS.Model.Domain;

namespace KhelaGhar.AMS.Model.DbAccess
{
    public class KhelaGharAMSDbContext : DbContext
    {
        public KhelaGharAMSDbContext(string name) : base(name) { }
        public KhelaGharAMSDbContext() { }

        //Add DbSet properties for root objects, thus:
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<SubDistrict> SubDistricts { get; set; }
        public DbSet<Asar> Asars { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<AsarActivity> AsarActivities { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Kormi> Kormis { get; set; }
        public DbSet<CommitteeMember> CommitteeMembers { get; set; }
        public DbSet<AsarCommittee> AsarCommittees { get; set; }
        public DbSet<DistrictCommittee> DistrictCommittees { get; set; }
        public DbSet<SubDistrictCommittee> SubDistrictCommittees { get; set; }
        public DbSet<CentralKhelaGhar> CentralKhelaGhars { get; set; }
        public DbSet<CentralCommittee> CentralCommittees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Initialisation
            //Use the Naked Objects > DbInitialiser template to add an initialiser, then reference thus:
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KhelaGharAMSDbContext>());
            //Database.SetInitializer(new KhelaGharAMSDbInitialiser());

            //Mappings
            //Use the Naked Objects > DbMapping template to create mapping classes & reference them thus:
            //modelBuilder.Configurations.Add(new EmployeeMapping());
            //modelBuilder.Configurations.Add(new CustomerMapping());
        }
    }
}