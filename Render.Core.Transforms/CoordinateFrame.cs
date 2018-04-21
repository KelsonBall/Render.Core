using Render.Core.Vectors;

namespace KelsonBall.Transforms
{
    /// <summary>
    /// A transform implementation providing full 4x4 afine transformation matricies for linear transformations in R3 space
    /// </summary>
    public class CoordinateFrame : Transform<rVector3>
    {
        public static CoordinateFrame Unit =>
            new CoordinateFrame
            {
                Scale = rVector3.one,
                Translation = rVector3.O,
                Rotation = rVector3.O
            };

        public CoordinateFrame() : base(4)
        {
        }

        protected void Calculate()
        {
            var stack = Transform3.NewTransformStack();
            stack.Push(translationTransform);
            stack.Push(rotationTransform);
            stack.Push(scaleTransform);
            Matrix = stack.Aggregate.Matrix;
        }

        public rVector3 Forward => ApplyTo(rVector3.î) - Position;
        public rVector3 Left => ApplyTo(rVector3.ĵ) - Position;
        public rVector3 Up => ApplyTo(rVector3.k̂) - Position;

        private Transform<rVector3> scaleTransform = Transform3.Scale(1);
        private rVector3 scale;
        public rVector3 Scale
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
            Scale = new rVector3(Scale.X * s, Scale.Y * s, Scale.Z * s);
            return this;
        }

        public CoordinateFrame ScaleBy(double x, double y, double z)
        {
            return ScaleBy(new rVector3((float)x, (float)y, (float)z));
        }

        public CoordinateFrame ScaleBy(rVector3 s)
        {
            Scale = new rVector3(Scale.X * s.X, Scale.Y * s.Y, Scale.Z * s.Z);
            return this;
        }

        private Transform<rVector3> rotationTransform = Transform3.Rotation(0, 1, 0, 0);
        private rVector3 rotation;
        public rVector3 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                var stack = Transform3.NewTransformStack();
                stack.Push(Transform3.Rotation(value.Z, rVector3.k̂));
                stack.Push(Transform3.Rotation(value.Y, rVector3.î));
                stack.Push(Transform3.Rotation(value.X, rVector3.ĵ));
                rotationTransform = stack.Aggregate;
                Calculate();
            }
        }

        public CoordinateFrame RotateBy(rVector3 Θ)
        {
            Rotation += Θ;
            return this;
        }

        public CoordinateFrame RotateBy(double x, double y, double z)
        {
            Rotation += (new rVector3((float)x, (float)y, (float)z));
            return this;
        }

        private Transform<rVector3> translationTransform = Transform3.Translation(0, 0, 0);
        private rVector3 translation;
        public rVector3 Translation
        {
            get => translation;
            set
            {
                translation = value;
                translationTransform = Transform3.Translation(value);
                Calculate();
            }
        }

        public CoordinateFrame TranslateBy(rVector3 translation)
        {
            Translation += translation;
            return this;
        }

        public CoordinateFrame TranslateBy(double x, double y, double z)
        {
            return TranslateBy(new rVector3((float)x, (float)y, (float)z));
        }

        public rVector3 Position => ApplyTo(rVector3.O);

        public override rVector3 ApplyTo(rVector3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (transform * affineVector).ToPVector3();
        }

        public override rVector3 ApplyInverse(rVector3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (Inverse * affineVector).ToPVector3();
        }
    }
}
