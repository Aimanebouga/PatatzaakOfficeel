using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatatzaakOfficeel.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public int DiscountPercentage { get; set; }
        public  virtual Product? Product { get; set; }
        public int ProductId { get; set; } 



    }
}
