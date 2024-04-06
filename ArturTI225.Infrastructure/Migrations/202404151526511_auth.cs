namespace ArturTI225.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "User_UserId", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "User_UserId" });
            AddColumn("dbo.Users", "Role", c => c.String());
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.Roles", "User_UserId");
            AddForeignKey("dbo.Roles", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
