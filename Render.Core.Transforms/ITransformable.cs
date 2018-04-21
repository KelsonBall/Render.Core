using MathNet.Numerics.LinearAlgebra;

namespace Render.Core.Transforms
{
    public interface ITransformable
    {
        void PushMatrix(Matrix<double> matrix);
        void PopMatrix(Matrix<double> matrix);
    }
}
