namespace Render.Core.Input
{
    public class MouseDragStartArgs : MouseEventArgs
    {
        public readonly double StartX;
        public readonly double StartY;

        public MouseDragStartArgs(double x, double y)
        {
            StartX = x;
            StartY = y;
        }
    }
}