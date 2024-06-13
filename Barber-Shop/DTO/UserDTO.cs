namespace Barber_Shop.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //TODO: create enum
        public string Role { get; set; }
    }
}
