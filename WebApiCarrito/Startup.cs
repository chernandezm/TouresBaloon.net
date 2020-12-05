using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Confluent.Kafka;
using Infraestructure.Context;
using Infraestructure.Messaging;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApiCarrito.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;


namespace WebApiCarrito
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var producerConfig = new ProducerConfig();
            Configuration.Bind("Producer", producerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAsyncRepository<Canasta>, CanastaRepository>();
            services.AddEntityFrameworkNpgsql().AddDbContext<CanastaContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("MyConexion")));
            services.AddScoped<ICanastaService, CanastaServices>();
            services.AddTransient<ICanastaPublisher, CanastaPublisher>();
            services.AddTransient<IOrdenConsumer, OrdenConsumer>();
            services.AddTransient<IOrdenPublisher, OrdenPublisher>();
           // services.AddHostedService<CanastaConsumer>();
            services.AddHealthChecks()
                .AddCheck("memoria", new ApiHealthCheck())
                .AddNpgSql(
                npgsqlConnectionString: Configuration.GetConnectionString("MyConexion"),
                healthQuery: "SELECT 1;",
                name: "Sql",
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
