using Render.Core.Vectors;

namespace Render.Core.Textures
{
    public class TextureMap
    {
        public readonly Texture Texture;
        public readonly Rektor3[] UvVerticies;

        public TextureMap(Texture texture, Rektor3[] uvVerticies)
        {
            Texture = texture;
            UvVerticies = uvVerticies;
        }
    }
}
