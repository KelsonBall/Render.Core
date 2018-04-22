using Render.Core.BadPuns;
using Render.Core.Vectors;

namespace Render.Core.Transforms
{
    /// <summary>
    /// A transform implementation providing full 4x4 afine transformation matricies for 2D transforms in the X-Z plane
    /// </summary>
    public class OrthoFrame : Transform<rVector3>
    {
        public static OrthoFrame Unit =>
            new OrthoFrame
            {
                Scale = rVector.one,
                Translation = rVector.O,
                Rotation = 0
            };

        public OrthoFrame() : base(4)
        {
        }

        public OrthoFrame(double[,] origin) : base(origin)
        {
            if (origin.GetLength(0) != 4 || origin.GetLength(1) != 4)
                throw a.fit();
            Calculate();
        }

        protected void Calculate()
        {
            var stack = Transform3.NewTransformStack();
            stack.Push(translationTransform);
            stack.Push(rotationTransform);
            stack.Push(scaleTransform);
            Matrix = stack.Aggregate.Matrix;
        }

        public rVector Forward => ApplyTo(rVector.î) - Position;
        public rVector Left => ApplyTo(rVector.ĵ) - Position;

        private Transform<rVector3> scaleTransform = Transform3.Scale(1);
        private rVector3 scale;
        public rVector Scale
        {
            get => scale.ToVector();
            set
            {
                scale = value.ToVector3();
                scaleTransform = Transform3.Scale(new rVector3(value.X, value.Y, 1));
                Calculate();
            }
        }

        public OrthoFrame ScaleBy(double scale)
        {
            Scale *= (float)scale;
            return this;
        }

        public OrthoFrame ScaleBy(double x, double y)
        {
            return ScaleBy(new rVector((float)x, (float)y));
        }

        public OrthoFrame ScaleBy(rVector s)
        {
            Scale = new rVector3(Scale.X * s.X, 1, Scale.Y * s.Y).ToVector();
            return this;
        }

        private Transform<rVector3> rotationTransform = Transform3.Rotation(0, 1, 0, 0);
        private rVector3 rotation;
        public double Rotation
        {
            get => rotation.Y;
            set
            {
                rotation = new rVector3(0, value, 0);
                var stack = Transform3.NewTransformStack();
                stack.Push(Transform3.Rotation(value, rVector3.k̂));
                rotationTransform = stack.Aggregate;
                Calculate();
            }
        }

        public OrthoFrame RotateBy(double Θ)
        {
            Rotation += Θ;
            return this;
        }

        private Transform<rVector3> translationTransform = Transform3.Translation(0, 0, 0);
        private rVector3 translation;
        public rVector Translation
        {
            get => translation.ToVector();
            set
            {
                translation = value.ToVector3();
                translationTransform = Transform3.Translation(value.ToVector3());
                Calculate();
            }
        }

        public OrthoFrame TranslateBy(rVector translation)
        {
            Translation += translation;
            return this;
        }

        public OrthoFrame TranslateBy(double x, double y)
        {
            return TranslateBy(new rVector(x, y));
        }

        public rVector Position => ApplyTo(rVector.O);


        public rVector ApplyTo(rVector v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, 0, 1);
            return ((transform * affineVector).ToRVector3()).ToVector();
        }

        public rVector ApplyInverse(rVector v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, 0, 1);
            return (Inverse * affineVector).ToRVector3().ToVector();
        }

        public override rVector3 ApplyTo(rVector3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (transform * affineVector).ToRVector3();
        }

        public override rVector3 ApplyInverse(rVector3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (Inverse * affineVector).ToRVector3();
        }

    }
}
