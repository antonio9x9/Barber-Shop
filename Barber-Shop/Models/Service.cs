using System.ComponentModel.DataAnnotations;

namespace Barber_Shop.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        [MaxLength(200, ErrorMessage = "Max 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(10.00, 999.00, ErrorMessage = "Value between 10 and 999.00")]
        public int MinDuration { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(0.01, 999.00, ErrorMessage = "Value between 0.01 and 999.00")]
        public double Price { get; set; }

    }
}
