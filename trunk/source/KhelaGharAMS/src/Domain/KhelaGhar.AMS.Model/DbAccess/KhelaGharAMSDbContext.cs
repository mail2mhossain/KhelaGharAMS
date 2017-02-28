using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using KhelaGhar.AMS.Model.Domain;
using KhelaGhar.AMS.Model.Domain.Areas;
using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Activities;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Domain.Committees;
using KhelaGhar.AMS.Model.Domain.UserAccounts;
using KhelaGhar.AMS.Model.Domain.Features;
using KhelaGhar.AMS.Model.Domain.MasterData;
using KhelaGhar.AMS.Model.Domain.Workers;
using KhelaGhar.AMS.Model.Domain.Members;
using KhelaGhar.AMS.Model.Domain.Conferences;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KhelaGhar.AMS.Model.DbAccess
{
	public class KhelaGharAMSDbContext : DbContext
	{
		public KhelaGharAMSDbContext (string name) : base(name) { }
		public KhelaGharAMSDbContext () { }

		//Add DbSet properties for root objects, thus:
		public DbSet<Area> Areas { get; set; }
		public DbSet<Asar> Asars { get; set; }
		public DbSet<WeeklyActivity> WeeklyActivities { get; set; }
		public DbSet<MasterActivity> MasterActivities { get; set; }
		public DbSet<Designation> Designations { get; set; }
		public DbSet<Attachment> Attachments { get; set; }
		public DbSet<Worker> Workers { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<Committee> Committees { get; set; }
		public DbSet<CommitteeMember> CommitteeMembers { get; set; }
		public DbSet<Conference> Conferences { get; set; }
		public DbSet<ConferenceDelegate> Delegates { get; set; }
		public DbSet<OrganizationalTour> OrganizationalTours { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Meeting> Meetings { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<LoginUser> LoginUsers { get; set; }
		public DbSet<UserRoles> UserRoles { get; set; }
		public DbSet<UserLogins> UserLogins { get; set; }
		public DbSet<UserClaims> UserClaims { get; set; }
		public DbSet<FeatureType> FeatureTypes { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<RoleFeatures> RoleFeatures { get; set; }

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
            //Initialisation
            //Use the Naked Objects > DbInitialiser template to add an initialiser, then reference thus:
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KhelaGharAMSDbContext>());
            //Database.SetInitializer(new KhelaGharAMSDbInitialiser());
            Database.SetInitializer<KhelaGharAMSDbContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			//Mappings
			//Use the Naked Objects > DbMapping template to create mapping classes & reference them thus:
			//modelBuilder.Configurations.Add(new EmployeeMapping());
			//modelBuilder.Configurations.Add(new CustomerMapping());

			//modelBuilder.Entity<Asar>()
			//.Property(f => f.DateOfEstablishment)
			//.HasColumnType("datetime2");

			//modelBuilder.Properties<DateTime>()
			//.Configure(c => c.HasColumnType("datetime2"));
		}
	}
}