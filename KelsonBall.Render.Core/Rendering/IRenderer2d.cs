using MathNet.Numerics.LinearAlgebra;
using OpenTK.Graphics;
using Render.Core.Textures;
using Render.Core.Vectors;

namespace Render.Core.Rendering
{
    public interface IRenderer2d
    {
        void Background(Color4 color);
        void Triangle(rVector a, rVector b, rVector c);
        void Rectangle(rVector position, rVector size);
        void Quad(rVector a, rVector b, rVector c, rVector d);
        void Ellipse(rVector position, rVector size);
        void Line(rVector a, rVector b);
        void Image(rImage image, rVector position);
        TextRenderResult Text(string text, rVector position);
        void Shape(rVector position, params rVector[] points);

        void MultMatrix(Matrix<double> matrix);
    }
}
