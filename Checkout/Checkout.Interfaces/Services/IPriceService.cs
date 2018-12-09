namespace Checkout.Interfaces.Services
{
    public interface IPriceService
    {
        decimal GetPrice(char storeKeepingUnit, int quantity = 1);
    }
}
