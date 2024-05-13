using FinTechBank.BusinessLogic.Services;
using FinTechBank.Controllers;
using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinTechBank.UnitTest
{
    public class ClienteTesting
    {
        private Mock<IClienteService> _clienteServiceMock;
        private ClienteController _clienteController;

        public ClienteTesting()
        {
            _clienteServiceMock = new Mock<IClienteService>();
            _clienteController = new ClienteController(_clienteServiceMock.Object);
        }

        [Fact]
        public async Task GetClientes_ReturnsOk()
        {
            // Arrange
            var clientes = new List<Cliente> 
            {
                new Cliente
                {
                    IdCliente = 1,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    NumeroCuenta = "123456789",
                    Saldo = 1000.50m,
                    FechaNacimiento = new DateTime(1980, 5, 10),
                    Direccion = "Calle 123",
                    Telefono = "1234567890",
                    CorreoElectronico = "juan@example.com",
                    TipoCliente = 1,
                    EstadoCivil = "Casado",
                    NumeroIdentifiacion = "ABC123456",
                    Profesion = "Ingeniero",
                    Genero = "Masculino",
                    Nacionalidad = "Mexicano",
                    FechaCreacion = DateTime.Now,
                    IdEstado = true
                }
            };
            //_clienteServiceMock.Setup(x => x.GetClientes()).ReturnsAsync(clientes);

            // Act
            var result = await _clienteController.GetClientes() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            // Puedes hacer más aserciones sobre el contenido devuelto si lo deseas.
        }

    }
}