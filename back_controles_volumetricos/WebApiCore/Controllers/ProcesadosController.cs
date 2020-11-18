using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SYE.Covol;
using System;
using System.Threading.Tasks;
using WebApiCore.Models;
namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class ProcesadosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Permicionarios([FromForm]int estatus)
        {
            try
            {
                return Ok(ReportesCovol.ObtenerPermisionarios(estatus));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConsolidadoMaestro([FromForm]SolicitudPeriodoConsolidado solicitud)
        {
            try
            {
                TryValidateModel(solicitud);
                if (ModelState.IsValid)
                {
                    object[] objeto = new object[4];
                    objeto[0] = solicitud.RFC;
                    objeto[1] = null;
                    objeto[2] = solicitud.FechaIni.ToDateTime();
                    objeto[3] = solicitud.FechaFin.ToDateTime();
                    return Ok(await ReportesCovol.ObtenerConsolidadoMaestro(objeto));
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}