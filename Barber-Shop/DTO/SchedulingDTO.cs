using Barber_Shop.Models;
using System.ComponentModel.DataAnnotations;

namespace Barber_Shop.DTO
{
    public class SchedulingDTO
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public String Observations { get; set; }        
        public Client Client { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
