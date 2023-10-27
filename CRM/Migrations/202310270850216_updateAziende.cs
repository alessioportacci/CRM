namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAziende : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clienti", "FkAzienda", "dbo.Aziende");
            DropForeignKey("dbo.Utenti", "FkAzienda", "dbo.Aziende");
            DropPrimaryKey("dbo.Aziende");
            AlterColumn("dbo.Aziende", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Aziende", "Nome", c => c.String(nullable: false, maxLength: 100, fixedLength: true));
            AddPrimaryKey("dbo.Aziende", "Id");
            AddForeignKey("dbo.Clienti", "FkAzienda", "dbo.Aziende", "Id");
            AddForeignKey("dbo.Utenti", "FkAzienda", "dbo.Aziende", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utenti", "FkAzienda", "dbo.Aziende");
            DropForeignKey("dbo.Clienti", "FkAzienda", "dbo.Aziende");
            DropPrimaryKey("dbo.Aziende");
            AlterColumn("dbo.Aziende", "Nome", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Aziende", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Aziende", "Id");
            AddForeignKey("dbo.Utenti", "FkAzienda", "dbo.Aziende", "Id");
            AddForeignKey("dbo.Clienti", "FkAzienda", "dbo.Aziende", "Id");
        }
    }
}
