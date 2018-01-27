using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>().ToTable("Jogos");
            modelBuilder.Entity<Amigo>().ToTable("Amigos");
            modelBuilder.Entity<Emprestimo>().ToTable("Emprestimos");
        }
    }
}
