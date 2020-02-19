using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using Dasa.Data.Context;
using Dasa.Data.Tables;


namespace Dasa.Data.Repository
{
    public class Repository : IRepository, IDisposable
    {

        private readonly ScraperDbContext _context;

        public Repository(ScraperDbContext context,
                        ILogger<Repository> logger)
        {
            _context = context;
        }

        public async Task<int> InserirBusca(RegistroBusca busca)
        {
            _context.RegistroBusca.Add(busca);
            await _context.SaveChangesAsync();
            return busca.Id;
        }

        public async Task InserirRoupa(Roupas roupa)
        {
            await _context.Roupas.AddAsync(roupa);
        }

        public async Task<RegistroBusca> EncontrarBuscaPorId(int id)
        {

            return await _context.RegistroBusca.FindAsync(id);

        }

        public IEnumerable<Roupas> RetornaRoupasPorBuscaId(int buscaId)
        {

            return from b in _context.Roupas where b.BuscaId == buscaId select b;

        }

        public BuscaConsolidada RetornaBuscaConsolidadaPorUrl(string url)
        {

            return (from p
                    in _context.BuscaConsolidada
                    where p.UrlProduto == url
                    select p).FirstOrDefault();

        }

        public IEnumerable<RoupasTamanho> RetornaTamanhosPorRoupaId(int roupaId)
        {

            return (from tam
                        in _context.RoupasTamanho
                    where tam.RoupaId == roupaId
                    select tam);

        }

        public async Task InserirBuscaConsolidada(BuscaConsolidada busca)
        {
            await _context.BuscaConsolidada.AddAsync(busca);
        }

        public IEnumerable<Roupas> RetornaRoupasPorURL(string url)
        {

            return (from vMax
                    in _context.Roupas
                    where vMax.UrlProduto == url
                    select vMax);

        }

        public async Task SalvarDadosAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public IQueryable<BuscaConsolidada> RetornaBuscaConsolidada()
        {

            return from bus in _context.BuscaConsolidada
                   select bus;

        }

    }
}