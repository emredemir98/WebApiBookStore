using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBookStore.DBOperations;

namespace WebApiBookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope()) //Uygulama aya�a kalkt���ndan initial datan�n in memory DB'ye yaz�lmas� i�in Program.cs
                                                           //i�erisinde configurasyon yap�l�yor
            {
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services); // yazd���m�z initialize fonksiyonu �a��r�l�r
            }
             host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
