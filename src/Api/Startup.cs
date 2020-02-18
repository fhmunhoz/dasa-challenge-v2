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
using Microsoft.EntityFrameworkCore;

using Dasa.Data.Repository;
using Dasa.Data.Context;

using Dasa.Catalogo.Interfaces;
using Dasa.Catalogo.Services;

using Dasa.WebScrap.Helpers;
using Dasa.WebScrap.Interfaces;
using Dasa.WebScrap.Services;
using Dasa.WebScrap.Domain;


namespace Dasa.Api
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

            services.AddDbContext<ScraperDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("default")));

            services.AddScoped<IRepository, Repository>();

            services.AddScoped<IBusca, Busca>();

            services.AddScoped<IScraper, Scraper>();
            services.AddScoped<IScraperBusca, ScraperBusca>();
            services.AddScoped<IScraperFactory, ScraperFactory>();
            services.AddScoped<IScraperSiteDistritoModas, ScraperSiteDistritoModa>();
            services.AddScoped<IScraperSitePosthaus, ScraperSitePostHaus>();
            services.AddScoped<IScraperSiteVKModas, ScraperSiteVkModas>();
            services.AddScoped<IScraperHelper, ScraperHelper>();
            services.Configure<List<TemplateBusca>>(Configuration.GetSection("ConfiguracoesBuscador:Sites"));


            services.AddControllers();
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

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
