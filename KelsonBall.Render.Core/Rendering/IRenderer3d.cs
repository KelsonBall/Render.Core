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
        IRenderer3d Triangle(Vector3 a, Vector3 b, Vector3 c);
        IRenderer3d Box(Vector3 position, Vector3 size);
        IRenderer3d Quad(Vector3 a, Vector3 b, Vector3 c, Vector3 d);
        IRenderer3d Ellipsoid(Vector3 position, Vector3 size);
        IRenderer3d Line(Vector3 a, Vector3 b);
        IRenderer3d Image(Texture image, Vector3 position, Vector3 normal);
        IRenderer3d Text(string text, Vector3 position, Vector3 normal);
        IRenderer3d Shape(Vector3 position, params Vector3[] points);

        IRenderer3d Model(Vector3 position, Model model, TextureMap map);
        IRenderer3d Model(Vector3 position, Model model, Color4 color);
        IRenderer3d Model(Vector3 position, Model model, Color4[] vertexColors);
    }
}
