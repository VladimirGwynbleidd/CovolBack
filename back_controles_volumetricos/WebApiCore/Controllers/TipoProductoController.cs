using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SYE.Covol;
using SYE.Covol.DAO;
using System;
namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class TipoProductoController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObtenerTipoProducto()
        {
            ICatalogo<TipoProducto> covol = new TipoProductoCovol();
            Success<TipoProducto> success;

            try
            {
                success = covol.Obtener();
                if (success.Exito)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound(success);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult AgregarTipoProducto([FromForm]TipoProducto tipoProducto)
        {
            ICatalogo<TipoProducto> covol = new TipoProductoCovol();
            Success<TipoProducto> success;

            try
            {
                success = covol.Agregar(tipoProducto);
                if (success.Valor > 0)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound(success);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpPut]
        public IActionResult ActualizarTipoProducto([FromForm]TipoProducto tipoProducto)
        {
            ICatalogo<TipoProducto> covol = new TipoProductoCovol();
            Success<TipoProducto> success;

            try
            {
                success = covol.Actualizar(tipoProducto);

                if (success.Valor > 0)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound(success);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpDelete]
        public IActionResult EliminarTipoProducto([FromQuery]int id)
        {
            TipoProducto tipoProducto = new TipoProducto();
            ICatalogo<TipoProducto> covol = new TipoProductoCovol();
            Success<TipoProducto> success;

            tipoProducto.IdProducto = id;

            try
            {
                success = covol.Eliminar(tipoProducto);

                if (success.Valor > 0)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound(success);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }

        }
    }
}