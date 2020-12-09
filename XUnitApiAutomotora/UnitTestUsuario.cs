using ApiAutomotoraFinal.Azure;
using ApiAutomotoraFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitApiAutomotora
{
    public class UnitTestUsuario
    {
        [Fact]
        public void TestObtenerUsuarios()
        {
            //Arrange
            bool conDatos = false;


            //Act
            var Result = UsuarioAzure.ObtenerUsuarios();
            conDatos = Result.Any();


            //Assert
            Assert.True(conDatos);

        }

       

        [Fact]
        public void TestObtenerUsuarioPorRut()
        {
            //arrange
            string RutTest = "20.446.958-K";
            string RutEsperado = "20.446.958-K";
            //act
            var Result = UsuarioAzure.ObtenerUsuario(RutTest);

            //assert
            Assert.Equal(RutEsperado, Result.RutUsuario);
        }

        [Fact]
        public void TestAgregarUsuario()
        {
            //arrange
            Usuario usuario = new Usuario();
            usuario.RutUsuario = "15.987.532-6";
            usuario.Nombre = "Juanito";
            usuario.Apellido = "Alcachofa";
            usuario.Edad = 40;
            usuario.Email = "juanalca@gmail.com";

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            //act
            resultadoObtenido = UsuarioAzure.AgregarUsuario(usuario);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestEliminarUsuarioPorRut()
        {
            //arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            string UsuarioEliminar = "10.234.567-8";

            //act
            resultadoObtenido = UsuarioAzure.EliminarUsuarioPorRut(UsuarioEliminar);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestActualizarUsuario()
        {
            //arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Usuario usuario = new Usuario();
            usuario.RutUsuario = "15.987.532-6";
            usuario.Nombre = "Juanito";
            usuario.Apellido = "Alcachofa";
            usuario.Edad = 40;
            usuario.Email = "juanalca@gmail.com";

            //act
            resultadoObtenido = UsuarioAzure.ActualizarUsuarioPorRut(usuario);

            usuario.Edad = 44;
            UsuarioAzure.ActualizarUsuarioPorRut(usuario);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }



    }
}
