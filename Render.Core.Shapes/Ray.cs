using Render.Core.Transforms;
using Render.Core.Vectors;
using System;

namespace KelsonBall.Geometry
{
    public abstract class Ray<T>
    {
        public readonly T Origin;
        public readonly T Direction;

        protected Ray(T origin, T direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public abstract Ray<T> ApplyTransform(Transform<T> transform);

        public abstract Ray<T> ApplyInverse(Transform<T> transform);
    }

    public class Ray2 : Ray<Rektor>
    {
        public Ray2(Rektor o, Rektor d) : base(o, default(Rektor) /* d.Unit() */) { throw new NotImplementedException(); }

        public override Ray<Rektor> ApplyTransform(Transform<Rektor> transform)
        {
            return new Ray2(transform.ApplyTo(Origin), transform.ApplyTo(Direction));
        }

        public override Ray<Rektor> ApplyInverse(Transform<Rektor> transform)
        {
            return new Ray2(transform.ApplyInverse(Origin), transform.ApplyInverse(Direction));
        }
    }

    public class Ray3 : Ray<Vector3>
    {
        public Ray3(Vector3 o, Vector3 d) : base(o, default(Vector3) /* d.Unit() */) { }

        public override Ray<Vector3> ApplyTransform(Transform<Vector3> transform)
        {
            return new Ray3(transform.ApplyTo(Origin), transform.ApplyTo(Direction));
        }

        public override Ray<Vector3> ApplyInverse(Transform<Vector3> transform)
        {
            return new Ray3(transform.ApplyInverse(Origin), transform.ApplyInverse(Direction));
        }
    }
}
