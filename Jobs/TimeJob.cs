using MediatR;
using Quartz;
using System;
using System.Threading.Tasks;
using Tamagotchi.Commands;
using Tamagotchi.Events;
using Tamagotchi.Exceptions;
using Tamagotchi.Models;

namespace Tamagotchi
{
    /// <summary>
    /// Periodically this job executed to publish Timepassed event for its subscribers.
    /// </summary>
    public class TimeJob : IJob
    {
        IMediator _mediator;
        IJobScheduler  _jobScheduler;
        IConsoleWriter _consoleWriter;
        public TimeJob(IMediator mediator, IJobScheduler jobScheduler, IConsoleWriter consoleWriter) { 
            _mediator = mediator; _jobScheduler = jobScheduler; _consoleWriter = consoleWriter; }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _mediator.Publish(new TimePassedEvent());

                await Task.Delay(1000); //delay for update to take place

                await _mediator.Send(new StatusCommand());
            }
            catch (GameEndException ex)
            {
                _consoleWriter.WriteLine(ex.Message);
                await _jobScheduler.PauseJob();

                GameStatus.Running = false; // needed to gracefully stop the game
            }
            catch (Exception ex)
            {
                _consoleWriter.WriteLine(ex.Message);
                await _jobScheduler.StopScheduler();

                GameStatus.Running = false;
            }
        }
    }
}
