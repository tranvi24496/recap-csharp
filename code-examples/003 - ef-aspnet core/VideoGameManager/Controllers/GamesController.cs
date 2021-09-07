using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameManager.Entities;

namespace VideoGameManager.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly VideoGameContext context;

        public GamesController(VideoGameContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Game> GetAllGames() => context.Games;

        [HttpGet]
        [Route("{id}")]
        public Game GetById(int id) => context.Games
            .Include(c => c.Genre)
            .FirstOrDefault(c => c.Id == id);

        [HttpPost]
        public async Task<Game> Add([FromBody] Game game)
        {
            context.Games.Add(game);
            await context.SaveChangesAsync();

            return game;
        }


        [HttpPut]
        public async Task<Game> Update([FromBody] Game game)
        {
            context.Games.Update(game);
            await context.SaveChangesAsync();
            return game;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<int> Delete(int id)
        {
            var game = await context.Games.FirstOrDefaultAsync(c => c.Id == id);
            if (game != null)
            {
                context.Games.Remove(game);
                await context.SaveChangesAsync();
                return id;
            }
            return -1;
        }
    }
}
