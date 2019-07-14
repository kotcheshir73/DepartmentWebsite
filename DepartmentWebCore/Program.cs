using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace DepartmentWebCore
{
    public class Program
    {
        private static IUnityContainer _container;
        public static void Main(string[] args)
        {
            _container = new UnityContainer();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUnityServiceProvider(_container)
                .UseStartup<Startup>();
    }
}