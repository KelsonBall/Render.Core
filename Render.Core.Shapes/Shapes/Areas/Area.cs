﻿using Render.Core.Transforms;
using Render.Core.Vectors;

namespace KelsonBall.Geometry.Areas
{
    public abstract class Area : IRegion<rVector>, IComposable<Area>, ITransformable<Area, rVector>
    {
        public abstract bool Contains(rVector point);

        public virtual Area And(Area region)
        {
            return new CompositeArea(this).And(region);
        }

        public virtual Area Not(Area region)
        {
            return new CompositeArea(this).Not(region);
        }

        public virtual Area Or(Area region)
        {
            return new CompositeArea(this).Or(region);
        }

        public virtual Area XOr(Area region)
        {
            return new CompositeArea(this).XOr(region);
        }

        public virtual Area Transform(Transform<rVector> transform)
        {
            var area = new TransformArea(this);
            area.Transform(transform);
            return area;
        }

    }
}
