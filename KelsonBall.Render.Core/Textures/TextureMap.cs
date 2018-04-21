using Render.Core.Vectors;

namespace Render.Core.Textures
{
    public class TextureMap
    {
        public readonly rImage Texture;
        public readonly rVector3[] UvVerticies;

        public TextureMap(rImage texture, rVector3[] uvVerticies)
        {
            Texture = texture;
            UvVerticies = uvVerticies;
        }
    }
}
