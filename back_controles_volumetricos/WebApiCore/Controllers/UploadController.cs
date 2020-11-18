using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SYE.Covol;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class UploadController : ControllerBase
    {
        public ILogger<UploadController> Logger { get; }

        public UploadController(ILogger<UploadController> logger)
        {
            Logger = logger;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    Logger?.LogInformation("Número de archivos: " + files.Count);
                    ResultadoCarga carga = new ResultadoCarga();
                    var tarea = await Task.Run(() => ProcesarArchivos(carga, files));

                    //tarea.Wait();
                    return Ok(tarea);
                }
                else
                {
                    Logger.LogError("No se ha detectado archivos para procesar");
                    return BadRequest(new
                    {
                        Mensaje = "No se ha detectado archivos para procesar"
                    });
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message);

                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        public async Task<ResultadoCarga> ProcesarArchivos(ResultadoCarga carga, List<IFormFile> files)
        {
            //Task task = new Task(CallMethod);
            //task.Start();
            //task.Wait();

            foreach (var file in Request.Form.Files)
            {
                //if (file.ContentType.Equals("text/xml") || file.ContentType.Equals("text/plain"))
                if (file.ContentType.Equals("text/xml") || file.ContentType.Equals("application/xml") || file.ContentType.Equals("text/plain"))
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    using (ControlesVolumeticos cv = new ControlesVolumeticos(Logger))
                    {
                        using (Stream inStream = file.OpenReadStream())
                        {

                            int dss = await ReadFile(cv, inStream, fileName);
                            if (dss > 0)
                            {
                                Logger.LogInformation(string.Format("Archivos Correctos procesados: {0}", ++carga.Correctos));
                            }
                            else if (dss == 0)
                            {
                                Logger.LogWarning($"Archivos Incorrectos: {++carga.Incorrectos}");
                            }
                            else
                            {
                                Logger.LogError(string.Format("Archivos con errores: {0}", ++carga.Errores));
                            }
                        }

                        Logger?.LogInformation("Después de leer el archivo.");
                    }
                }
            }

            return carga;

        }
        public async Task<int> ReadFile(ControlesVolumeticos cv, Stream inStream, string fileName)
        {
            try
            {

                Logger?.LogInformation("Empezamos a procesar el archivo.");
                int dss = await cv.ProcesaXML(inStream, fileName);
                Logger?.LogInformation("Terminamos de procesar el archivo.");
                return dss;
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}