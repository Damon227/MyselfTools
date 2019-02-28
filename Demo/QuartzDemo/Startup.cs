using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using KC.Foundation.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace QuartzDemo
{
    public class Startup
    {
        public static IServiceProvider ConfigeServices()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            IServiceCollection services = new ServiceCollection();

            //注入

            ILoggerFactory loggerFactory = new LoggerFactory();
            services.AddSingleton(_ => loggerFactory);

            services.AddLogging().AddConsoleLogger();
            services.Configure<FileLoggerOptions>(config.GetSection("Logging:FileLogger")).AddFileLogger();

            services.AddOptions();
            services.Configure<DataOptions>(config.GetSection("Data"));

            services.AddSingleton<IDemoService, DemoService>();

            services.AddSingleton<IJobFactory, MyJobFactory>();
            services.Add(new ServiceDescriptor(typeof(DemoJob), typeof(DemoJob), ServiceLifetime.Singleton));

            services.AddSingleton(provider =>
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = factory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                //await scheduler.Start();
                return scheduler;
            });

            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}