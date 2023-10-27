namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtentiRuoli")]
    public partial class UtentiRuoli
    {
        public int Id { get; set; }

        public int FkUtente { get; set; }

        public int FkRuolo { get; set; }

        public virtual Ruoli Ruoli { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
