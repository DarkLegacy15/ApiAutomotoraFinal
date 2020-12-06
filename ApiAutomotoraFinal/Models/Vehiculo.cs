using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAutomotoraFinal.Models
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Modelo { get; set; }
        public int IdMarca { get; set; }
        public string Estilo { get; set; }
        public int IdTipo { get; set; }


        public Marca marca { get; set; }
        public Tipo tipo { get; set; }
    }
}
