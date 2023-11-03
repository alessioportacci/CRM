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
        [Key]
        public int Id { get; set; }

        public virtual Servizi Servizi{ get; set; }

    }
}
