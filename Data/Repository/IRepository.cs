using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dasa.Data.Tables;
using System.Linq;

namespace Dasa.Data.Repository
{

    public interface IRepository : IDisposable
    {

        Task<int> InserirBusca(RegistroBusca busca);

        Task InserirRoupa(Roupas roupa);

        Task SalvarDadosAsync();

        Task<RegistroBusca> EncontrarBuscaPorId(int id);

        IEnumerable<Roupas> RetornaRoupasPorBuscaId(int buscaId);

        BuscaConsolidada RetornaBuscaConsolidadaPorUrl(string url);

        IEnumerable<RoupasTamanho> RetornaTamanhosPorRoupaId(int roupaId);

        Task InserirBuscaConsolidada(BuscaConsolidada busca);

        IEnumerable<Roupas> RetornaRoupasPorURL(string url);

        IQueryable<BuscaConsolidada> RetornaBuscaConsolidadaPorCategoria(string termoBusca);

    }

}