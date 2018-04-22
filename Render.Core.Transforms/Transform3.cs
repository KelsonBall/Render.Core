//using KelsonBall.Vectors;
using Render.Core.Vectors;
using static System.Math;

namespace Render.Core.Transforms
{
    //return new Transform3(new double[,]{
    //            { 1, 0, 0, 0 },
    //            { 0, 1, 0, 0 },
    //            { 0, 0, 1, 0 },
    //            { 0, 0, 0, 1 },
    //        });

    public class Transform3 : Transform<Rektor3>
    {
        public static TransformStack<Rektor3> NewTransformStack() => new TransformStack<Rektor3>(new Transform3());

        public override Rektor3 ApplyTo(Rektor3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (transform * affineVector);
        }

        public override Rektor3 ApplyInverse(Rektor3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (inverse * affineVector);
        }

        private Transform3() : base(4) { }

        private Transform3(double[,] matrix) : base(matrix) { }

        public static Transform3 Translation(double x, double y, double z)
        => new Transform3(new double[,]{
                    { 1, 0, 0, x },
                    { 0, 1, 0, y },
                    { 0, 0, 1, z },
                    { 0, 0, 0, 1 },
            });

        public static Transform3 Translation(Rektor3 v) => Translation(v.X, v.Y, v.Z);

        public static Transform3 Rotation(double Θ, double l, double m, double n)
        {
            double cos = Cos(Θ);
            double icos = 1 - cos;

            double sin = Sin(Θ);

            return new Transform3(new double[,]{
                    { l * l * icos + 1 * cos, m * l * icos - n * sin, n * l * icos + m * sin, 0 },
                    { l * m * icos + n * sin, m * m * icos + 1 * cos, n * m * icos - l * sin, 0 },
                    { l * n * icos - m * sin, m * n * icos + l * sin, n * n * icos + 1 * cos, 0 },
                    { 0                     ,                      0,                      0, 1 },
                });
        }

        public static Transform3 Rotation(double Θ, Rektor3 axis) => Rotation(Θ, axis.X, axis.Y, axis.Z);

        public static Transform3 Scale(double s)
        => new Transform3(new double[,]{
                    { s, 0, 0, 0 },
                    { 0, s, 0, 0 },
                    { 0, 0, s, 0 },
                    { 0, 0, 0, 1 },
            });


        public static Transform3 Scale(double x, double y, double z)
        => new Transform3(new double[,]{
                    { x, 0, 0, 0 },
                    { 0, y, 0, 0 },
                    { 0, 0, z, 0 },
                    { 0, 0, 0, 1 },
            });

        public static Transform3 Scale(Rektor3 v) => Scale(v.X, v.Y, v.Z);
    }

    public static class Transform3StackOperations
    {
        public static TransformStack<Rektor3> Translate(TransformStack<Rektor3> stack, Rektor3 offset)
        {
            stack.Push(Transform3.Translation(offset));
            return stack;
        }

        public static TransformStack<Rektor3> Rotate(TransformStack<Rektor3> stack, double angle, Rektor3 axis)
        {
            stack.Push(Transform3.Rotation(angle, axis));
            return stack;
        }

        public static TransformStack<Rektor3> Scale(TransformStack<Rektor3> stack, double scale)
        {
            stack.Push(Transform3.Scale(scale));
            return stack;
        }

        public static TransformStack<Rektor3> Scale(TransformStack<Rektor3> stack, Rektor3 scale)
        {
            stack.Push(Transform3.Scale(scale));
            return stack;
        }
    }
}
