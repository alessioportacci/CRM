namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppuntamentiTipologia")]
    public partial class AppuntamentiTipologia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppuntamentiTipologia()
        {
            Appuntamenti = new HashSet<Appuntamenti>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipologia { get; set; }

        [Required]
        [StringLength(50)]
        public string Colore { get; set; }

        [Required]
        [StringLength(50)]
        public string Colore2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Colore3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appuntamenti> Appuntamenti { get; set; }
    }
}
