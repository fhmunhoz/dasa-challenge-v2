using System.Threading.Tasks;
using System.Collections.Generic;

namespace Dasa.WebScrap.Interfaces
{

    public interface IScraper
    {
        Task ExtrairDadosSites();
        Task<int> PersistirScraping(int buscaId, IEnumerable<ResultadoScrap> resultados);
        Task ConsolidarScraping(int buscaId);
    }
     
}

