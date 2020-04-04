using System;
using System.Configuration;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using PuzzleBoard;
using PuzzleStorage;
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
            int counter = 1;

            Console.WriteLine("Puzzle Generator...");
            ConsoleKeyInfo key;
            do
            {
                var rwd = serviceProvider.GetRequiredService<IRelatableWordsDictionary>();
                var puzzle = BuildPuzzleWithRetries(serviceProvider, rwd);

                if (puzzle != null)
                { 
                    puzzle.Display();
                    var saver = new BoardSaver(puzzle, serviceProvider.GetRequiredService<IBoardListEntryFactory>());
                    saver.Save(counter);
                    counter++;
                }
                Console.WriteLine("Try again? (y/n)");
                key = Console.ReadKey();
                Console.WriteLine();
            } while (!"Nn".Contains(key.KeyChar));
        }

        private static IBoard BuildPuzzleWithRetries(
            IServiceProvider serviceProvider,
            IRelatableWordsDictionary rwd)
        {
            int retryCount = 7;

            while (retryCount-- > 0)
            {
                try
                {
                    rwd.PrepareDictionary();
                    var pg = new PuzzleGenerator(serviceProvider, rwd);
                    var puzzle = pg.Execute();
                    return puzzle;
                }
                catch (PuzzleException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    if (e.Ranking == PuzzleExceptionRanking.noRetry)
                        break;
                }
            }
            return null;
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var config = new Config(ConfigurationManager.AppSettings);

            serviceCollection
                .AddSingleton<HtmlWeb, HtmlWeb>()
                .AddSingleton<IConfig>(config)
                .AddSingleton<IPuzzleSize>(new PuzzleSize(11))
                .AddTransient<IDecisionMaker, DecisionMaker>()
                .AddSingleton<IWordFilter, WordFilter>()
                .AddTransient<IWordSource, WordSource>()
                .AddTransient<IRelatableWordsDictionary, RelatableWordsDictionary>()
                .AddTransient<IDirectionCounts, DirectionCounts>()
                .AddTransient<IRandomPicker, RandomPicker>()
                .AddTransient<IBoard, Board>()
                .AddTransient<IBoardList, BoardList>()
                .AddTransient<IBoardListEntryFactory, BoardListEntryFactory>()
                .AddTransient<PlacementChecker, PlacementChecker>();
        }
    }
}
