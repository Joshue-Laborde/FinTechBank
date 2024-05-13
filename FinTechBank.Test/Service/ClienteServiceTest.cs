using AutoMapper;
using FinTechBank.BusinessLogic.Services;
using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.Dtos;
using FinTechBank.Domain.Enums;
using FinTechBank.Domain.RequestModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;

namespace FinTechBank.Test.Service
{
    public class ClienteServiceTest
    {
        private Cliente cliente;
        private ClienteDto clienteDto;
        private ClienteRequest clienteRequest;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            mapper = Substitute.For<IMapper>();
            cliente = new Cliente()
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
            };
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

        private IServiceProvider CreateContext(string nameDB)
        {
            var services = new ServiceCollection();

            services.AddDbContext<FinTechBankContext>(opt => opt.UseInMemoryDatabase(databaseName: nameDB),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            return services.BuildServiceProvider();
        }

        [Test]
        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task When_Add_Customer_Services(HttpStatusCode code)
        {
            //Arrange
            var nameDb = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDb);

            var db = serviceProvider.GetService<FinTechBankContext>();
            db.Add(cliente);

            //Act
            if (code == HttpStatusCode.OK)
                mapper.Map<Cliente>(clienteRequest).ReturnsForAnyArgs(cliente);
            else
                mapper.Map<Cliente>(clienteRequest).ThrowsForAnyArgs(x => { throw new Exception(); });

            ClienteService services = new ClienteService(db, mapper);
            var responseServices = await services.GuardarCliente(clienteRequest);

            //Assert
            Assert.AreEqual(code, (responseServices.HttpCode));
        }
    }
}
