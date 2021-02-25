using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace SchedulerPOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                    q.UseMicrosoftDependencyInjectionScopedJobFactory();
                    q.AddJob<ReportingJob>(x => x.WithIdentity("ReportingJob")
                                                 .StoreDurably(true));
                    q.AddTrigger(x => x.WithIdentity("ReportingTrigger")
                                       .ForJob("ReportingJob")
                                       .WithCalendarIntervalSchedule(x => x.WithIntervalInDays(1)));
                    }
                    );
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                });
    }
}
