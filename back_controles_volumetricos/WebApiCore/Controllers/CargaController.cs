using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SYE.Covol;
using SYE.Covol.BI;
using SYE.Covol.DAO.Carga;
using System;
using WebApiCore.Models;
namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class CargaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Marcadores()
        {
            ICarga covol = new CargaCovol();
            Success<Marcadores> success;

            try
            {
                success = covol.ObtenerMarcadores();
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpGet, HttpPost]
        public IActionResult Bitacora([FromForm]string level)
        {
            ICarga covol = new CargaCovol();
            Success<LogresultadoCarga> success;

            try
            {
                success = covol.ObtenerBitacora(DateTime.Now.AddMonths(-1), DateTime.Now, level);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpGet, HttpPost]
        public IActionResult DetalleLog([FromForm]Registro registro)
        {
            ICarga covol = new CargaCovol();
            Success<RegistroLog> success;
            try
            {
                success = covol.ObtenerdetalleLog(registro.RFC, registro.Permisionario, registro.Fecha, registro.Level);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }
    }
}