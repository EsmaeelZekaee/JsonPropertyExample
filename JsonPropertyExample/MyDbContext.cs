using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JsonPropertyExample
{
    public class MyDbContext : DbContext
    {
        public DbSet<Attribute> Attributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=JsonPropertyExample3;Trusted_Connection=True;ConnectRetryCount=0");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                foreach (var property in entityType.ClrType.GetProperties().Where(x => x != null && x.GetCustomAttribute<HasJsonConversionAttribute>() != null))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(property.PropertyType, property.Name)
                        .HasJsonConversion();
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }

    
}
