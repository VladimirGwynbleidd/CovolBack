using SYE.Covol;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestWebapi
{
    public class RespuestaTest
    {
        private Success<SubProducto> productos { get => new Success<SubProducto>(); }
        private Success<Marcadores> carga { get => new Success<Marcadores>(); }
        
        [Fact]
        public void ObtenerListaTest()
        {
            productos.Exito = true;
            productos.Mensaje = "Prueba exitosa";
            productos.ResponseDataEnumerable = new List<SubProducto>()
            {
                new SubProducto {
                    IdSubProducto = 0,
                    ClaveProducto = "01",
                    ClaveSubProducto = "01Test",
                    Comentario = "Test",
                    DetalleSubProducto = "Prueba de unidad"
                }
            };
            productos.ResponseData = new List<SubProducto>();
            Assert.IsType<Success<SubProducto>>(productos);
        }

        [Fact]
        public void ObtenerCargaTest()
        {
            carga.Exito = true;
            carga.Mensaje = "Prueba exitosa";
            carga.ResponseDataEnumerable = new List<Marcadores>()
            {
                new Marcadores{ Level = "ERROR", Registros = 1 },
                new Marcadores{ Level = "INFO", Registros = 3 },
                new Marcadores{ Level="WARN", Registros=1 }
            };
            carga.ResponseData = new List<Marcadores>();
            Assert.IsType<Success<Marcadores>>(carga);
        }
    }
}
