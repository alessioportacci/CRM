namespace CRM.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clienti")]
    public partial class Clienti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clienti()
        {
            Appuntamenti = new HashSet<Appuntamenti>();
        }

        public int Id { get; set; }

        public DateTime DataRegistrazione { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(50)]
        public string Localita { get; set; }

        public DateTime? DataNascita { get; set; }

        [StringLength(20)]
        public string Genere { get; set; }

        public string Note { get; set; }

        public int FkAzienda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appuntamenti> Appuntamenti { get; set; }

        public virtual Aziende Aziende { get; set; }
    }
}
