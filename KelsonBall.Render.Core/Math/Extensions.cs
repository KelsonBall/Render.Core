namespace KelsonBall.Render.Core.Math
{
    public static class Extensions
    {
        public static double ScaledAlong(this double value, double start = 0, double end = 1)
            => (value - start) / (end - start);
    }
}
