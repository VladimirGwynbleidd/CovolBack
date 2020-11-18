﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SYE.Covol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class CrucesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Reporte([FromForm]ReporteExcel reporte)
        {
            TryValidateModel(reporte);
            if (ModelState.IsValid)
            {
                MemoryStream memory = new MemoryStream();
                using (ExcelPackage package = new ExcelPackage(memory))
                {
                    object[] objeto = new object[4];
                    objeto[0] = reporte.RFC;
                    objeto[1] = reporte.NoCRE;
                    objeto[2] = reporte.FechaIni;
                    objeto[3] = reporte.FechaFin;
                    Task<List<Cabecera>> CabeceraTask = ReportesCovol.ObtenerCabecera(objeto);
                    Task<List<Consolidado>> ConsolidadoTask = ReportesCovol.ObtenerConsolidado(objeto);
                    Task<List<Movimiento>> MovimientosTask = ReportesCovol.ObtenerMovimientos(objeto);
                    Task<List<Movimiento>> ExcedentesTask = ReportesCovol.ObtenerExcedentes(objeto);
                    ExcelWorksheet worksheet;
                    List<Task> allTasks = new List<Task> { CabeceraTask, ConsolidadoTask, MovimientosTask, ExcedentesTask };
                    while (allTasks.Any())
                    {
                        Task finished = await Task.WhenAny(allTasks);
                        if (finished == CabeceraTask)
                        {
                            allTasks.Remove(CabeceraTask);
                            List<Cabecera> Cabecera = await CabeceraTask;
                            worksheet = package.Workbook.Worksheets.Add("Cabecera");
                            worksheet.Cells[1, 1].Value = "Version";
                            worksheet.Cells[1, 2].Value = "No. Permiso CRE";
                            worksheet.Cells[1, 3].Value = "RFC";
                            worksheet.Cells[1, 4].Value = "No. Certificado";
                            if (Cabecera != null)
                            {
                                Log.Info("Cabecera lista");
                                int row = 2;
                                foreach (Cabecera item in Cabecera)
                                {
                                    worksheet.Cells[row, 1].Value = item.Version;
                                    worksheet.Cells[row, 2].Value = item.NumeroPermisoCRE;
                                    worksheet.Cells[row, 3].Value = item.RFC;
                                    worksheet.Cells[row, 4].Value = item.Certificado;
                                    row++;
                                }
                                Cabecera.Clear();
                            }
                        }
                        else if (finished == ConsolidadoTask)
                        {
                            allTasks.Remove(ConsolidadoTask);
                            List<Consolidado> Consolidado = await ConsolidadoTask;
                            worksheet = package.Workbook.Worksheets.Add("Consolidado");
                            worksheet.Cells[1, 1].Value = "Permiso CRE";
                            worksheet.Cells[1, 2].Value = "Tipo Gasolina";
                            worksheet.Cells[1, 3].Value = "Litros de Venta";
                            worksheet.Cells[1, 4].Value = "Total";
                            worksheet.Cells[1, 5].Value = "Litros Excedentes";
                            worksheet.Cells[1, 6].Value = "Litros Estimados";
                            worksheet.Cells[1, 7].Value = "Monto Est";
                            worksheet.Cells[1, 8].Value = "Valor Est";
                            if (Consolidado != null)
                            {
                                int row = 2;
                                foreach (Consolidado item in Consolidado)
                                {
                                    worksheet.Cells[row, 1].Value = item.NumeroPermisocre;
                                    worksheet.Cells[row, 2].Value = item.TipoGasolina;
                                    worksheet.Cells[row, 3].Value = item.LitrosdeVenta.ToString("#,###,###.00");
                                    worksheet.Cells[row, 4].Value = item.Total.ToString("C2", CultureInfo.CreateSpecificCulture("es-MX"));
                                    worksheet.Cells[row, 5].Value = item.LtsExcedentes.ToString("#,###,###.00");
                                    worksheet.Cells[row, 6].Value = item.LtsEtimad.ToString("#,###,###.00");
                                    worksheet.Cells[row, 7].Value = item.MontoEstimulo.ToString("C2", CultureInfo.CreateSpecificCulture("es-MX"));
                                    worksheet.Cells[row, 8].Value = item.ValorEst.ToString("F2");
                                    row++;
                                }
                                Consolidado.Clear();
                            }
                        }
                        else if (finished == MovimientosTask)
                        {
                            allTasks.Remove(MovimientosTask);
                            List<Movimiento> Movimientos = await MovimientosTask;
                            worksheet = package.Workbook.Worksheets.Add("Movimientos");
                            worksheet.Cells[1, 1].Value = "#CRE";
                            worksheet.Cells[1, 2].Value = "No. Certificado";
                            worksheet.Cells[1, 3].Value = "Fecha Venta";
                            worksheet.Cells[1, 4].Value = "Fecha Corte";
                            worksheet.Cells[1, 5].Value = "Registro";
                            worksheet.Cells[1, 6].Value = "Producto";
                            worksheet.Cells[1, 7].Value = "No.Transacc.Venta";
                            worksheet.Cells[1, 8].Value = "No.Dispensario";
                            worksheet.Cells[1, 9].Value = "Id Manguera";
                            worksheet.Cells[1, 10].Value = "Vol.Despachado";
                            worksheet.Cells[1, 11].Value = "Total";
                            if (Movimientos != null)
                            {
                                int row = 2;
                                foreach (Movimiento item in Movimientos)
                                {
                                    worksheet.Cells[row, 1].Value =item.NumeroPermisoCRE;
                                    worksheet.Cells[row, 2].Value = item.NoCertificado;
                                    worksheet.Cells[row, 3].Value = item.FechaYHoraTransaccionVenta.ToString("dd/MM/yyyy");
                                    worksheet.Cells[row, 4].Value = item.FechaYHoraCorte.ToString("dd/MM/yyyy");
                                    worksheet.Cells[row, 5].Value = item.TipoDeRegistro;
                                    worksheet.Cells[row, 6].Value = item.SubProducto;
                                    worksheet.Cells[row, 7].Value = item.NumeroUnicoTransaccionVenta;
                                    worksheet.Cells[row, 8].Value = item.NumeroDispensario;
                                    worksheet.Cells[row, 9].Value = item.IdentificadorManguera;
                                    worksheet.Cells[row, 10].Value = item.VolumenDespachado.ToString("F2");
                                    worksheet.Cells[row, 11].Value = item.ImporteTotalTransaccion.ToString("C2", CultureInfo.CreateSpecificCulture("es-MX"));
                                    row++;
                                }
                                Movimientos.Clear();
                            }
                        }
                        if (finished == ExcedentesTask)
                        {
                            allTasks.Remove(ExcedentesTask);
                            List<Movimiento> Excedentes = await ExcedentesTask;
                            worksheet = package.Workbook.Worksheets.Add("Excedentes");
                            worksheet.Cells[1, 1].Value = "#CRE";
                            worksheet.Cells[1, 2].Value = "No. Certificado";
                            worksheet.Cells[1, 3].Value = "Fecha Venta";
                            worksheet.Cells[1, 4].Value = "Fecha Corte";
                            worksheet.Cells[1, 5].Value = "Registro";
                            worksheet.Cells[1, 6].Value = "Producto";
                            worksheet.Cells[1, 7].Value = "No.Transacc.Venta";
                            worksheet.Cells[1, 8].Value = "No.Dispensario";
                            worksheet.Cells[1, 9].Value = "Id Manguera";
                            worksheet.Cells[1, 10].Value = "Vol.Despachado";
                            worksheet.Cells[1, 11].Value = "Total";
                            if (Excedentes != null)
                            {
                                int row = 2;
                                foreach (Movimiento item in Excedentes)
                                {
                                    worksheet.Cells[row, 1].Value = item.NumeroPermisoCRE;
                                    worksheet.Cells[row, 2].Value = item.NoCertificado;
                                    worksheet.Cells[row, 3].Value = item.FechaYHoraTransaccionVenta.ToString("dd/MM/yyyy");
                                    worksheet.Cells[row, 4].Value = item.FechaYHoraCorte.ToString("dd/MM/yyyy");
                                    worksheet.Cells[row, 5].Value = item.TipoDeRegistro;
                                    worksheet.Cells[row, 6].Value = item.SubProducto;
                                    worksheet.Cells[row, 7].Value = item.NumeroUnicoTransaccionVenta;
                                    worksheet.Cells[row, 8].Value = item.NumeroDispensario;
                                    worksheet.Cells[row, 9].Value = item.IdentificadorManguera;
                                    worksheet.Cells[row, 10].Value = item.VolumenDespachado.ToString("F2");
                                    worksheet.Cells[row, 11].Value = item.ImporteTotalTransaccion.ToString("C2", CultureInfo.CreateSpecificCulture("es-MX"));
                                    row++;
                                }
                                Excedentes.Clear();
                            }
                        }
                        else
                        {
                            allTasks.Remove(finished);
                        }
                    }
                    package.Save();
                    ReportesCovol.CambiarStatus(objeto);
                }
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Remporte{0}.xlsx", DateTime.Now.ToString("ddMMyyyy_HHmm")));
            }
            else
                return NotFound();
        }

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
        public IActionResult Consolidados([FromForm]string rfc, [FromForm]string numeroPermisoCRE, [FromForm]int estatus)
        {
            try
            {
                return Ok(ReportesCovol.ObtenerConsolidadosPendientes(rfc, numeroPermisoCRE, estatus));
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
                    objeto[1] = solicitud.NoCRE;
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

        [HttpPost]
        public IActionResult VerificarArchivosProcesados()
        {
            try
            {
                    return Ok(ReportesCovol.VerificarArchivosProcesados());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}