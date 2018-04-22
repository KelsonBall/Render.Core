using KelsonBall.Geometry.Areas;
using Render.Core.Transforms;
using Render.Core.Vectors;
using System;
using System.Collections.Generic;

namespace KelsonBall.Geometry.Shapes
{
    public abstract class Shape : IBorder<Rektor>, IRegion<Rektor>, IComposable<Shape>, ITransformable<Shape, Rektor>
    {
        private readonly TransformStack<Rektor> transformStack = Transform2.NewTransformStack();

        public Area Area { get; private set; }
        public Perimeter Perimeter { get; private set; }

        public Shape(Area area, Perimeter perimeter)
        {
            Area = area;
            Perimeter = perimeter;
        }

        public Shape And(Shape region)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Rektor point)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rektor> Intersections(Ray<Rektor> ray)
        {
            foreach (var intersect in Perimeter.Intersections(ray))
                if (Area.Contains(intersect))
                    yield return intersect;
        }

        public Shape Not(Shape region)
        {
            throw new NotImplementedException();
        }

        public Shape Or(Shape region)
        {
            throw new NotImplementedException();
        }

        public Shape Transform(Transform<Rektor> transform)
        {
            Area.Transform(transform);
            Perimeter.Transform(transform);
            return this;
        }

        public Rektor[] VertexArray()
        {
            throw new NotImplementedException();
        }

        public Shape XOr(Shape region)
        {
            throw new NotImplementedException();
        }
    }
}
