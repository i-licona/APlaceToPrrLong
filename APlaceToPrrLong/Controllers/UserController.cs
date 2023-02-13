using APlaceToPrrLong.DTOs.User;
using APlaceToPrrLong.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace APlaceToPrrLong.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AplicationDbContext context;
        private readonly IMapper mapper;

        public UserController(AplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GenericListResponse<UserDTO>>> Get()
        {
            try
            {
                //Obtener recursos
                var data = await context.Users.ToListAsync();
                var result = mapper.Map<List<UserDTO>>(data);
                GenericListResponse<UserDTO> response = new GenericListResponse<UserDTO>(result, "Recursos obtenidos correctamente", 200);
                return Ok(response);
            }
            catch (Exception e)
            {

                GenericListResponse<UserDTO> response = new GenericListResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> GetById(int id)
        {
            var data = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            //validar si existe el usuario
            if (data == null) 
            { 

                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "No se encontro el recurso solicitado", 404);
                NotFound(response);
            }
            try
            {
                var result = mapper.Map<UserDTO>(data);
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(result, "Recursos obtenidos correctamente", 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<UserDTO>>> Post([FromBody] CreateUserDTO userDTO)
        {
            var data = mapper.Map<User>(userDTO);
            try
            {
                context.Users.Add(data);
                await context.SaveChangesAsync();
                var result = mapper.Map<UserDTO>(data);
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(result, "Recursos creados correctamente", 201);
                return Ok(response);
            }
            catch (Exception e)
            {

                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> Put(int id, [FromBody] CreateUserDTO userDTO)
        {
            var userDb = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            //validar si existe el usuario
            if (userDb == null)
            {
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "No se encontro el recurso solicitado", 404);
                NotFound(response);
            }
            try
            {
                //Remplazar los datos enviados por el usuario
                userDb = mapper.Map(userDTO, userDb);
                await context.SaveChangesAsync();
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Recursos actualizados correctamente", 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> Delete(int id)
        {
            //Validar si existe el registro
            var existRegister = await context.Users.AnyAsync( x=> x.Id == id );
            if (!existRegister)
            {
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "No se encontro el recurso solicitado", 404);
                NotFound(response);
            }
            try
            {
                //Eliminar registro
                context.Remove(new User { Id = id });
                await context.SaveChangesAsync();
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Recursos eliminados correctamente", 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                GenericResponse<UserDTO> response = new GenericResponse<UserDTO>(null, "Ha ocurrido un error", 400, e.Message);
                return BadRequest(response);
            }
        }
    }
}
