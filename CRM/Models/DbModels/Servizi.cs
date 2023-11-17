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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servizi()
        {
            AppuntamentiServizi = new HashSet<AppuntamentiServizi>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Servizio { get; set; }

        public string Icona { get; set; }

        public int FkAzienda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppuntamentiServizi> AppuntamentiServizi { get; set; }

        public virtual Aziende Aziende { get; set; }
    }
}
