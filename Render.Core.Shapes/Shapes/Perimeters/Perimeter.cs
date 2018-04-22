using Render.Core.Transforms;
using Render.Core.Vectors;
using System;
using System.Collections.Generic;

namespace KelsonBall.Geometry
{
    public abstract class Perimeter : IBorder<Rektor>, ITransformable<Perimeter, Rektor>, IComposable<Perimeter>
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

        public virtual Perimeter Transform(Transform<Rektor> transform)
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable<Rektor> Intersections(Ray<Rektor> ray);

        public Rektor[] VertexArray()
        {
            throw new NotImplementedException();
        }
    }
}
