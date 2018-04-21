﻿//using KelsonBall.Vectors;
using Render.Core.Vectors;
using static System.Math;

namespace Render.Core.Transforms
{

    public class Transform2 : Transform<rVector>
    {
        public static TransformStack<rVector> NewTransformStack() => new TransformStack<rVector>(new Transform2());

        private Transform2() : base(3)
        {

        }

        private Transform2(double[,] matrix) : base(matrix)
        {
        }

        public override rVector ApplyTo(rVector v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, 1);
            return ((rVector3)(transform * affineVector)).ToVector();
        }

        public override rVector ApplyInverse(rVector v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, 1);
            return ((rVector3)(inverse * affineVector)).ToVector();
        }

        internal static Transform2 Translation(double x, double y)
        => new Transform2(new double[,]{
                { 1, 0, x },
                { 0, 1, y },
                { 0, 0, 1 }
            });

        internal static Transform2 Rotation(double Θ)
        {
            double c = Cos(Θ);
            double s = Sin(Θ);
            return new Transform2(new double[,]{
                { c, -s, 0 },
                { s,  c, 0 },
                { 0,  0, 1 }
            });
        }

        internal static Transform2 Scale(double s)
        => new Transform2(new double[,]{
                { s, 0, 0 },
                { 0, s, 0 },
                { 0, 0, 1 }
            });

        internal static Transform2 Scale(double x, double y)
        => new Transform2(new double[,]{
                { x, 0, 0 },
                { 0, y, 0 },
                { 0, 0, 1 }
            });
    }

    public static class Transform2StackOperations
    {
        public static TransformStack<rVector> Translate(TransformStack<rVector> stack, rVector v)
        {
            stack.Push(Transform2.Translation(v.X, v.Y));
            return stack;
        }

        public static TransformStack<rVector> Rotate(TransformStack<rVector> stack, double angle)
        {
            stack.Push(Transform2.Rotation(angle));
            return stack;
        }

        public static TransformStack<rVector> Scale(TransformStack<rVector> stack, double s)
        {
            stack.Push(Transform2.Scale(s));
            return stack;
        }

        public static TransformStack<rVector> Scale(TransformStack<rVector> stack, rVector v)
        {
            stack.Push(Transform2.Scale(v.X, v.Y));
            return stack;
        }
    }
}
