using System.Linq;
using KitchenService.Core;
using KitchenService.Core.Interfaces;
using KitchenService.DataAccess.Model;
using KitchenService.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KitchenService.Api
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
            services.AddDbContext<KitchenContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AzureSql"));
            });

            services.AddScoped<IFridgeItemRepository, FridgeItemRepository>();
            services.AddScoped<IFridgeService, FridgeService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowLocalNgServeAndAzure",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                                          "http://2006-kitchen-site.azurewebsites.net",
                                                          "https://2006-kitchen-site.azurewebsites.net")
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials();
                                  });
            });

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.SuppressAsyncSuffixInActionNames = false;

                // lower priority of text/plain among supported content types
                var stringFormatter = options.OutputFormatters.OfType<StringOutputFormatter>().FirstOrDefault();
                if (stringFormatter != null)
                {
                    options.OutputFormatters.Remove(stringFormatter);
                    options.OutputFormatters.Add(stringFormatter);
                }
            })
                .AddXmlDataContractSerializerFormatters();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kitchen API V1");
            });

            app.UseRouting();

            // apply CORS policy globally
            app.UseCors("AllowLocalNgServeAndAzure");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
