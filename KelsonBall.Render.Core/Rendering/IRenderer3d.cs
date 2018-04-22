using OpenTK.Graphics;
using Render.Core.Textures;
using Render.Core.Vectors;

namespace Render.Core.Rendering
{
    public interface IRenderer3d
    {
        IRenderer3d BeginDraw();
        IRenderer3d EndDraw();

        IRenderer3d Background(Color4 color);
        IRenderer3d Triangle(Rektor3 a, Rektor3 b, Rektor3 c);
        IRenderer3d Box(Rektor3 position, Rektor3 size);
        IRenderer3d Quad(Rektor3 a, Rektor3 b, Rektor3 c, Rektor3 d);
        IRenderer3d Ellipsoid(Rektor3 position, Rektor3 size);
        IRenderer3d Line(Rektor3 a, Rektor3 b);
        IRenderer3d Image(Texture image, Rektor3 position, Rektor3 normal);
        IRenderer3d Text(string text, Rektor3 position, Rektor3 normal);
        IRenderer3d Shape(Rektor3 position, params Rektor3[] points);

        IRenderer3d Model(Rektor3 position, Model model, TextureMap map);
        IRenderer3d Model(Rektor3 position, Model model, Color4 color);
        IRenderer3d Model(Rektor3 position, Model model, Color4[] vertexColors);

    }
}
