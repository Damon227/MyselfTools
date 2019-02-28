using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace QuartzDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ////创建一个标准调度器工厂
            //ISchedulerFactory factory = new StdSchedulerFactory();
            ////通过从标准调度器工厂获得一个调度器，用来启动任务
            //IScheduler scheduler = await factory.GetScheduler();
            ////调度器的线程开始执行，用以触发Trigger
            //await scheduler.Start();

            //IJobDetail job1 = JobBuilder.Create<DemoJob>().WithIdentity("DemoJob1", "Group1").Build();
            //ITrigger trigger1 = TriggerBuilder.Create()
            //    .WithIdentity("Trigger1", "Group1")
            //    .StartNow()
            //    .WithSimpleSchedule(t => t.WithIntervalInSeconds(3).RepeatForever())
            //    .Build();

            //await scheduler.ScheduleJob(job1, trigger1);

            // 依赖注入方式
            IServiceProvider serviceProvider = Startup.ConfigeServices();
            IScheduler scheduler = serviceProvider.GetService<IScheduler>();
            await scheduler.Start();

            IJobDetail job1 = JobBuilder.Create<DemoJob>().WithIdentity("DemoJob1", "Group1").Build();
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("Trigger1", "Group1")
                .StartNow()
                .WithSimpleSchedule(t => t.WithIntervalInSeconds(3).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job1, trigger1);

            Thread.Sleep(Timeout.Infinite);
        }
    }

    public class DemoJob : IJob
    {
        private readonly IDemoService _demoService;

        public DemoJob(IDemoService demoService)
        {
            _demoService = demoService;
        }

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
        /// fires that is associated with the <see cref="T:Quartz.IJob" />.
        /// </summary>
        /// <remarks>
        /// The implementation may wish to set a  result object on the
        /// JobExecutionContext before this method exits.  The result itself
        /// is meaningless to Quartz, but may be informative to
        /// <see cref="T:Quartz.IJobListener" />s or
        /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
        /// execution.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        public async Task Execute(IJobExecutionContext context)
        {
            _demoService.Work();
        }
    }
}
