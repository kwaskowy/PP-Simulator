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

            // Upewnij siê, ¿e Razor Pages s¹ zmapowane
            app.MapRazorPages();

            app.Run();
        }
    }
}
