namespace Render.Core.Input
{
    public class MouseUpArgs : MouseEventArgs
    {
        public readonly MouseButton Button;
        public readonly uint Ticks;
        public readonly double Milliseconds;

        public MouseUpArgs(MouseButton button, uint ticks, double ms)
        {
            Button = button;
            Ticks = ticks;
            Milliseconds = ms;
        }
    }
}