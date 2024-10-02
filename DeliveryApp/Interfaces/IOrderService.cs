using DeliveryApp.DTO.ProductDTO;
using System.Collections.Generic;

namespace DeliveryApp.Interfaces
{
    public interface IOrderService
    {
        void AddEntity(OrderDto entity);
        List<OrderDto> GetUndelivered();
        bool TakeOrder(long orderid, long userId);
        List<OrderDto> History(long userid);
        List<OrderDto> HistoryDeliverer(long userid);
        OrderDto GetCurrentOrder(long userid);
    }
}
