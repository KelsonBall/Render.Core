using Render.Core.Textures;
using Render.Core.Extensions;

namespace KelsonBall.Render.Core.Extensions
{
    public static class ImagingExtensions
    {
        public static RGBA[,] ToRgbaArray(this int[] data, int width, int height)
        {
            var result = new RGBA[width, height];
            for (int i = 0; i < data.Length; i++)
                result[i % width, i / width] = new RGBA(data[i].Depack(3), data[i].Depack(2), data[i].Depack(1), data[i].Depack(0));
            return result;
        }
    }
}
