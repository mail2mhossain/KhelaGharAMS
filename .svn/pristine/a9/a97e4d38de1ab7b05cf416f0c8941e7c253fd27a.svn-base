namespace KhelaGhar.AMS.Model.MigrationAMSAuditContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AreaAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 150),
                        AreaTypeID = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AsarActivityAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AsarId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AsarAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        DateOfEstablishment = c.DateTime(nullable: false),
                        TotalMembers = c.Int(nullable: false),
                        AddressLine = c.String(maxLength: 350),
                        AreaId = c.Int(nullable: false),
                        AsarStatus = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AsarCommitteeAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AsarId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CentralCommitteeAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CentralKhelaGharId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommitteeAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommitteeType = c.Int(nullable: false),
                        TotalMembers = c.Int(nullable: false),
                        DateOfFormation = c.DateTime(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommitteeMemberAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KormiId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DistrictCommitteeAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubDistrictCommitteeAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubDistrictId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        User = c.String(),
                        Date = c.DateTime(),
                        DomainID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubDistrictCommitteeAudits");
            DropTable("dbo.DistrictCommitteeAudits");
            DropTable("dbo.CommitteeMemberAudits");
            DropTable("dbo.CommitteeAudits");
            DropTable("dbo.CentralCommitteeAudits");
            DropTable("dbo.AsarCommitteeAudits");
            DropTable("dbo.AsarAudits");
            DropTable("dbo.AsarActivityAudits");
            DropTable("dbo.AreaAudits");
        }
    }
}
