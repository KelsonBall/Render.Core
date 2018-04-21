using KelsonBall.Geometry.Areas;
using System.Numerics;
using System.Collections.Generic;

namespace KelsonBall.Geometry.Shapes.Primitives
{
    public interface IRectangle
    {
        Vector2 Dimensions { get; }
    }

    public class Rectangle : Shape, IRectangle
    {
        public Vector2 Dimensions { get; private set; }

        public Rectangle(Vector2 dimensions) : base(new RectangleArea(dimensions), new RectanglePerimeter(dimensions))
        {
            throw new System.NotImplementedException();
        }
    }

    public class RectangleArea : Area
    {
        public RectangleArea(Vector2 dimensions)
        {
            throw new System.NotImplementedException();
        }

        public override bool Contains(Vector2 point)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RectanglePerimeter : Perimeter
    {
        public RectanglePerimeter(Vector2 dimensions)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Vector2> Intersections(Ray<Vector2> ray)
        {
            throw new System.NotImplementedException();
        }
    }
}
