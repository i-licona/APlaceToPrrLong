using APlaceToPrrLong.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace APlaceToPrrLong.DTOs.Pet
{
    public class PetDTO
    {
        public string Description { get; set; }
        public string Nombre { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime PostDate { get; set; }
        public UserDTO User { get; set; }
    }
}
