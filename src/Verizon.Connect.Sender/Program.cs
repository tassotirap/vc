namespace Verizon.Connect.Sender
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using Verizon.Connect.Sender.DI;

    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            DIBootStrapper.RegisterServices(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            SenderApplication senderApplication = serviceProvider.GetService<SenderApplication>();
            await senderApplication.Start(10, 1);
        }
    }
}
