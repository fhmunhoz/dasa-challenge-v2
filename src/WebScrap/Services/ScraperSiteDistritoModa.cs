using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using System.Web;

using Dasa.WebScrap.Interfaces;
using Dasa.WebScrap.Models;
using Dasa.Data.Repository;
using Dasa.WebScrap.Helpers;

namespace Dasa.WebScrap.Services
{
    public class ScraperSiteDistritoModa : IScraperSiteDistritoModas
    {
        private TemplateBusca _template { get; set; }
        private IBrowsingContext _browsingContext { get; set; }

        private readonly ILogger _logger;
        private readonly IScraperBusca _scraperBusca;
        private readonly IScraperHelper _helper;

        public ScraperSiteDistritoModa(ILogger<ScraperSitePostHaus> logger,
                                        IScraperBusca scraperBusca,
                                        IScraperHelper helper)
        {
            _logger = logger;
            _scraperBusca = scraperBusca;
            _helper = helper;
        }

        private bool PossuiMaisPaginas(IDocument paginaCategoria)
        {

            var botaoPaginacao = paginaCategoria.QuerySelector(_template.SeletorBotaoProximaPagina);

            //Caso o botão de proxima pagina não seja encontrado ou HREF aponte para a propria página (#)
            //considera que não existe próxima página
            if (botaoPaginacao == null || botaoPaginacao.GetAttribute("href") == "#")
            {
                return false;
            }

            return true;
        }

        private async Task ExtrairDadosPorCategoria(string urlGridCategoria,
                                                    string nomeCategoria,
                                                    List<ResultadoScrap> resultados)
        {

            var paginaCategoria = await _browsingContext.OpenAsync(urlGridCategoria);
            var produtos = paginaCategoria.QuerySelectorAll(_template.SeletorGridProdutos);

            foreach (var produtoGrid in produtos)
            {

                //var eleLinkProduto = produtoGrid.QuerySelector(_template.SeletorLinkProduto);
                var urlProduto = produtoGrid.GetAttribute("href");

                try
                {

                    var paginaProduto = await _browsingContext.OpenAsync(urlProduto);
                    var nomeProduto = paginaProduto.QuerySelector(_template.SeletorNome).GetAttribute("content");

                    var roupa = new ResultadoScrap();
                    roupa.Origem = _template.Nome;
                    roupa.UrlProduto = urlProduto;
                    roupa.Nome = nomeProduto;
                    roupa.Categoria = nomeCategoria;
                    roupa.Descricao = paginaProduto.QuerySelector(_template.SeletorDescricao).GetAttribute("content");
                    roupa.UrlImagem = paginaProduto.QuerySelector(_template.SeletorUrlImagem).GetAttribute("content");
                    roupa.Preco = _helper.TratamentoPreco(paginaProduto.QuerySelector(_template.SeletorPreco).InnerHtml.Trim());

                    roupa.Tamanhos = new List<string>();
                    var elesTamanho = paginaProduto.QuerySelectorAll(_template.SeletorTamanhos);
                    foreach (var tamanho in elesTamanho)
                    {
                        roupa.Tamanhos.Add(string.IsNullOrWhiteSpace(tamanho.InnerHtml) ? "" : tamanho.InnerHtml.Trim());
                    }

                    resultados.Add(roupa);
                    _logger.LogInformation("Produto {nomeProduto} da categoria {nomeCategoria} extraido do página: {urlProduto}",
                        nomeProduto, nomeCategoria, urlProduto);

                }
                catch (System.Exception ex)
                {
                    var msgErro = ex.Message;
                    _logger.LogError("Produto da página {urlProduto} e categoria {nomeCategoria} não foi extraido. Erro: {msgErro}",
                        urlProduto, nomeCategoria, msgErro);
                }

            }

            if (PossuiMaisPaginas(paginaCategoria))
            {
                var urlProximaPagina = _helper.UrlProximaPagina(urlGridCategoria, _template.QueryStringPaginacao);
                await ExtrairDadosPorCategoria(urlProximaPagina, nomeCategoria, resultados);
            }

        }

        private async Task ExtrairDadosPagina(List<ResultadoScrap> resultados)
        {

            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            _browsingContext = BrowsingContext.New(config);

            //Encontra cada link de produto na tela principal de busca
            var gridProdutos = await _browsingContext.OpenAsync(_template.UrlInicial);

            //Encontras as TAGS com a url para a o grid de produtos por categoria
            var categorias = gridProdutos.QuerySelectorAll(_template.SeletorMenuCategorias);

            var buscaId = 0;
            foreach (var categoria in categorias)
            {

                var urlGridCategoria = categoria.GetAttribute("href");

                //Categoria está registrada no menu de grid de produtos, não foi encontrada 
                //dentro da página de detalhes do produto, 
                //padrão semelhante foi visto nos outros 3 sites
                //var nomeCategoria = categoria.QuerySelector(_template.SelectorCategoria).InnerHtml;
                var nomeCategoria = categoria.GetAttribute("title");

                await ExtrairDadosPorCategoria(urlGridCategoria, nomeCategoria, resultados);
                buscaId = await _scraperBusca.PersistirScraping(buscaId, resultados);
                resultados.Clear();

            }

            await _scraperBusca.ConsolidarScraping(buscaId);

        }

        public async Task ProcessaDadosPagina(TemplateBusca template)
        {
            _template = template;
            List<ResultadoScrap> resultado = new List<ResultadoScrap>();
            await ExtrairDadosPagina(resultado);
        }


    }

}