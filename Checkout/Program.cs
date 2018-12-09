using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Checkout.Interfaces.Repositories;
using Checkout.Interfaces.Services;
using Checkout.Repositories;
using Checkout.Services;
using System;

namespace Checkout
{
    public class Program
    {
        private static IWindsorContainer _container;
        private static IReceiptService _receipt;

        public static void Main(string[] args)
        {
            _container = new WindsorContainer();
            RegisterDependencies();
            _receipt = _container.Resolve<IReceiptService>();

            if (args.Length == 0)
            {
                string order = string.Empty;
                string receipt = _receipt.Print(order);
                Console.WriteLine(receipt);
            }
            else
            {
                foreach (string order in args)
                {
                    string receipt = _receipt.Print(order);
                    Console.WriteLine(receipt);
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        private static void RegisterDependencies()
        {
            _container.Register(Component.For<IReceiptService>().ImplementedBy<ReceiptService>());
            _container.Register(Component.For<ICalculationService>().ImplementedBy<CalculationService>());
            _container.Register(Component.For<IPriceService>().ImplementedBy<PriceService>());
            _container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>());
        }
    }
}
