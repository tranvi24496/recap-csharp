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
    [Route("api/gamegenres")]
    public class GameGenresController : ControllerBase
    {
        private readonly VideoGameContext context;

        public GameGenresController(VideoGameContext context)
        {
            this.context = context;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<int> Delete(int id)
        {
            var returnValue = -1;
            try
            {
                var genre = await context.Genres.FirstOrDefaultAsync(c => c.Id == id);
                if (genre != null)
                {
                    context.Genres.Remove(genre);
                    await context.SaveChangesAsync();
                    return id;
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnValue;
        }
    }
}
