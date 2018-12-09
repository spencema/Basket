using Checkout.Exceptions;
using Checkout.Interfaces.Repositories;
using Checkout.Interfaces.Services;
using Checkout.Models;

namespace Checkout.Services
{
    public class PriceService : IPriceService
    {
        private readonly IProductRepository _productRepository;

        public PriceService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public decimal GetPrice(char storeKeepingUnit, int quantity = 1)
        {
            if (quantity == 0)
            {
                return decimal.Zero;
            }

            try
            {
                Product product = _productRepository.Get(storeKeepingUnit);
                // Check whether or not the product is part of a multibuy offer and calculate its price accordingly.
                decimal price = product.IsMultibuy ?
                    CalculateDiscountedPrice(product, quantity) : CalculateStandardPrice(product, quantity);
                return price;
            }
            catch (ProductNotFoundException)
            {
                // In a real application we would do logging here before re-throwing.
                throw;
            }
        }

        private decimal CalculateDiscountedPrice(Product product, int quantity)
        {
            // Calculate how many products are part of the multibuy offer.
            int multibuys = quantity / product.MultibuyQuantity.Value;
            // Calculate how many products are not part of the multibuy offer.
            int additional = quantity % product.MultibuyQuantity.Value;
            // Calculate the price based on both quantities.
            decimal discountedPrice = (multibuys * product.PriceDiscounted.Value) + (additional * product.Price);
            return discountedPrice;
        }

        private decimal CalculateStandardPrice(Product product, int quantity)
        {
            return product.Price * quantity;
        }
    }
}
