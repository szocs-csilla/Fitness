namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Berletek")]
    public partial class Berletek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Berletek()
        {
            Ugyfel_Berlet = new HashSet<Ugyfel_Berlet>();
        }

        [Key]
        public int Berlet_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nev { get; set; }

        public decimal Ar { get; set; }

        public int? Idotartam { get; set; }

        public int? Belepes { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Mikortol { get; set; }

        public bool Aktivitas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ugyfel_Berlet> Ugyfel_Berlet { get; set; }
    }
}
