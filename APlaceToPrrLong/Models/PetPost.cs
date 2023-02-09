using System.ComponentModel.DataAnnotations;

namespace APlaceToPrrLong.Models
{
    public class PetPost
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
