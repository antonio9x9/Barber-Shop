using System.ComponentModel.DataAnnotations;

namespace Barber_Shop.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
