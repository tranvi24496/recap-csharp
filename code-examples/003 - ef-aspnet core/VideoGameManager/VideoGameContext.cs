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
            :base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> Genres { get; set; }
    }
}
