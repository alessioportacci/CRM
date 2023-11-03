using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CRM.Models.DbModels
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Appuntamenti> Appuntamenti { get; set; }
        public virtual DbSet<AppuntamentiTipologia> AppuntamentiTipologia { get; set; }
        public virtual DbSet<Aziende> Aziende { get; set; }
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<Ruoli> Ruoli { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }
        public virtual DbSet<UtentiRuoli> UtentiRuoli { get; set; }
        public virtual DbSet<Servizi> Servizi { get; set; }
        public virtual DbSet<AppuntamentiServizi> AppuntamentiServizi { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppuntamentiTipologia>()
                .HasMany(e => e.Appuntamenti)
                .WithRequired(e => e.AppuntamentiTipologia)
                .HasForeignKey(e => e.FkTipologia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aziende>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Aziende>()
                .HasMany(e => e.Clienti)
                .WithRequired(e => e.Aziende)
                .HasForeignKey(e => e.FkAzienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aziende>()
                .HasMany(e => e.Utenti)
                .WithOptional(e => e.Aziende)
                .HasForeignKey(e => e.FkAzienda);

            modelBuilder.Entity<Clienti>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Clienti>()
                .HasMany(e => e.Appuntamenti)
                .WithRequired(e => e.Clienti)
                .HasForeignKey(e => e.FkCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ruoli>()
                .HasMany(e => e.UtentiRuoli)
                .WithRequired(e => e.Ruoli)
                .HasForeignKey(e => e.FkRuolo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Appuntamenti)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.FkUtente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.UtentiRuoli)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.FkUtente)
                .WillCascadeOnDelete(false);


        }
    }
}
