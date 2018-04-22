using KelsonBall.Geometry.Shapes.Primitives;
using KelsonBall.Geometry.Areas;
using Render.Core.Vectors;

namespace KelsonBall.Geometry.Shapes.Areas.Primitives
{
    public class RectangleArea : Area, IRectangle
    {
        public Rektor Dimensions { get; private set; }

        public RectangleArea(Rektor dimensions)
        {
            Dimensions = dimensions;
        }

        public override bool Contains(Rektor point)
        {
            return (point.X >= -Dimensions.X && point.X <= Dimensions.X)
                && (point.Y >= -Dimensions.Y && point.Y <= Dimensions.Y);
        }
    }
}
