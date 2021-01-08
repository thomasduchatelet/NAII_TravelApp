using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> AppUsers { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Trip>()
                .HasMany(t => t.Items)
                .WithOne()
                .HasForeignKey(i => i.TripId);
            builder.Entity<Trip>()
                .HasMany(t => t.Todos)
                .WithOne()
                .HasForeignKey(i => i.TripId);

            builder.Entity<Trip>()
                .HasOne(t => t.Itinerary)
                .WithOne()
                .HasForeignKey<Itinerary>(i => i.TripId);

            builder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId);

            builder.Entity<Todo>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId);

            builder.Entity<Itinerary>()
                .HasMany(i => i.Locations)
                .WithOne()
                .HasForeignKey(l => l.ItineraryId);

            builder.Entity<User>()
                .HasMany(u => u.Trips)
                .WithOne()
                .HasForeignKey(t => t.UserId);
            builder.Entity<User>()
                .HasMany(u => u.Categories)
                .WithOne()
                .HasForeignKey(t => t.UserId);
            builder.Entity<User>()
                .HasMany(u => u.Items)
                .WithOne()
                .HasForeignKey(t => t.UserId);
            builder.Entity<User>()
                .HasMany(u => u.Itineraries)
                .WithOne()
                .HasForeignKey(t => t.UserId);
            builder.Entity<User>()
                .HasMany(u => u.Todos)
                .WithOne()
                .HasForeignKey(t => t.UserId);
            builder.Entity<User>()
                .HasMany(u => u.Locations)
                .WithOne()
                .HasForeignKey(t => t.UserId);

        }
    }
}
