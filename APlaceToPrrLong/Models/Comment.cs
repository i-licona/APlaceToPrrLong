namespace APlaceToPrrLong.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public PetPost PetPost { get; set; }
    }
}
