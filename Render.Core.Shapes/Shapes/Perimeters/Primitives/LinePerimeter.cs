using Render.Core.Vectors;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace KelsonBall.Geometry.Perimeters.Primitives
{
    public class LinePerimeter : Perimeter
    {
        public readonly double Length;

        public LinePerimeter(double length)
        {
            Length = length;
        }

        public override IEnumerable<rVector> Intersections(Ray<rVector> ray)
        {
            throw new NotImplementedException();
        }
    }
}
