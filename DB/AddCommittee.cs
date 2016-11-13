namespace KhelaGharAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommittee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Committees",
                c => new
                {
                    CommitteeId = c.Int(nullable: false, identity: true),
                    CommitteeType = c.Int(nullable: false),
                    TotalMembers = c.Int(nullable: false),
                    DateOfFormation = c.DateTime(nullable: false),
                    Asar_Id = c.Int(nullable: true), 
                })
                .PrimaryKey(t => t.CommitteeId)
                //.ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .Index(t => t.Asar_Id);

            AddForeignKey("dbo.Committees", "Asar_Id", "dbo.Asars", "Id", cascadeDelete: true);
            AddColumn("dbo.Designations", "DesignationOrder", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropIndex("dbo.Committees", new[] { "Asar_Id" });
            DropForeignKey("dbo.Committees", "Asar_Id", "dbo.Asars");
            DropColumn("dbo.Designations", "DesignationOrder");
            DropTable("dbo.Committees"); 
        }
    }
}
