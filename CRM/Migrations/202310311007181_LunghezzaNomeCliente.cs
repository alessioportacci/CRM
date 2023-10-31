namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LunghezzaNomeCliente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clienti", "Nome", c => c.String(nullable: false, maxLength: 200, fixedLength: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clienti", "Nome", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
        }
    }
}
