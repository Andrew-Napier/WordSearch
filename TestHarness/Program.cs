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

            HarnessGenerator(serviceProvider);
        }

        private static void HarnessVerifier(IServiceProvider serviceProvider)
        {
            for (int i = 1; i <= 25; i++)
            {
                var bl = new BoardLoader(serviceProvider.GetRequiredService<IBoard>(),
                    serviceProvider.GetRequiredService<IBoardListEntryFactory>());
                IBoard game = bl.Load(i);
                game.Display();
                Console.WriteLine();
            }
        }

        static void HarnessGenerator(ServiceProvider serviceProvider)
        {
            int counter = 1;

            Console.WriteLine("Puzzle Generator...");
            while (counter <= 2)
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
                Console.WriteLine();
            }
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
                    var pg = new PuzzleGenerator(
                        serviceProvider.GetRequiredService<IBoard>(),
                        serviceProvider.GetRequiredService<PlacementChecker>(),
                        serviceProvider.GetRequiredService<IPuzzleSize>(),
                        rwd,
                        serviceProvider.GetRequiredService<IDecisionMaker>(),
                        serviceProvider.GetRequiredService<IDirectionCounts>(),
                        serviceProvider.GetRequiredService<IRandomPicker>(),
                        serviceProvider.GetRequiredService<IBoardList>());

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
