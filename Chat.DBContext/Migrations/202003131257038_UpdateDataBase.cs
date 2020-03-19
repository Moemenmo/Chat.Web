namespace Chat.DBContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataBase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Theards", "FirstUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Theards", "SecondUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Theards", new[] { "FirstUserId" });
            DropIndex("dbo.Theards", new[] { "SecondUserId" });
            CreateTable(
                "dbo.TheardApplicationUsers",
                c => new
                    {
                        Theard_Id = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Theard_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Theards", t => t.Theard_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Theard_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Theards", "FirstUserId");
            DropColumn("dbo.Theards", "SecondUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Theards", "SecondUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Theards", "FirstUserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.TheardApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TheardApplicationUsers", "Theard_Id", "dbo.Theards");
            DropIndex("dbo.TheardApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TheardApplicationUsers", new[] { "Theard_Id" });
            DropTable("dbo.TheardApplicationUsers");
            CreateIndex("dbo.Theards", "SecondUserId");
            CreateIndex("dbo.Theards", "FirstUserId");
            AddForeignKey("dbo.Theards", "SecondUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Theards", "FirstUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
