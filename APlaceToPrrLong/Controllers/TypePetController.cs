using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using APlaceToPrrLong.Models;
using APlaceToPrrLong.DTOs.TypePet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APlaceToPrrLong.Controllers
{
  [ApiController]
  [Route("api/typePet")]
  public class TypePetController : ControllerBase
  {
    private AplicationDbContext context;
    private readonly IMapper mapper;
    public TypePetController(
      AplicationDbContext context, 
      IMapper mapper
    )
    {
      this.mapper = mapper;
      this.context = context;
    }
    /* Get all pet types */
    [HttpGet]
    public async Task<ActionResult<GenericListResponse<TypePet>>> Get(){
      try{
        var data = await context.TypePets.ToListAsync();
        GenericListResponse<TypePet> response = new GenericListResponse<TypePet>(
          data, "Recursos obtenidos correctamente", 200
        );
        return Ok(response);
      }catch (Exception e){
        GenericListResponse<TypePet> response = new GenericListResponse<TypePet>(
          null, "Ha ocurrido un error", 400, e.Message
        );
        return BadRequest(response);
      }
    }
    /* Create a pet type */
    [HttpPost]
    public async Task<ActionResult<GenericResponse<TypePet>>> Post([FromBody] TypePetDTO typePet){
      try{
        TypePet data = mapper.Map<TypePet>(typePet); 
        context.TypePets.Add(data);
        await context.SaveChangesAsync();
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          data,
          "Recursos creados correctamente", 
          201
        );
        return Ok(response);
      }
      catch (Exception e){
        GenericListResponse<TypePet> response = new GenericListResponse<TypePet>(
          null, "Ha ocurrido un error", 400, e.Message
        );
        return BadRequest(response);
      }
    }
    /* Update a pet type */
    [HttpPut("{id:int}")]  
    public async Task<ActionResult<GenericResponse<TypePet>>> Put(int id, [FromBody] TypePetDTO typePet){
      var dataDb = await context.TypePets.FirstOrDefaultAsync( x => x.Id == id);
      if (dataDb != null ){
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          null, 
          "No se encontro el recurso solicitado", 
          404
        );
        NotFound(response);
      }

      try{
        //Remplazar los datos enviados por el usuario
        dataDb = mapper.Map(typePet, dataDb);
        await context.SaveChangesAsync();
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          null, 
          "Recursos actualizados correctamente", 
          200
        );
        return Ok(response);
      }catch (Exception e){
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          null, 
          "Ha ocurrido un error", 
          400, 
          e.Message
        );
        return BadRequest(response);
      }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<GenericResponse<TypePet>>> Delete(int id){
      var existRegister = await context.TypePets.AnyAsync( x => x.Id == id );
      if (!existRegister){
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          null, 
          "No se encontro el recurso solicitado", 
          404
        );
        NotFound(response);
      }

      try{
        context.Remove( new TypePet{ Id = id });
        await context.SaveChangesAsync();
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          null, 
          "Recursos eliminados correctamente", 
          200
        );
        return Ok(response);
      }catch (Exception e){
        GenericResponse<TypePet> response = new GenericResponse<TypePet>(
          null, 
          "Ha ocurrido un error", 
          400, 
          e.Message
        );
        return BadRequest(response);
      }
    }
  }
}