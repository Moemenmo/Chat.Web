namespace Chat.DBContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTheard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Theards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstUserId = c.String(maxLength: 128),
                        SecondUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FirstUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.SecondUserId)
                .Index(t => t.FirstUserId)
                .Index(t => t.SecondUserId);
            
            AddColumn("dbo.Histories", "TheardId", c => c.String());
            AddColumn("dbo.Histories", "Theard_Id", c => c.Guid());
            CreateIndex("dbo.Histories", "Theard_Id");
            AddForeignKey("dbo.Histories", "Theard_Id", "dbo.Theards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "Theard_Id", "dbo.Theards");
            DropForeignKey("dbo.Theards", "SecondUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Theards", "FirstUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Theards", new[] { "SecondUserId" });
            DropIndex("dbo.Theards", new[] { "FirstUserId" });
            DropIndex("dbo.Histories", new[] { "Theard_Id" });
            DropColumn("dbo.Histories", "Theard_Id");
            DropColumn("dbo.Histories", "TheardId");
            DropTable("dbo.Theards");
        }
    }
}
