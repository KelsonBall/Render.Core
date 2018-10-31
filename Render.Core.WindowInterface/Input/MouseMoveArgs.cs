namespace Render.Core.Input
{
    public class MouseMoveArgs : MouseEventArgs
    {
        public readonly double CurrentX;
        public readonly double CurrentY;

        public readonly double DeltaX;
        public readonly double DeltaY;

        public MouseMoveArgs(IMouseState state, double x, double y, double dx, double dy) : base(state) =>
            (CurrentX, CurrentY, DeltaX, DeltaY) = (x, y, dx, dy);        
    }
}