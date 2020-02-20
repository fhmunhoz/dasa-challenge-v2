using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dasa.WebScrap.Interfaces;
using Dasa.WebScrap.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackGroundWorkerController : ControllerBase
    {

        private readonly IScraper _scraper;

        public BackGroundWorkerController(IScraper scraper)
        {
            _scraper = scraper;
        }

        [HttpGet("")]
        public ActionResult GetScrapBackGroudndWorkers()
        {

            try
            {
                return Ok(_scraper.RetornaRegistrosDeScraping());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("ativarWebScrapSite")]
        public async Task<ActionResult> AtivaWebScraping(RegistroScrap registro)
        {

            try
            {

                await _scraper.AtivarWebScrapingSite(registro);
                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPatch()]
        [Route("desativarWebScrapSite")]
        public async Task<ActionResult> DesativaWebScraping(RegistroScrap registro)
        {

            try
            {

                await _scraper.DesativarWebScrapingSite(registro);
                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("ativarBackgroudWorker/{ativar}/{rodarImadiatamente}")]
        public ActionResult AtivaDEsativaBackGroudWorker(bool ativar, bool rodarImadiatamente)
        {

            try
            {

                if (ativar)
                    _scraper.AtivarBackGroundWorker(rodarImadiatamente);
                else
                    _scraper.DesativarBackGroundWorker();

                return Ok(new { 
                    sucesso = true,
                    mensagem = "Operação realizada com sucesso"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = ex.Message
                });
            }
        }

    }
}