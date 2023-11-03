namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Servizi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppuntamentiServizi", "Appuntamenti_Id", "dbo.Appuntamenti");
            DropIndex("dbo.AppuntamentiServizi", new[] { "Appuntamenti_Id" });
            AddColumn("dbo.Appuntamenti", "AppuntamentiServizi_id", c => c.Int());
            CreateIndex("dbo.Appuntamenti", "AppuntamentiServizi_id");
            AddForeignKey("dbo.Appuntamenti", "AppuntamentiServizi_id", "dbo.AppuntamentiTipologia", "id");
            DropColumn("dbo.AppuntamentiServizi", "Appuntamenti_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppuntamentiServizi", "Appuntamenti_Id", c => c.Int());
            DropForeignKey("dbo.Appuntamenti", "AppuntamentiServizi_id", "dbo.AppuntamentiTipologia");
            DropIndex("dbo.Appuntamenti", new[] { "AppuntamentiServizi_id" });
            DropColumn("dbo.Appuntamenti", "AppuntamentiServizi_id");
            CreateIndex("dbo.AppuntamentiServizi", "Appuntamenti_Id");
            AddForeignKey("dbo.AppuntamentiServizi", "Appuntamenti_Id", "dbo.Appuntamenti", "Id");
        }
    }
}
