using Checkout.Interfaces.Services;
using System.Linq;

namespace Checkout.Services
{
    public class CalculationService : ICalculationService
    {
        private IPriceService _priceService;

        public CalculationService(IPriceService priceService)
        {
            _priceService = priceService;
        }

        /// <summary>
        /// Calculate the total cost of an order.
        /// </summary>
        /// <param name="order">a string containing stock keeping units, for example: AAA</param>
        /// <returns>The total cost of all the products including any discounts</returns>
        public decimal Total(string order)
        {
            if (string.IsNullOrWhiteSpace(order))
            {
                return decimal.Zero;
            }

            char[] products = order.ToCharArray();
            char[] distinctProducts = order.Distinct().ToArray();
            decimal total = decimal.Zero;

            foreach (char distinctProduct in distinctProducts)
            {
                int quantity = products.Where(x => x == distinctProduct).Count();
                total += _priceService.GetPrice(distinctProduct, quantity);
            }

            return total;
        }
    }
}
