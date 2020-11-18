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
    public class ValorEstimuloController : ControllerBase
    {

        [HttpPost]
        public IActionResult Permisionarios(string estado, string zona)
        {
            Success<PermicionarioMapa> success;

            try
            {
                success = ValorEstimuloCovol.ObtenerPermisionarios(estado, zona);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult EstacionesServicio(string estado, string rfc)
        {
            Success<EstacionesServicio> success;
            try
            {
                success = ValorEstimuloCovol.ObtenerEstacionesServicio(estado, rfc);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult Zonas(string zona)
        {
            Success<Zona> success;
            try
            {
                success = ValorEstimuloCovol.ObtenerZonas(zona);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpPut]
        public IActionResult ActualizarValorEstimulo([FromForm] Estimulo estimulo)
        {
            ICatalogo<Estimulo> covol = new EstimuloCOVOL();
            Success<Estimulo> success;

            try
            {
                success = covol.Actualizar(estimulo);

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