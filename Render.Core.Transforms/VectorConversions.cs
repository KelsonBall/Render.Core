using MathNet.Numerics.LinearAlgebra.Double;
using v = MathNet.Numerics.LinearAlgebra.Vector<double>;
using Render.Core.Vectors;

namespace KelsonBall.Transforms
{
    public static class VectorConversions
    {
        public static v GetMathVector(params double[] values) => DenseVector.OfArray(values);

        public static v ToMathVector(this rVector3 vector) => GetMathVector(vector.X, vector.Y, vector.Z);

        public static v ToMathVector(this rVector vector) => GetMathVector(vector.X, vector.Y);

        public static rVector3 ToRVector3(this v source) => new rVector3((float)source[0], (float)source[1], (float)source[2]);

        public static rVector ToRVector(this v source) => new rVector((float)source[0], (float)source[1]);
    }
}
