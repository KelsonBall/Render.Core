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
        IRenderer3d Triangle(rVector3 a, rVector3 b, rVector3 c);
        IRenderer3d Box(rVector3 position, rVector3 size);
        IRenderer3d Quad(rVector3 a, rVector3 b, rVector3 c, rVector3 d);
        IRenderer3d Ellipsoid(rVector3 position, rVector3 size);
        IRenderer3d Line(rVector3 a, rVector3 b);
        IRenderer3d Image(rImage image, rVector3 position, rVector3 normal);
        IRenderer3d Text(string text, rVector3 position, rVector3 normal);
        IRenderer3d Shape(rVector3 position, params rVector3[] points);

        IRenderer3d Model(rVector3 position, Model model, TextureMap map);
        IRenderer3d Model(rVector3 position, Model model, Color4 color);
        IRenderer3d Model(rVector3 position, Model model, Color4[] vertexColors);

    }
}
