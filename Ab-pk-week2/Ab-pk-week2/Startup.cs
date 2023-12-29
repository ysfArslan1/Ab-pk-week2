using Ab_pk_week2.DBOperations;
using Ab_pk_week2.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Ab_pk_week2
{
    public class Startup
    {
        public IConfiguration Configuration { get;  }
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers()
                .AddNewtonsoftJson(); // httppatch için eklendi
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // dbcontentext servicelere eklendi ve inmemory oluşturuldu
            services.AddDbContext<BankDbContext>(Options => Options.UseInMemoryDatabase(databaseName: "BankDb"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 
            app.UseMiddleware<HttpLoggingMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(x => { x.MapControllers(); });

        }
    }
}
