using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;

namespace Pizzeria.Models
{
    public partial class EorContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public EorContext()
        {
        }

        public EorContext(DbContextOptions<EorContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost; initial catalog=eor;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasMany(u => u.Ordenes)
                      .WithOne(o => o.Usuario);
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.HasMany(p => p.Ordenes)
                      .WithOne(o => o.Pizza);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Pizzeria.Models.Pizza> Pizza { get; set; }

        public DbSet<Pizzeria.Models.Orden> Orden { get; set; }
    }
}
