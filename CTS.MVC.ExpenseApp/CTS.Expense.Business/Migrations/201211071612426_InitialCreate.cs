namespace CTS.Expense.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthYear = c.DateTime(nullable: false),
                        title = c.String(nullable: false, maxLength: 50),
                        ErNumber = c.Int(),
                        Billable = c.Boolean(nullable: false),
                        ProejctID = c.Boolean(nullable: false),
                        Proejct_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Proejct_Id)
                .Index(t => t.Proejct_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "ClientId" });
            DropIndex("dbo.Reports", new[] { "Proejct_Id" });
            DropForeignKey("dbo.Projects", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Reports", "Proejct_Id", "dbo.Projects");
            DropTable("dbo.Clients");
            DropTable("dbo.Projects");
            DropTable("dbo.Reports");
        }
    }
}
