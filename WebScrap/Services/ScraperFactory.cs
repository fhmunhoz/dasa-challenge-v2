using System;
using Microsoft.Extensions.DependencyInjection;
using Dasa.WebScrap.Interfaces;

namespace Dasa.WebScrap.Services
{

    public class ScraperFactory : IScraperFactory
    {

        private readonly IScraperSiteDistritoModas _distrito;
        private readonly IScraperSitePosthaus _post;
        private readonly IScraperSiteVKModas _vk;

        public ScraperFactory(IScraperSiteDistritoModas distrito,
                            IScraperSitePosthaus post,
                            IScraperSiteVKModas vk)
        {            
            _vk = vk;
            _post = post;
            _distrito = distrito;
        }
        public IScraperSite RetornaScraperPorNome(string name)
        {
            if (name == "Posthaus")
            {
                return _post;
            }
            else if (name == "DistritoModas")
            {
                return _distrito;
            }
            else if (name == "VKModas")
            {
                return _vk;
            }
            else
            {
                throw new NotImplementedException(string.Format("Serviço {0} não encontrado", name));
            }
        }

    }

}

