using Vapor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vapor.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Review>()
                .HasOne(r => r.Item)
                .WithMany(i => i.Reviews);
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .Property(i => i.Price)
                .HasConversion<Double>();
            base.OnModelCreating(builder);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Item> ShopCart { get; set; }
        //public DbSet<FilterTag> FilterTags { get; set; }
    }
}
