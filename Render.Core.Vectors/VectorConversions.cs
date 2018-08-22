using MathNet.Numerics.LinearAlgebra.Double;
using v = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace Render.Core.Vectors
{
    public static class VectorConversions
    {
        public static v GetMathVector(params double[] values) => DenseVector.OfArray(values);

        public static v ToMathVector(this Vector3 vector) => GetMathVector(vector.X, vector.Y, vector.Z);

        public static v ToMathVector(this Rektor vector) => GetMathVector(vector.X, vector.Y);

        public static Vector3 ToRVector3(this v source) => new Vector3((float)source[0], (float)source[1], (float)source[2]);

        public static Rektor ToRVector(this v source) => new Rektor((float)source[0], (float)source[1]);
    }
}
