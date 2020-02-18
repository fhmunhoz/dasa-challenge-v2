using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System;
using System.Linq;

using Dasa.WebScrap.Interfaces;
using Dasa.WebScrap.Models;

namespace Dasa.WebScrap.Services
{

    public class Scraper : IScraper
    {

        private readonly IOptions<List<TemplateBusca>> _templates;
        private readonly IScraperFactory _scraperFactory;
        private readonly ILogger _logger;
        public Scraper(IOptions<List<TemplateBusca>> templates,
                        IScraperFactory scraperFactory,
                        ILogger<Scraper> logger
                        )
        {
            _templates = templates;
            _logger = logger;
            _scraperFactory = scraperFactory;
        }

        public async Task ExtrairDadosSites()
        {

            foreach (var busca in _templates.Value)
            {

                if (string.IsNullOrEmpty(busca.UrlInicial))
                {
                    //Se o seletor principal estiver em branco, não será possivel fazer a leitura
                    continue;
                }

                try
                {

                    var service = _scraperFactory.RetornaScraperPorNome(busca.Nome);
                    await service.ProcessaDadosPagina(busca);
                }
                catch (System.Exception ex)
                {

                    var nomeSite = busca.Nome;
                    var msgErro = ex.Message;
                    _logger.LogError("Ocorreu um erro ao importar os dados do site {nomeSite}. Erro: {msgErro}", nomeSite, msgErro);

                }


            }

        }

    }


}
