namespace DeliveryApp.DTO.ProductDTO
{
    public class OrderPartDto
    {
        public ProductDto Product { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
