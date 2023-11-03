namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servizi")]
    public partial class Servizi
    {
        [Key]  
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Servizio { get; set; }

        public string Icona { get; set; }

        public virtual Aziende Aziende { get; set; }

    }
}
