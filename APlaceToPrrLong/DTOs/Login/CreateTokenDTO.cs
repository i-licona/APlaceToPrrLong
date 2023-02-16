namespace APlaceToPrrLong.DTOs.Login
{
    public class CreateTokenDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public CreateTokenDTO(string email, string name, string lastName)
        {
            Email = email;
            Name = name;
            LastName = lastName;
        }
    }
}
