using System.Threading.Tasks;
using System.Collections.Generic;
using Dasa.WebScrap.Models;

namespace Dasa.WebScrap.Interfaces
{

    public interface IScraper
    {
        Task ExtrairDadosSites();

        List<RegistroScrap> RetornaRegistrosDeScraping();

        Task AtivarWebScrapingSite(RegistroScrap nomeSite);
        Task DesativarWebScrapingSite(RegistroScrap nomeSite);

        void AtivarBackGroundWorker(bool rodarImadiatamente);
        void DesativarBackGroundWorker();
        
    }

}

