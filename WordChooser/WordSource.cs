using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace WordChooser
{
    public class WordSource : IWordSource
    {
        private HtmlDocument _doc;
        private IConfig _config;
        private IWordFilter _wordFilter;

        public WordSource(HtmlWeb web, IConfig config, IWordFilter wordFilter)
        {
            _config = config;
            _doc = web.Load(_config.SourceUrl());
            _wordFilter = wordFilter;
        }

        public IEnumerable<string> GetListOfWords()
        {
            var nodes = _doc.DocumentNode.SelectNodes(_config.XtextPath()).Nodes();
            var acceptableWords = new HashSet<string>();

            foreach (var node in nodes)
            {
                if (!node.GetClasses().Contains(_config.XnonTextClass()))
                {
                    var words = node.InnerText.Split(_config.WhiteSpace());
                    foreach(var acceptedWord in _wordFilter.Filter(words))
                    {
                        if (!acceptableWords.Contains(acceptedWord))
                        {
                            acceptableWords.Add(acceptedWord);
                        }
                    }
                }
            }

            return acceptableWords;
        }

        public IEnumerable<string> GetTitle()
        {
            return _doc.DocumentNode
                .SelectSingleNode(_config.XtitlePath())
                .InnerText
                .Split(_config.WhiteSpace());
        }
    }
}
