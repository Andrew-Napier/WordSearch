using System;
using System.Configuration;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using PuzzleBoard;
using WordChooser;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {

            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Puzzle Generator...");
            ConsoleKeyInfo key;
            do
            {
                var rwd = serviceProvider.GetRequiredService<IRelatableWordsDictionary>();
                rwd.PrepareDictionary();
                var pg = new PuzzleGenerator(serviceProvider, rwd);
                try
                {
                    pg.Execute();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Try again? (y/n)");
                key = Console.ReadKey();
            } while (!"Nn".Contains(key.KeyChar));
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var config = new Config(ConfigurationManager.AppSettings);

            serviceCollection
                .AddSingleton<HtmlWeb, HtmlWeb>()
                .AddSingleton<IConfig>(config)
                .AddSingleton<IWordFilter, WordFilter>()
                .AddTransient<IWordSource, WordSource>()
                .AddTransient<IRelatableWordsDictionary, RelatableWordsDictionary>()
                .AddTransient<IDirectionCounts, DirectionCounts>()
                .AddTransient<IRandomPicker, RandomPicker>()
                .AddTransient<Board, Board>()
                .AddTransient<PlacementChecker, PlacementChecker>();

            /*
            x IConfig config = new Config(ConfigurationManager.AppSettings);
            x serviceCollection.AddSingleton<IConfig>(config);
            x serviceCollection.AddSingleton<IWordFilter>(new WordFilter());


            x serviceCollection.Add(new ServiceDescriptor(WordSource, IWordSource));
                Console.WriteLine("Word Source...");
            x var ws = new WordSource(web, config, wf);
            x Console.WriteLine("Dictionary ...");
            x var rwd = new RelatableWordsDictionary(ws);
            x Console.WriteLine("Word Filter...");
            rwd.PrepareDictionary();

            Console.WriteLine("Random Generator...");
            x var randomGenerator = new RandomPicker(DateTime.UtcNow.Ticks.GetHashCode());
            Console.WriteLine("Board...");
            var lettersGrid = new Board(11);
            Console.WriteLine("Placement Checker...");
            var placeGenerator = new PlacementChecker(11, randomGenerator);
            */
        }
    }
}
