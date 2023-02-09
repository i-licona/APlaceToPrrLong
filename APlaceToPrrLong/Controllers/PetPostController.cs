using APlaceToPrrLong.DTOs.Pet;
using APlaceToPrrLong.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APlaceToPrrLong.Controllers;
//hshhshs
[Route("api/pet")]
[ApiController]
public class PetPostController : Controller
{
    private readonly IMapper mapper;
    // GET
    private readonly AplicationDbContext context;

    public PetPostController(AplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<GenericListResponse<PetDTO>>> Get()
    {
        try
        {
            var data = await context.PetPosts.ToListAsync();
            var result = mapper.Map<List<PetDTO>>(data);
            return Ok(new GenericListResponse<PetDTO>
                (result, "Recursos Obtenidos de forma Exitosa", 200));
        }
        catch (Exception e)
        {
            return BadRequest(new GenericListResponse<PetDTO>
                (null, "Ha ocurrido un error", 400, e.Message));
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenericResponse<PetDTO>>> GetPetbyId(int id)
    {
        var data = await context.PetPosts.FirstOrDefaultAsync(p => p.Id == id);
        if (data == null)
            return NotFound(new GenericResponse<PetDTO>(null, "Contenido no encontrado", 404));
        try
        {
            var result = mapper.Map<PetDTO>(data);
            return Ok(new GenericResponse<PetDTO>(result, "Recursos Obtenidos de forma exitosa", 200));
        }
        catch (Exception e)
        {
            return BadRequest(new GenericResponse<PetDTO>
                (null, "Ha ocurrido un error", 400, e.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse<PetDTO>>> Post([FromBody] AddPetDTO petDTO)
    {
        var data = mapper.Map<PetPost>(petDTO);
        try
        {
            context.PetPosts.Add(data);
            await context.SaveChangesAsync();
            var result = mapper.Map<PetDTO>(context);
            return Ok(new GenericResponse<PetDTO>
                (result, "Datos recolectados correctamente", 201));
        }
        catch (Exception e)
        {
            return BadRequest(new GenericResponse<PetDTO>
                (null, "Ha ocurrido un error", 400,e.Message));
        }
    }
}