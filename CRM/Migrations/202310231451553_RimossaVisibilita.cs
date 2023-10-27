namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RimossaVisibilita : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clienti", "VisbilitaAziendale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clienti", "VisbilitaAziendale", c => c.Boolean(nullable: false));
        }
    }
}
