using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }
        public DbSet<Employees> employees { get; set; }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EntityContext>
    {
        public EntityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EntityContext>();

            optionsBuilder.UseNpgsql("your_connection_string_here");

            return new EntityContext(optionsBuilder.Options);
        }
    }
}
