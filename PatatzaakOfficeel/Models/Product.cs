using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatatzaakOfficeel.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public string? ImageURL { get; set; }
        public  virtual Category?  Category { get; set; }
        public int CategoryID { get; set; }
        public  virtual Discount?  Discount { get; set; }

        public int? DiscountID { get; set; }




        //public Product(int id, string name, string description, decimal price)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //}
    }
}
