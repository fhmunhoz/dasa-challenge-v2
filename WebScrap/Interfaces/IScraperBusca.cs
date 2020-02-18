using System.Threading.Tasks;
using System.Collections.Generic;
using Dasa.WebScrap.Models;

namespace Dasa.WebScrap.Interfaces
{

    public interface IScraperBusca
    {
        Task<int> PersistirScraping(int buscaId, IEnumerable<ResultadoScrap> resultados);
        Task ConsolidarScraping(int buscaId);
    }

}

