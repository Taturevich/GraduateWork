namespace BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DomainArea",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DomainAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Domain_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domain", t => t.Domain_Id)
                .Index(t => t.Domain_Id);
            
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        DomainArea_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DomainArea", t => t.DomainArea_Id)
                .Index(t => t.DomainArea_Id);
            
            CreateTable(
                "dbo.EventLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        EventCallerName = c.String(),
                        EventDuration = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(),
                        SenderType = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hash = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mark = c.String(),
                        ImageName = c.String(),
                        Sortament = c.String(),
                        Specification = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Passwords", "User_Id", "dbo.User");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.User");
            DropForeignKey("dbo.EventLogs", "User_Id", "dbo.User");
            DropForeignKey("dbo.DomainAttributes", "Domain_Id", "dbo.Domain");
            DropForeignKey("dbo.Domain", "DomainArea_Id", "dbo.DomainArea");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Passwords", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.EventLogs", new[] { "User_Id" });
            DropIndex("dbo.Domain", new[] { "DomainArea_Id" });
            DropIndex("dbo.DomainAttributes", new[] { "Domain_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Passwords");
            DropTable("dbo.Messages");
            DropTable("dbo.User");
            DropTable("dbo.EventLogs");
            DropTable("dbo.Domain");
            DropTable("dbo.DomainAttributes");
            DropTable("dbo.DomainArea");
            DropTable("dbo.Categories");
            DropTable("dbo.Bots");
        }
    }
}
