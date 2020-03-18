using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sakila.model;

namespace sakila.repositorio
{
    public class SakilaContext : DbContext
    {
        public DbSet<actor> actors { get; set; }
        public DbSet<film> films { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("SakilaContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<actor>(entity =>
            {
                entity.ToTable("actor");
                entity.HasKey(e => e.actor_id);
                entity.Property(e => e.first_name).IsRequired();
                entity.Property(e => e.last_name).IsRequired();
                entity.Property(e => e.last_update).IsRequired();
            });

            modelBuilder.Entity<film>(entity =>
            {
                entity.ToTable("film");
                entity.HasKey(e => e.film_id);
                entity.Property(e => e.title).IsRequired();
            });
        }
    }
}
