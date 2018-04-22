using KelsonBall.Geometry.Shapes.Primitives;
using System.Numerics;
using KelsonBall.Geometry.Areas;
using Render.Core.Vectors;

namespace KelsonBall.Geometry.Shapes.Areas.Primitives
{
    public class RectangleArea : Area, IRectangle
    {
        public rVector Dimensions { get; private set; }

        public RectangleArea(rVector dimensions)
        {
            Dimensions = dimensions;
        }

        public override bool Contains(rVector point)
        {
            return (point.X >= -Dimensions.X && point.X <= Dimensions.X)
                && (point.Y >= -Dimensions.Y && point.Y <= Dimensions.Y);
        }
    }
}
