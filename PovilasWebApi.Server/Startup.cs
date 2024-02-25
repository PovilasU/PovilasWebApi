using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PovilasWebApi.Server.Data;
using PovilasWebApi.Server.Services;


namespace PovilasWebApi.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new ProductService(Configuration.GetConnectionString("DefaultConnection")));

        
                // Register ProductService with the dependency injection container
                services.AddScoped<ProductService>();

                // Add other services as needed
                // ...

                // Add controllers
                services.AddControllers();
       

            /*
                    // Register DbContext
                    services.AddDbContext<MyAppContext>(options =>
                            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"))); // UseMySQL is deprecated, UseMySql is recommended
            */


            // Configure CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // existing code...



            // Enable CORS
            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
