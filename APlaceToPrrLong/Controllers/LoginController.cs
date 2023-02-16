
using APlaceToPrrLong.DTOs.Login;
using APlaceToPrrLong.DTOs.User;
using APlaceToPrrLong.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APlaceToPrrLong.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IDataProtector dataProtector;

        public LoginController(AplicationDbContext context,
            IMapper mapper,
             IDataProtectionProvider dataProtectionProvider,
             IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
            dataProtector = dataProtectionProvider.CreateProtector(configuration["KeyProtector"]);
        }

        [HttpPost("create-account")]
        public async Task<ActionResult<GenericResponse<TokenDTO>>> CreateAccount([FromBody] CreateUserDTO userDTO)
        {
            userDTO.Password = dataProtector.Protect(userDTO.Password);
            var data = mapper.Map<User>(userDTO);
            try
            {
                context.Users.Add(data);
                await context.SaveChangesAsync();
                var result = mapper.Map<UserLoginDTO>(data);
                CreateTokenDTO dataToken = new CreateTokenDTO(data.Email, data.Name, data.LastName);
                TokenDTO newToken = await CreateToken(dataToken);

                if (newToken != null)
                {
                    GenericResponse<TokenDTO> response = new GenericResponse<TokenDTO>(newToken, "Recursos creados correctamente", 201);
                    return Ok(response);
                }
                else
                {
                    GenericResponse<TokenDTO> response = new GenericResponse<TokenDTO>(null, "Ocurrio un error", 400);
                    return BadRequest(response);
                }
                
            }
            catch (Exception e)
            {

                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
            //var textoDesencriptado = dataProtector.Unprotect(textoCifrado);
        }

        [HttpPost("login")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> Login([FromBody] UserCredentials credentials)
        {

            var data = await context.Users.FirstOrDefaultAsync(x => x.Email == credentials.Email);
            //validar si existe el usuario
            if (data == null)
            {

                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "No hay una cuenta asosiada a ese correo", 404);
                return NotFound(response);
            }
            try
            {
                var unProtectedPassword = dataProtector.Unprotect(data.Password);
                if (unProtectedPassword == credentials.Password)
                {
                    CreateTokenDTO dataToken = new CreateTokenDTO(data.Email, data.Name, data.LastName);
                    TokenDTO newToken = await CreateToken(dataToken);

                    if (newToken != null)
                    {
                        GenericResponse<TokenDTO> response = new GenericResponse<TokenDTO>(newToken, "Recursos creados correctamente", 201);
                        return Ok(response);
                    }
                    else
                    {
                        GenericResponse<TokenDTO> response = new GenericResponse<TokenDTO>(null, "Ocurrio un error", 400);
                        return BadRequest(response);
                    }
                }
                else
                {
                    GenericListResponse<UserDTO> response = new GenericListResponse<UserDTO>(null, "La contraseña no es correcta", 400);
                    return BadRequest(response);
                }

                
            }
            catch (Exception e)
            {
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("refresh-token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GenericResponse<TokenDTO>>> RefreshToken()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var nameClaim = HttpContext.User.Claims.Where(claim => claim.Type == "name").FirstOrDefault();
            var lastNameClaim = HttpContext.User.Claims.Where(claim => claim.Type == "lastName").FirstOrDefault();
            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                string name = nameClaim.Value;
                string lastName = lastNameClaim.Value;

                CreateTokenDTO dataToken = new CreateTokenDTO(email, name, lastName);
                TokenDTO newToken = await CreateToken(dataToken);
                GenericResponse<TokenDTO> response = new GenericResponse<TokenDTO>(newToken, "Recursos creados correctamente", 201);
                return Ok(response);
            }
            else
            {
                GenericResponse<TokenDTO> response = new GenericResponse<TokenDTO>(null, "Ocurrio un error", 400);
                return BadRequest(response);
            }
        }

        private async Task<TokenDTO> CreateToken(CreateTokenDTO createTokenDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", createTokenDTO.Email),
                new Claim("name", createTokenDTO.Name),
                new Claim("lastName", createTokenDTO.LastName),
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(30);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new TokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };

        }
    }

}
