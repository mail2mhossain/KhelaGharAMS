namespace KhelaGhar.AMS.Model.MigrationAMSDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAsarNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AsarNotes",
                c => new
                    {
                        AsarNoteId = c.Int(nullable: false, identity: true),
                        Asar_Id = c.Int(nullable: false),
                        Note_NoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AsarNoteId)
                .ForeignKey("dbo.Asars", t => t.Asar_Id, cascadeDelete: true)
                .ForeignKey("dbo.Notes", t => t.Note_NoteId, cascadeDelete: true)
                .Index(t => t.Asar_Id)
                .Index(t => t.Note_NoteId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        NoteDate = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 350),
                        Description = c.String(nullable: false, maxLength: 350),
                        Decision = c.String(maxLength: 350),
                        AuditFields_InsertedBy = c.String(nullable: false),
                        AuditFields_InsertedDateTime = c.DateTime(nullable: false),
                        AuditFields_LastUpdatedBy = c.String(nullable: false),
                        AuditFields_LastUpdatedDateTime = c.DateTime(nullable: false),
                        NoteType_NoteTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.NoteTypes", t => t.NoteType_NoteTypeId, cascadeDelete: true)
                .Index(t => t.NoteType_NoteTypeId);
            
            CreateTable(
                "dbo.NoteTypes",
                c => new
                    {
                        NoteTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.NoteTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AsarNotes", "Note_NoteId", "dbo.Notes");
            DropForeignKey("dbo.Notes", "NoteType_NoteTypeId", "dbo.NoteTypes");
            DropForeignKey("dbo.AsarNotes", "Asar_Id", "dbo.Asars");
            DropIndex("dbo.Notes", new[] { "NoteType_NoteTypeId" });
            DropIndex("dbo.AsarNotes", new[] { "Note_NoteId" });
            DropIndex("dbo.AsarNotes", new[] { "Asar_Id" });
            DropTable("dbo.NoteTypes");
            DropTable("dbo.Notes");
            DropTable("dbo.AsarNotes");
        }
    }
}
