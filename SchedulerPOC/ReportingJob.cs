using Quartz;
using System;
using System.Threading.Tasks;

namespace SchedulerPOC
{
    public class ReportingJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Job triggered: " + DateTime.Now);
                return Task.FromResult(0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}
