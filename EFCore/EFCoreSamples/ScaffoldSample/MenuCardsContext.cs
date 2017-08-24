using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ScaffoldSample
{
    public partial class MenuCardsContext : DbContext
    {
        public virtual DbSet<MenuCards> MenuCards { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDb;database=MenuCards;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCards>(entity =>
            {
                entity.HasKey(e => e.MenuCardId);

                entity.ToTable("MenuCards", "mc");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("Menus", "mc");

                entity.HasIndex(e => e.MenuCardId);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Text).HasMaxLength(120);

                entity.HasOne(d => d.MenuCard)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.MenuCardId);
            });
        }
    }
}
