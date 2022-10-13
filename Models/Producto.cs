using System;
using System.Collections.Generic;

#nullable disable

namespace APIEKS.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Costo { get; set; }
        public int? IdMarca { get; set; }

        public virtual Marca IdMarcaNavigation { get; set; }
    }
}
