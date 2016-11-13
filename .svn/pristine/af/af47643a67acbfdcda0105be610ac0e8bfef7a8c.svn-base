namespace KhelaGhar.AMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommitteeMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommitteeMembers",
                c => new
                    {
                        CommitteeMemberId = c.Int(nullable: false, identity: true),
                        Kormi_KormiId = c.Int(nullable: false),
                        Designation_DesignationId = c.Int(nullable: false),
                        Committee_CommitteeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommitteeMemberId)
                .ForeignKey("dbo.Kormis", t => t.Kormi_KormiId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.Designation_DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId, cascadeDelete: true)
                .Index(t => t.Kormi_KormiId)
                .Index(t => t.Designation_DesignationId)
                .Index(t => t.Committee_CommitteeId);
            
        }
        
        public override void Down()
        {
            
        }
    }
}
