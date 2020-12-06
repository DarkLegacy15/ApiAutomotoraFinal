using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAutomotoraFinal.Models
{
    public class Cotizar
    {
        public int IdCotizar { get; set; }
        public string RutUsuario { get; set; }
        public int IdVehiculo { get; set; }

        public Usuario usuario { get; set; }
        public Vehiculo vehiculo { get; set; }
    }
}
