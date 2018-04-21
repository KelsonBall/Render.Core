using KelsonBall.Transforms;
using Render.Core.Vectors;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace KelsonBall.Geometry
{
    public abstract class Perimeter : IBorder<rVector>, ITransformable<Perimeter, rVector>, IComposable<Perimeter>
    {

        public Perimeter And(Perimeter region)
        {
            throw new NotImplementedException();
        }

        public Perimeter Not(Perimeter region)
        {
            throw new NotImplementedException();
        }

        public Perimeter Or(Perimeter region)
        {
            throw new NotImplementedException();
        }

        public Perimeter XOr(Perimeter region)
        {
            throw new NotImplementedException();
        }

        public virtual Perimeter Transform(Transform<rVector> transform)
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable<rVector> Intersections(Ray<rVector> ray);

        public rVector[] VertexArray()
        {
            throw new NotImplementedException();
        }
    }
}
