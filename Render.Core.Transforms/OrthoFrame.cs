using Render.Core.BadPuns;
using Render.Core.Vectors;

namespace Render.Core.Transforms
{
    /// <summary>
    /// A transform implementation providing full 4x4 afine transformation matricies for 2D transforms in the X-Z plane
    /// </summary>
    public class OrthoFrame : Transform<Rektor3>
    {
        public static OrthoFrame Unit =>
            new OrthoFrame
            {
                Scale = Rektor.one,
                Translation = Rektor.O,
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

        public Rektor Forward => ApplyTo(Rektor.î) - Position;
        public Rektor Left => ApplyTo(Rektor.ĵ) - Position;

        private Transform<Rektor3> scaleTransform = Transform3.Scale(1);
        private Rektor3 scale;
        public Rektor Scale
        {
            get => scale.ToVector();
            set
            {
                scale = value.ToVector3();
                scaleTransform = Transform3.Scale(new Rektor3(value.X, value.Y, 1));
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
            return ScaleBy(new Rektor((float)x, (float)y));
        }

        public OrthoFrame ScaleBy(Rektor s)
        {
            Scale = new Rektor3(Scale.X * s.X, 1, Scale.Y * s.Y).ToVector();
            return this;
        }

        private Transform<Rektor3> rotationTransform = Transform3.Rotation(0, 1, 0, 0);
        private Rektor3 rotation;
        public double Rotation
        {
            get => rotation.Y;
            set
            {
                rotation = new Rektor3(0, value, 0);
                var stack = Transform3.NewTransformStack();
                stack.Push(Transform3.Rotation(value, Rektor3.k̂));
                rotationTransform = stack.Aggregate;
                Calculate();
            }
        }

        public OrthoFrame RotateBy(double Θ)
        {
            Rotation += Θ;
            return this;
        }

        private Transform<Rektor3> translationTransform = Transform3.Translation(0, 0, 0);
        private Rektor3 translation;
        public Rektor Translation
        {
            get => translation.ToVector();
            set
            {
                translation = value.ToVector3();
                translationTransform = Transform3.Translation(value.ToVector3());
                Calculate();
            }
        }

        public OrthoFrame TranslateBy(Rektor translation)
        {
            Translation += translation;
            return this;
        }

        public OrthoFrame TranslateBy(double x, double y)
        {
            return TranslateBy(new Rektor(x, y));
        }

        public Rektor Position => ApplyTo(Rektor.O);


        public Rektor ApplyTo(Rektor v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, 0, 1);
            return ((transform * affineVector).ToRVector3()).ToVector();
        }

        public Rektor ApplyInverse(Rektor v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, 0, 1);
            return (Inverse * affineVector).ToRVector3().ToVector();
        }

        public override Rektor3 ApplyTo(Rektor3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (transform * affineVector).ToRVector3();
        }

        public override Rektor3 ApplyInverse(Rektor3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (Inverse * affineVector).ToRVector3();
        }

    }
}
