namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Login")]
    public partial class Login
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [StringLength(25)]
        public string Password { get; set; }
    }
}
