﻿namespace Verizon.Connect.QueryService
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json.Serialization;

    using Swashbuckle.AspNetCore.Swagger;

    using Verizon.Connect.Domain.Plot.Repositories;
    using Verizon.Connect.Infra.Data;
    using Verizon.Connect.Infra.Data.Options;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Verizon.Connect.QueryService"); });

            app.UseResponseCompression();

            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(
                options =>
                    {
                        options.Providers.Add<BrotliCompressionProvider>();
                        options.Providers.Add<GzipCompressionProvider>();
                    });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options =>
                        {
                            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                        });

            // Redis
            services.Configure<RedisOptions>(this.Configuration.GetSection("Redis"));
            services.AddSingleton<RedisRepository>();
            services.AddSingleton<IPlotRepository, PlotRepository>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Verizon.Connect.QueryService", Version = "v1" }); });


        }
    }
}