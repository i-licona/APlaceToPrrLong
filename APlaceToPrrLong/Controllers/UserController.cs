using APlaceToPrrLong.DTOs.User;
using APlaceToPrrLong.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return Ok(new GenericListResponse<UserDTO>
                {
                    Data = result,
                    Message = "Recursos obtenidos correctamente",
                    Status = 200,
                });
            }
            catch (Exception e)
            {

                return BadRequest(new GenericListResponse<UserDTO>
                {
                    Data = null,
                    Message = "Ha ocurrido un error",
                    Status = 400,
                    Error = e.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> GetById(int id)
        {
            var data = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            //validar si existe el usuario
            if (data == null) 
            { 
                NotFound(new GenericResponse<UserDTO>{
                    Data = null,
                    Message = "No se encontro el recurso solicitado",
                    Status = 404,
                });
            }
            try
            {
                var result = mapper.Map<UserDTO>(data);
                return Ok(new GenericResponse<UserDTO>
                {
                    Data = result,
                    Message = "Recursos obtenidos correctamente",
                    Status = 200,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "Ha ocurrido un error",
                    Status = 400,
                    Error = e.Message
                });
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
                return Ok( new GenericResponse<UserDTO>
                {
                    Data = result,
                    Message = "Recursos creados correctamente",
                    Status = 201,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "Ha ocurrido un error",
                    Status = 400,
                    Error = e.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> Put(int id, [FromBody] CreateUserDTO userDTO)
        {
            var userDb = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            //validar si existe el usuario
            if (userDb == null)
            {
                NotFound(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "No se encontro el recurso solicitado",
                    Status = 404,
                });
            }
            try
            {
                //Remplazar los datos enviados por el usuario
                userDb = mapper.Map(userDTO, userDb);
                await context.SaveChangesAsync();
                return Ok(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "Recursos actualizados correctamente",
                    Status = 200,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "Ha ocurrido un error",
                    Status = 400,
                    Error = e.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GenericResponse<UserDTO>>> Delete(int id)
        {
            //Validar si existe el registro
            var existRegister = await context.Users.AnyAsync( x=> x.Id == id );
            if (!existRegister)
            {
                NotFound(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "No se encontro el recurso solicitado",
                    Status = 404,
                });
            }
            try
            {
                //Eliminar registro
                context.Remove(new User { Id = id });
                await context.SaveChangesAsync();
                return Ok(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "Recursos actualizados correctamente",
                    Status = 200,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new GenericResponse<UserDTO>
                {
                    Data = null,
                    Message = "Ha ocurrido un error",
                    Status = 400,
                    Error = e.Message
                });
            }
        }
    }
}
