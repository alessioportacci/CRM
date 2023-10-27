namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntiColoriTipologia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppuntamentiTipologia", "Colore2", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AppuntamentiTipologia", "Colore3", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppuntamentiTipologia", "Colore3");
            DropColumn("dbo.AppuntamentiTipologia", "Colore2");
        }
    }
}
