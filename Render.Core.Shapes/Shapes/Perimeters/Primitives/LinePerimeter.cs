using Render.Core.Vectors;
using System;
using System.Collections.Generic;

namespace KelsonBall.Geometry.Perimeters.Primitives
{
    public class LinePerimeter : Perimeter
    {
        public readonly double Length;

        public LinePerimeter(double length)
        {
            Length = length;
        }

        public override IEnumerable<Rektor> Intersections(Ray<Rektor> ray)
        {
            throw new NotImplementedException();
        }
    }
}
