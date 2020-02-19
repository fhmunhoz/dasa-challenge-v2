using System;
using Xunit;
using Dasa.Catalogo.Services;
using Dasa.Data.Repository;
using Dasa.Data.Tables;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;

//Quando é feito o Mock do iqueriable, gera um erro
//The provider for the source IQueryable doesn't implement IAsyncQueryProvider
//A biblioteca descrica abaixo, implmenta operações do EFCore para o Moq
//https://stackoverflow.com/questions/51023223/the-provider-for-the-source-iqueryable-doesnt-implement-iasyncqueryprovider
//https://www.nuget.org/packages/MockQueryable.Moq/

namespace Dasa.Catalogo.Test
{

    public class BuscaServiceTest
    {

        private Mock<IRepository> _mockRepo;
        private Busca _target;
        private Mock<ILogger<Busca>> _mocklogger;

        public BuscaServiceTest()
        {            
        }

        private List<BuscaConsolidada> MocBuscaConsolidada() {

            List<BuscaConsolidada> mock = new List<BuscaConsolidada>();

            var primeiroProduto = new BuscaConsolidada
            {
                Categoria = "BLAZER",
                Nome = "Produto 001",
                PrecoOrdenacao = 1
            };
            mock.Add(primeiroProduto);

            var segundoProduto = new BuscaConsolidada
            {
                Categoria = "JAQUETA",
                Nome = "Produto 002",
                PrecoOrdenacao = 2
            };
            mock.Add(segundoProduto);

            var terceiroProduto = new BuscaConsolidada
            {
                Categoria = "CALÇA",
                Nome = "Produto 003",
                PrecoOrdenacao = 3
            };
            mock.Add(terceiroProduto);

            var quartoProduto = new BuscaConsolidada
            {
                Categoria = "CALÇA",
                Nome = "Produto 004",
                PrecoOrdenacao = 4
            };
            mock.Add(quartoProduto);

            var quintoProduto = new BuscaConsolidada
            {
                Categoria = "CALÇÃO",
                Nome = "Produto 005",
                PrecoOrdenacao = 5
            };
            mock.Add(quintoProduto);

            return mock;

        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void TestePaginacaoBuscaConsolidadePorCategoria(int itensPorPagina)
        {
            
            _mockRepo = new Mock<IRepository>();
            _mocklogger = new Mock<ILogger<Busca>>();
            _target = new Busca(_mockRepo.Object, _mocklogger.Object);
            var retornoMocRepo = MocBuscaConsolidada().AsQueryable().BuildMock();
            _mockRepo.Setup(x => x.RetornaBuscaConsolidada()).Returns(retornoMocRepo.Object);            

            var resultadoService = _target.CompararProdutos("cal", 1, itensPorPagina);

            Assert.Equal(itensPorPagina, resultadoService.Result.Count);            

        }

        [Fact]
        public void TesteOrdenacaoBuscaConsolidadePorCategoria()
        {
            
            _mocklogger = new Mock<ILogger<Busca>>();
            _mockRepo = new Mock<IRepository>();
            _target = new Busca(_mockRepo.Object, _mocklogger.Object);
            
            var produtos = MocBuscaConsolidada();
            var retornoMocRepo = produtos.AsQueryable().BuildMock(); 
            _mockRepo.Setup(x => x.RetornaBuscaConsolidada()).Returns(retornoMocRepo.Object);            

            var resultadoService = _target.CompararProdutos("cal", 1, 2);
            var primeiroResultadoService = resultadoService.Result[0];
            var primeiroResultadoEsperado = produtos.OrderByDescending(o => o.PrecoOrdenacao).ToList()[0];

            Assert.Equal(primeiroResultadoEsperado.Nome, primeiroResultadoService.Nome);

        }
       
        [Theory]
        [InlineData("calça")]
        [InlineData("cal")]
        [InlineData("blazer")]
        [InlineData("saia")]
        [InlineData("")]
        public void TesteBuscaPorCategoria(string categoria) {

            _mocklogger = new Mock<ILogger<Busca>>();
            _mockRepo = new Mock<IRepository>();
            _target = new Busca(_mockRepo.Object, _mocklogger.Object);

            var produtos = MocBuscaConsolidada();
            var retornoMocRepo = produtos.AsQueryable().BuildMock();
            _mockRepo.Setup(x => x.RetornaBuscaConsolidada()).Returns(retornoMocRepo.Object);

            var resultadoService = _target.CompararProdutos(categoria, 1, 10);            
            var resultadoEsperado = (from p in produtos where p.Categoria.ToLower().Contains(categoria.ToLower()) select p).ToList();

            Assert.Equal(resultadoEsperado.Count ,resultadoService.Result.Count);            

        }

        [Theory]
        [InlineData("calça")]
        [InlineData("blazer")]
        [InlineData("saia")]
        [InlineData("")]
        public void TesteBuscaCategoriaPorNome(string categoria)
        {

            _mocklogger = new Mock<ILogger<Busca>>();
            _mockRepo = new Mock<IRepository>();
            _target = new Busca(_mockRepo.Object, _mocklogger.Object);

            var produtos = MocBuscaConsolidada();
            var retornoMocRepo = produtos.AsQueryable().BuildMock();
            _mockRepo.Setup(x => x.RetornaBuscaConsolidada()).Returns(retornoMocRepo.Object);

            var resultadoService = _target.BuscaCategorias(categoria);
            var resultadoEsperado = (from p in produtos where p.Categoria.ToLower().Contains(categoria.ToLower()) select p.Categoria).Distinct().ToList();

            Assert.Equal(resultadoEsperado.Count, resultadoService.Count);

        }

    }
}
