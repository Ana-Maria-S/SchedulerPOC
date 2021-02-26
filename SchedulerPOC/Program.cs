using Microsoft.Extensions.Hosting;
using Quartz;
using System;

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
                                           .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(23,50).InTimeZone(TimeZoneInfo.Utc)));
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                });
    }
}
