using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.Dtos;
using FinTechBank.Domain.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.Domain.Interface
{
    public interface IClienteService
    {
        Task<List<ClienteDto>> GetClientes();
        Task<ClienteDto> GetClienteById(long id);
        Task<GenericResponse> GuardarCliente(ClienteRequest request);
        Task<ClienteDto> ActualizarCliente(PutClienteRequest requestId, ClienteRequest request);
        Task<Cliente> EliminarCliente(DeleteClienteRequest requestId); 
    }
}
