namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Belepesek")]
    public partial class Belepesek
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Kartya_Szam { get; set; }

        [Column(TypeName = "date")]
        public DateTime Datum { get; set; }

        [StringLength(10)]
        public string sadsa { get; set; }

        public virtual Ugyfel_Berlet Ugyfel_Berlet { get; set; }
    }
}
