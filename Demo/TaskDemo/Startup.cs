using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KC.Fengniaowu.Eos.ResourceStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskDemo
{
    internal class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                //.AddJsonFile("appsettings.Development.json", true, true)
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<ResourceStorageProviderOptions>(config.GetSection("Blob"));

            services.AddSingleton<IResourceStorageProvider, AzureBlobStorageProvider>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
