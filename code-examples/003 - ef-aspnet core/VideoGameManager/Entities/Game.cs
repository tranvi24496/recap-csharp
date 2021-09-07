using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameManager.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public GameGenre Genre { get; set; }
        public int? GameGenreId { get; set; }
        public int PersonRating { get; set; }
    }
}
