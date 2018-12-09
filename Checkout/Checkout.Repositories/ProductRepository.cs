using Checkout.Exceptions;
using Checkout.Interfaces.Repositories;
using Checkout.Models;
using System.Collections.Generic;

namespace Checkout.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<char, Product> _products;

        public ProductRepository()
        {
            _products = new Dictionary<char, Product>()
            {
                { 'A', new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 } },
                { 'B', new Product() { IsMultibuy = true, Price = 30.00m, PriceDiscounted = 45.00m, MultibuyQuantity = 2 } },
                { 'C', new Product() { Price = 20.00m } },
                { 'D', new Product() { Price = 15.00m } },
                // Extra product to demonstrate 'buy one get one free'
                { 'E', new Product { IsMultibuy = true, Price = 25.00m, PriceDiscounted = 25.00m, MultibuyQuantity = 2 } }
            };
        }

        public Product Get(char storeKeepingUnit)
        {
            try
            {
                return _products[storeKeepingUnit];
            }
            catch (KeyNotFoundException)
            {
                // Instead of re-throwing throw a more specific exception.
                throw new ProductNotFoundException($"'{storeKeepingUnit}' is not a known product. Please contact your store manager.");
            }
        }
    }
}
