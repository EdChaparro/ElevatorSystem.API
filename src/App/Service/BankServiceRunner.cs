using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntrepidProducts.ElevatorSystemService
{
    public class BankServiceRunner
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<BankWorker>();
                });
    }
}
