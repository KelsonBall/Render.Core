namespace Render.Core.Input
{
    public class MouseDragStartArgs : MouseEventArgs
    {
        public readonly object DragTarget;
        public readonly double StartX;
        public readonly double StartY;

        public MouseDragStartArgs(IMouseState state, object target, double x, double y) : base(state) =>
            (DragTarget, StartX, StartY) = (target, x, y);
            
    }
}