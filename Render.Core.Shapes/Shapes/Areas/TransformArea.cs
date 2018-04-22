using Render.Core.Transforms;
using Render.Core.Vectors;

namespace KelsonBall.Geometry.Areas
{
    public class TransformArea : Area
    {
        public readonly Area Root;

        private readonly TransformStack<Rektor> transformStack = Transform2.NewTransformStack();

        internal TransformArea(Area root)
        {
            Root = root;
        }

        public override bool Contains(Rektor point) => Root.Contains(transformStack.Aggregate.ApplyInverse(point));

        public override Area Transform(Transform<Rektor> transform)
        {
            transformStack.Push(transform);
            return this;
        }
    }
}
