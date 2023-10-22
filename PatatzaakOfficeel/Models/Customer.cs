using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatatzaakOfficeel.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
      
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public int OrderId { get; set; }

        
    }
}
