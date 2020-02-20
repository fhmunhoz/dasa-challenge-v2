using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dasa.Catalogo.Interfaces;


namespace Dasa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuscaController : ControllerBase
    {
        private readonly IBusca _busca;

        public BuscaController(IBusca busca)
        {            
            _busca = busca;
        }

        [HttpGet("{termoBusca}/{paginaAtual:int}/{itensPorPagina:int}")]
        public async Task<ActionResult> Getbusca(string termoBusca, int paginaAtual, int itensPorPagina)
        {

            if (string.IsNullOrWhiteSpace(termoBusca))
                return BadRequest("Preencha algum termo de busca");
            if (itensPorPagina <= 0)
                return BadRequest("Itens por página deve ser maior do que zero");
            if (paginaAtual <= 0)
                return BadRequest("Pagina atual deve ser maior do que zero");

            try
            {
                var retorno = await _busca.CompararProdutos(termoBusca, paginaAtual, itensPorPagina);

                return Ok(new
                {
                    resultados = retorno,
                    PaginaAtual = retorno.PaginaAtual,
                    TotalPaginas = retorno.TotalPaginas,
                    PossuiPaginaAnterior = retorno.PossuiPaginaAnterior,
                    PossuiProximaPagina = retorno.PossuiProximaPagina
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("categorias/{termoBusca}")]
        public ActionResult GetCategorias(string termoBusca)
        {

            if (string.IsNullOrWhiteSpace(termoBusca))
                return BadRequest("Preencha algum termo de busca");

            try
            {

                var listaGererica = new List<Tuple<string, string>>();
                _busca.BuscaCategorias(termoBusca)
                .ForEach(c => listaGererica.Add(new Tuple<string, string>(c, c)));

                return Ok(listaGererica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }

        }

        // [HttpGet("scrap")]
        // public ActionResult InciarLeituraSites()
        // {

        //     try
        //     {
        //         var cron = "0 1 * * *"; //Uma vez ao dia
        //         Hangfire.RecurringJob.AddOrUpdate(() =>
        //         _scraper.ExtrairDadosSites(),
        //         cron,
        //         TimeZoneInfo.Local,
        //         "scrapersites");

        //         return Ok(new {
        //             sucesso = true,
        //             mensagem = "Serviço de scrap iniciado com sucesso"
        //         });

        //     }
        //     catch (System.Exception ex)
        //     {                
        //         return BadRequest(new {
        //             sucesso = false,
        //             mensagem = ex.Message
        //         });
        //     }
        // }

    }
}