using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Registro
    {
        public string RFC { get; set; }
        public string Permisionario { get; set; }
        public string Fecha { get; set; }
        public string Level { get; set; }
    }
}
