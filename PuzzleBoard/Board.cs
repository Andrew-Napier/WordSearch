using System;
using System.Collections.Generic;

namespace PuzzleBoard
{
    public class Board
    {
        private char[,] _lettersGrid;
        private int _size;

        public Board(int boardSize)
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

        public int BlanksRemaining
        {
            get
            {
                var count = 0;
                for(int r = 0; r < _size; r++)
                {
                    for(int c = 0; c < _size; c++)
                    {
                        if (isEmpty(r,c))
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public bool isEmpty(int r, int c)
        {
            return _lettersGrid[r, c] == '.';
        }

        public bool isMatching(char letter, int row, int col)
        {
            return _lettersGrid[row, col] == letter;
        }

        public Board AddWord(string word, StartingPosition position)
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

        public Board BlatWord(string word)
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
                    if (isEmpty(row,col))
                    {
                        newGrid[row, col] = word[i];
                        i++;
                    }
                    if (i >= word.Length) break;
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
