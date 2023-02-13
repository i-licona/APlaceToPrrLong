using APlaceToPrrLong.DTOs.Pet;
using APlaceToPrrLong.DTOs.User;
using APlaceToPrrLong.Models;
using AutoMapper;

namespace APlaceToPrrLong.Utilities
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            //Parametro 1: El objeto origen
            //Parametro 2: El objeto resultado
            CreateMap<CreateUserDTO, User>();
            //Reverse Map, realiza la operación inversa
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<AddPetDTO, PetPost>();
            CreateMap<PetPost, PetDTO>().ReverseMap();

            
        }
    }
}
