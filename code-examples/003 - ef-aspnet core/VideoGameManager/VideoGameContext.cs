using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameManager.Entities;

namespace VideoGameManager
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Handle case when delete an item has relations
            modelBuilder.Entity<GameGenre>()
                .HasMany(h => h.Games)
                .WithOne(w => w.Genre)
                .HasForeignKey(c => c.GameGenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> Genres { get; set; }

    }
}
