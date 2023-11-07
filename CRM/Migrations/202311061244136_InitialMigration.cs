namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appuntamenti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FkCliente = c.Int(nullable: false),
                        FkUtente = c.Int(nullable: false),
                        FkTipologia = c.Int(nullable: false),
                        DataAggiunta = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        OraInizio = c.Time(nullable: false, precision: 7),
                        OraFine = c.Time(nullable: false, precision: 7),
                        Descrizione = c.String(nullable: false, maxLength: 200),
                        Note = c.String(maxLength: 500),
                        Concluso = c.Boolean(nullable: false),
                        VisibilitaGlobale = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clienti", t => t.FkCliente)
                .ForeignKey("dbo.Utenti", t => t.FkUtente)
                .ForeignKey("dbo.AppuntamentiTipologia", t => t.FkTipologia)
                .Index(t => t.FkCliente)
                .Index(t => t.FkUtente)
                .Index(t => t.FkTipologia);
            
            CreateTable(
                "dbo.AppuntamentiServizi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FkAppuntamento = c.Int(nullable: false),
                        FkServizio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servizi", t => t.FkServizio)
                .ForeignKey("dbo.Appuntamenti", t => t.FkAppuntamento)
                .Index(t => t.FkAppuntamento)
                .Index(t => t.FkServizio);
            
            CreateTable(
                "dbo.Servizi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Servizio = c.String(nullable: false, maxLength: 100),
                        Icona = c.String(),
                        FkAzienda = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aziende", t => t.FkAzienda)
                .Index(t => t.FkAzienda);
            
            CreateTable(
                "dbo.Aziende",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, fixedLength: true),
                        PIVA = c.String(nullable: false, maxLength: 20),
                        Regione = c.String(maxLength: 50),
                        Provincia = c.String(maxLength: 2),
                        Citta = c.String(maxLength: 50),
                        CAP = c.String(maxLength: 5),
                        Indirizzo = c.String(nullable: false, maxLength: 200),
                        Telefono = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clienti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataRegistrazione = c.DateTime(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 200, fixedLength: true),
                        Email = c.String(maxLength: 200),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        Localita = c.String(maxLength: 50),
                        DataNascita = c.DateTime(),
                        Genere = c.String(maxLength: 20),
                        Note = c.String(),
                        FkAzienda = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aziende", t => t.FkAzienda)
                .Index(t => t.FkAzienda);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 200),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        Propic = c.String(maxLength: 100),
                        LastOnline = c.DateTime(),
                        Blind = c.Boolean(nullable: false),
                        FkAzienda = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aziende", t => t.FkAzienda)
                .Index(t => t.FkAzienda);
            
            CreateTable(
                "dbo.UtentiRuoli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FkUtente = c.Int(nullable: false),
                        FkRuolo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ruoli", t => t.FkRuolo)
                .ForeignKey("dbo.Utenti", t => t.FkUtente)
                .Index(t => t.FkUtente)
                .Index(t => t.FkRuolo);
            
            CreateTable(
                "dbo.Ruoli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ruolo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppuntamentiTipologia",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Tipologia = c.String(nullable: false, maxLength: 50),
                        Colore = c.String(nullable: false, maxLength: 50),
                        Colore2 = c.String(nullable: false, maxLength: 50),
                        Colore3 = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appuntamenti", "FkTipologia", "dbo.AppuntamentiTipologia");
            DropForeignKey("dbo.AppuntamentiServizi", "FkAppuntamento", "dbo.Appuntamenti");
            DropForeignKey("dbo.Utenti", "FkAzienda", "dbo.Aziende");
            DropForeignKey("dbo.UtentiRuoli", "FkUtente", "dbo.Utenti");
            DropForeignKey("dbo.UtentiRuoli", "FkRuolo", "dbo.Ruoli");
            DropForeignKey("dbo.Appuntamenti", "FkUtente", "dbo.Utenti");
            DropForeignKey("dbo.Servizi", "FkAzienda", "dbo.Aziende");
            DropForeignKey("dbo.Clienti", "FkAzienda", "dbo.Aziende");
            DropForeignKey("dbo.Appuntamenti", "FkCliente", "dbo.Clienti");
            DropForeignKey("dbo.AppuntamentiServizi", "FkServizio", "dbo.Servizi");
            DropIndex("dbo.UtentiRuoli", new[] { "FkRuolo" });
            DropIndex("dbo.UtentiRuoli", new[] { "FkUtente" });
            DropIndex("dbo.Utenti", new[] { "FkAzienda" });
            DropIndex("dbo.Clienti", new[] { "FkAzienda" });
            DropIndex("dbo.Servizi", new[] { "FkAzienda" });
            DropIndex("dbo.AppuntamentiServizi", new[] { "FkServizio" });
            DropIndex("dbo.AppuntamentiServizi", new[] { "FkAppuntamento" });
            DropIndex("dbo.Appuntamenti", new[] { "FkTipologia" });
            DropIndex("dbo.Appuntamenti", new[] { "FkUtente" });
            DropIndex("dbo.Appuntamenti", new[] { "FkCliente" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.AppuntamentiTipologia");
            DropTable("dbo.Ruoli");
            DropTable("dbo.UtentiRuoli");
            DropTable("dbo.Utenti");
            DropTable("dbo.Clienti");
            DropTable("dbo.Aziende");
            DropTable("dbo.Servizi");
            DropTable("dbo.AppuntamentiServizi");
            DropTable("dbo.Appuntamenti");
        }
    }
}
