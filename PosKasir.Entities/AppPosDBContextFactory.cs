using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosKasir.Entities
{
    public class AppPosDBContextFactory : IDesignTimeDbContextFactory<AppPosDBContext>
    {
        public AppPosDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppPosDBContext>();
            builder.UseSqlite("Data Source=reference.db");
            return new AppPosDBContext(builder.Options);
        }
    }
}
