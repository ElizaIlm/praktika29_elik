using Library_ilmasheva.Classes.Database;
using Library_ilmasheva.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Library_ilmasheva.Context
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext()
        {
            Database.EnsureCreated();
            Books.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}