using AutoMapper;
using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace FinTechBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;
        private readonly FinTechBankContext _context;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration config, FinTechBankContext context, IMapper mapper)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
            this._context = context;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] UsuarioRequest request) 
        {
            try
            {
                var usuario = _context.Usuario.Where(x => x.IdEstado && x.Correo == request.Correo && x.Clave == request.Clave).FirstOrDefault();

                if(usuario != null)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Correo));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status200OK, new {token = tokenCreado, message = "Exitoso!"});
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { token = "", message = "Usuario no existente!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { token = ex });
            }
        }
    }
}
