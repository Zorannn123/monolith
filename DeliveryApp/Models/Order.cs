using System.Collections.Generic;
using System;
using DeliveryApp.Enums;

namespace DeliveryApp.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public List<OrderPart> OrderParts { get; set; }
        public OrderStatus State { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeOfDelivery { get; set; }
        public long DeliveredBy { get; set; }
    }
}
