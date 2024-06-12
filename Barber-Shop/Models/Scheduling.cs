using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber_Shop.Models
{
    public class Scheduling
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime Date{ get; set; }

        [Required(ErrorMessage = "Required field")]
        public int Status{ get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Observations { get; set; }

        [ForeignKey("Id")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
