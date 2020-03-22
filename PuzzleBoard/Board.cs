using System;
using System.Collections.Generic;

namespace PuzzleBoard
{
    public class Board : IBoard
    {
        private char[,] _lettersGrid;
        private int _size;
        private const int boardSize = 11;

        public Board()
        {
            _size = boardSize;
            _lettersGrid = new char[boardSize,boardSize];
            for(int r = 0; r < boardSize; r++)
            {
                for(int c = 0; c < boardSize; c++)
                {
                    _lettersGrid[r, c] = '.';
                }
            }
        }

        public Board(char[,] source)
        {
            _lettersGrid = source;
            _size = source.GetUpperBound(0) + 1;
        }

        public int BlanksRemaining()
        {
            var count = 0;
            for(int r = 0; r < _size; r++)
            {
                for(int c = 0; c < _size; c++)
                {
                    if (IsEmpty(r,c))
                    {
                        count++;
                    }
                }
            }
            return count;
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

            return new Board(newGrid);
        }

        public IBoard BlatWord(string word)
        {
            var newGrid = _lettersGrid;
            word = word.ToUpperInvariant();
            List<char> letters = new List<char>();
            foreach (var letter in word.ToCharArray())
            {
                letters.Add(letter);
            }
            letters.Sort();

            var i = 0;
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    if (IsEmpty(row,col))
                    {
                        newGrid[row, col] = letters[i];
                        i++;
                    }
                    if (i >= letters.Count) break;
                }
            }
            return new Board(newGrid);
        }

        public void Display()
        {
            Console.WriteLine();
            for (int r = 0; r < _size; r++)
            {
                for (int c = 0; c < _size; c++)
                {
                    Console.Write($"{_lettersGrid[r, c]} ");
                }
                Console.WriteLine();                  
            }
        }
    }
}
