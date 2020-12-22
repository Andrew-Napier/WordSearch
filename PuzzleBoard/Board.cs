using System;
using System.Collections.Generic;

namespace PuzzleBoard
{
    public class Board : IBoard
    {
        private char[,] _lettersGrid;
        private int _size;
        private IBoardList _boardList;


        public Board(IBoardList boardList, IPuzzleSize size)
        {
            _boardList = boardList;
            _size = size.Max();
            _lettersGrid = new char[_size, _size];

            Enumerate((r, c) =>
                {
                    _lettersGrid[r, c] = '.';
                });
        }

        public Board(char[,] source, IBoardList boardList)
        {
            _boardList = boardList;
            _lettersGrid = source;
            _size = source.GetUpperBound(0) + 1;
        }

        public int BlanksRemaining()
        {
            var count = 0;
            Enumerate((r, c) =>
                {
                    if (IsEmpty(r, c))
                    {
                        count++;
                    }
                });
            return count;
        }

        public void Enumerate(Action<int, int> action)
        {
            for (int r = 0; r < _size; r++)
            {
                for (int c = 0; c < _size; c++)
                {
                    action(r, c);
                }
            }
        }

        public bool IsEmpty(int r, int c)
        {
            return _lettersGrid[r, c] == '.';
        }

        public bool IsMatching(char letter, int row, int col)
        {
            return _lettersGrid[row, col] == letter;
        }

        public IBoard AddWord(string word, StartingPosition position)
        {
            var newGrid = _lettersGrid;
            int i = 0;
            foreach(char letter in word.ToCharArray())
            {
                int r = position.Row + (position.Direction.RowDirection() * i);
                int c = position.Col + (position.Direction.ColDirection() * i);
                newGrid[r, c] = letter;
                i++;
            }
            _boardList.AddWord(word, position);

            return new Board(newGrid, _boardList);
        }

        public IBoard BlatWord(string word)
        {
            var newGrid = _lettersGrid;
            word = word.ToUpperInvariant();
            _boardList.BlatWord(word);
            List<char> letters = new List<char>();
            foreach (var letter in word.ToCharArray())
            {
                letters.Add(letter);
            }
            letters.Sort();

            var i = 0;
            Enumerate((r, c) =>
                {
                    if (IsEmpty(r, c))
                    {
                        newGrid[r, c] = letters[i];
                        i++;
                    }
                    if (i >= letters.Count) return;
                });

            return new Board(newGrid, _boardList);
        }

        public void Display()
        {
            Console.WriteLine();
            Enumerate((r, c) =>
                {
                    Console.Write($"{_lettersGrid[r, c]}");
                    Console.Write((c == _size - 1) ? "\n" : " ");
                });
            _boardList.Sort();
            foreach (var entry in _boardList.GetEntries())
            {
                Console.WriteLine($"- {entry.GetWord()}");
            }
        }

        public IBoardList List() => _boardList;
    }
}
