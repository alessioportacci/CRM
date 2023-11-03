namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntaAppuntamentiServizi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppuntamentiServizi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Appuntamenti_Id = c.Int(),
                        Servizi_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appuntamenti", t => t.Appuntamenti_Id)
                .ForeignKey("dbo.Servizi", t => t.Servizi_Id)
                .Index(t => t.Appuntamenti_Id)
                .Index(t => t.Servizi_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppuntamentiServizi", "Servizi_Id", "dbo.Servizi");
            DropForeignKey("dbo.AppuntamentiServizi", "Appuntamenti_Id", "dbo.Appuntamenti");
            DropIndex("dbo.AppuntamentiServizi", new[] { "Servizi_Id" });
            DropIndex("dbo.AppuntamentiServizi", new[] { "Appuntamenti_Id" });
            DropTable("dbo.AppuntamentiServizi");
        }
    }
}
