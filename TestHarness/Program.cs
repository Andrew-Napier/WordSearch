using System;
using System.Collections.Generic;
using System.Configuration;
using HtmlAgilityPack;
using PuzzleBoard;
using WordChooser;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var web = new HtmlWeb();


            var config = new Config(ConfigurationManager.AppSettings);
            Console.WriteLine("Word Filter...");
            var wf = new WordFilter();
            Console.WriteLine("Word Source...");
            var ws = new WordSource(web, config, wf);
            Console.WriteLine("Dictionary ...");
            var rwd = new RelatableWordsDictionary(ws);
            Console.WriteLine("Word Filter...");
            rwd.PrepareDictionary();

            Console.WriteLine("Random Generator...");
            var randomGenerator = new RandomPicker(DateTime.UtcNow.Ticks.GetHashCode());
            Console.WriteLine("Board...");
            var lettersGrid = new Board(11);
            Console.WriteLine("Placement Checker...");
            var placeGenerator = new PlacementChecker(11, randomGenerator);
            var wordGenerator = rwd;
            Console.WriteLine("Direction counts...");
            var directionCounts = new Dictionary<WordDirections, int>();
            foreach(WordDirections d in Enum.GetValues(typeof(WordDirections)))
            {
                directionCounts[d] = 0;
            }
            Console.WriteLine("Puzzle Generator...");
            var pg = new PuzzleGenerator(lettersGrid, placeGenerator, wordGenerator, directionCounts, randomGenerator);
            pg.Execute();
        }
    }
}
