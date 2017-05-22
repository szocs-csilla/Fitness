namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ugyfelek")]
    public partial class Ugyfelek
    {
        [Key]
        public int Ugyfel_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nev { get; set; }

        [Column("E-mail")]
        [StringLength(50)]
        public string E_mail { get; set; }

        [Column(TypeName = "date")]
        public DateTime Szuletesi_Datum { get; set; }

        [Column(TypeName = "image")]
        public byte[] Kep { get; set; }
    }
}
