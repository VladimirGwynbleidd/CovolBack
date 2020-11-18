using System;
using System.ComponentModel.DataAnnotations;
namespace SYE.Covol
{
    [Serializable]
    public class SubProducto
    {
        //[Required]
        public int IdSubProducto { get; set; }
        //[Required, StringLength(4, MinimumLength = 1)]
        public string ClaveProducto { get; set; }
        //[Required, StringLength(6, MinimumLength = 1)]
        public string ClaveSubProducto { get; set; }
        //[Required, StringLength(200, MinimumLength = 3)]
        public string DetalleSubProducto { get; set; }
        //[Required, StringLength(200, MinimumLength = 3)]
        public string Comentario { get; set; }
    }
}
