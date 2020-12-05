using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infraestructure.Messaging;
using ApplicationCore.Interfaces;
using Infraestructure.Services;
using Confluent.Kafka;
using ApplicationCore.Entities;
using Infraestructure.Repositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CanastaConsumer
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
            services.AddScoped<IAsyncRepository<Canasta>, CanastaRepository>();
            services.AddEntityFrameworkNpgsql().AddDbContext<CanastaContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("MyConexion")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICanastaPublisher, CanastaPublisher>();
            services.AddScoped<ICanastaService, CanastaServices>();
            services.AddHostedService<Infraestructure.Messaging.CanastaConsumer>();
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
            });
        }
    }
}
