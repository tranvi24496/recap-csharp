using EntitiyFrameworkRelations.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntitiyFrameworkRelations
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new BrickContextFactory();
            //AddDataAsync(factory);

            QueryDataAsync(factory);


            Console.ReadKey();

        }
        static async Task AddDataAsync(BrickContextFactory factory)
        {
            var contextDb = factory.CreateDbContext();

            Vendor bricking, heldDerSteine;

            await contextDb.Vendors.AddRangeAsync(new[]
            {
                bricking = new Vendor(){ VendorName = "Brick King"},
                heldDerSteine = new Vendor(){ VendorName = "Held Der Steine" }
            });
            await contextDb.SaveChangesAsync();

            Tag rare, ninjago, minecraft;

            await contextDb.Tags.AddRangeAsync(new[]
            {
               rare = new Tag(){Title = "Rare"},
               ninjago = new Tag(){Title = "Ninja go"},
               minecraft = new Tag(){Title = "MineCraft"},
            });
            await contextDb.SaveChangesAsync();

            await contextDb.AddAsync(new BasePlate()
            {
                Title = "Base plate 16 x 16 with red water pattern",
                Color = Color.Red,
                Tags = new() { ninjago, minecraft },
                Length = 16,
                Width = 16,
                Availabilities = new()
                {
                    new() { Vendor = bricking, AvailableAmount = 5, Price = 6.5m },
                    new() { Vendor = heldDerSteine, AvailableAmount = 10, Price = 5.9m }
                }
            });
            await contextDb.SaveChangesAsync();

            Console.WriteLine("Done");

        }

        static async Task QueryDataAsync(BrickContextFactory factory)
        {
            var contextDb = factory.CreateDbContext();

            //var avaiableData = await contextDb.Availabilities
            //    .Include(b => b.Brick)
            //    .Include(b=>b.Vendor)
            //    .ToArrayAsync();

            //foreach (var item in avaiableData)
            //{
            //    Console.WriteLine($"{item.Brick?.Title} - {item.Price} - {item.Vendor?.VendorName}");
            //}


            //var brickWithVendorAndTags = await contextDb.Bricks
            //    .Include(nameof(Brick.Availabilities) + "." + nameof(BrickAvailability.Vendor))
            //     .Include(b => b.Tags)
            //    //.Where(c=>c.Tags.Any(t=>t.Title.Contains("i")))
            //    .ToArrayAsync();

            //foreach (var item in brickWithVendorAndTags)
            //{
            //    Console.Write($"{item.Title} - ");
            //    if (item.Tags.Any()) Console.WriteLine($"({string.Join(',', item.Tags.Select(c => c.Title).ToArray())})");

            //    if (item.Availabilities.Any()) Console.WriteLine($"{string.Join('-', item.Availabilities.Select(c => c.Vendor.VendorName).ToArray())}");
            //}


            var brickData = await contextDb.Bricks.ToArrayAsync();
            foreach (var item in brickData)
            {
                await contextDb.Entry(item).Collection(c => c.Tags).LoadAsync();    //Explicitly
                Console.Write($"{item.Title} ");
                if (item.Tags.Any()) Console.WriteLine($"({string.Join(',', item.Tags.Select(c => c.Title).ToArray())})");
            }
        }
    }
}
