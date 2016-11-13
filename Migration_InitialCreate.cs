namespace KhelaGharAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Description = c.String(maxLength: 250),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .Index(t => t.Division_Id);
            
            CreateTable(
                "dbo.SubDistricts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Description = c.String(maxLength: 250),
                        District_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.District_Id)
                .Index(t => t.District_Id);
            
            CreateTable(
                "dbo.Asars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        DateOfEstablishment = c.DateTime(nullable: false),
                        TotalMembers = c.Int(nullable: false),
                        AddressLine = c.String(maxLength: 350),
                        AsarStatus = c.Int(nullable: false),
                        Division_Id = c.Int(nullable: false),
                        District_Id = c.Int(nullable: false),
                        SubDistrict_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id, cascadeDelete: true)
                .ForeignKey("dbo.Districts", t => t.District_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubDistricts", t => t.SubDistrict_Id, cascadeDelete: true)
                .Index(t => t.Division_Id)
                .Index(t => t.District_Id)
                .Index(t => t.SubDistrict_Id);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.AsarActivities",
                c => new
                    {
                        AsarActivityId = c.Int(nullable: false, identity: true),
                        Asar_Id = c.Int(nullable: false),
                        Activity_ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AsarActivityId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.Activity_ActivityId, cascadeDelete: true)
                .Index(t => t.Asar_Id)
                .Index(t => t.Activity_ActivityId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        DesignationType = c.Int(nullable: false),
                        DesignationOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        CommitteeId = c.Int(nullable: false, identity: true),
                        CommitteeType = c.Int(nullable: false),
                        TotalMembers = c.Int(nullable: false),
                        DateOfFormation = c.DateTime(nullable: false),
                        Asar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CommitteeId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id)
                .Index(t => t.Asar_Id);
            
            CreateTable(
                "dbo.CommitteeMembers",
                c => new
                    {
                        CommitteeMemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        MobileNo = c.String(maxLength: 25),
                        Designation_DesignationId = c.Int(nullable: false),
                        Committee_CommitteeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommitteeMemberId)
                .ForeignKey("dbo.Designations", t => t.Designation_DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .Index(t => t.Designation_DesignationId)
                .Index(t => t.Committee_CommitteeId);

            CreateTable(
                "dbo.UserProfile",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    UserName = c.String(maxLength: 56),
                })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CommitteeMembers", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.CommitteeMembers", new[] { "Designation_DesignationId" });
            DropIndex("dbo.Committees", new[] { "Asar_Id" });
            DropIndex("dbo.AsarActivities", new[] { "Activity_ActivityId" });
            DropIndex("dbo.AsarActivities", new[] { "Asar_Id" });
            DropIndex("dbo.Asars", new[] { "SubDistrict_Id" });
            DropIndex("dbo.Asars", new[] { "District_Id" });
            DropIndex("dbo.Asars", new[] { "Division_Id" });
            DropIndex("dbo.SubDistricts", new[] { "District_Id" });
            DropIndex("dbo.Districts", new[] { "Division_Id" });
            DropIndex("dbo.UserProfile", new[] { "UserId" });
            DropForeignKey("dbo.CommitteeMembers", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteeMembers", "Designation_DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Committees", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.AsarActivities", "Activity_ActivityId", "dbo.Activities");
            DropForeignKey("dbo.AsarActivities", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.Asars", "SubDistrict_Id", "dbo.SubDistricts");
            DropForeignKey("dbo.Asars", "District_Id", "dbo.Districts");
            DropForeignKey("dbo.Asars", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.SubDistricts", "District_Id", "dbo.Districts");
            DropForeignKey("dbo.Districts", "Division_Id", "dbo.Divisions");
            DropTable("dbo.CommitteeMembers");
            DropTable("dbo.Committees");
            DropTable("dbo.Designations");
            DropTable("dbo.AsarActivities");
            DropTable("dbo.Activities");
            DropTable("dbo.Asars");
            DropTable("dbo.SubDistricts");
            DropTable("dbo.Districts");
            DropTable("dbo.Divisions");
            DropTable("dbo.UserProfile");
        }
    }
}
