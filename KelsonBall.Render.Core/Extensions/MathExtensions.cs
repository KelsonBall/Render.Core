using System;
using System.Collections.Generic;

namespace Render.Core.Extensions
{
    public static class MathExtensions
    {
        public static IEnumerable<double> Lerp(this double start, double to, double? step = null, double? count = null)
        {
            if (step.HasValue && count.HasValue)
                throw new Exception("Do not define both step and count");
            if (!step.HasValue)
                step = (to - start) / count.Value;
            while (start < to)
                yield return (start += step.Value);
            yield return to;
        }

        public static IEnumerable<int> Lerp(this int start, int to, int? step = null, int? count = null)
        {
            if (step.HasValue && count.HasValue)
                throw new Exception("Do not define both step and count");
            if (!step.HasValue)
                step = (to - start) / count.Value;
            while (start < to)
                yield return (start += step.Value);
            yield return to;
        }
    }
}
