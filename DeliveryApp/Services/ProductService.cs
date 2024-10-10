using AutoMapper;
using DeliveryApp.DTO.ProductDTO;
using DeliveryApp.Infrastructure;
using DeliveryApp.Interfaces;
using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DeliveryAppDbContext _dbContext;

        public ProductService(IMapper mapper, DeliveryAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public bool AddEntity(ProductDto entity)
        {
            try
            {
                Product product = _mapper.Map<Product>(entity);
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public List<ProductDto> GetAll()
        {
            return _mapper.Map<List<ProductDto>>(_dbContext.Products.ToList());
        }

        public long GetWithId(ProductDto product)
        {
            var entity = _dbContext.Products
                .FirstOrDefault(s => s.Name == product.Name && s.Ingredients == product.Ingredients);

            if (entity == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            return _mapper.Map<ProductDto>(entity).ProductId;
        }

        public bool RemoveEntity(long id)
        {
            try
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == id);

                if (product == null)
                {
                    return false;
                }

                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error removing product: {e.Message}");
                return false;
            }

            return true;
        }
    }
}
