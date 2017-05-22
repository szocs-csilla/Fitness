namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext1")
        {
        }

        public virtual DbSet<Belepesek> Belepesek { get; set; }
        public virtual DbSet<Berletek> Berletek { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Ugyfel_Berlet> Ugyfel_Berlet { get; set; }
        public virtual DbSet<Ugyfelek> Ugyfelek { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Belepesek>()
                .Property(e => e.sadsa)
                .IsFixedLength();

            modelBuilder.Entity<Berletek>()
                .Property(e => e.Ar)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Berletek>()
                .HasMany(e => e.Ugyfel_Berlet)
                .WithRequired(e => e.Berletek)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<Login>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Ugyfel_Berlet>()
                .HasMany(e => e.Belepesek)
                .WithRequired(e => e.Ugyfel_Berlet)
                .WillCascadeOnDelete(false);
        }
    }
}
