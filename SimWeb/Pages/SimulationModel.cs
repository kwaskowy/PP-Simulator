using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Simulator;
using System.Text.Json;

namespace SimWeb.Pages
{
    public class SimulationModel : PageModel
    {
        private readonly IDistributedCache _cache;

        public SimulationModel(IDistributedCache cache)
        {
            _cache = cache;
        }

        public SimulationHistory SimulationHistory { get; private set; }

        public void OnGet()
        {
            var cachedHistory = _cache.GetString("SimulationHistory");

            if (!string.IsNullOrEmpty(cachedHistory))
            {
                SimulationHistory = JsonSerializer.Deserialize<SimulationHistory>(cachedHistory);
            }
        }
    }
}
