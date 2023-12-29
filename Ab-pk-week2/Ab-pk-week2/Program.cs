using Ab_pk_week2.DBOperations;
using System.Reflection.PortableExecutable;

namespace Ab_pk_week2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run(); // inmemory olmadan

            var host = CreateHostBuilder(args).Build();
            // in memory de baslang�c olarak database kontrolu ve veri ekleme i�in kullan�l�yor
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                webBuilder.UseStartup<Startup>();
            });
    }
}