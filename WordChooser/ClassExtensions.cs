#nullable enable

namespace WordChooser
{
    static public class ClassExtensions
    {
        public static bool IsContainingOnlyLetters(this string target)
        {
            var normalisedTarget = target.ToUpperInvariant().ToCharArray();            
            foreach (char letter in normalisedTarget)
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
