using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosKasir.Entities
{
    public class AppPosDBContext : DbContext
    {
        public AppPosDBContext(DbContextOptions<AppPosDBContext> options) : base(options)
        {
        }

        public DbSet<Semen> Semens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Semen>().ToTable("semen");

            modelbuilder.Entity<Semen>()
                .Property(Q => Q.CreatedBy)
                .HasDefaultValue("SYSTEM");

            modelbuilder.Entity<Semen>()
            .Property(Q => Q.CreatedAt)
            .HasDefaultValue("Date('Now')");
        }
    }
}

