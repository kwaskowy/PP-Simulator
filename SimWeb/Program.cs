namespace SimWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Dodanie Razor Pages
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Upewnij si�, �e Razor Pages s� zmapowane
            app.MapRazorPages();

            app.Run();
        }
    }
}
