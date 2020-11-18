using System;
using System.ComponentModel.DataAnnotations;

namespace SYE.Covol
{
    [Serializable]
    public class Perfil
    {
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public string dperfil { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public bool carga { get; set; }
        public bool catalogo { get; set; }
        public bool cruce { get; set; }
    }
}
