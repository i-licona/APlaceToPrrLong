namespace APlaceToPrrLong.DTOs.Login
{
    public class UserCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserCredentials(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}
