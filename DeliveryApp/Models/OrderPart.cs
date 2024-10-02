namespace DeliveryApp.Models
{
    public class OrderPart
    {
        public long OrderPartId { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
