namespace PatatzaakOfficeel.Models
{
    public class Orderitem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int? Quantity { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
      
    }
}
