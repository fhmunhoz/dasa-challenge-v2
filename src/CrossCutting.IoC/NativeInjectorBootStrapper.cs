
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Dasa.Data.Repository;
using Dasa.Data.Context;

using Dasa.Catalogo.Interfaces;
using Dasa.Catalogo.Services;

using Dasa.WebScrap.Helpers;
using Dasa.WebScrap.Interfaces;
using Dasa.WebScrap.Services;
using Dasa.WebScrap.Models;

using System.Collections.Generic;

namespace Dasa.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            //services.AddDbContext<ScraperDbContext>(options => options.UseSqlite(configuration.GetConnectionString("default")));
            services.AddDbContext<ScraperDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("default")));

            services.AddScoped<IRepository, Repository>();

            services.AddScoped<IBusca, Busca>();

            services.AddScoped<IScraper, Scraper>();
            services.AddScoped<IScraperBusca, ScraperBusca>();
            services.AddScoped<IScraperFactory, ScraperFactory>();
            services.AddScoped<IScraperSiteDistritoModas, ScraperSiteDistritoModa>();
            services.AddScoped<IScraperSitePosthaus, ScraperSitePostHaus>();
            services.AddScoped<IScraperSiteVKModas, ScraperSiteVkModas>();
            services.AddScoped<IScraperHelper, ScraperHelper>();

            services.Configure<List<TemplateBusca>>(setting =>
            {
                configuration.GetSection("ConfiguracoesBuscador:Sites").Bind(setting);
            });

            //services.Configure<List<TemplateBusca>>(configuration.GetSection("ConfiguracoesBuscador:Sites"));

        }
    }
}