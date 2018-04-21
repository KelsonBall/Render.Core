using KelsonBall.Transforms;
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

    public class Ray2 : Ray<rVector>
    {
        public Ray2(rVector o, rVector d) : base(o, default(rVector) /* d.Unit() */) { throw new NotImplementedException(); }

        public override Ray<rVector> ApplyTransform(Transform<rVector> transform)
        {
            return new Ray2(transform.ApplyTo(Origin), transform.ApplyTo(Direction));
        }

        public override Ray<rVector> ApplyInverse(Transform<rVector> transform)
        {
            return new Ray2(transform.ApplyInverse(Origin), transform.ApplyInverse(Direction));
        }
    }

    public class Ray3 : Ray<rVector3>
    {
        public Ray3(rVector3 o, rVector3 d) : base(o, default(rVector3) /* d.Unit() */) { }

        public override Ray<rVector3> ApplyTransform(Transform<rVector3> transform)
        {
            return new Ray3(transform.ApplyTo(Origin), transform.ApplyTo(Direction));
        }

        public override Ray<rVector3> ApplyInverse(Transform<rVector3> transform)
        {
            return new Ray3(transform.ApplyInverse(Origin), transform.ApplyInverse(Direction));
        }
    }
}
