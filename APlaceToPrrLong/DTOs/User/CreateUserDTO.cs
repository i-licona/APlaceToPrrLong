using APlaceToPrrLong.Models;
using System.ComponentModel.DataAnnotations;

namespace APlaceToPrrLong.DTOs.User
{
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string MLastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        public string Picture { get; set; }
    }
}
