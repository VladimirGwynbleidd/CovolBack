using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SYE.Covol;
using SYE.Covol.DAO;
using System;

namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class PerfilController : ControllerBase
    {
        public ILogger<PerfilController> Logger { get; }
        public PerfilController(ILogger<PerfilController> logger)
        {
            Logger = logger;
        }
        [HttpGet]
        public IActionResult CatalogoPerfiles()
        {
            Logger.LogInformation("CatalogoPerfiles");
            IPerfil<Perfil> covol = new PerfilCovol();
            Success<Perfil> success;
            try
            {
                success = covol.CatalogoPerfiles();
                return Ok(success);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return BadRequest(JsonConvert.DeserializeObject(ex.Message));
            }
        }

        [HttpGet]
        public IActionResult ObtenerPerfil([FromQuery] Perfil perfil)
        {
            ICatalogo<Perfil> covol = new PerfilCovol();
            Success<Perfil> success;

            try
            {
                success = covol.Obtener(String.IsNullOrEmpty(perfil.RFC) ? null : perfil);
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
        public IActionResult AgregarPerfil([FromForm]Perfil perfil)
        {
            ICatalogo<Perfil> covol = new PerfilCovol();
            Success<Perfil> success;

            try
            {
                success = covol.Agregar(perfil);
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
        public IActionResult ActualizarPerfil([FromForm]Perfil Perfil)
        {
            ICatalogo<Perfil> covol = new PerfilCovol();
            Success<Perfil> success;

            try
            {
                success = covol.Actualizar(Perfil);

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
        public IActionResult EliminarPerfil([FromQuery]int id)
        {
            Perfil Perfil = new Perfil();
            Perfil.IdUsuario = id;

            ICatalogo<Perfil> covol = new PerfilCovol();
            Success<Perfil> success;

            try
            {
                success = covol.Eliminar(Perfil);

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