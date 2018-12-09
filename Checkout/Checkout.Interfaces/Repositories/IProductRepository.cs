using Checkout.Models;

namespace Checkout.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Product Get(char storeKeepingUnit);
    }
}
