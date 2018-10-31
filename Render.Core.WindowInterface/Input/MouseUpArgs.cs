namespace Render.Core.Input
{
    public class MouseUpArgs : MouseEventArgs
    {
        public readonly MouseButton Button;
        public readonly uint Ticks;
        public readonly double Milliseconds;

        public MouseUpArgs(IMouseState state, MouseButton button, uint ticks, double ms) : base(state) =>
            (Button, Ticks, Milliseconds) = (button, ticks, ms);        
    }
}