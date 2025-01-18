using Microsoft.Extensions.Caching.Memory;
using Simulator;
using Simulator.Maps;

namespace SimWeb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            InitializeSimulation(app.Services);

            app.Run();
        }

        private static void InitializeSimulation(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

            var map = new BigBounceMap(8, 6);
            var creatures = new List<Creature>
            {
                new Elf("Elandor"),
                new Orc("Gorbag")
            };

            var animals = new List<IMappable>
            {
                new Animals { Description = "Rabbits", Size = 10 },
                new Birds { Description = "Eagles", Size = 5, CanFly = true },
                new Birds { Description = "Ostriches", Size = 3, CanFly = false }
            };

            var positions = new List<Point>
            {
                new Point(2, 2),
                new Point(5, 3),
                new Point(3, 4),
                new Point(0, 0),
                new Point(7, 5)
            };

            for (int i = 0; i < positions.Count; i++)
            {
                if (i < creatures.Count)
                    map.Add(positions[i], creatures[i]);
                else
                    map.Add(positions[i], animals[i - creatures.Count]);
            }

            string moves = "ruldldrruudurdlldluur";

            var simulation = new Simulation(map, creatures, positions.Take(creatures.Count).ToList(), moves);
            var simulationHistory = new SimulationHistory(simulation);

            var cacheManager = new SimulationCacheManager(cache);
            cacheManager.SaveHistory(simulationHistory);
        }
    }
}
