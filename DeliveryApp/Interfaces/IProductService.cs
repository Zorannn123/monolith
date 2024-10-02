using DeliveryApp.DTO.ProductDTO;
using System.Collections.Generic;

namespace DeliveryApp.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAll();
        bool AddEntity(ProductDto entity);
        bool RemoveEntity(long id);
        bool ModifyEntity(ProductDto entity, long id);
        long GetWithId(ProductDto product);
    }
}
