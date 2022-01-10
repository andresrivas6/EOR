using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-1H96F1JR; initial catalog=eor;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                //entity.HasOne(u => u.Rol)
                //      .WithMany(r => r.Usuarios);

                //entity.HasOne(a => a.UnidadApoyo)
                //      .WithMany(b => b.Usuarios);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
