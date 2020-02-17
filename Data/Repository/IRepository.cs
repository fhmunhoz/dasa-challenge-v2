using System;
using System.Threading.Tasks;
using Dasa.CrossCutting.DTO;
using System.Collections.Generic;
using Dasa.CrossCutting.Helpers;
using Dasa.Data.Tables;


namespace Dasa.Data.Respository
{

    public interface IRespository : IDisposable
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
        
        IEnumerable<BuscaConsolidada> RetornaBuscaConsolidadaPorCategoria(string termoBusca);

    }

}