namespace LoJackal.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoJackal : DbContext
    {
        public LoJackal()
            : base("name=LoJackal")
        {
        }

        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Communication>()
                .Property(e => e.ComputerName)
                .IsUnicode(false);

            modelBuilder.Entity<Communication>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Configuration>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<Configuration>()
                .Property(e => e.Value)
                .IsUnicode(false);
        }
    }
}
