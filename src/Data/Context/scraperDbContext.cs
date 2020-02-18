using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dasa.Data.Tables;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dasa.Data.Context
{

    // public class ScraperDbContextFactory : IDesignTimeDbContextFactory<ScraperDbContext>
    // {
    //     public ScraperDbContext CreateDbContext(string[] args)
    //     {
    //         var optionsBuilder = new DbContextOptionsBuilder<ScraperDbContext>();
    //         optionsBuilder.UseSqlite("Data Source=..\\Data\\DB\\dasa-challenge.sqlite");

    //         return new ScraperDbContext(optionsBuilder.Options);
    //     }
    // }

    public class ScraperDbContext : DbContext
    {

        public ScraperDbContext(DbContextOptions options)
        : base(options)
        { }

        public DbSet<Roupas> Roupas { get; set; }
        public DbSet<RoupasTamanho> RoupasTamanho { get; set; }
        public DbSet<RegistroBusca> RegistroBusca { get; set; }
        public DbSet<BuscaConsolidada> BuscaConsolidada { get; set; }

    }
}