using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz.DependencyInjection.Microsoft.Extensions;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider;

            try
            {
                serviceProvider = BuildServiceCollection();

                await serviceProvider.GetService<IGame>().Start();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            Console.WriteLine("Game has ended! Press enter to exit");
            Console.ReadLine();
        }

        public static ServiceProvider BuildServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddMemoryCache()
                .AddQuartz()
                .AddMediatR(Assembly.GetExecutingAssembly());

            serviceCollection.AddTransient<IGame, Game>();
            serviceCollection.AddTransient<TimeJob>();
            serviceCollection.AddTransient<IConsoleWriter, ConsoleWriter>();

            serviceCollection.AddSingleton<ITestableCache, TestableCache>();
            serviceCollection.AddSingleton<ICommandFactory, CommandFactory>();
            serviceCollection.AddSingleton<IJobScheduler, JobScheduler>();
            serviceCollection.AddSingleton<IFoodContainer>(_ =>
            {
                return new FoodContainer(
                    new Food("fish", 5),
                    new Food("chicken", 10),
                    new Food("lamb", 15),
                    new Food("cow", 20));
            });

            return serviceCollection.BuildServiceProvider();
        }
    }
}
