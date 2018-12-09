namespace Checkout.Interfaces.Services
{
    public interface ICalculationService
    {
        decimal Total(string order);
    }
}
