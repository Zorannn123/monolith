using System.Collections.Generic;
using System;
using DeliveryApp.Enums;

namespace DeliveryApp.DTO.ProductDTO
{
    public class OrderDto
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public List<OrderPartDto> OrderParts { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeOfDelivery { get; set; }
        public long DeliveredBy { get; set; }
    }
}
