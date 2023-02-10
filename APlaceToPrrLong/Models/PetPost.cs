namespace APlaceToPrrLong.Models
{
    public class PetPost
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
