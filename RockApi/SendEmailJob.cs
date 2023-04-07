using System;
using Quartz;
using System.Threading.Tasks;

namespace RockApi
{
    public class SendEmailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Test Job");
            // Code that sends a periodic email to the user (for example)
            // Note: This method must always return a value 
            // This is especially important for trigger listers watching job execution 
            return Task.FromResult(true);
        }
    }
}

