using MathNet.Numerics.LinearAlgebra;

namespace KelsonBall.Transforms
{
    public interface ITransformable
    {
        void PushMatrix(Matrix<double> matrix);
        void PopMatrix(Matrix<double> matrix);
    }
}
