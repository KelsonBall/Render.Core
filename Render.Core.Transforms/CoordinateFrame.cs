using Render.Core.Vectors;

namespace Render.Core.Transforms
{
    /// <summary>
    /// A transform implementation providing full 4x4 afine transformation matricies for linear transformations in R3 space
    /// </summary>
    public class CoordinateFrame : Transform<Vector3>
    {
        public static CoordinateFrame Unit =>
            new CoordinateFrame
            {
                Scale = Vector3.one,
                Translation = Vector3.O,
                Rotation = Vector3.O
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

        public Vector3 Forward => ApplyTo(Vector3.î) - Position;
        public Vector3 Left => ApplyTo(Vector3.ĵ) - Position;
        public Vector3 Up => ApplyTo(Vector3.k̂) - Position;

        private Transform<Vector3> scaleTransform = Transform3.Scale(1);
        private Vector3 scale;
        public Vector3 Scale
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
            Scale = new Vector3(Scale.X * s, Scale.Y * s, Scale.Z * s);
            return this;
        }

        public CoordinateFrame ScaleBy(double x, double y, double z)
        {
            return ScaleBy(new Vector3((float)x, (float)y, (float)z));
        }

        public CoordinateFrame ScaleBy(Vector3 s)
        {
            Scale = new Vector3(Scale.X * s.X, Scale.Y * s.Y, Scale.Z * s.Z);
            return this;
        }

        private Transform<Vector3> rotationTransform = Transform3.Rotation(0, 1, 0, 0);
        private Vector3 rotation;
        public Vector3 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                var stack = Transform3.NewTransformStack();
                stack.Push(Transform3.Rotation(value.Z, Vector3.k̂));
                stack.Push(Transform3.Rotation(value.Y, Vector3.î));
                stack.Push(Transform3.Rotation(value.X, Vector3.ĵ));
                rotationTransform = stack.Aggregate;
                Calculate();
            }
        }

        public CoordinateFrame RotateBy(Vector3 Θ)
        {
            Rotation += Θ;
            return this;
        }

        public CoordinateFrame RotateBy(double x, double y, double z)
        {
            Rotation += (new Vector3((float)x, (float)y, (float)z));
            return this;
        }

        private Transform<Vector3> translationTransform = Transform3.Translation(0, 0, 0);
        private Vector3 translation;
        public Vector3 Translation
        {
            get => translation;
            set
            {
                translation = value;
                translationTransform = Transform3.Translation(value);
                Calculate();
            }
        }

        public CoordinateFrame TranslateBy(Vector3 translation)
        {
            Translation += translation;
            return this;
        }

        public CoordinateFrame TranslateBy(double x, double y, double z)
        {
            return TranslateBy(new Vector3((float)x, (float)y, (float)z));
        }

        public Vector3 Position => ApplyTo(Vector3.O);

        public override Vector3 ApplyTo(Vector3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (transform * affineVector);
        }

        public override Vector3 ApplyInverse(Vector3 v)
        {
            var affineVector = VectorConversions.GetMathVector(v.X, v.Y, v.Z, 1);
            return (Inverse * affineVector);
        }
    }
}
