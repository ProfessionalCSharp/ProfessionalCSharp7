using BlazorWasmSample.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var books = Enumerable.Range(1, 10).Select(x => new Book { BookId = x, Title = $"sample {x}", Publisher = "sample pub" }).ToArray();
            modelBuilder.Entity<Book>().HasData(books);
        }

        public DbSet<Book>  Books { get; set; }
    }
}
