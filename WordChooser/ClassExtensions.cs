using System;

namespace WordChooser
{
    static public class ClassExtensions
    {
        public static bool IsContainingOnlyLetters(this string target)
        {
            var normalisedTarget = target.ToUpperInvariant().ToCharArray();
            foreach (char letter in target.ToUpperInvariant().ToCharArray())
            {
                if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(letter))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
