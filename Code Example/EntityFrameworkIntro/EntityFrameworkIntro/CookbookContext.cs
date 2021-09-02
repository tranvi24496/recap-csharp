using EntityFrameworkIntro.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkIntro
{
    public class CookbookContext : DbContext
    {
        public CookbookContext(DbContextOptions<CookbookContext> options)
            : base(options)
        {

        }
        public DbSet<Dish> Dishs { get; set; }
        public DbSet<DishIngredient> Ingredients { get; set; }
    }
}
