using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiAutomotoraFinal.Models;
using ApiAutomotoraFinal.Azure;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiAutomotoraFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        // GET: api/Vehiculo/all
        [HttpGet("all")]
        public JsonResult ObtenerVehiculos()
        {
            var VehiculosR = VehiculoAzure.ObtenerVehiculos();
            return new JsonResult(VehiculosR);
        }

        // GET api/Vehiculo/{id}-{modelo}
        [HttpGet("{Vehiculo}")]
        public JsonResult ObtenerVehiculo(string vehiculo)
        {
            var conversionExitosa = int.TryParse(vehiculo, out int idConvertido);

            Vehiculo VehiculoR;

            if (conversionExitosa)
            {
                VehiculoR = VehiculoAzure.ObtenerVehiculo(idConvertido);
            }
            else
            {
                VehiculoR = VehiculoAzure.ObtenerVehiculo(vehiculo);
            }

            if (VehiculoR is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {vehiculo}");
            }
            else
            {
                return new JsonResult(VehiculoR);
            }
        }

        // POST api/<VehiculoController>
        [HttpPost]
        public void AgregarVehiculo([FromBody] Vehiculo vehiculo)
        {
            VehiculoAzure.AgregarVehiculo(vehiculo);
        }

        // PUT api/<VehiculoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VehiculoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
