using System.Collections.Generic;

namespace Render.Core.Extensions
{
    public class FontExtensions
    {
        
    }

    public static class AlphabetProvider
    {
        public static IEnumerable<char> Enumerate()
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                yield return c;
            }
        }
    }
}
