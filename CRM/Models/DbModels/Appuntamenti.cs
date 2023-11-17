namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appuntamenti")]
    public partial class Appuntamenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appuntamenti()
        {
            AppuntamentiServizi = new HashSet<AppuntamentiServizi>();
        }

        public int Id { get; set; }

        [Display(Name = "Cliente")]
        public int FkCliente { get; set; }

        [Display(Name = "Utente")]
        public int FkUtente { get; set; }

        [Display(Name = "Tipologia")]
        public int FkTipologia { get; set; }

        public DateTime DataAggiunta { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Ora inizio")]
        public TimeSpan OraInizio { get; set; }

        [Display(Name = "Ora fine")]
        public TimeSpan OraFine { get; set; }

        [Required]
        [StringLength(200)]
        public string Descrizione { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public bool Concluso { get; set; }

        [Display(Name = "Per tutti?")]
        public bool VisibilitaGlobale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppuntamentiServizi> AppuntamentiServizi { get; set; }

        [NotMapped]
        public string Servizi { get; set; }

        public virtual AppuntamentiTipologia AppuntamentiTipologia { get; set; }

        public virtual Clienti Clienti { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
