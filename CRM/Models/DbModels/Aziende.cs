namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Aziende")]
    public partial class Aziende
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aziende()
        {
            Clienti = new HashSet<Clienti>();
            Utenti = new HashSet<Utenti>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Partita IVA")]
        public string PIVA { get; set; }

        [StringLength(50)]
        public string Regione { get; set; }

        [StringLength(2)]
        public string Provincia { get; set; }

        [StringLength(50)]
        public string Citta { get; set; }

        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        [StringLength(200)]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clienti> Clienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utenti> Utenti { get; set; }
    }
}
