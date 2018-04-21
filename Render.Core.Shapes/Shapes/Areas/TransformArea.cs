using Render.Core.Transforms;
using System.Numerics;

namespace KelsonBall.Geometry.Areas
{
    public class TransformArea : Area
    {
        public readonly Area Root;

        private readonly TransformStack<rVector> transformStack = Transform2.NewTransformStack();

        internal TransformArea(Area root)
        {
            Root = root;
        }

        public override bool Contains(rVector point) => Root.Contains(transformStack.Aggregate.ApplyInverse(point));

        public override Area Transform(Transform<rVector> transform)
        {
            transformStack.Push(transform);
            return this;
        }
    }
}
