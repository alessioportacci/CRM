namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppuntamentiServizi")]
    public partial class AppuntamentiServizi
    {
        public int Id { get; set; }

        public int FkAppuntamento { get; set; }

        public int FkServizio { get; set; }

        public virtual Appuntamenti Appuntamenti { get; set; }

        public virtual Servizi Servizi { get; set; }
    }
}
