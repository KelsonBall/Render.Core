using KelsonBall.Geometry.Areas;
using System.Numerics;
using System.Collections.Generic;
using Render.Core.Vectors;

namespace KelsonBall.Geometry.Shapes.Primitives
{
    public interface IRectangle
    {
        rVector Dimensions { get; }
    }

    public class Rectangle : Shape, IRectangle
    {
        public rVector Dimensions { get; private set; }

        public Rectangle(rVector dimensions) : base(new RectangleArea(dimensions), new RectanglePerimeter(dimensions))
        {
            throw new System.NotImplementedException();
        }
    }

    public class RectangleArea : Area
    {
        public RectangleArea(rVector dimensions)
        {
            throw new System.NotImplementedException();
        }

        public override bool Contains(rVector point)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RectanglePerimeter : Perimeter
    {
        public RectanglePerimeter(rVector dimensions)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<rVector> Intersections(Ray<rVector> ray)
        {
            throw new System.NotImplementedException();
        }
    }
}
