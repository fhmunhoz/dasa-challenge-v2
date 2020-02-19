using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;

using Dasa.Data.Repository;
using Dasa.Catalogo.Interfaces;
using Dasa.Catalogo.Helpers;
using Dasa.Catalogo.Models;

namespace Dasa.Catalogo.Services
{
    public class Busca : IBusca
    {

        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public Busca(IRepository repository,
                        ILogger<Busca> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PagingHelper<BuscaViewModel>> CompararProdutos(string termoBusca, int paginaAtual, int itensPagina)
        {

            try
            {
                var resultados = (from bus in _repository.RetornaBuscaConsolidada()
                                  where bus.Categoria.ToLower().Contains(termoBusca.ToLower())
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
                                      PrecoOrdenacao = bus.PrecoOrdenacao,
                                      MenorPreco = bus.MenorPreco,
                                      ProdutoNovo = bus.ProdutoNovo
                                  }).OrderByDescending(o => o.PrecoOrdenacao);

                var resultadosPaginados = await PagingHelper<BuscaViewModel>.CriarPaginacao(resultados, paginaAtual, itensPagina);
                return resultadosPaginados;

            }
            catch (System.Exception ex)
            {
                var msgErro = ex.Message;
                _logger.LogError("Ocorreu um erro ao retornar os itens. Erro: {msgErro}", msgErro);
                throw new Exception("Ocorreu um erro ao retornar os itens." + msgErro);
            }

        }

        public List<string> BuscaCategorias(string termoBusca)
        {

            var categorias = (from cats in _repository.RetornaBuscaConsolidada()
                              where
                                cats.Categoria.ToLower().Contains(termoBusca.ToLower())
                              select cats.Categoria).Distinct().ToList();

            return categorias.OrderBy(o => o).ToList();

        }

    }
}