namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntaServizi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Servizi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Aziende_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aziende", t => t.Aziende_Id)
                .Index(t => t.Aziende_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servizi", "Aziende_Id", "dbo.Aziende");
            DropIndex("dbo.Servizi", new[] { "Aziende_Id" });
            DropTable("dbo.Servizi");
        }
    }
}
