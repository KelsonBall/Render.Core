using MathNet.Numerics.LinearAlgebra;
using OpenTK.Graphics;
using Render.Core.Textures;
using Render.Core.Vectors;

namespace Render.Core.Rendering
{
    public interface IRenderer2d
    {
        void Background(Color4 color);
        void Triangle(Rektor a, Rektor b, Rektor c);
        void Rectangle(Rektor position, Rektor size);
        void Quad(Rektor a, Rektor b, Rektor c, Rektor d);
        void Ellipse(Rektor position, Rektor size);
        void Line(Rektor a, Rektor b);
        void Image(Texture image, Rektor position);
        TextRenderResult Text(string text, Rektor position);
        void Shape(Rektor position, params Rektor[] points);

        void MultMatrix(Matrix<double> matrix);
    }
}
