using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiOrden.Controllers
{
    [Route("api/orden")]
    [ApiController]
    public class OrdenController : Controller
    {
        private IOrdenService _servicio;

        public OrdenController(IOrdenService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IEnumerable<OrdenDTO> GetOrden()
        {
            var ordenes = _servicio.GetOrdenAsync();
            return ordenes.Result;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrdenDTO> GetOrdenPorId(int id)
        {
            try
            {
                var orden = _servicio.GetOrdenAsync(id);
                return orden.Result;
            }
            catch (ItemNoExisteException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OrdenDTO> CreateOrden([FromBody] OrdenDTO orden)
        {
            if (!ModelState.IsValid) return BadRequest();

            var ordenCreada = _servicio.CreateOrdenAsync(orden);
            return ordenCreada.Result;
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOrden(int id)
        {
            try
            {
                _servicio.DeleteOrdenAsync(id);
                return Ok();
            }
            catch (ItemNoExisteException e)
            {
                return NotFound(e.Message);
            }
        }

        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdateOrden()
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid) return BadRequest();

        //        _servicio.UpdateOrdenAsync();
        //        return Ok();
        //    }
        //    catch (ItemNoExisteException e)
        //    {
        //        return NotFound();
        //    }
        //}

    }
}