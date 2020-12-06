using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAutomotoraFinal.Models
{
    public class Detalle
    {
        public int IdDetalle { get; set; }
        public int IdCompra { get; set; }
        public int IdVehiculo { get; set; }


        public Compra compra { get; set; }
        public Vehiculo vehiculo { get; set; }
    }
}
