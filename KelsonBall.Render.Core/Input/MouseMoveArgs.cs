namespace Render.Core.Input
{
    public class MouseMoveArgs : MouseEventArgs
    {
        public readonly double X;
        public readonly double Y;

        public readonly double DeltaX;
        public readonly double DeltaY;

        public MouseMoveArgs(double x, double y, double dx, double dy)
        {
            X = x;
            Y = y;
            DeltaX = dx;
            DeltaY = dy;
        }
    }
}