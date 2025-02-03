namespace Proiect_ABD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserRoleColumnNames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.String(),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaintenanceRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        PerformedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.PerformedBy_Id)
                .Index(t => t.PerformedBy_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _name = c.String(),
                        _email = c.String(),
                        _password = c.String(),
                        _role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._id)
                .ForeignKey("dbo.Roles", t => t._role_id, cascadeDelete: true)
                .Index(t => t._role_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        _role_id = c.Int(nullable: false, identity: true),
                        _role_name = c.String(),
                    })
                .PrimaryKey(t => t._role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaintenanceRecords", "PerformedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "_role_id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "_role_id" });
            DropIndex("dbo.MaintenanceRecords", new[] { "PerformedBy_Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.MaintenanceRecords");
            DropTable("dbo.Equipments");
        }
    }
}
