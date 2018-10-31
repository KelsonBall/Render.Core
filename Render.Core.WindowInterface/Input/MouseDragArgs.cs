namespace Render.Core.Input
{
    public class MouseDragArgs : MouseEventArgs
    {        
        public readonly MouseDragStartArgs Start;
        public readonly double DeltaX;
        public readonly double DeltaY;

        public MouseDragArgs(IMouseState state, MouseDragStartArgs start, double x, double y) : base(state) =>        
            (Start, DeltaX, DeltaY) = (start, x, y);        
    }
}