using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
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
            services.AddCors(options =>
            {
                // defining the policy
                options.AddPolicy(name: "AllowLocalNgServe",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200")
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials();
                                  });
            });

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;

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
            app.UseCors("AllowLocalNgServe");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
