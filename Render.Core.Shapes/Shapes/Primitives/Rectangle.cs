using KelsonBall.Geometry.Areas;
using System.Collections.Generic;
using Render.Core.Vectors;

namespace KelsonBall.Geometry.Shapes.Primitives
{
    public interface IRectangle
    {
        Rektor Dimensions { get; }
    }

    public class Rectangle : Shape, IRectangle
    {
        public Rektor Dimensions { get; private set; }

        public Rectangle(Rektor dimensions) : base(new RectangleArea(dimensions), new RectanglePerimeter(dimensions))
        {
            throw new System.NotImplementedException();
        }
    }

    public class RectangleArea : Area
    {
        public RectangleArea(Rektor dimensions)
        {
            throw new System.NotImplementedException();
        }

        public override bool Contains(Rektor point)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RectanglePerimeter : Perimeter
    {
        public RectanglePerimeter(Rektor dimensions)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Rektor> Intersections(Ray<Rektor> ray)
        {
            throw new System.NotImplementedException();
        }
    }
}
