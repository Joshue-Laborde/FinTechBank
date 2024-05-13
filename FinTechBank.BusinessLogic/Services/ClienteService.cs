using AutoMapper;
using FinTechBank.BusinessLogic.Extensions;
using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.Dtos;
using FinTechBank.Domain.Interface;
using FinTechBank.Domain.RequestModel;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FinTechBank.BusinessLogic.Services
{
    public class ClienteService : IClienteService
    {
        private readonly FinTechBankContext _context;
        private readonly IMapper _mapper;

        public ClienteService(FinTechBankContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetClientes()
        {
            try
            {
                var clientes = await _context.Cliente.Where(x => x.IdEstado).ToListAsync();
                var result = _mapper.Map<List<ClienteDto>>(clientes);

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes: {ex.Message}");
                return new List<ClienteDto>();
            }
        }

        public async Task<ClienteDto> GetClienteById(long id)
        {
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.IdEstado && x.IdCliente == id);
                var result = _mapper.Map<ClienteDto>(cliente);

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes: {ex.Message}");
                return new ClienteDto();
            }
        }

        public async Task<GenericResponse> GuardarCliente(ClienteRequest request)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(request);
                cliente.FechaCreacion = DateTime.Now;
                cliente.IdEstado = true;
                _context.Cliente.Add(cliente);
                _context.SaveChanges();

                var response = new GenericResponse()
                {
                    HttpCode = HttpStatusCode.OK,
                    Message = "Cliente Agregado!"
                };

                return response;


            }
            catch (Exception ex)
            {
                return new GenericResponse()
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Message = $"ERROR. No se pudo guardar el cliente: {ex.Message}"
                };
            }
        }

        public async Task<ClienteDto> ActualizarCliente(PutClienteRequest requestId, ClienteRequest request)
        {
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.IdEstado && x.IdCliente == requestId.IdCliente);

                if(cliente != null ) 
                {
                    cliente = _mapper.Map<Cliente>(cliente, request);
                    cliente.IdCliente = requestId.IdCliente;

                    _context.Cliente.Update(cliente);
                    _context.Entry(cliente).State = EntityState.Modified;
                    _context.SaveChanges();

                    return _mapper.Map<ClienteDto>(cliente);
                }
                else
                {
                    return null; 
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes: {ex.Message}");
                return new ClienteDto();
            }
        }

        public async Task<Cliente> EliminarCliente(DeleteClienteRequest requestId)
        {
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.IdEstado && x.IdCliente == requestId.IdCliente);
                cliente.IdEstado = false;
                _context.SaveChanges();
                
                return cliente;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes: {ex.Message}");
                return new Cliente();
            }
        }

    }
}
