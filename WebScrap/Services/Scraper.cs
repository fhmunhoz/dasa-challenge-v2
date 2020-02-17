using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System;

using Dasa.WebScrap.Interfaces;
using Dasa.WebScrap.Domain;
using Dasa.Data.Respository;

namespace Dasa.WebScrap.Services
{

    public class Scraper : IScraper
    {

        private readonly IOptions<List<TemplateBusca>> _templates;
        private readonly IScraperFactory _scraperFactory;
        private readonly IRespository _repository;
        private readonly ILogger _logger;
        public Scraper(IOptions<List<TemplateBusca>> templates,
                        IScraperFactory scraperFactory,
                        ILogger<Scraper> logger,
                        IRespository repository
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

        public async Task<int> PersistirScraping(int buscaId, IEnumerable<ResultadoScrap> resultados)
        {
            try
            {

                foreach (var resultado in resultados)
                {

                    if (buscaId == 0)
                    {
                        //Gera o ID de busca, para agrupar as consultas em datas diferentes
                        RegistroBusca registro = new RegistroBusca
                        {
                            DataHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm"),
                            NomeSiteOrigem = resultado.Origem

                        };

                        buscaId = await _repository.InserirBusca(registro);
                    }

                    var roupa = new Roupas
                    {
                        BuscaId = buscaId,
                        Categoria = resultado.Categoria,
                        Descricao = resultado.Descricao,
                        Nome = resultado.Nome,
                        Preco = resultado.Preco,
                        UrlImagem = resultado.UrlImagem,
                        UrlProduto = resultado.UrlProduto
                    };

                    roupa.Tamanhos = new List<RoupasTamanho>();
                    resultado.Tamanhos.ForEach(t =>
                        roupa.Tamanhos.Add(new RoupasTamanho
                        {
                            Tamanho = t
                        })
                    );

                    await _repository.InserirRoupa(roupa);

                }

                await _repository.SalvarDadosAsync();
                _logger.LogInformation("Resultado persistido");

                return buscaId;

            }
            catch (System.Exception ex)
            {
                var msgErro = ex.Message;
                _logger.LogError("Erro ao persitir resultado. Erro: {msgErro}", msgErro);
                throw;
            }

        }

        public async Task ConsolidarScraping(int id)
        {
            var busca = await _repository.EncontrarBuscaPorId(id);

            try
            {
                //var resultadosBusca = from b in _context.Roupas where b.BuscaId == id select b;
                var resultadosBusca = _repository.RetornaRoupasPorBuscaId(id);

                foreach (var item in resultadosBusca)
                {
                    //Será usado como chave para consolidar a busca
                    var urlProduto = item.UrlProduto;

                    var produtoConsolidado = _repository.RetornaBuscaConsolidadaPorUrl(urlProduto);

                    //Tamanho deverá ser atualizado em ambos os casos de novo ou produto já existente                    
                    var tamanhos = string.Join(',', (from tam in
                                                        _repository.RetornaTamanhosPorRoupaId(item.Id)
                                                     select tam.Tamanho).ToArray());

                    //Caso for um novo produto,
                    //adiciona novamente a lista de consolidado com a flag novo produto
                    if (produtoConsolidado == null)
                    {
                        produtoConsolidado = new BuscaConsolidada
                        {
                            Categoria = item.Categoria,
                            Descricao = item.Descricao,
                            MaiorPreco = false,
                            MenorPreco = false,
                            Nome = item.Nome,
                            Origem = busca.NomeSiteOrigem,
                            Preco = item.Preco,
                            ProdutoNovo = true,
                            UrlImagem = item.UrlImagem,
                            UrlProduto = item.UrlProduto,
                            Tamanhos = tamanhos
                        };

                        await _repository.InserirBuscaConsolidada(produtoConsolidado);

                    }
                    else
                    {

                        //Encontro o valor maximo e minimo do mesmo produto nas outras buscas                    
                        var precosMesmoProduto = (from vMax
                                            in _repository.RetornaRoupasPorURL(item.UrlProduto)
                                                  where vMax.BuscaId != busca.Id
                                                  select vMax.Preco).ToList();

                        decimal valorMinimo = 0;
                        decimal valorMaximo = 0;

                        if (precosMesmoProduto.Any())
                        {
                            valorMinimo = precosMesmoProduto.Min();
                            valorMaximo = precosMesmoProduto.Max();
                        }

                        //Caso não retorno resultados, a flag será false para o menor ou maior valor
                        produtoConsolidado.MenorPreco = valorMinimo > 0 ? produtoConsolidado.Preco < valorMinimo : false;
                        produtoConsolidado.MaiorPreco = valorMaximo > 0 ? produtoConsolidado.Preco > valorMaximo : false;
                        produtoConsolidado.ProdutoNovo = false;
                        produtoConsolidado.Tamanhos = tamanhos;
                        produtoConsolidado.Nome = item.Nome;
                        produtoConsolidado.Descricao = item.Nome;

                    }

                }

                await _repository.SalvarDadosAsync();
            }
            catch (System.Exception ex)
            {
                var msgErro = ex.Message;
                var origem = busca.NomeSiteOrigem;

                _logger.LogError("Erro ao consolidar a busca {origem}. Erro: {msgErro}", msgErro);
                throw new Exception("Ocorreu um erro ao consolidar a busca.");
                throw;
            }

        }

    }


}
