namespace Render.Core.Input
{
    public class MouseDragEndArgs : MouseEventArgs
    {
        public readonly MouseDragStartArgs Start;
        public readonly MouseDragArgs[] Drags;
        public readonly double FinalX;
        public readonly double FinalY;

        public MouseDragEndArgs(IMouseState state, MouseDragStartArgs start, MouseDragArgs[] drags, double x, double y) : base(state) =>
            (Start, Drags, FinalX, FinalY) = (start, drags, x, y);
    }
}