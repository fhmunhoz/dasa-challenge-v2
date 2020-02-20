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
        [Route("ativar")]
        public async Task<ActionResult> AtivaWebScraping(RegistroScrap registro)
        {

            try
            {

                await _scraper.AtivarWebScrapingSite(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPatch()]
        [Route("desativar")]
        public async Task<ActionResult> DesativaWebScraping(RegistroScrap registro)
        {

            try
            {

                await _scraper.DesativarWebScrapingSite(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

    }
}