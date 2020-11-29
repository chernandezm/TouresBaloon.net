using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCarrito.Controllers
{
    [Route("api/canasta")]
    [ApiController]
    public class CanastaController : ControllerBase
    {
        private ICanastaService _servicio;

        public CanastaController(ICanastaService canastaService)
        {
            _servicio = canastaService;
        }

        [HttpGet]
        public IEnumerable<CanastaDTO> GetCanasta()
        {
            var canastas = _servicio.GetCanastaAsync();
            return canastas.Result;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CanastaDTO> GetCanastaPorId(int id)
        {
            try
            {
                var canasta = _servicio.GetCanastaAsync(id);
                return canasta.Result;
            }
            catch (ItemNoExisteException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CanastaDTO> CreateCanasta([FromBody] CanastaDTO canasta)
        {
            if (!ModelState.IsValid) return BadRequest();

            var canastaCreada = _servicio.CreateCanastaAsync(canasta);
            return canastaCreada.Result;
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCanasta(int id)
        {
            try
            {
                _servicio.DeleteCanastaAsync(id);
                return Ok();
            }
            catch (ItemNoExisteException e)
            {
                return NotFound(e.Message);
            }
        }


    }
}