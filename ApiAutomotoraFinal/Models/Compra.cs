using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAutomotoraFinal.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public string RutUsuario { get; set; }


        public Usuario usuario { get; set; }
    }
}
