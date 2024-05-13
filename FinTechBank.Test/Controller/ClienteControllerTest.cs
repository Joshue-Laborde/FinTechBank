using FinTechBank.Controllers;
using FinTechBank.Domain.Dtos;
using FinTechBank.Domain.Enums;
using FinTechBank.Domain.Interface;
using FinTechBank.Domain.RequestModel;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.Test.Controller
{
    public class ClienteControllerTest
    {
        private IClienteService clienteService;
        private ClienteRequest clienteRequest;

        [SetUp]
        public void SetUp()
        {
            clienteService = Substitute.For<IClienteService>();
            clienteRequest = new ClienteRequest()
            {
                Nombre = "Joshue",
                Apellido = "Laborde",
                NumeroCuenta = "123456789",
                Saldo = 1000.50m,
                FechaNacimiento = new DateTime(1980, 5, 10),
                Direccion = "Calle 123",
                Telefono = "1234567890",
                CorreoElectronico = "juan@example.com",
                TipoCliente = TipoClienteEnum.CORPORATIVO,
                EstadoCivil = "Casado",
                NumeroIdentifiacion = "ABC123456",
                Profesion = "Ingeniero",
                Genero = "Masculino",
                Nacionalidad = "Mexicano",
            };
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task When_Add_Customer_Controller(HttpStatusCode code)
        {
            //Arrange
            GenericResponse response = new GenericResponse()
            {
                HttpCode = code
            };

            //Act
            clienteService.GuardarCliente(clienteRequest).ReturnsForAnyArgs(response);
            ClienteController controller = new ClienteController(clienteService);
            ObjectResult responseController = (ObjectResult)await controller.GuardarCliente(clienteRequest);

            //Assert
            Assert.AreEqual((int)code, responseController.StatusCode.Value);
        }
    }
}
