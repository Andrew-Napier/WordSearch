using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using PuzzleBoard;
using PuzzleBoard.Domain.Interfaces;
using PuzzleBoard.Domain.Models;
using PuzzleBoard.Domain.Services;
#nullable enable


namespace TestHarness;

class Program
{
    static void Main(string[] args)
    {

        IServiceCollection serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        //HarnessGenerator(serviceProvider);
        HarnessVerifier(serviceProvider);
    }

    private static void HarnessVerifier(IServiceProvider serviceProvider)
    {
        var answers = new List<string>();
        for (int i = 1; i <= 25; i++)
        {
            var bl = new BoardLoader(serviceProvider.GetRequiredService<IBoard>(),
                serviceProvider.GetRequiredService<IBoardListEntryFactory>());
            IBoard game = bl.Load(i);
            if (IsPuzzleValid(game))
            {
                Console.WriteLine($"Puzzle {i}");
                answers.Add($"{i}: {game.List().GetBlattedWord()}");
                game.Display();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Puzzle {i} has integrity issues");
            }
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

            if (IsPuzzleValid(puzzle))
            {
                Debug.Assert(puzzle != null);
                puzzle!.Display();
                var saver = new BoardSaver(puzzle, serviceProvider.GetRequiredService<IBoardListEntryFactory>());
                saver.Save(counter);
                counter++;
            }
            Console.WriteLine();
        }
    }

    private static bool IsPuzzleValid(IBoard? puzzle)
    {
        if (puzzle == null)
        {
            return false;
        }

        var boardList = puzzle.List();
        bool valid = true;
        foreach(var entry in boardList.GetEntries())
        {
            var wordToFind = entry.GetWord();
            puzzle.Enumerate((row, col) =>
            {
                int r = 10 - row;
                int c = 10 - col;
                if (valid)
                {
                    WordDirection.Enumerate((d) =>
                        {
                            // if wordToFind.isFoundAt(r,c,d)
                            //      && entry.GetPosition != currentPosition
                            //     valid = false;
                            bool works = true;
                            for (int l = 0; l < wordToFind.Length; l++)
                            {
                                works = puzzle.IsMatching(wordToFind[l], r, c);
                                if (!works) break;

                                r += d.RowDirection();
                                c += d.ColDirection();
                            }
                            if (works)
                            {
                                var pos = entry.GetPosition();
                                if (pos.Col != (10 - col)
                                    || pos.Row != (10 - row)
                                    || pos.Direction != d)
                                {
                                    Console.WriteLine($"Found match for {wordToFind} at [{10 - col},{10 - row}], but was meant to be at: [{pos.Col},{pos.Row}]");
                                    valid = false;
                                    return false;
                                }
                            }
                            return true;
                        },
                        true);
                }
            });
        }
        return true;
    }

    private static IBoard? BuildPuzzleWithRetries(
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