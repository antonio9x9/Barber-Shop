using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber_Shop.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime BirthDate { get; set; }


    }
}
