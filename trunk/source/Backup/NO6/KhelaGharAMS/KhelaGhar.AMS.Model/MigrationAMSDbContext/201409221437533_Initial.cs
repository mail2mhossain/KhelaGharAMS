namespace KhelaGhar.AMS.Model.MigrationAMSDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 150),
                        AreaType_AreaTypeId = c.Int(nullable: false),
                        Parent_AreaId = c.Int(),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.AreaTypes", t => t.AreaType_AreaTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Areas", t => t.Parent_AreaId)
                .Index(t => t.AreaType_AreaTypeId)
                .Index(t => t.Parent_AreaId);
            
            CreateTable(
                "dbo.AreaTypes",
                c => new
                    {
                        AreaTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Parent_AreaTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.AreaTypeId)
                .ForeignKey("dbo.AreaTypes", t => t.Parent_AreaTypeId)
                .Index(t => t.Parent_AreaTypeId);
            
            CreateTable(
                "dbo.AsarActivities",
                c => new
                    {
                        AsarActivityId = c.Int(nullable: false, identity: true),
                        Activity_ActivityId = c.Int(nullable: false),
                        Asar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AsarActivityId)
                .ForeignKey("dbo.Activities", t => t.Activity_ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .Index(t => t.Activity_ActivityId)
                .Index(t => t.Asar_Id);
            
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
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                        Area_AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.Area_AreaId, cascadeDelete: true)
                .Index(t => t.Area_AreaId);
            
            CreateTable(
                "dbo.AsarCommittees",
                c => new
                    {
                        AsarCommitteeId = c.Int(nullable: false, identity: true),
                        Asar_Id = c.Int(nullable: false),
                        Committee_CommitteeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AsarCommitteeId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .Index(t => t.Asar_Id)
                .Index(t => t.Committee_CommitteeId);
            
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        CommitteeId = c.Int(nullable: false, identity: true),
                        CommitteeType = c.Int(nullable: false),
                        TotalMembers = c.Int(nullable: false),
                        DateOfFormation = c.DateTime(nullable: false),
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommitteeId);
            
            CreateTable(
                "dbo.AsarRoutines",
                c => new
                    {
                        AsarRoutineId = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        StartTime = c.String(nullable: false),
                        EndTime = c.String(nullable: false),
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                        Asar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AsarRoutineId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .Index(t => t.Asar_Id);
            
            CreateTable(
                "dbo.CentralCommittees",
                c => new
                    {
                        CentralCommitteeId = c.Int(nullable: false, identity: true),
                        CentralKhelaGhar_CentralKhelaGharId = c.Int(nullable: false),
                        Committee_CommitteeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CentralCommitteeId)
                .ForeignKey("dbo.CentralKhelaGhars", t => t.CentralKhelaGhar_CentralKhelaGharId, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .Index(t => t.CentralKhelaGhar_CentralKhelaGharId)
                .Index(t => t.Committee_CommitteeId);
            
            CreateTable(
                "dbo.CentralKhelaGhars",
                c => new
                    {
                        CentralKhelaGharId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        AddressLine = c.String(maxLength: 350),
                    })
                .PrimaryKey(t => t.CentralKhelaGharId);
            
            CreateTable(
                "dbo.CommitteeMembers",
                c => new
                    {
                        CommitteeMemberId = c.Int(nullable: false, identity: true),
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                        Committee_CommitteeId = c.Int(nullable: false),
                        Designation_DesignationId = c.Int(nullable: false),
                        Kormi_KormiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommitteeMemberId)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.Designation_DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.Kormis", t => t.Kormi_KormiId, cascadeDelete: true)
                .Index(t => t.Committee_CommitteeId)
                .Index(t => t.Designation_DesignationId)
                .Index(t => t.Kormi_KormiId);
            
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
                "dbo.Kormis",
                c => new
                    {
                        KormiId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        MobileNo = c.String(nullable: false, maxLength: 25),
                        Email = c.String(),
                        AddressLine = c.String(maxLength: 350),
                        Status = c.Int(nullable: false),
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                        Area_AreaId = c.Int(),
                        Asar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.KormiId)
                .ForeignKey("dbo.Areas", t => t.Area_AreaId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id)
                .Index(t => t.Area_AreaId)
                .Index(t => t.Asar_Id);
            
            CreateTable(
                "dbo.DistrictCommittees",
                c => new
                    {
                        DistrictCommitteeId = c.Int(nullable: false, identity: true),
                        Committee_CommitteeId = c.Int(nullable: false),
                        District_AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictCommitteeId)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .ForeignKey("dbo.Areas", t => t.District_AreaId, cascadeDelete: true)
                .Index(t => t.Committee_CommitteeId)
                .Index(t => t.District_AreaId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Guardian1 = c.String(nullable: false, maxLength: 250),
                        Guardian2 = c.String(maxLength: 250),
                        MobileNo = c.String(maxLength: 25),
                        SchoolName = c.String(maxLength: 250),
                        DOB = c.DateTime(nullable: false),
                        SchoolStandard = c.String(maxLength: 50),
                        BloodGroup = c.String(maxLength: 10),
                        PresentAddress = c.String(maxLength: 250),
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                        Asar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .Index(t => t.Asar_Id);
            
            CreateTable(
                "dbo.SubDistrictCommittees",
                c => new
                    {
                        SubDistrictCommitteeId = c.Int(nullable: false, identity: true),
                        Committee_CommitteeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubDistrictCommitteeId)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .Index(t => t.Committee_CommitteeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubDistrictCommittees", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.Members", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.DistrictCommittees", "District_AreaId", "dbo.Areas");
            DropForeignKey("dbo.DistrictCommittees", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteeMembers", "Kormi_KormiId", "dbo.Kormis");
            DropForeignKey("dbo.Kormis", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.Kormis", "Area_AreaId", "dbo.Areas");
            DropForeignKey("dbo.CommitteeMembers", "Designation_DesignationId", "dbo.Designations");
            DropForeignKey("dbo.CommitteeMembers", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CentralCommittees", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CentralCommittees", "CentralKhelaGhar_CentralKhelaGharId", "dbo.CentralKhelaGhars");
            DropForeignKey("dbo.AsarRoutines", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.AsarCommittees", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.AsarCommittees", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.AsarActivities", "Asar_Id", "dbo.Asars");
            DropForeignKey("dbo.Asars", "Area_AreaId", "dbo.Areas");
            DropForeignKey("dbo.AsarActivities", "Activity_ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Areas", "Parent_AreaId", "dbo.Areas");
            DropForeignKey("dbo.Areas", "AreaType_AreaTypeId", "dbo.AreaTypes");
            DropForeignKey("dbo.AreaTypes", "Parent_AreaTypeId", "dbo.AreaTypes");
            DropIndex("dbo.SubDistrictCommittees", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.Members", new[] { "Asar_Id" });
            DropIndex("dbo.DistrictCommittees", new[] { "District_AreaId" });
            DropIndex("dbo.DistrictCommittees", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.Kormis", new[] { "Asar_Id" });
            DropIndex("dbo.Kormis", new[] { "Area_AreaId" });
            DropIndex("dbo.CommitteeMembers", new[] { "Kormi_KormiId" });
            DropIndex("dbo.CommitteeMembers", new[] { "Designation_DesignationId" });
            DropIndex("dbo.CommitteeMembers", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.CentralCommittees", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.CentralCommittees", new[] { "CentralKhelaGhar_CentralKhelaGharId" });
            DropIndex("dbo.AsarRoutines", new[] { "Asar_Id" });
            DropIndex("dbo.AsarCommittees", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.AsarCommittees", new[] { "Asar_Id" });
            DropIndex("dbo.Asars", new[] { "Area_AreaId" });
            DropIndex("dbo.AsarActivities", new[] { "Asar_Id" });
            DropIndex("dbo.AsarActivities", new[] { "Activity_ActivityId" });
            DropIndex("dbo.AreaTypes", new[] { "Parent_AreaTypeId" });
            DropIndex("dbo.Areas", new[] { "Parent_AreaId" });
            DropIndex("dbo.Areas", new[] { "AreaType_AreaTypeId" });
            DropTable("dbo.SubDistrictCommittees");
            DropTable("dbo.Members");
            DropTable("dbo.DistrictCommittees");
            DropTable("dbo.Kormis");
            DropTable("dbo.Designations");
            DropTable("dbo.CommitteeMembers");
            DropTable("dbo.CentralKhelaGhars");
            DropTable("dbo.CentralCommittees");
            DropTable("dbo.AsarRoutines");
            DropTable("dbo.Committees");
            DropTable("dbo.AsarCommittees");
            DropTable("dbo.Asars");
            DropTable("dbo.AsarActivities");
            DropTable("dbo.AreaTypes");
            DropTable("dbo.Areas");
            DropTable("dbo.Activities");
        }
    }
}
