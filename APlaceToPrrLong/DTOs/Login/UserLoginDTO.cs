using System.ComponentModel.DataAnnotations;

namespace APlaceToPrrLong.DTOs.Login
{
    public class UserLoginDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string MLastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Picture { get; set; }
    }
}
