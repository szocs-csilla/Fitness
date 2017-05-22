namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ugyfel_Berlet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ugyfel_Berlet()
        {
            Belepesek = new HashSet<Belepesek>();
        }

        public int Ugyfel_ID { get; set; }

        public int Berlet_ID { get; set; }

        [Key]
        [StringLength(50)]
        public string Kartya_Szam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Belepesek> Belepesek { get; set; }

        public virtual Berletek Berletek { get; set; }
    }
}
