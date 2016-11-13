namespace KhelaGharAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AsarCommittee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AsarCommittees",
                c => new
                {
                    AsarCommitteeId = c.Int(nullable: false, identity: true),
                    Asar_Id = c.Int(),
                    Committee_CommitteeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AsarCommitteeId);

            CreateIndex("dbo.AsarCommittees", "Asar_Id");
            CreateIndex("dbo.AsarCommittees", "Committee_CommitteeId");

            AddForeignKey("dbo.AsarCommittees", "Asar_Id", "dbo.Asars", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AsarCommittees", "Committee_CommitteeId", "dbo.Committees", "CommitteeId", cascadeDelete: true);

            DropForeignKey("dbo.Committees", "Asar_Id", "dbo.Asars");
            DropIndex("dbo.Committees", new[] { "Asar_Id" });
            DropColumn("dbo.Committees", "Asar_Id");
        }
        
        public override void Down()
        {
            
        }
    }
}
