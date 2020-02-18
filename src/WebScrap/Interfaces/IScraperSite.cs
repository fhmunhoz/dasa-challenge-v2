using System.Threading.Tasks;
using Dasa.WebScrap.Models;
using System.Collections.Generic;


namespace Dasa.WebScrap.Interfaces
{

    public interface IScraperSitePosthaus : IScraperSite
    {
    }


    public interface IScraperSiteVKModas : IScraperSite
    {
    }

    public interface IScraperSiteDistritoModas : IScraperSite
    {
    }

    public interface IScraperSite
    {
        Task ProcessaDadosPagina(TemplateBusca template);
    }

}

