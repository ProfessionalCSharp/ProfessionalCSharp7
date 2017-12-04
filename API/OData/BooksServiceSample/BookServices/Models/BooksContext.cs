using BooksServiceSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookServices.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext()
        {

        }
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeBuilder<Book> bookBuilder = modelBuilder.Entity<Book>();
            bookBuilder.HasMany(b => b.Chapters)
                .WithOne(c => c.Book)
                .HasForeignKey(c => c.BookId);
            bookBuilder.Property(b => b.Title)
                .HasMaxLength(120)
                .IsRequired();
            bookBuilder.Property(b => b.Isbn)
                .HasMaxLength(20)
                .IsRequired(false);

            EntityTypeBuilder<Chapter> chapterBuilder = modelBuilder.Entity<Chapter>();
            chapterBuilder.Property(c => c.Title)
                .HasMaxLength(120);
        }
    }
}
