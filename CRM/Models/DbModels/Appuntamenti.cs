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
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        public int FkCliente { get; set; }

        [Display(Name = "Creatore")]
        public int FkUtente { get; set; }

        [Display(Name = "Tipologia")]
        public int FkTipologia { get; set; }

        [Display(Name = "Data Aggiunta")]
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

        [Display(Name = "Concluso?")]
        public bool Concluso { get; set; }

        [Display(Name = "Visibile per tutti?")]
        public bool VisibilitaGlobale { get; set; }

        public virtual AppuntamentiTipologia AppuntamentiTipologia { get; set; }

        public virtual AppuntamentiTipologia AppuntamentiServizi { get; set; }

        public virtual Clienti Clienti { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
