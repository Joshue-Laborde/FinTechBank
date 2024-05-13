using FinTechBank.Domain.Dtos;
using FinTechBank.Domain.Interface;
using FinTechBank.Domain.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinTechBank.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this._clienteService = clienteService;
        }

        [HttpGet]
        [Route("clientes")]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.GetClientes();

                if(clientes == null)  return  NotFound();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("clientes/{id}")]
        public async Task<IActionResult> GetClienteById(long id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteById(id);

                if (cliente == null) return NotFound();

                return Ok(cliente);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("clientes/nuevo")]
        public async Task<IActionResult> GuardarCliente([FromBody] ClienteRequest request)
        {
            try
            {
                GenericResponse response = new GenericResponse();
                response = await _clienteService.GuardarCliente(request);

                if (response.HttpCode == HttpStatusCode.OK)
                    return Ok(response);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            catch( Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("clientes/{id:long}")]
        public async Task<IActionResult> GuardarCliente(long id, [FromBody] ClienteRequest request)
        {
            try
            {
                PutClienteRequest indicador = new PutClienteRequest { IdCliente = id };

                var cliente = await _clienteService.ActualizarCliente(indicador, request);

                if (cliente == null) return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("clientes/{id}")]
        public async Task<IActionResult> EliminarCliente(long id)
        {
            try
            {
                DeleteClienteRequest indicador = new DeleteClienteRequest { IdCliente = id };
                var cliente = await _clienteService.EliminarCliente(indicador);

                if (cliente == null) return NotFound();

                return Ok(cliente);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
