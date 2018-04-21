using System;
using System.Collections.Generic;

namespace Render.Core.Extensions
{
    public static class IndexableExtensions
    {
        public static IEnumerable<TOut> Indecies<TIn, TOut>(this TIn[] source, Func<int, TOut> transform)
        {
            for (int i = 0; i < source.Length; i++)
            {
                yield return transform(i);
            }
        }

        public static T Previous<T>(this T[] of, int i) => of[(i == 0) ? of.Length - 1 : i - 1];

        public static T Next<T>(this T[] of, int i) => of[(i + 1) % of.Length];
    }
}
