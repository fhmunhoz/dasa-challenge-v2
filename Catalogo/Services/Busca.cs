using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;

using Dasa.Data.Respository;
using Dasa.Catalogo.Interfaces;
using Dasa.Catalogo.Models;
using Dasa.CrossCutting.Helpers;

namespace Dasa.Catalogo.Services
{
    public class Busca : IBusca
    {

        private readonly IRespository _repository;
        private readonly ILogger _logger;

        public Busca(IRespository repository,
                        ILogger<Busca> logger)
        {
            _repository = repository;
            _logger = logger;
        }
 
        public async Task<PagingHelper<BuscaViewModel>> CompararProdutos(string termoBusca, int paginaAtual, int itensPagina)
        {

            try
            {
                var resultados = (from bus in _repository.RetornaBuscaConsolidadaPorCategoria(termoBusca)
                                  select new BuscaViewModel
                                  {
                                      Categoria = bus.Categoria,
                                      Descricao = bus.Descricao,
                                      Nome = bus.Nome,
                                      Origem = bus.Origem,
                                      Preco = bus.Preco,
                                      Tamanhos = bus.Tamanhos,
                                      UrlImagem = bus.UrlImagem,
                                      UrlProduto = bus.UrlProduto,
                                      PrecoOrdenacao = bus.PrecoOrdenacao
                                  }).OrderByDescending(o => o.PrecoOrdenacao);

                var resultadosPaginados = await PagingHelper<BuscaViewModel>.CriarPaginacao(resultados, paginaAtual, itensPagina);
                return resultadosPaginados;

            }
            catch (System.Exception ex)
            {
                var msgErro = ex.Message;
                _logger.LogError("Ocorreu um erro ao retornar os itens. Erro: {msgErro}", msgErro);
                throw new Exception("Ocorreu um erro ao retornar os itens.");
            }

        }

    }
}