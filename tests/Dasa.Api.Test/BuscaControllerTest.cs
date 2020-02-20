using System;
using Xunit;
using Dasa.Api.Controllers;
using Dasa.Catalogo.Interfaces;
using Dasa.Catalogo.Helpers;
using Dasa.Catalogo.Models;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MockQueryable.Moq;

namespace Dasa.Api.Test
{
    public class BuscaControllerTest
    {

        BuscaController _controllerBusca;

        private List<string> MocListaCategorias() {

            var cats = new List<string>();

            cats.AddRange(
                new string[] { "BLUSAS","JAQUETAS","CACHECOL","SHORTS","CAMISAS" }
                );

            return cats;

        }

        private List<BuscaViewModel> MocBusca() {

            List<BuscaViewModel> mocBusca = new List<BuscaViewModel>();
            mocBusca.Add(new BuscaViewModel { 
                Categoria = "BLUSA",
                Nome = "BLUSA 001",
                Preco = 150
            });

            mocBusca.Add(new BuscaViewModel
            {
                Categoria = "MACACAO",
                Nome = "MACACAO 002",
                Preco = 250
            });

            mocBusca.Add(new BuscaViewModel
            {
                Categoria = "JAQUETA",
                Nome = "JAQUETA 003",
                Preco = 25
            });

            return mocBusca;

        }

        //[Fact]
        //public void TesteGetBusca()
        //{

        //    Mock<IBusca> mockBuscaService = new Mock<IBusca>();
        //    _controllerBusca = new BuscaController(mockBuscaService.Object);
        //    var m = MocBusca().AsQueryable().BuildMock();
        //    //var mocBuscaEsperado = PagingHelper<BuscaViewModel>.CriarPaginacao(m, 1, 3);

        //    mockBuscaService.Setup(x => x.CompararProdutos("TESTE", 1,3)).Returns(m.Object);
            
        //    var resultadoGetCategorias = _controllerBusca.Getbusca("TESTE",1,3);
        //    var resultadoGet = Assert.IsType<OkObjectResult>(resultadoGetCategorias);
        //    //Falta etstar esse metodo, só que para isso preciso converter para o metodo anonimo
        //    var tipoResultadoEsperado = new
        //    {
        //        resultados = mocBuscaEsperado,
        //        PaginaAtual = 0,
        //        TotalPaginas = false,
        //        PossuiPaginaAnterior = false,
        //        PossuiProximaPagina = false
        //    };
        //    var resultadoGetObtido = CastTo(resultadoGet.Value, tipoResultadoEsperado);

        //    Assert.Equal(mocBuscaEsperado.Result.Count, resultadoGetObtido.resultados.Result.Count);

        //}

        //private static T CastTo<T>(object value, T targetType)
        //{
        //    // targetType above is just for compiler magic
        //    // to infer the type to cast x to

        //    return (T)value;
        //}

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TesteGetCategoriasNulo(string categoria)
        {

            Mock<IBusca> mockBuscaService = new Mock<IBusca>();
            _controllerBusca = new BuscaController(mockBuscaService.Object);
            var cats = new List<string>();
            cats.AddRange(
                new string[] { "CACHECOL", "CAMISAS"  }
                );

            mockBuscaService.Setup(x => x.BuscaCategorias(categoria)).Returns(cats);
            
            var listaGererica = new List<Tuple<string, string>>();
            cats.ForEach(c => listaGererica.Add(new Tuple<string, string>(c, c)));

            var resultadoGetCategorias = _controllerBusca.GetCategorias(categoria);
            Assert.IsType<BadRequestObjectResult>(resultadoGetCategorias);
            
        }

        [Fact]
        public void TesteGetCategorias()
        {

            Mock<IBusca> mockBuscaService = new Mock<IBusca>();
            _controllerBusca = new BuscaController(mockBuscaService.Object);
            var cats = new List<string>();
            cats.AddRange(
                new string[] { "CACHECOL", "CAMISAS" }
                );

            mockBuscaService.Setup(x => x.BuscaCategorias("teste")).Returns(cats);

            var listaGererica = new List<Tuple<string, string>>();
            cats.ForEach(c => listaGererica.Add(new Tuple<string, string>(c, c)));

            var resultadoGetCategorias = _controllerBusca.GetCategorias("teste");
            var viewResult = Assert.IsType<OkObjectResult>(resultadoGetCategorias);            

            Assert.IsAssignableFrom<List<Tuple<string, string>>>(viewResult.Value);

            Assert.Equal(cats.Count, ((List<Tuple<string, string>>)viewResult.Value).Count);

        }

    }
}
