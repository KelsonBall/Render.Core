using Render.Core.Vectors;

namespace Render.Core.Textures
{
    public class TextureMap
    {
        public readonly Texture Texture;
        public readonly Vector3[] UvVerticies;

        public TextureMap(Texture texture, Vector3[] uvVerticies)
        {
            Texture = texture;
            UvVerticies = uvVerticies;
        }
    }
}
