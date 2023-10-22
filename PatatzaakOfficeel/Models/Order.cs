﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatatzaakOfficeel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? TotalCost { get; set; }
        public virtual ICollection<Orderitem> Orderitems { get; set; }
        public virtual Customer? Customer { get; set; }
        public int CustomerId { get; set; }

        

    }
}
