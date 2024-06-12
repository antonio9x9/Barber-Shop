using System.ComponentModel.DataAnnotations;

namespace Barber_Shop.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinDuration { get; set; }
        public double Price { get; set; }
    }
}
