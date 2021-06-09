using Quartz;
using System;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public interface IJobScheduler
    {
        Task CreateScheduler();
        Task StopScheduler();
        Task PauseJob();
        Task ResumeJob();
        Task StartJob();
    }

    public class JobScheduler : IJobScheduler
    {
        IScheduler _scheduler; ISchedulerFactory _schedulerFactory; JobKey _jobKey; IConsoleWriter _consoleWriter;

        public JobScheduler(ISchedulerFactory schedulerFactory, IConsoleWriter consoleWriter) { _schedulerFactory = schedulerFactory; _consoleWriter = consoleWriter; }

        public async Task CreateScheduler()
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler();

                var job = JobBuilder.Create<TimeJob>()
                    .WithIdentity("TimeJob", "PeriodicUpdates")
                    .Build();

                _jobKey = job.Key;

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("TimeJobTrigger", "PeriodicUpdates")
                    .StartAt(DateTime.Now.AddSeconds(24))
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(24)
                    .RepeatForever())
                    .Build();

                await _scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se) { _consoleWriter.WriteLine(se.Message); }
            catch (Exception ex) { _consoleWriter.WriteLine(ex.Message); }
        }

        public async Task StartJob() { await _scheduler.Start(); }
        public async Task PauseJob() { await _scheduler.PauseJob(_jobKey); }
        public async Task ResumeJob() { await _scheduler.ResumeJob(_jobKey); }
        public async Task StopScheduler() { await _scheduler.Shutdown(); }
    }
}
