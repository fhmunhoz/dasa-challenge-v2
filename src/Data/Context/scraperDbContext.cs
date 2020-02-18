using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dasa.Data.Tables;

namespace Dasa.Data.Context
{
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