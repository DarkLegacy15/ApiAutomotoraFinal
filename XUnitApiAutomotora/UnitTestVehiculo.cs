using System;
using Xunit;
using ApiAutomotoraFinal.Azure;
using System.Linq;
using ApiAutomotoraFinal.Models;

namespace XUnitApiAutomotora
{
    public class UnitTestVehiculo
    {
        [Fact]
        public void TestObtenerVehiculos()
        {
            //Arrange
            bool conDatos = false;


            //Act
            var Result = VehiculoAzure.ObtenerVehiculos();
            conDatos = Result.Any();


            //Assert
            Assert.True(conDatos);

        }

        [Fact]
        public void TestObtenerVehiculo()
        {
            //arrange
            string nombreModeloTest = "G500";
            int idEsperado = 2;
            //act
            var Result = VehiculoAzure.ObtenerVehiculo(nombreModeloTest);

            //assert
            Assert.Equal(idEsperado, Result.IdVehiculo);
        }

        [Fact]
        public void TestObtenerVehiculoPorId()
        {
            //arrange
            int idVehiculoTest = 1;
            int idEsperado = 1;
            //act
            var Result = VehiculoAzure.ObtenerVehiculo(idVehiculoTest);

            //assert
            Assert.Equal(idEsperado, Result.IdVehiculo);
        }

        [Fact]
        public void TestAgregarVehiculo()
        {
            //arrange
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Modelo = "X7";
            vehiculo.IdMarca = 2;
            vehiculo.Estilo = "SUV";
            vehiculo.IdTipo = 1;

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            //act
            resultadoObtenido = VehiculoAzure.AgregarVehiculo(vehiculo);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestEliminarVehiculoPorId()
        {
            //arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            int vehiculoEliminar = 1002;

            //act
            resultadoObtenido = VehiculoAzure.EliminarVehiculoPorID(vehiculoEliminar);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestActualizarVehiculo()
        {
            //arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Vehiculo vehiculo = new Vehiculo();
            vehiculo.IdVehiculo = 1003;
            vehiculo.Modelo = "X7";
            vehiculo.IdMarca = 2;
            vehiculo.Estilo = "SUV";
            vehiculo.IdTipo = 1;

            //act
            resultadoObtenido = VehiculoAzure.ActualizarVehiculoPorId(vehiculo);

            vehiculo.IdMarca = 1;
            VehiculoAzure.ActualizarVehiculoPorId(vehiculo);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


    }
}
