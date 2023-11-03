namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIconeNomeServizi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servizi", "Servizio", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Servizi", "Icona", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servizi", "Icona");
            DropColumn("dbo.Servizi", "Servizio");
        }
    }
}
