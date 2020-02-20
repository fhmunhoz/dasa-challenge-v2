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
using Dasa.Data.Repository;
using Hangfire;

namespace Dasa.WebScrap.Services
{

    public class Scraper : IScraper
    {

        private const string BACKGROUND_WORKER_NAME = "webscraping";

        private readonly IOptions<List<TemplateBusca>> _templates;
        private readonly IScraperFactory _scraperFactory;
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        public Scraper(IOptions<List<TemplateBusca>> templates,
                        IScraperFactory scraperFactory,
                        ILogger<Scraper> logger,
                        IRepository repository
                        )
        {
            _templates = templates;
            _logger = logger;
            _scraperFactory = scraperFactory;
            _repository = repository;
        }

        public async Task ExtrairDadosSites()
        {

            foreach (var busca in _templates.Value)
            {

                var regBusca = _repository.EncontraRegistroScrapingPorId(busca.Nome);

                if (regBusca == null || !regBusca.Ativo || regBusca.EmProcessamento)
                {
                    //Se o registro de busca não foi achado ou está desativado ou já está em processamento
                    continue;
                }

                regBusca.EmProcessamento = true;
                _repository.AlteraRegistroScraping(regBusca);
                await _repository.SalvarDadosAsync();

                try
                {

                    var service = _scraperFactory.RetornaScraperPorNome(busca.Nome);
                    await service.ProcessaDadosPagina(busca);

                    regBusca.DataUltimoScraping = DateTime.Now;
                    regBusca.EmProcessamento = false;
                    await _repository.SalvarDadosAsync();

                }
                catch (Exception ex)
                {

                    var nomeSite = busca.Nome;
                    var msgErro = ex.Message;
                    _logger.LogError("Ocorreu um erro ao importar os dados do site {nomeSite}. Erro: {msgErro}", nomeSite, msgErro);

                }


            }

        }

        public List<RegistroScrap> RetornaRegistrosDeScraping()
        {
                                    
            var regs = (from reg in _repository.ListaRegistrosScraping()
                        select new RegistroScrap
                        {
                            Ativo = reg.Ativo,
                            DataUltimoScraping = reg.DataUltimoScraping,
                            NomeSite = reg.NomeSite
                        }).OrderBy(o => o.NomeSite).ToList();

            return regs;

        }

        public async Task AtivarWebScrapingSite(RegistroScrap registro)
        {

            var reg = _repository.EncontraRegistroScrapingPorId(registro.NomeSite);
            if (reg is null)
                throw new Exception(string.Format("Web Scraping do site  {0} não encontrado", registro.NomeSite));

            reg.Ativo = true;
            reg.EmProcessamento = false;
            _repository.AlteraRegistroScraping(reg);
            await _repository.SalvarDadosAsync();
          
        }

        public async Task DesativarWebScrapingSite(RegistroScrap registro)
        {
            var reg = _repository.EncontraRegistroScrapingPorId(registro.NomeSite);
            if (reg is null)
                throw new Exception(string.Format("Web Scraping do site {0} não encontrado", registro.NomeSite));
         
            reg.Ativo = false;
            reg.EmProcessamento = false;
            _repository.AlteraRegistroScraping(reg);
            await _repository.SalvarDadosAsync();
        }

        public void AtivarBackGroundWorker(bool rodarImadiatamente) {

            var cron = "0 1 * * *"; //Uma vez ao dia
            RecurringJob.AddOrUpdate(() =>
                    ExtrairDadosSites(),
                    cron,
                    TimeZoneInfo.Local,
                    BACKGROUND_WORKER_NAME.ToLower());

            if (rodarImadiatamente) {
                BackgroundJob.Enqueue(() =>
                    ExtrairDadosSites());
            }

        }

        public void DesativarBackGroundWorker()
        {
            RecurringJob.RemoveIfExists(BACKGROUND_WORKER_NAME.ToLower());
        }

    }


}
