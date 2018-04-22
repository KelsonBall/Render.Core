using Render.Core.Vectors;

namespace Render.Core.Transforms
{
    /// <summary>
    /// A transform implementation providing full 4x4 afine transformation matricies for linear transformations in R3 space
    /// </summary>
    public class CoordinateFrame : Transform<Rektor3>
    {
        public static CoordinateFrame Unit =>
            new CoordinateFrame
            {
                Scale = Rektor3.one,
                Translation = Rektor3.O,
                Rotation = Rektor3.O
            };

        public CoordinateFrame() : base(4)
        {
        }

        protected void Calculate()
        {
            var stack = Transform3.NewTransformStack();
            stack.Push(rotationTransform);
            stack.Push(translationTransform);
            stack.Push(scaleTransform);
            Matrix = stack.Aggregate.Matrix;
        }

        public Rektor3 Forward => ApplyTo(Rektor3.î) - Position;
        public Rektor3 Left => ApplyTo(Rektor3.ĵ) - Position;
        public Rektor3 Up => ApplyTo(Rektor3.k̂) - Position;

        private Transform<Rektor3> scaleTransform = Transform3.Scale(1);
        private Rektor3 scale;
        public Rektor3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                scaleTransform = Transform3.Scale(value);
                Calculate();
            }
        }

        public CoordinateFrame ScaleBy(double scale)
        {
            float s = (float)scale;
            Scale = new Rektor3(Scale.X * s, Scale.Y * s, Scale.Z * s);
            return this;
        }

        public CoordinateFrame ScaleBy(double x, double y, double z)
        {
            return ScaleBy(new Rektor3((float)x, (float)y, (float)z));
        }

        public CoordinateFrame ScaleBy(Rektor3 s)
        {
            Scale = new Rektor3(Scale.X * s.X, Scale.Y * s.Y, Scale.Z * s.Z);
            return this;
        }

        private Transform<Rektor3> rotationTransform = Transform3.Rotation(0, 1, 0, 0);
        private Rektor3 rotation;
        public Rektor3 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                var stack = Transform3.NewTransformStack();
                stack.Push(Transform3.Rotation(value.Z, Rektor3.k̂));
                stack.Push(Transform3.Rotation(value.Y, Rektor3.î));
                stack.Push(Transform3.Rotation(value.X, Rektor3.ĵ));
                rotationTransform = stack.Aggregate;
                Calculate();
            }
        }

        public CoordinateFrame RotateBy(Rektor3 Θ)
        {
            Rotation += Θ;
            return this;
        }

        public CoordinateFrame RotateBy(double x, double y, double z)
        {
            Rotation += (new Rektor3((float)x, (float)y, (float)z));
            return this;
        }

        private Transform<Rektor3> translationTransform = Transform3.Translation(0, 0, 0);
        private Rektor3 translation;
        public Rektor3 Translation
        {
            get => translation;
            set
            {
                translation = value;
                translationTransform = Transform3.Translation(value);
                Calculate();
            }
        }

        public CoordinateFrame TranslateBy(Rektor3 translation)
        {
            Translation += translation;
            return this;
        }

        public CoordinateFrame TranslateBy(double x, double y, double z)
        {
            return TranslateBy(new Rektor3((float)x, (float)y, (float)z));
        }

        public Rektor3 Position => ApplyTo(Rektor3.O);

        public override Rektor3 ApplyTo(Rektor3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (transform * affineVector);
        }

        public override Rektor3 ApplyInverse(Rektor3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (Inverse * affineVector);
        }
    }
}
