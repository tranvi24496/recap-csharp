using EntitiyFrameworkRelations.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiyFrameworkRelations
{
    public class BrickContext : DbContext
    {
        public BrickContext(DbContextOptions<BrickContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Save data of delivered class in base class
            modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
            modelBuilder.Entity<MinifigHead>().HasBaseType<Brick>();
        }
        public DbSet<Brick> Bricks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<BrickAvailability> Availabilities { get; set; }
    }
}
