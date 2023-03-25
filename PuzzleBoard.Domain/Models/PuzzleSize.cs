using System;
using PuzzleBoard.Domain.Interfaces;
#nullable enable

namespace PuzzleBoard.Domain.Models;

public class PuzzleSize : IPuzzleSize
{
    readonly int _value;

    public PuzzleSize(int value)
    {
        _value = value;
    }

    public int Max() => _value;
}