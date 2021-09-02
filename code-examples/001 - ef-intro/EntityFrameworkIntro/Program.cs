using EntityFrameworkIntro.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new CookbookContextFactory();

            // EntityStatesAsync(factory);
            //ChangeStatesAsync(factory);
            //AttachEntitiesAsync(factory);
            //NoTrackingAsync(factory);
            //RawSqlAsync(factory);
            //TransactionAsync(factory);
            ExpressionTreeAsync(factory);
            Console.ReadKey();
        }
        public static async Task EntityStatesAsync(CookbookContextFactory factory)
        {
            using var dbContext = factory.CreateDbContext();
            Console.WriteLine("Add dish to dishs");
            var dish = new Dish() { Title = "Sea Pizza", Note = "This is so good", Star = 3 };

            dbContext.Dishs.Add(dish);
            Console.WriteLine($"State1 : {dbContext.Entry(dish).State}");
            await dbContext.SaveChangesAsync();

            Console.WriteLine($"State2 : {dbContext.Entry(dish).State}");
            Console.WriteLine("Update star");
            dish.Star = 5;
            dbContext.Dishs.Update(dish);
            Console.WriteLine($"State3 : {dbContext.Entry(dish).State}");
            await dbContext.SaveChangesAsync();
            Console.WriteLine($"State4 : {dbContext.Entry(dish).State}");

            dbContext.Dishs.Where(c => c.Title.Contains("Sea"))
                .ToList()
                .ForEach(c => Console.WriteLine($"Dish : {c.Id} - {c.Title}"));

            Console.WriteLine("Remove dish to dishs");
            dbContext.Dishs.Remove(dish);
            Console.WriteLine($"State5 : {dbContext.Entry(dish).State}");
            await dbContext.SaveChangesAsync();
            Console.WriteLine($"State6 : {dbContext.Entry(dish).State}");
            Console.WriteLine($"Success : " + dish.Id);
        }

        public static async Task ChangeStatesAsync(CookbookContextFactory factory)
        {
            using var dbContext = factory.CreateDbContext();
            Console.WriteLine("Add dish to dishs");
            var dish = new Dish() { Title = "Sea Pizza", Note = "This is so good", Star = 3 };

            dbContext.Dishs.Add(dish);
            await dbContext.SaveChangesAsync();
            dish.Star = 5;
            var entry = dbContext.Entry(dish);
            var origin = dbContext.Entry(dish).OriginalValues[nameof(dish.Star)].ToString();
            var item = dbContext.Dishs.FirstOrDefault(c => c.Id == dish.Id);

            using var dbContext2 = factory.CreateDbContext();
            var item2 = dbContext2.Dishs.FirstOrDefault(c => c.Id == dish.Id);
        }

        public static async Task AttachEntitiesAsync(CookbookContextFactory factory)
        {
            //Using when you want check data from client => Restrict insert duplicate or update when data no change.
            using var dbContext = factory.CreateDbContext();
            Console.WriteLine("Add dish to dishs");
            var dish = new Dish() { Title = "Sea Pizza", Note = "This is so good", Star = 3 };

            dbContext.Dishs.Add(dish);
            await dbContext.SaveChangesAsync();

            dbContext.Entry(dish).State = EntityState.Detached;
            var state = dbContext.Entry(dish).State;
        }

        public static async Task NoTrackingAsync(CookbookContextFactory factory)
        {
            //Using when you want readonly data from database => Increment performance
            using var dbContext = factory.CreateDbContext();

            var dishs = await dbContext.Dishs.AsNoTracking().ToArrayAsync();
            dishs[0].Star = 5;
            var state = dbContext.Entry(dishs[0]).State;  //If not using AsNoTracking() => state = Modified
        }

        public static async Task RawSqlAsync(CookbookContextFactory factory)
        {
            using var dbContext = factory.CreateDbContext();
            //Using FromSqlRaw when want get data from Database
            var dishs = await dbContext.Dishs
                .FromSqlRaw("SELECT * FROM Dishs")
                .ToArrayAsync();

            //Using  FromSqlInterpolated when have parameters
            var filter = "Sea%";
            dishs = await dbContext.Dishs
               .FromSqlInterpolated($"SELECT * FROM Dishs WHERE Title LIKE {filter}")
               .AsNoTracking()
               .ToArrayAsync();

            //BAD CODE
            //SQL INJECTION
            dishs = await dbContext.Dishs
               .FromSqlRaw($"SELECT * FROM Dishs WHERE Title LIKE '" + filter + "'")
               .AsNoTracking()
               .ToArrayAsync();

            //Using when you want execute Create, Update, Delete
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Dishs WHERE Id NOT IN (SELECT DISTINCT(DishId) FROM Ingredients)");
        }

        public static async Task TransactionAsync(CookbookContextFactory factory)
        {
            using var dbContext = factory.CreateDbContext();
            using var trans = dbContext.Database.BeginTransaction();
            try
            {
                //Block in transaction
                var dish = new Dish() { Title = "Sea Pizza", Note = "This is so good", Star = 3 };
                dbContext.Dishs.Add(dish);
                await dbContext.SaveChangesAsync();

                await dbContext.Database.ExecuteSqlRawAsync("SELECT 1/0 AS ERROR");

                await trans.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                await trans.RollbackAsync();
            }
        }

        public static async Task ExpressionTreeAsync(CookbookContextFactory factory)
        {
            using var dbContext = factory.CreateDbContext();
            // If have database, it using expression tree to describing C# code
            // So EF can translate C# code to Sql command 
            var dishs = await dbContext.Dishs
                .Where(d => d.Title.StartsWith("Sea"))
                .ToArrayAsync();

            Func<Dish, bool> f = d => d.Title.StartsWith("Sea");
            Expression<Func<Dish, bool>> ex = d => d.Title.StartsWith("Sea");

            var dishs2 = new[]
            {
                new Dish() { Title = "Sea Pizza", Note = "This is so good", Star = 3 },
                new Dish() { Title = "Sea Pizza 2", Note = "This is so good 2", Star = 4 }
            };

            //With InMemory : Using Lambda expression (Not expression tree)
            dishs2 = dishs2
               .Where(d => d.Title.StartsWith("Sea"))
               .ToArray();
        }
    }
}
