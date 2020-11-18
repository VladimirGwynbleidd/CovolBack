using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SYE.Covol;
using SYE.Covol.DAO;
using System;
namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class SubProductoController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObtenerSubProducto()
        {
            ICatalogo<SubProducto> covol = new SubProductoCovol();
            Success<SubProducto> success;
            try
            {
                success = covol.Obtener();
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AgregarSubProducto([FromForm] SubProducto subProducto)
        {
            ICatalogo<SubProducto> covol = new SubProductoCovol();
            Success<SubProducto> success;
            try
            {
                TryValidateModel(subProducto);

                if (ModelState.IsValid)
                {
                    success = covol.Agregar(subProducto);
                    if (success.Valor > 0)
                    {
                        return Ok(success);
                    }
                    else
                    {
                        return NotFound(success);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ActualizarSubProducto([FromForm]SubProducto subProducto)
        {
            ICatalogo<SubProducto> covol = new SubProductoCovol();
            Success<SubProducto> success;
            try
            {
                TryValidateModel(subProducto);
                if (ModelState.IsValid)
                {
                    success = covol.Actualizar(subProducto);
                    if (success.Valor > 0)
                    {
                        return Ok(success);
                    }
                    else
                    {
                        return NotFound(success);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult EliminarSubProducto([FromQuery]int id)
        {
            SubProducto subProducto = new SubProducto();
            subProducto.IdSubProducto = id;
            ICatalogo<SubProducto> covol = new SubProductoCovol();
            Success<SubProducto> success;
           try
            {
                success = covol.Eliminar(subProducto);
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

                return BadRequest(ex.Message);
            }


        }
    }
}